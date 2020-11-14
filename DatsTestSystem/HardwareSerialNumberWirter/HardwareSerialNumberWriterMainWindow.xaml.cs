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
    /// HardwareSerialNumberWriterMainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class HardwareSerialNumberWriterMainWindow : Window
    {
        /*
         * 存在问题 第一次输入操作人姓名的时候 点击X也会进入烧写主界面 未解决
         */
        public HardwareSerialNumberWriterMainWindow()
        {
            InitializeComponent();
            this.WindowStartupLocation = WindowStartupLocation.CenterScreen;

            HardwareSerialNumberWriterInputUserNameWindow hardwareSerialNumberWriterInputUserNameWindow = new HardwareSerialNumberWriterInputUserNameWindow();
            // hardwareSerialNumberWriterInputUserNameWindow.Owner = this;
            hardwareSerialNumberWriterInputUserNameWindow.ShowDialog();

            this.OperatorNameTextBlock.DataContext = hardwareSerialNumberWriterInputUserNameWindow.operatorName;
        }

        private void InitialButton_Click(object sender, RoutedEventArgs e)
        {
            HardwareSerialNumberWriterInitialSNinofWindow hardwareSerialNumberWriterInitialSNinofWindow = new HardwareSerialNumberWriterInitialSNinofWindow();
            hardwareSerialNumberWriterInitialSNinofWindow.Owner = this;
            hardwareSerialNumberWriterInitialSNinofWindow.ShowDialog();
        }

        private void ModifyUserName_Click(object sender, RoutedEventArgs e)
        {
            HardwareSerialNumberWriterInputUserNameWindow hardwareSerialNumberWriterInputUserNameWindow = new HardwareSerialNumberWriterInputUserNameWindow();
            hardwareSerialNumberWriterInputUserNameWindow.Owner = this;
            hardwareSerialNumberWriterInputUserNameWindow.ShowDialog();

            if(hardwareSerialNumberWriterInputUserNameWindow.operatorName != null)
            {
                this.OperatorNameTextBlock.DataContext = hardwareSerialNumberWriterInputUserNameWindow.operatorName;
            }
            
        }

        private void InitialSNListFromFileButton_Click(object sender, RoutedEventArgs e)
        {
            /*
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Title = "请选择需要导入的Json配置文件";
            openFileDialog.Filter = "json files (*.json)|*.json|All files (*.*)|*.*";
            openFileDialog.InitialDirectory = Application.StartupPath;
            if (openFileDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                CreatJsonFromInitalizationWindow creatJsonFromInitalizationWindow = new CreatJsonFromInitalizationWindow();
                SNinitalize sNinitalize = creatJsonFromInitalizationWindow.CreateSNFromJsonFile(openFileDialog.FileName);

                foreach (var i in sNinitalize.SN.SNnumber)
                {
                    sNStringInListBoxes.Add(new SNStringInListBox() { snstring = i.number });
                }
            }
            */
        }

        private void AddOneSNstringButton_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
