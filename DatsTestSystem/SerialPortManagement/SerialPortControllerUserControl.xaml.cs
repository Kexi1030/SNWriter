using System;
using System.Collections.Generic;
using System.IO.Ports;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace DatsTestSystem.SerialPortManagement
{
    /// <summary>
    /// SerialPortController.xaml 的交互逻辑
    /// </summary>
    public partial class SerialPortController : UserControl
    {
        public SerialPortController()
        {
            InitializeComponent();

            //List<string> PortName = SerialPort.GetPortNames().ToList();
            //Console.WriteLine(PortName);
            PortNamesComboBox.ItemsSource = SerialPort.GetPortNames();
            PortNamesComboBox.SelectedIndex = 0;

            List<string> BaudRate = new List<string>() {"9600" ,"19200","38400","57600"};
            BaudRateComboBox.ItemsSource = BaudRate;
            BaudRateComboBox.SelectedIndex = 0;

            List<int> DataBits = new List<int>() { 8, 7, 6 ,5};
            DataBitsComboBox.ItemsSource = DataBits;
            DataBitsComboBox.SelectedIndex = 0;

            List<string> StopBits = new List<string>() { "1", "2", "3" };
            StopBitsComboBox.ItemsSource = StopBits;
            StopBitsComboBox.SelectedIndex = 0;

            List<string> Parity = new List<string>() { "None", "Odd", "Even", "Mark", "Space" };
            ParityComboBox.ItemsSource = Parity;
            ParityComboBox.SelectedIndex = 0;
        }

    }
}
