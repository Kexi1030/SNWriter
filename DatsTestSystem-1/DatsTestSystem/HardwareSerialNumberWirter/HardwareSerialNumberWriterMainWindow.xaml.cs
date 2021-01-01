using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Forms;
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

        public string CurrentString { get; set; }

        private string FrameBack { get; set; }

        private string FileLoad { get; set; }

        public SerialportConfigurationInformation portconfiginfo;
        public SerialPortManagementClass serialPortManagementClass;
        PortControlWindow portControlWindow;

        ObservableCollection<string> sNStringInListBoxes = new ObservableCollection<string>();

        public HardwareSerialNumberWriterMainWindow()
        {
            InitializeComponent();
            portControlWindow = new PortControlWindow();
            portControlWindow.HardwareSerialNumberWriterMainWindow = this;
            portControlWindow.initConfigurationInformation();

            this.Closing += windowClosingFunc;

            this.WindowStartupLocation = WindowStartupLocation.CenterOwner;

            SNList.ItemsSource = sNStringInListBoxes;

            this.FileLoad = DateTime.Now.ToShortDateString();
        }

        public void ShowOperatorNameInputWindow()
        {
            HardwareSerialNumberWriterInputUserNameWindow hardwareSerialNumberWriterInputUserNameWindow = new HardwareSerialNumberWriterInputUserNameWindow();
            // hardwareSerialNumberWriterInputUserNameWindow.Owner = this;
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
            hardwareSerialNumberWriterInitialSNinofWindow.Owner = this;
            hardwareSerialNumberWriterInitialSNinofWindow.WindowStartupLocation = WindowStartupLocation.CenterOwner;
            hardwareSerialNumberWriterInitialSNinofWindow.ShowDialog();

            if (hardwareSerialNumberWriterInitialSNinofWindow.observableCollection != null)
            {
                foreach (string i in hardwareSerialNumberWriterInitialSNinofWindow.observableCollection)
                {
                    string sNStringContainBlock = StringProcess(i);
                    // 对其中的每个string每隔4个添加空格
                    sNStringInListBoxes.Add(sNStringContainBlock);
                }
            }
        }

        private void ModifyUserName_Click(object sender, RoutedEventArgs e)
        {
            HardwareSerialNumberWriterInputUserNameWindow hardwareSerialNumberWriterInputUserNameWindow = new HardwareSerialNumberWriterInputUserNameWindow();
            hardwareSerialNumberWriterInputUserNameWindow.Owner = this;
            hardwareSerialNumberWriterInputUserNameWindow.WindowStartupLocation = WindowStartupLocation.CenterOwner;
            hardwareSerialNumberWriterInputUserNameWindow.ShowDialog();

            if (hardwareSerialNumberWriterInputUserNameWindow.operatorName != null)
            {
                this.OperatorNameTextBlock.DataContext = hardwareSerialNumberWriterInputUserNameWindow.operatorName;
            }

        }

        private void InitialSNListFromFileButton_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Title = "请选择需要导入的Json配置文件";
            openFileDialog.Filter = "json files (*.json)|*.json|All files (*.*)|*.*";
            openFileDialog.InitialDirectory = System.Windows.Forms.Application.StartupPath;
            if (openFileDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                JsonCreate jsonCreate = new JsonCreate();
                this.FileLoad = openFileDialog.FileName.Split('.')[0];
                JsonFormat JsonData = jsonCreate.CreateSNFromJsonFile(openFileDialog.FileName);

                foreach (var i in JsonData.SnList)
                {
                    sNStringInListBoxes.Add(StringProcess(i));
                }
            }
        }

        private void AddOneSNstringButton_Click(object sender, RoutedEventArgs e)
        {
            HardwareSerialNumberWriterAddOneSNpopupWindow hardwareSerialNumberWriterAddOneSNpopupWindow = new HardwareSerialNumberWriterAddOneSNpopupWindow();
            hardwareSerialNumberWriterAddOneSNpopupWindow.Owner = this;
            hardwareSerialNumberWriterAddOneSNpopupWindow.WindowStartupLocation = WindowStartupLocation.CenterOwner;
            hardwareSerialNumberWriterAddOneSNpopupWindow.ShowDialog();

            if (hardwareSerialNumberWriterAddOneSNpopupWindow.addOneSnString != null)
            {
                sNStringInListBoxes.Add(StringProcess(hardwareSerialNumberWriterAddOneSNpopupWindow.addOneSnString));

                // 写入到Json文件中去
                JsonCreate jsonCreate = new JsonCreate();
                JsonFormat ReturnJsonCreate = jsonCreate.CreateSNFromJsonFile(FileLoad + ".json"); // 需要更改 文件路径需要更改

                List<string> newSnList = new List<string>(ReturnJsonCreate.SnList);
                newSnList.Add(hardwareSerialNumberWriterAddOneSNpopupWindow.addOneSnString);

                ReturnJsonCreate.SnList = newSnList.ToArray();

                jsonCreate.CreateJson(ReturnJsonCreate);
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
            CurrentSNTextBlock.Text = SNList.SelectedItem.ToString();
        }

        private void StartButton_Click(object sender, RoutedEventArgs e)
        {
            // 打开状态帧分发的线程
            // OpenDistrubuteThread();

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

            CommandFrameGeneration commandFrameGeneration = new CommandFrameGeneration(CurrentSn);
            CurrentString = commandFrameGeneration.FwWriteString;

            Console.WriteLine("开始烧写.....");
            if(serialPortManagementClass._readThread.ThreadState == System.Threading.ThreadState.Unstarted)
            {
                serialPortManagementClass._readThread.Start();
            }

            if (SendData(commandFrameGeneration.FwReadString)) // 如果查询发送成功有返回
            {
                //Console.WriteLine("烧写前查询检查结束");
                SendData(commandFrameGeneration.FwWriteString); // 烧写硬件序列号
                Console.WriteLine("硬件序列号正在写入1...");
            }
            else
            {
                if (SendData(commandFrameGeneration.FwReadString)) //再次发送查询序列号 如果有返回
                {
                    SendData(commandFrameGeneration.FwWriteString); // 烧写硬件序列号
                    Console.WriteLine("硬件序列号正在写入2...");
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

            /*
            else
            {
                // 变色或添加图片未完成 
                this.SNFWStatusTextBlock.Text += "烧写失败\n";

                Thread SaveChangethread = new Thread(() => SaveChangeToJson(CurrentSn, OperatorName, "失败"));
                SaveChangethread.Start();

                MessageBoxPopUpToNextSNOR();
                return;
            }

            ButtonsStatusChange(true);
            */

            //serialPortManagementClass.Close();
        }

        private void SuccessfulFwFunc(string CurrentSn, String OperatorName)
        {
            FrameBack = null;

            Console.WriteLine("当前板读出序列号为{0}\n烧写成功\n", CurrentSn);
            Thread SaveChangethread = new Thread(() => SaveChangeToJson(CurrentSn, OperatorName, "成功"));
            SaveChangethread.Start();
            SnListToNext(); // 切换到下一个序列号

            //CloseDistrubuteThread(); // 关闭状态帧分发的线程
        }

        private void FailFwFunc(string CurrentSn, String OperatorName)
        {
            FrameBack = null;

            Console.WriteLine("烧写失败\n");
            Thread SaveChangethread = new Thread(() => SaveChangeToJson(CurrentSn, OperatorName, "失败"));
            SaveChangethread.Start();

            //CloseDistrubuteThread(); // 关闭状态帧分发的线程
            MessageBoxPopUpToNextSNOR();
        }


        private bool SendData(string data)
        {
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
            // 跳到下一个如果已经写完了那就跳过 未完成
            SNList.SelectedIndex += 1;

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
        private static void SaveChangeToJson(string currentSn, string OperatorName, string status)
        {
            JsonCreate jsonCreate = new JsonCreate();
            JsonFormat JsonReturn = jsonCreate.CreateSNFromJsonFile("OUTPUT.json"); // 需要修改
            List<EachSNStatus> AllStatus; // 存放所有的烧写信息
            if (JsonReturn.eachSNStatuses == null)
            {
                AllStatus = new List<EachSNStatus>();
            }
            else
            {
                AllStatus = new List<EachSNStatus>(JsonReturn.eachSNStatuses);
            }
            AllStatus.Add(new EachSNStatus() { SnString = currentSn, OperatorName = OperatorName, OperateTime = DateTime.Now.ToString(), Done = status });
            JsonReturn.eachSNStatuses = AllStatus.ToArray();
            jsonCreate.CreateJson(JsonReturn);
        }

        private void FWWindowClosing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            System.Windows.Application.Current.Shutdown(); // 只是关掉了UI 后台进程没有关闭
        }

        private void EndButton_Click(object sender, RoutedEventArgs e)
        {
            // 对当前列表进行检查是否还有没烧写完毕的序列号  未完成

            MessageBoxResult result = System.Windows.MessageBox.Show("当前序列号未全部烧写完成，是否退出", "退出", MessageBoxButton.YesNo, MessageBoxImage.Question);

            switch (result)
            {
                case MessageBoxResult.Yes:
                    break;

            }

            ThreadStart threadStart = new ThreadStart(() => ReportCreation.CreateReport(FileLoad + ".json"));
            Thread CreateReportThread = new Thread(threadStart);
            CreateReportThread.Start();

            MessageBoxResult messageBoxResult = System.Windows.MessageBox.Show("正在生成报告，是否需要查看？", "报告生成", MessageBoxButton.YesNo, MessageBoxImage.Question);
            switch (messageBoxResult)
            {
                case MessageBoxResult.Yes:
                    Process.Start(FileLoad + ".pdf");
                    break;
                case MessageBoxResult.No:
                    break;
            }
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
        }
    }
}
