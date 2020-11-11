using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
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

namespace SNWrite
{
    /// <summary>
    /// PopupAddOneSN.xaml 的交互逻辑
    /// </summary>
    public partial class PopupAddOneSN : Window
    {
        public PopupAddOneSN()
        {
            InitializeComponent();
        }

        private void SNInputBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            // 还需要对SN进行逻辑判断
            if(SNInputBox.Text.Trim().Length > 0)
            {
                DefineOneSNButton.IsEnabled = true;
            }
            else
            {
                DefineOneSNButton.IsEnabled = false;
            }
        }
    }
}
