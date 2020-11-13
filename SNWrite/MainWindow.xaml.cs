using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
using System.Windows.Interop;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using SNWrite.Models;
using MessageBox = System.Windows.Forms.MessageBox;

namespace SNWrite
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    /// 



    public partial class MainWindow : Window
    {
        ObservableCollection<SNStringInListBox> sNStringInListBoxes = new ObservableCollection<SNStringInListBox>
        {
            new SNStringInListBox(){snstring = "1"},
            new SNStringInListBox(){snstring = "2"},
        };

        public MainWindow()
        {
            InitializeComponent();
            this.WindowStartupLocation = WindowStartupLocation.CenterScreen;

            InputUser inputUser = new InputUser();
            inputUser.ShowDialog();
           
            this.OperatorNameTextBlock.DataContext = inputUser.OName;


            //this.SNList.DataContext = sNStringInListBox;

            SNList.ItemsSource = sNStringInListBoxes;
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
            inputUser.ShowDialog();

            this.OperatorNameTextBlock.DataContext = inputUser.OName;
        }

        private void InitialSNListFromFileButton_Click(object sender, RoutedEventArgs e)
        {
            FolderBrowserDialog folderBrowserDialog = new FolderBrowserDialog();
            DialogResult result = folderBrowserDialog.ShowDialog();
        }

        private void AddOneSNstringButton_Click(object sender, RoutedEventArgs e)
        {
            // 这里需要对输入的onesn进行校验 未完成
            PopupAddOneSN popupAddOneSN = new PopupAddOneSN();
            popupAddOneSN.Owner = this;
            popupAddOneSN.WindowStartupLocation = WindowStartupLocation.CenterScreen;
            popupAddOneSN.ShowDialog();

            sNStringInListBoxes.Add(popupAddOneSN.SNStringInListBox);
        }
    }


}
