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

namespace DatsTestSystem.HardwareSerialNumberWirter
{
    /// <summary>
    /// HardwareSerialNumberWriterMainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class HardwareSerialNumberWriterMainWindow : Window
    {
        ObservableCollection<string> sNStringInListBoxes = new ObservableCollection<string>();

        public HardwareSerialNumberWriterMainWindow(Models.OperatorName operatorName)
        {
            InitializeComponent();
            this.WindowStartupLocation = WindowStartupLocation.CenterOwner;

            this.OperatorNameTextBlock.DataContext = operatorName;

            SNList.ItemsSource = sNStringInListBoxes;
            // SNList.DrawMode = DrawMode.OwnerDrawFixed;
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
                JsonFormat ReturnJsonCreate = jsonCreate.CreateSNFromJsonFile("OUTPUT.json"); // 需要更改 文件路径需要更改

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
            PortControlWindow portControlWindow = new PortControlWindow();
            portControlWindow.Owner = this;
            portControlWindow.WindowStartupLocation = WindowStartupLocation.CenterOwner;
            portControlWindow.Show();
        }

        private void SNList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            CurrentSNTextBlock.Text = SNList.SelectedItem.ToString();
        }

        private void StartButton_Click(object sender, RoutedEventArgs e)
        {
            // 将别的按钮设置失效
            ButtonsStatusChange(false);

            string CurrentSn = this.CurrentSNTextBlock.Text.Replace(" ", "");

            // 生成需要发送的数据 包括查询帧和配置序列号帧
            CommandFrameGeneration commandFrameGeneration = new CommandFrameGeneration(CurrentSn);
            this.SNFWStatusTextBlock.Text += string.Format("当前操作的序列号是{0}\n\n", CurrentSn);

            CommandAggregate commandAggregate = new CommandAggregate();
            commandAggregate.FWSend(commandFrameGeneration.FwReadString);
            string StringBackNo1 = commandAggregate.StringBack;
            if(StringBackNo1 == null)
            {
                commandAggregate.FWSend(commandFrameGeneration.FwReadString);
                if(commandAggregate.StringBack == null) // 再次查询不到
                {
                    this.SNFWStatusTextBlock.Text += "查询失败\n";

                    MessageBoxPopUpToNextSNOR();
                    return;
                }
            }

            this.SNFWStatusTextBlock.Text += string.Format("{0}\t正在烧写\n", CurrentSn);

            commandAggregate.FWSend(commandFrameGeneration.FwWriteString);

            commandAggregate.FWSend(commandFrameGeneration.FwReadString);
            string StringBack = commandAggregate.StringBack;
            if(StringBack == null)
            {
                commandAggregate.FWSend(commandFrameGeneration.FwReadString);
                if (commandAggregate.StringBack == null) // 再次查询不到
                {
                    this.SNFWStatusTextBlock.Text += "查询失败\n烧写失败\n";
                    MessageBoxPopUpToNextSNOR();
                    return;
                }
            }

            bool EqualOr = StatusFrameAnalysis.SnComparision(StringBack, this.CurrentSNTextBlock.Text.Replace(" ", ""));
            if (EqualOr)
            {
                // 变色或者添加图片 未完成
                // 写入 Json文件 未完成

                JsonCreate jsonCreate = new JsonCreate();
                JsonFormat JsonReturn = jsonCreate.CreateSNFromJsonFile("OUTPUT.json"); // 需要修改
                List<EachSNStatus> eachSNStatuses = new List<EachSNStatus>(JsonReturn.eachSNStatuses);
                eachSNStatuses.Add(new EachSNStatus() { SnString = CurrentSn, OperatorName = this.OperatorNameTextBlock.Text, OperateTime = DateTime.Now.ToString(), Done = "成功" });
                JsonReturn.eachSNStatuses = eachSNStatuses.ToArray();



                SnListToNext();
                this.SNFWStatusTextBlock.Text += "烧写成功\n";
            }
            else
            {
                this.SNFWStatusTextBlock.Text += "烧写失败\n";
                MessageBoxPopUpToNextSNOR();
                return;
            }


            ButtonsStatusChange(true);
        }

        private void ButtonsStatusChange(bool status)
        {
            StartButton.IsEnabled = status;
            EndButton.IsEnabled = status;
            ModifyUserName.IsEnabled = status;
            PortControllerButton.IsEnabled = status;
            InitialButton.IsEnabled = status;
            InitialSNListFromFileButton.IsEnabled = status;
            AddOneSNstringButton.IsEnabled = status;
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
                    this.SNFWStatusTextBlock.Text += string.Format("{0}已经被跳过\n", SNPassed);
                    ButtonsStatusChange(true);
                    return;
                case MessageBoxResult.No:
                    ButtonsStatusChange(true);
                    return;
            }
        }
    }
}
