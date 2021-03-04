using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using DatsTestSystem.Log;
using DatsTestSystem.SerialPortManagement.Models;

namespace DatsTestSystem.SerialPortManagement
{
    /// <summary>
    /// PortControlWindow.xaml 的交互逻辑
    /// </summary>
    public partial class PortControlWindow : Window
    {
        public SerialportConfigurationInformation configurationInformation { get; set; }
        public HardwareSerialNumberWirter.HardwareSerialNumberWriterMainWindow HardwareSerialNumberWriterMainWindow;
        public SerialPortManagementClass SerialPortManagementClass;
        public PortControlWindow()
        {
            InitializeComponent();

        }
        public void initConfigurationInformation()
        {
            configurationInformation = new SerialportConfigurationInformation()
            {
                PortName = "COM1",
                BaudRate = 9600,
                DataBits = 8,
                StopBits = 1,
                Parity = 0
            };
            HardwareSerialNumberWriterMainWindow.portconfiginfo = configurationInformation;
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void OKButton_Click(object sender, RoutedEventArgs e)
        {
            SerialportConfigurationInformation serialportConfigurationInformation = new SerialportConfigurationInformation();

            serialportConfigurationInformation.PortName = (string)SerialPortController.PortNamesComboBox.SelectedItem;
            serialportConfigurationInformation.BaudRate = Convert.ToInt32(SerialPortController.BaudRateComboBox.SelectedItem);
            serialportConfigurationInformation.DataBits = Convert.ToInt32(SerialPortController.DataBitsComboBox.SelectedItem);
            serialportConfigurationInformation.StopBits = SerialPortController.StopBitsComboBox.SelectedIndex + 1;
            serialportConfigurationInformation.Parity = SerialPortController.ParityComboBox.SelectedIndex;

            configurationInformation = serialportConfigurationInformation;
            HardwareSerialNumberWriterMainWindow.portconfiginfo = configurationInformation;

            PortChangedFunc();

            this.Close();
        }

        /// <summary>
        /// 串口配置
        /// </summary>
        public void PortChangedFunc()
        {
            SerialPortManagementClass.inifPort(configurationInformation);
            SerialPortManagementClass.Open();
            Logger.Debug("Init Port Done---" + configurationInformation.PortName + "---"+ configurationInformation.BaudRate + "---" + configurationInformation.Parity + "---" + configurationInformation.DataBits + "---" + configurationInformation.StopBits);
            Console.WriteLine("串口配置完成");
        }

        /// <summary>
        /// 重写OnClosing方法使窗口能够反复打开
        /// </summary>
        /// <param name="e"></param>
        protected override void OnClosing(CancelEventArgs e)
        {
            e.Cancel = true;
            this.Hide();
        }

        private void SerialPortController_Loaded(object sender, RoutedEventArgs e)
        {

        }
    }
}
