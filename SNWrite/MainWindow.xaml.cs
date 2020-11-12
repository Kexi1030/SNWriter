using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using SNWrite.Models;

namespace SNWrite
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    /// 

    class SNinList
    {
        public SNinList(string sn)
        {
            this.sn = sn;
        }

        public string sn { set; get; }
    }

    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            this.WindowStartupLocation = WindowStartupLocation.CenterScreen;

            InputUser inputUser = new InputUser();
            //inputUser.Owner = this;
            inputUser.ShowDialog();

            //OperatorName operatorName = inputUser.OName;
           
            this.OperatorNameTextBlock.DataContext = inputUser.OName;

            SNList.ItemsSource = new List<SNinList>
            {
                new SNinList("0301 2032 0017 5152")
                ,new SNinList("0301 2032 0018 5152")
            };

            SNList.DisplayMemberPath = "sn";

        }

        private void InitialButton_Click(object sender, RoutedEventArgs e)
        {
            InitializationInput initializationInput = new InitializationInput();
            initializationInput.Owner = this;
            initializationInput.ShowDialog();
        }



        private void ModifyUserName_Click(object sender, RoutedEventArgs e)
        {
            InputUser inputUser = new InputUser();
            //inputUser.Owner = this;
            inputUser.ShowDialog();
        }

        private void InitialSNListFromFileButton_Click(object sender, RoutedEventArgs e)
        {
            FolderBrowserDialog folderBrowserDialog = new FolderBrowserDialog();
            DialogResult result = folderBrowserDialog.ShowDialog();
        }
    }


}
