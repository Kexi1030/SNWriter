using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using DatsTestSystem.HardwareSerialNumberWirter;
using DatsTestSystem.SerialPortManagement;
using DatsTestSystem.CommandAggregationStatusDistribution;
using System.IO;
using System.Windows.Controls;

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
            //hardwareSerialNumberWriterMainWindow.Owner = this;
            hardwareSerialNumberWriterMainWindow.WindowStartupLocation = WindowStartupLocation.CenterOwner;
        }

        public class TextBoxOutputter : TextWriter
        {
            TextBox textBox = null;

            public TextBoxOutputter(TextBox output)
            {
                textBox = output;
            }

            public override void Write(char value)
            {
                base.Write(value);
                textBox.Dispatcher.BeginInvoke(new Action(() =>
                {
                    textBox.AppendText(value.ToString());
                }));
            }

            public override Encoding Encoding
            {
                get { return System.Text.Encoding.UTF8; }
            }
        }

        private void init()
        {
            hardwareSerialNumberWriterMainWindow = new HardwareSerialNumberWriterMainWindow();
            commandAggregate = new CommandAggregate();
            statusDistribution = new StatusDistribution();
            serialPortManagementClass = new SerialPortManagementClass();

            TextBoxOutputter outputter;
            outputter = new TextBoxOutputter(hardwareSerialNumberWriterMainWindow.SNFWStatusTextBlock);
            Console.SetOut(outputter);

            serialPortManagementClass.DataReadyEvent += statusDistribution.AddMsg;
            commandAggregate.CommandDataToPort += serialPortManagementClass.AddData;
            statusDistribution.DataDistrubution += hardwareSerialNumberWriterMainWindow.getFrameBack;
            hardwareSerialNumberWriterMainWindow.sntocommandaggregate += commandAggregate.AddMsg;

            hardwareSerialNumberWriterMainWindow.StatusDistribution = statusDistribution;
            hardwareSerialNumberWriterMainWindow.commandAggregate = commandAggregate;
            hardwareSerialNumberWriterMainWindow.serialPortManagementClass = serialPortManagementClass;
        }

        private void HardwareSerialNumberButton_Click(object sender, RoutedEventArgs e)
        {
            hardwareSerialNumberWriterMainWindow.ShowOperatorNameInputWindow();
        }
    }
}
