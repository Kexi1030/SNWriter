using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;
using DatsTestSystem.HardwareSerialNumberWirter.Commands;
using DatsTestSystem.HardwareSerialNumberWirter.Models.JsonModels;
using OpenFileDialog = System.Windows.Forms.OpenFileDialog;
using DatsTestSystem.SerialPortManagement;
using DatsTestSystem.HardwareSerialNumberWirter.Models;
using DatsTestSystem.CommandAggregationStatusDistribution;
using System.Threading;
using System.Threading.Tasks;
using System.Diagnostics;
using System.Text;
using DatsTestSystem.SerialPortManagement.Models;
using DatsTestSystem.Log;

namespace DatsTestSystem.HardwareSerialNumberWirter
{
    /// <summary>
    /// HardwareSerialNumberWriterMainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class HardwareSerialNumberWriterMainWindow : Window
    {
        public delegate void SNToCommandAggregate(string snString);
        public event SNToCommandAggregate sntocommandaggregate;

        public StatusDistribution StatusDistribution;
        public CommandAggregate commandAggregate;
       

        public string currentjsonfile { get; set; } // 当前操作的json文件路径
        public string CurrentString { get; set; }
        public int selectindex { get; set; }
        private string FrameBack { get; set; }

        private string FileLoad { get; set; }

        public SerialportConfigurationInformation portconfiginfo;
        public SerialPortManagementClass serialPortManagementClass;
        PortControlWindow portControlWindow;

        ObservableCollection<ListBoxItems> sNStringInListBoxes = new ObservableCollection<ListBoxItems>();
        int a; // 当前sNStringInListBoxes的下标

        public HardwareSerialNumberWriterMainWindow()
        {
            InitializeComponent();

            portControlWindow = new PortControlWindow();
            portControlWindow.Title = "串口配置";
            portControlWindow.HardwareSerialNumberWriterMainWindow = this;
            portControlWindow.initConfigurationInformation();


            this.Closing += windowClosingFunc;

            this.WindowStartupLocation = WindowStartupLocation.CenterOwner;

            SNList.ItemsSource = sNStringInListBoxes;
        }

        public void ShowOperatorNameInputWindow()
        {
            HardwareSerialNumberWriterInputUserNameWindow hardwareSerialNumberWriterInputUserNameWindow = new HardwareSerialNumberWriterInputUserNameWindow();
            hardwareSerialNumberWriterInputUserNameWindow.Title = "用户名称";
            hardwareSerialNumberWriterInputUserNameWindow.WindowStartupLocation = WindowStartupLocation.CenterScreen;
            hardwareSerialNumberWriterInputUserNameWindow.ShowDialog();

            if (hardwareSerialNumberWriterInputUserNameWindow.NameOr)
            {
                this.OperatorNameTextBlock.DataContext = hardwareSerialNumberWriterInputUserNameWindow.operatorName;
                this.ShowDialog();
            }
        }

        /// <summary>
        /// 新建按钮点击
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void InitialButton_Click(object sender, RoutedEventArgs e)
        {
            HardwareSerialNumberWriterInitialSNinofWindow hardwareSerialNumberWriterInitialSNinofWindow = new HardwareSerialNumberWriterInitialSNinofWindow();
            hardwareSerialNumberWriterInitialSNinofWindow.Title = "序列号配置";
            hardwareSerialNumberWriterInitialSNinofWindow.Owner = this;
            hardwareSerialNumberWriterInitialSNinofWindow.WindowStartupLocation = WindowStartupLocation.CenterOwner;
            currentjsonfile = DateTime.Now.ToString("yyyy-MM-dd") + "_"+this.OperatorNameTextBlock.Text + ".json";
            hardwareSerialNumberWriterInitialSNinofWindow.filename = currentjsonfile;
            hardwareSerialNumberWriterInitialSNinofWindow.ShowDialog();

            if (sNStringInListBoxes.Count > 0)
                sNStringInListBoxes.Clear();

            if (hardwareSerialNumberWriterInitialSNinofWindow.observableCollection != null)
            {
                foreach (var i in hardwareSerialNumberWriterInitialSNinofWindow.observableCollection)
                {
                    string sNStringContainBlock = StringProcess(i.snstring);
                    // 对其中的每个string每隔4个添加空格
                    sNStringInListBoxes.Add(new ListBoxItems() { snstring = sNStringContainBlock, done = 0 });
                }
            }
        }

        private void ModifyUserName_Click(object sender, RoutedEventArgs e)
        {
            HardwareSerialNumberWriterInputUserNameWindow hardwareSerialNumberWriterInputUserNameWindow = new HardwareSerialNumberWriterInputUserNameWindow();
            hardwareSerialNumberWriterInputUserNameWindow.Title = "用户名称修改";
            hardwareSerialNumberWriterInputUserNameWindow.Owner = this;
            hardwareSerialNumberWriterInputUserNameWindow.WindowStartupLocation = WindowStartupLocation.CenterOwner;
            hardwareSerialNumberWriterInputUserNameWindow.ShowDialog();

            if (hardwareSerialNumberWriterInputUserNameWindow.operatorName != null)
            {
                this.OperatorNameTextBlock.DataContext = hardwareSerialNumberWriterInputUserNameWindow.operatorName;
                Logger.Debug("Operator Name Change to " + hardwareSerialNumberWriterInputUserNameWindow.operatorName.operatorname);
            }
        }

        private void InitialSNListFromFileButton_Click(object sender, RoutedEventArgs e)
        {
            Logger.Debug("Loading From Json File......");
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Title = "请选择需要导入的Json配置文件";
            openFileDialog.Filter = "json files (*.json)|*.json|All files (*.*)|*.*";
            openFileDialog.InitialDirectory = System.Windows.Forms.Application.StartupPath;
            if (openFileDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                // 如果之前的序列号列表已经有值了则需要清除重新导入
                if(sNStringInListBoxes.Count > 0)
                {
                    sNStringInListBoxes.Clear();
                }

                string[] xstring = openFileDialog.FileName.Split('\\');
                this.Title = xstring[xstring.Length - 1];

                JsonCreate jsonCreate = new JsonCreate();
                this.FileLoad = openFileDialog.FileName.Split('.')[0];
                JsonFormat JsonData = jsonCreate.CreateSNFromJsonFile(openFileDialog.FileName);
                currentjsonfile = FileLoad+".json";
                Logger.Debug("Load From Json File Done----" + currentjsonfile);

                foreach (var i in JsonData.SnList)
                {
                    sNStringInListBoxes.Add(new ListBoxItems() { snstring = StringProcess(i), done = 0 });
                }
                if(JsonData.eachSNStatuses != null)
                {
                    // 将状态写入
                    foreach (var i in JsonData.eachSNStatuses)
                    {
                        if (i.Done == "成功")
                        {
                            int indextemp = -1;
                            for (int x = 0; x < sNStringInListBoxes.Count; x++)
                            {
                                if (sNStringInListBoxes[x].done == 0 && sNStringInListBoxes[x].snstring == StringProcess(i.SnString))
                                {
                                    indextemp = x;
                                }
                            }
                            sNStringInListBoxes[indextemp].done = 1;
                        }
                        else if (i.Done == "失败")
                        {
                            int indextemp = -1;
                            for (int x = 0; x < sNStringInListBoxes.Count; x++)
                            {
                                if (sNStringInListBoxes[x].done == 0 && sNStringInListBoxes[x].snstring == StringProcess(i.SnString))
                                {
                                    indextemp = x;
                                }
                            }
                            sNStringInListBoxes[indextemp].done = -1;
                        }
                    }
               
                }
            }
        }

        private void AddOneSNstringButton_Click(object sender, RoutedEventArgs e)
        {
            if(currentjsonfile == null)
            {
                System.Windows.MessageBox.Show("请先新建或者从文件中导入", "添加错误", MessageBoxButton.OK);
                return;
            }
            HardwareSerialNumberWriterAddOneSNpopupWindow hardwareSerialNumberWriterAddOneSNpopupWindow = new HardwareSerialNumberWriterAddOneSNpopupWindow();
            hardwareSerialNumberWriterAddOneSNpopupWindow.Title = "添加单条序列号";
            hardwareSerialNumberWriterAddOneSNpopupWindow.Owner = this;
            hardwareSerialNumberWriterAddOneSNpopupWindow.WindowStartupLocation = WindowStartupLocation.CenterOwner;
            hardwareSerialNumberWriterAddOneSNpopupWindow.ShowDialog();

            if (hardwareSerialNumberWriterAddOneSNpopupWindow.addOneSnString != null)
            {
                foreach(var item in sNStringInListBoxes)
                {
                    if(item.snstring == StringProcess(hardwareSerialNumberWriterAddOneSNpopupWindow.addOneSnString))
                    {
                        MessageBox.Show("请勿添加重复的硬件序列号", "错误", MessageBoxButton.OK, MessageBoxImage.Warning);
                        return;
                    }
                }
                sNStringInListBoxes.Add(new ListBoxItems() { snstring = StringProcess(hardwareSerialNumberWriterAddOneSNpopupWindow.addOneSnString), done = 0 });
                Logger.Debug("One SN Added---" + StringProcess(hardwareSerialNumberWriterAddOneSNpopupWindow.addOneSnString));

                // 写入到Json文件中去
                JsonCreate jsonCreate = new JsonCreate();
                JsonFormat ReturnJsonCreate = jsonCreate.CreateSNFromJsonFile(currentjsonfile); // 需要更改 文件路径需要更改

                List<string> newSnList = new List<string>(ReturnJsonCreate.SnList);
                newSnList.Add(hardwareSerialNumberWriterAddOneSNpopupWindow.addOneSnString);

                ReturnJsonCreate.SnList = newSnList.ToArray();

                jsonCreate.CreateJson(ReturnJsonCreate,currentjsonfile);
            }
        }

        private string StringProcess(string demo)
        {
            char[] demoChar = demo.ToCharArray();
            string returnString = "";

            for (int i = 0; i < demoChar.Length; i++)
            {
                if (i % 4 == 0 && i > 0)
                {
                    returnString += " ";
                }
                returnString += demoChar[i];
            }

            return returnString;
        }

        private void PortControllerButton_Click(object sender, RoutedEventArgs e)
        {
            portControlWindow.Owner = this;
            portControlWindow.SerialPortManagementClass = this.serialPortManagementClass;
            portControlWindow.WindowStartupLocation = WindowStartupLocation.CenterOwner;
            portControlWindow.Show();
        }

        private void SNList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (SNList.SelectedIndex != -1)
            {
                a = SNList.SelectedIndex;
            }
            selectindex = a;
            //Console.WriteLine(SNList.SelectedIndex);
            //Console.WriteLine("a:{0}", a);
            //a = SNList.SelectedIndex;
            try
            {
                CurrentSNTextBlock.Text = sNStringInListBoxes[a].snstring;
            }
            catch (Exception ex)

            { }
        }

