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
using System.Windows.Navigation;
using System.Windows.Shapes;
using DatsTestSystem.HardwareSerialNumberWirter;

namespace DatsTestSystem
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            this.WindowStartupLocation = WindowStartupLocation.CenterScreen;
        }

        private void HardwareSerialNumberButton_Click(object sender, RoutedEventArgs e)
        {
            HardwareSerialNumberWriterInputUserNameWindow hardwareSerialNumberWriterInputUserNameWindow = new HardwareSerialNumberWriterInputUserNameWindow();
            hardwareSerialNumberWriterInputUserNameWindow.Owner = this;
            hardwareSerialNumberWriterInputUserNameWindow.WindowStartupLocation = WindowStartupLocation.CenterOwner;
            hardwareSerialNumberWriterInputUserNameWindow.ShowDialog();
            

            if(hardwareSerialNumberWriterInputUserNameWindow.NameOr)
            {
                HardwareSerialNumberWriterMainWindow hardwareSerialNumberWriterMainWindow = new HardwareSerialNumberWriterMainWindow(hardwareSerialNumberWriterInputUserNameWindow.operatorName);
                hardwareSerialNumberWriterMainWindow.Owner = this;
                hardwareSerialNumberWriterMainWindow.ShowDialog();
            }
        }
    }
}
