using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
using SNWrite.Commands;
using SNWrite.Models;

namespace SNWrite
{
    /// <summary>
    /// InitializationInput.xaml 的交互逻辑
    /// </summary>
    public partial class InitializationInput : Window
    {

        public ObservableCollection<SNStringInListBox> observableCollection
        {
            get;
            set;
        }
        public InitializationInput()
        {
            InitializeComponent();
            this.WindowStartupLocation = WindowStartupLocation.CenterScreen;

            initalizeComboBoxs();

        }

        private void initalizeComboBoxs()
        {
            // 准备数据源
            List<ModelSelect> modelSelects = new List<ModelSelect>()
            {
                new ModelSelect(){modelSelect = "单路 0x02"},
                new ModelSelect(){modelSelect = "双路6.125m道间距版本 0x03"},
                new ModelSelect(){modelSelect = "双路12.5m道间距版本 0x04"},
                new ModelSelect(){modelSelect = "双路3.125m道间距版本 0x05"}
            };
            this.ModelSelectComboBox.ItemsSource = modelSelects;
            this.ModelSelectComboBox.DisplayMemberPath = "modelSelect";
            this.ModelSelectComboBox.SelectedIndex = 0;

            List<PCBASelected> pCBASelecteds = new List<PCBASelected>()
            {
                new PCBASelected(){pcbaSelected="元森快捷制版 / 无锡鸿睿焊接 0x00"},
                new PCBASelected(){pcbaSelected="崇达制版/凌华焊接 0x01"}
            };

            this.PCBASelectComboBox.ItemsSource = pCBASelecteds;
            this.PCBASelectComboBox.DisplayMemberPath = "pcbaSelected";
            this.PCBASelectComboBox.SelectedIndex = 0;


            List<Year> years = new List<Year>()
            {
                new Year(){year=(DateTime.Now.Year-1).ToString()},
                new Year(){year=(DateTime.Now.Year).ToString()},
                new Year(){year=(DateTime.Now.Year+1).ToString()},
            };

            this.YearSelectComboBox.ItemsSource = years;
            this.YearSelectComboBox.DisplayMemberPath = "year";
            this.YearSelectComboBox.SelectedIndex = 1;

            List<WeekOfYear> weekOfYears = new List<WeekOfYear>()
            {
                new WeekOfYear(){weekofyear = ((DateTime.Now.DayOfYear/7)-1).ToString()},
                new WeekOfYear(){weekofyear = (DateTime.Now.DayOfYear/7).ToString()},
                new WeekOfYear(){weekofyear = ((DateTime.Now.DayOfYear/7)+1).ToString()},
            };

            this.WeekSelectComboBox.ItemsSource = weekOfYears;
            this.WeekSelectComboBox.DisplayMemberPath = "weekofyear";
            this.WeekSelectComboBox.SelectedIndex = 1;
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            // 对输入的年份进行正确性分析
        }

        private void WeekSelectComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            // 对输入的周次进行正确性分析
        }

        private void OKButton_Click(object sender, RoutedEventArgs e)
        {
            // 对所有输入的信息进行正确性分析 未完成
            SNinitalize sNinitalize = new SNinitalize();
            sNinitalize.Model = ModelSelectComboBox.Text;
            sNinitalize.PCBAfactory = PCBASelectComboBox.Text;
            sNinitalize.Year = YearSelectComboBox.Text;
            sNinitalize.Week = WeekSelectComboBox.Text;
            sNinitalize.SerialNumber = SerialNumberTextBox.Text;
            sNinitalize.HardWareNumber = HardWareNumberTextBox.Text;
            sNinitalize.FirmWareNumber = FirmWareNumberTextBox.Text;

            CreatJsonFromInitalizationWindow creatJsonFromInitalizationWindow = new CreatJsonFromInitalizationWindow();
            sNinitalize.SN = creatJsonFromInitalizationWindow.CreateSNFromsNinitalize(sNinitalize);
            
            creatJsonFromInitalizationWindow.CreateJson(sNinitalize);

            ObservableCollection<SNStringInListBox> temp = new ObservableCollection<SNStringInListBox>();
            foreach (var i in sNinitalize.SN.SNnumber)
            {
                temp.Add(new SNStringInListBox() { snstring = i.number });
            }

            observableCollection = temp;

            this.Close();
        }
    }
}