        private void StartButton_Click(object sender, RoutedEventArgs e)
        {
            // 如果更换了串口状态需要重新连接


            //打开状态帧分发的线程
             OpenDistrubuteThread();
         
            SNFWStatusTextBlock.Text = null;

            // 将别的按钮设置失效
            ButtonsStatusChange(false);

            string OperatorName = this.OperatorNameTextBlock.Text;
            if (this.CurrentSNTextBlock.Text == "") // 如果没有选中sn
            {
                Console.WriteLine("请选择你要烧写的序列号\n结束");
                ButtonsStatusChange(true);
                serialPortManagementClass.Close();
                return;
            }
            string CurrentSn = this.CurrentSNTextBlock.Text.Replace(" ", "");
            Console.WriteLine(string.Format("当前操作的序列号是{0}", CurrentSn));
            Logger.Debug("Current SN ---" + CurrentSn);

            CommandFrameGeneration commandFrameGeneration = new CommandFrameGeneration(CurrentSn);
            CurrentString = commandFrameGeneration.FwWriteString;

            Console.WriteLine("开始烧写.....");
            if(serialPortManagementClass._readThread.ThreadState == System.Threading.ThreadState.Unstarted)
            {
                //Console.WriteLine("打开了串口的读线程");
                serialPortManagementClass._readThread.Start();
            }

            if (SendData(commandFrameGeneration.FwReadString)) // 如果查询发送成功有返回
            {
                //Console.WriteLine("烧写前查询检查结束");
                SendData(commandFrameGeneration.FwWriteString); // 烧写硬件序列号
                //Console.WriteLine(commandFrameGeneration.FwWriteString);
                Console.WriteLine("硬件序列号正在写入...");
            }
            else
            {
                if (SendData(commandFrameGeneration.FwReadString)) //再次发送查询序列号 如果有返回
                {
                    SendData(commandFrameGeneration.FwWriteString); // 烧写硬件序列号
                    Console.WriteLine("硬件序列号正在写入...");
                }
                else
                {
                    Console.WriteLine("当前Dat无法查询\n结束");
                    FailFwFunc(CurrentSn, OperatorName);
                    ButtonsStatusChange(true);
                    return;
                }
            }

            // 烧写完进行查询
            if (SendData(commandFrameGeneration.FwReadString)) // 如果查询发送成功有返回
            {
                Console.WriteLine("正在进行烧写检查......");
                bool EqualOr = StatusFrameAnalysis.SnComparision(FrameBack, CurrentSn.Replace(" ", ""));
                if (EqualOr)
                {
                    SuccessfulFwFunc(CurrentSn, OperatorName);
                }
                else
                {
                    Console.WriteLine("查询结果和待烧写序列号不匹配，烧写失败");
                    FailFwFunc(CurrentSn, OperatorName);
                }
                ButtonsStatusChange(true);
                return;
            }
            else
            {
                if (SendData(commandFrameGeneration.FwReadString))
                {
                    Console.WriteLine("正在进行烧写检查......");
                    bool EqualOr = StatusFrameAnalysis.SnComparision(FrameBack, CurrentSn);

                    if (EqualOr)
                    {
                        SuccessfulFwFunc(CurrentSn, OperatorName);
                    }
                    else
                    {
                        Console.WriteLine("查询结果和待烧写序列号不匹配，烧写失败");
                        FailFwFunc(CurrentSn, OperatorName);
                    }
                    ButtonsStatusChange(true);
                    return;
                }
                else
                {
                    Console.WriteLine("查询失败");
                    FailFwFunc(CurrentSn, OperatorName);
                    ButtonsStatusChange(true);
                    return;
                }
            }

            //serialPortManagementClass.Close();
        }

