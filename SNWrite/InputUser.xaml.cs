using System;
using System.Collections.Generic;
using System.Globalization;
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
using SNWrite.Models;

namespace SNWrite
{
    /// <summary>
    /// InputUser.xaml 的交互逻辑
    /// </summary>
    public partial class InputUser : Window
    {

        public InputUser()
        {
            InitializeComponent();
            this.WindowStartupLocation = WindowStartupLocation.CenterScreen;
        }

        private void NameInputBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            if(NameInputBox.Text.Trim().Length > 0 )
            {
                DefineOperatorButton.IsEnabled = true;
            }
            else
            {
                DefineOperatorButton.IsEnabled = false;
            }
        }

        private string DefineOperator_Click(object sender, RoutedEventArgs e)
        {
            operatorName.operatorname = this.NameInputBox.Text.Trim();
            this.Close();
        }
    }
}
