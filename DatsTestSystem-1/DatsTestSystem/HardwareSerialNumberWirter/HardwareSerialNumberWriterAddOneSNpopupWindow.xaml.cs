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

namespace DatsTestSystem.HardwareSerialNumberWirter
{
    /// <summary>
    /// HardwareSerialNumberWriterAddOneSNpopupWindow.xaml 的交互逻辑
    /// </summary>
    public partial class HardwareSerialNumberWriterAddOneSNpopupWindow : Window
    {

        public string addOneSnString { get; set; }

        public HardwareSerialNumberWriterAddOneSNpopupWindow()
        {
            InitializeComponent();

            this.WindowStartupLocation = WindowStartupLocation.CenterOwner;
        }

        private void DefineOneSNButton_Click(object sender, RoutedEventArgs e)
        {
            string x = SNInputBox.Text.Trim();
            if (x.Substring(0,2) != "00")
                MessageBox.Show("当前序列号输入有误，请重新输入");
            else if(x.Substring(2,2) != "82")
                MessageBox.Show("当前序列号输入有误，请重新输入");
            else if(x.Substring(4,2) != "02" && x.Substring(4, 2) != "03" && x.Substring(4, 2) != "04" && x.Substring(4, 2) != "05")
                MessageBox.Show("当前序列号输入有误，请重新输入");
            else if(x.Substring(6,2) != "00" && x.Substring(6,2) != "01")
                MessageBox.Show("当前序列号输入有误，请重新输入");
            else
                this.Close();
        }

        private void SNInputBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (SNInputBox.Text.Trim().Length != 20)
            {
                DefineOneSNButton.IsEnabled = false;
            }
            else
            {
                DefineOneSNButton.IsEnabled = true;
            }
        }
    }
}