        private void SuccessfulFwFunc(string CurrentSn, String OperatorName)
        {
            Logger.Debug(CurrentSn + "----Succeed");
            FrameBack = null;

            Console.WriteLine("当前板读出序列号为{0}\n烧写成功\n", CurrentSn);
            Thread SaveChangethread = new Thread(() => SaveChangeToJson(CurrentSn, OperatorName, "成功"));
            SaveChangethread.Start();

            string temp = sNStringInListBoxes[a].snstring;
            sNStringInListBoxes[a] = new ListBoxItems() { snstring = temp, done = 1 }; // 将snlist中的序列号变成绿色

            SnListToNext(); // 切换到下一个序列号

            //CloseDistrubuteThread(); // 关闭状态帧分发的线程
        }

        private void FailFwFunc(string CurrentSn, String OperatorName)
        {
            Logger.Debug(CurrentSn + "----Failed");
            FrameBack = null;

            Console.WriteLine("烧写失败\n");
            Thread SaveChangethread = new Thread(() => SaveChangeToJson(CurrentSn, OperatorName, "失败"));
            SaveChangethread.Start();

            string temp = sNStringInListBoxes[a].snstring;
            sNStringInListBoxes[a] = new ListBoxItems() { snstring = temp, done = -1 }; // 将snlist中的序列号变成红色

            //CloseDistrubuteThread(); // 关闭状态帧分发的线程
            MessageBoxPopUpToNextSNOR();
        }


