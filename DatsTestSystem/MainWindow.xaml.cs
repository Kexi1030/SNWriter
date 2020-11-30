using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using DatsTestSystem.HardwareSerialNumberWirter;
using DatsTestSystem.SerialPortManagement;
using DatsTestSystem.CommandAggregationStatusDistribution;

namespace DatsTestSystem
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        HardwareSerialNumberWriterMainWindow thisFWWindow;
        
        public MainWindow()
        {
            InitializeComponent();

            SerialPortManagementClass serialPortManagementClass = new SerialPortManagementClass();
            CommandAggregate commandAggregate = new CommandAggregate();
            StatusDistribution statusDistribution = new StatusDistribution();
            HardwareSerialNumberWriterMainWindow hardwareSerialNumberWriterMainWindow = new HardwareSerialNumberWriterMainWindow();
            thisFWWindow = hardwareSerialNumberWriterMainWindow;
            thisFWWindow.WindowStartupLocation = WindowStartupLocation.CenterOwner;

            hardwareSerialNumberWriterMainWindow.command = commandAggregate;
            hardwareSerialNumberWriterMainWindow.status = statusDistribution;
            hardwareSerialNumberWriterMainWindow.SPM = serialPortManagementClass;

            this.WindowStartupLocation = WindowStartupLocation.CenterScreen;
        }

        private void HardwareSerialNumberButton_Click(object sender, RoutedEventArgs e)
        {
            thisFWWindow.Owner = this;
            thisFWWindow.ShowOperatorNameInputWindow();
        }
    }
}
