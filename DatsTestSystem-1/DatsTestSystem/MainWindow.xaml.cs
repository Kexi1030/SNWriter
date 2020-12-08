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
        SerialPortManagementClass serialPortManagementClass;
        CommandAggregate commandAggregate;
        StatusDistribution statusDistribution;
        HardwareSerialNumberWriterMainWindow hardwareSerialNumberWriterMainWindow;

        public MainWindow()
        {
            InitializeComponent();
            this.WindowStartupLocation = WindowStartupLocation.CenterScreen;

            init();
            initFW();
        }

        private void initFW()
        {
            hardwareSerialNumberWriterMainWindow = new HardwareSerialNumberWriterMainWindow();
            hardwareSerialNumberWriterMainWindow.Owner = this;
            hardwareSerialNumberWriterMainWindow.WindowStartupLocation = WindowStartupLocation.CenterOwner;
        }

        private void init()
        {
            serialPortManagementClass = new SerialPortManagementClass();
            commandAggregate = new CommandAggregate();
            statusDistribution = new StatusDistribution();

            hardwareSerialNumberWriterMainWindow.StatusDistribution = statusDistribution;

            serialPortManagementClass.DataReadyEvent += statusDistribution.AddMsg;
            commandAggregate.CommandDataToPort += serialPortManagementClass.AddData;
            statusDistribution._DataToSn += hardwareSerialNumberWriterMainWindow.getFrameBack;
            hardwareSerialNumberWriterMainWindow.sntocommandaggregate += commandAggregate.AddMsg;
        }

        private void HardwareSerialNumberButton_Click(object sender, RoutedEventArgs e)
        {
            hardwareSerialNumberWriterMainWindow.ShowOperatorNameInputWindow();
        }
    }
}