        private bool SendData(string data)
        {
            Logger.Info("SendData---" + data);
            sntocommandaggregate(data); // 发送到指令帧汇聚模块
            OpenDistrubuteThread(); // 打开状态帧分发的线程

            if (data == "F500000000E00300FFFF5F") // 如果是查询硬件序列号的帧
            {
                DateTime opendistrubutetime = DateTime.Now;

                while (FrameBack == null)
                {
                    DateTime datetimenow = DateTime.Now;
                    var timespan = (datetimenow - opendistrubutetime).TotalMilliseconds;
                    if (timespan > 2000) // 如果超时
                    {
                        //Console.WriteLine(timespan.ToString());
                        Console.WriteLine("超时\t没有收到返回的数据帧");

                        //Thread closeDistrubuteThread = new Thread(() =>
                        // {
                        //     Thread.Sleep(1000);
                        //     CloseDistrubuteThread();
                        // });
                        //closeDistrubuteThread.Start();
                        //CloseDistrubuteThread();
                        return false;
                    }
                }
                CloseDistrubuteThread();
                return true;
            }
            else // 如果是烧写硬件序列号
            {
                FrameBack = null;
                CloseDistrubuteThread();
                return true;
            }
        }



        /// <summary>
        /// 更改其他按钮的可用信息 在烧写过程中不允许点击其他按钮
        /// </summary>
        /// <param name="status"></param>
        private void ButtonsStatusChange(bool status)
        {
            StartButton.IsEnabled = status;
            EndButton.IsEnabled = status;
            ModifyUserName.IsEnabled = status;
            PortControllerButton.IsEnabled = status;
            InitialButton.IsEnabled = status;
            InitialSNListFromFileButton.IsEnabled = status;
            AddOneSNstringButton.IsEnabled = status;
            StartButton.IsEnabled = status;
        }

