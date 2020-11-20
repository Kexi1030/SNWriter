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
using Application = System.Windows.Forms.Application;
using MessageBox = System.Windows.Forms.MessageBox;
using SNWrite.Commands;

namespace SNWrite
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    /// 

    public partial class MainWindow : Window
    {
        ObservableCollection<SNStringInListBox> sNStringInListBoxes = new ObservableCollection<SNStringInListBox>();

        public MainWindow()
        {
            InitializeComponent();
            this.WindowStartupLocation = WindowStartupLocation.CenterScreen;

           // InputUser inputUser = new InputUser();
            //inputUser.ShowDialog();
           
            //this.OperatorNameTextBlock.DataContext = inputUser.OName;

            SNList.ItemsSource = sNStringInListBoxes;
        }

        private void InitialButton_Click(object sender, RoutedEventArgs e)
        {
            InitializationInput initializationInput = new InitializationInput();
            initializationInput.Owner = this;
            initializationInput.ShowDialog();

             
            foreach (var i in initializationInput.observableCollection)
            {
                sNStringInListBoxes.Add(i);
            }
        }

        private void ModifyUserName_Click(object sender, RoutedEventArgs e)
        {
            InputUser inputUser = new InputUser();
            inputUser.ShowDialog();

            this.OperatorNameTextBlock.DataContext = inputUser.OName;
        }

        private void InitialSNListFromFileButton_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Title = "请选择需要导入的Json配置文件";
            openFileDialog.Filter = "json files (*.json)|*.json|All files (*.*)|*.*";
            openFileDialog.InitialDirectory = Application.StartupPath;
            if(openFileDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                CreatJsonFromInitalizationWindow creatJsonFromInitalizationWindow = new CreatJsonFromInitalizationWindow();
                SNinitalize sNinitalize = creatJsonFromInitalizationWindow.CreateSNFromJsonFile(openFileDialog.FileName);

                foreach (var i in sNinitalize.SN.SNnumber)
                {
                    sNStringInListBoxes.Add(new SNStringInListBox() { snstring = i.number });
                }
            }



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
