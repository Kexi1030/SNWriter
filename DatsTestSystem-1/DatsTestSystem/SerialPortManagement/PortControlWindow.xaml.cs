using System;
using System.Collections.Generic;
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
using DatsTestSystem.SerialPortManagement.Models;

namespace DatsTestSystem.SerialPortManagement
{
    public enum Parity
    {
        //
        // 摘要:
        //     不发生奇偶校验检查。
        None = 0,
        //
        // 摘要:
        //     设置奇偶校验位，使位数等于奇数。
        Odd = 1,
        //
        // 摘要:
        //     设置奇偶校验位，使位数等于偶数。
        Even = 2,
        //
        // 摘要:
        //     将奇偶校验位保留为 1。
        Mark = 3,
        //
        // 摘要:
        //     将奇偶校验位保留为 0。
        Space = 4
    }

    public enum StopBits
    {
        //
        // 摘要:
        //     不使用停止位。 System.IO.Ports.SerialPort.StopBits 属性不支持此值。
        None = 0,
        //
        // 摘要:
        //     使用一个停止位。
        One = 1,
        //
        // 摘要:
        //     使用两个停止位。
        Two = 2,
        //
        // 摘要:
        //     使用 1.5 个停止位。
        OnePointFive = 3
    }


    /// <summary>
    /// PortControlWindow.xaml 的交互逻辑
    /// </summary>
    public partial class PortControlWindow : Window
    {
        public SerialportConfigurationInformation configurationInformation { get; set; }
        public HardwareSerialNumberWirter.HardwareSerialNumberWriterMainWindow HardwareSerialNumberWriterMainWindow;

        public PortControlWindow()
        {
            InitializeComponent();

            configurationInformation = new SerialportConfigurationInformation()
            {
                PortName = "COM1",
                BaudRate = 9600,
                DataBits = 8,
                StopBits = 1,
                Parity = (Parity)0
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
            serialportConfigurationInformation.StopBits = (StopBits)Convert.ToInt32(SerialPortController.StopBitsComboBox.SelectedItem.ToString());
            serialportConfigurationInformation.Parity = (Parity)Convert.ToInt32(SerialPortController.ParityComboBox.SelectedIndex.ToString());

            configurationInformation = serialportConfigurationInformation;
            HardwareSerialNumberWriterMainWindow.portconfiginfo = configurationInformation;
            this.Close();
        }
    }
}
