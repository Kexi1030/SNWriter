using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Forms;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using DatsTestSystem.HardwareSerialNumberWirter.Commands;
using DatsTestSystem.HardwareSerialNumberWirter.Models.JsonModels;
using OpenFileDialog = System.Windows.Forms.OpenFileDialog;
using DatsTestSystem.SerialPortManagement;
using DatsTestSystem.HardwareSerialNumberWirter.Models;

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
        }

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
    }
}