        /// <summary>
        /// 序列号跳到下一个如果已经写完了那就跳过 返回被跳过的序列号
        /// </summary>
        private string SnListToNext()
        {
            string ReturnString;
            ReturnString = this.CurrentSNTextBlock.Text.Replace(" ", "");

            for(int i = selectindex ;i<sNStringInListBoxes.Count;i++)
            {
                if(sNStringInListBoxes[i].done == 1) // 如果这个硬件序列号已经烧写完成
                {
                }
                else
                {
                    SNList.SelectedIndex = i;
                    break;
                }
            }

            return ReturnString;
        }

        /// <summary>
        /// 弹出框询问是否跳过当前的序列号 
        /// </summary>
        private void MessageBoxPopUpToNextSNOR()
        {
            MessageBoxResult messageBoxResult = System.Windows.MessageBox.Show("是否跳过当前的硬件序列号？", "错误", MessageBoxButton.YesNo, MessageBoxImage.Question);
            switch (messageBoxResult)
            {
                case MessageBoxResult.Yes:
                    string SNPassed = SnListToNext(); // 跳到下一个序列号
                    Console.WriteLine("{0}已经被跳过\n", SNPassed);
                    ButtonsStatusChange(true);
                    return;
                case MessageBoxResult.No:
                    ButtonsStatusChange(true);
                    return;
            }
        }

