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
using DatsTestSystem.HardwareSerialNumberWirter.Models;
using DatsTestSystem.Log;

namespace DatsTestSystem.HardwareSerialNumberWirter
{
    /// <summary>
    /// HardwareSerialNumberWriterInputUserNameWindow.xaml 的交互逻辑
    /// </summary>
    public partial class HardwareSerialNumberWriterInputUserNameWindow : Window
    {
        public OperatorName operatorName { get; set; } = null;
        public bool NameOr { get; set; } = false; // 是否输入了名字

        public HardwareSerialNumberWriterInputUserNameWindow()
        {
            InitializeComponent();
            // this.WindowStartupLocation = WindowStartupLocation.CenterScreen;
        }

        private void NameInputBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (NameInputBox.Text.Trim().Length > 0)
            {
                DefineOperatorButton.IsEnabled = true;
            }
            else
            {
                DefineOperatorButton.IsEnabled = false;
            }
        }

        private void DefineOperator_Click(object sender, RoutedEventArgs e)
        {
            operatorName = new OperatorName() { operatorname = NameInputBox.Text.Trim() };
            NameOr = true;

            Logger.Debug("OperatorName---" + NameInputBox.Text.Trim());
            this.Close();
        }
    }
}
