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
        SerialPortManagementClass serialPortManagementClass;
        CommandAggregate commandAggregate;
        StatusDistribution statusDistribution;
        HardwareSerialNumberWriterMainWindow hardwareSerialNumberWriterMainWindow;

        public MainWindow()
        {
            InitializeComponent();

            serialPortManagementClass = new SerialPortManagementClass();
            commandAggregate = new CommandAggregate();
            statusDistribution = new StatusDistribution();
            hardwareSerialNumberWriterMainWindow = new HardwareSerialNumberWriterMainWindow();

            serialPortManagementClass.DataReadyEvent += statusDistribution.AddMsg;
            commandAggregate.CommandDataToPort += serialPortManagementClass.AddData;

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