        /// <summary>
        /// 将每次烧写的状态信息保存到json文件中去
        /// </summary>
        /// <param name="currentSn"></param>
        /// <param name="OperatorName"></param>
        /// <param name="status"></param>
        private void SaveChangeToJson(string currentSn, string OperatorName, string status)
        {
            JsonCreate jsonCreate = new JsonCreate();
            JsonFormat JsonReturn = jsonCreate.CreateSNFromJsonFile(FileLoad+".json"); 
            List<EachSNStatus> AllStatus; // 存放所有的烧写信息
            if (JsonReturn.eachSNStatuses == null)
            {
                AllStatus = new List<EachSNStatus>();
            }
            else
            {
                AllStatus = new List<EachSNStatus>(JsonReturn.eachSNStatuses);
            }

            // 需要对之前的status列表判断是否有重复的status

            int index_temp = AllStatus.FindIndex(s => s.SnString.Equals(currentSn));
            if (index_temp == -1) // 没有重复
            {
                AllStatus.Add(new EachSNStatus() { SnString = currentSn, OperatorName = OperatorName, OperateTime = DateTime.Now.ToString(), Done = status });
            }
            else
            {
                AllStatus[index_temp] = new EachSNStatus() { SnString = currentSn, OperatorName = OperatorName, OperateTime = DateTime.Now.ToString(), Done = status };
            }
            JsonReturn.eachSNStatuses = AllStatus.ToArray();
            jsonCreate.CreateJson(JsonReturn,currentjsonfile);
        }

        private void FWWindowClosing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            Logger.Debug(this.OperatorNameTextBlock.Text + "---quit\n");
            System.Windows.Application.Current.Shutdown(); // 只是关掉了UI 后台进程没有关闭
        }

        private void EndButton_Click(object sender, RoutedEventArgs e)
        {
            if(sNStringInListBoxes.Count == 0)// 如果当前序列号还没有导入
            {
                MessageBox.Show("请导入配置文件或者新建序列号", "错误",MessageBoxButton.OK,MessageBoxImage.Error);
                return;
            }

            // 对当前硬件序列号列表进行判断是否全部烧写完成 如果没有完成弹窗提醒
            bool writealldone = true;
            for(int i = 0;i<sNStringInListBoxes.Count;i++)
            {
                if(sNStringInListBoxes[i].done != 1)
                {
                    writealldone = false;
                    break;
                }
            }

            if(!writealldone)
            {
                MessageBoxResult result = System.Windows.MessageBox.Show("当前序列号未全部烧写完成，是否退出", "退出", MessageBoxButton.YesNo, MessageBoxImage.Question);
                switch (result)
                {
                    case MessageBoxResult.Yes: // 如果选择退出就保存报告
                        break;
                    case MessageBoxResult.No:
                        return;
                }
            }

            ThreadStart threadStart = new ThreadStart(() => ReportCreation.CreateReport(FileLoad + ".json"));
            Thread CreateReportThread = new Thread(threadStart);
            CreateReportThread.Start();

            MessageBoxResult messageBoxResult = System.Windows.MessageBox.Show("正在生成报告，是否需要查看？", "报告生成", MessageBoxButton.YesNo, MessageBoxImage.Question);
            switch (messageBoxResult)
            {
                case MessageBoxResult.Yes:
                    Process.Start(FileLoad + "_报告.pdf");
                    break;
                case MessageBoxResult.No:
                    break;
            }

            this.Close();
        }

        public void getFrameBack(byte[] data)
        {
            FrameBack = StrAndByteProcessClass.bytetoString(data);
            //FrameBack = null;
        }

        private void OpenDistrubuteThread()
        {
            StatusDistribution.OpenDisThread();
        }

        private void CloseDistrubuteThread()
        {
            StatusDistribution.CloseDisThread();
        }

        private void windowClosingFunc(object sender, System.ComponentModel.CancelEventArgs e)
        {
            // 将状态帧的分发模块之前的捆绑消除
            StatusDistribution.DataDistrubution -= this.getFrameBack;
            System.Environment.Exit(0); // 彻底退出程序
        }
    }
}
