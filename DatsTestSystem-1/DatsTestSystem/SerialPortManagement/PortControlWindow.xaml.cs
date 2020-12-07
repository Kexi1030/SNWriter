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
    /// <summary>
    /// PortControlWindow.xaml 的交互逻辑
    /// </summary>
    public partial class PortControlWindow : Window
    {
        public SerialportConfigurationInformation configurationInformation { get; set; }

        public PortControlWindow()
        {
            InitializeComponent();
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void OKButton_Click(object sender, RoutedEventArgs e)
        {
            SerialportConfigurationInformation serialportConfigurationInformation = new SerialportConfigurationInformation();

            serialportConfigurationInformation.PortName = (string)SerialPortController.PortNamesComboBox.SelectedItem;
            serialportConfigurationInformation.BaudRate = (int)SerialPortController.BaudRateComboBox.SelectedItem;
            serialportConfigurationInformation.DataBits = (int)SerialPortController.DataBitsComboBox.SelectedItem;
            serialportConfigurationInformation.StopBits = (string)SerialPortController.StopBitsComboBox.SelectedItem;
            serialportConfigurationInformation.Parity = (string)SerialPortController.ParityComboBox.SelectedItem;

            configurationInformation = serialportConfigurationInformation;
        }
    }
}
