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

namespace DatsTestSystem.HardwareSerialNumberWirter
{
    /// <summary>
    /// HardwareSerialNumberWriterInitialSNinofWindow.xaml 的交互逻辑
    /// </summary>
    public partial class HardwareSerialNumberWriterInitialSNinofWindow : Window
    {
        public HardwareSerialNumberWriterInitialSNinofWindow()
        {
            InitializeComponent();
            this.WindowStartupLocation = WindowStartupLocation.CenterScreen;

            InitComboBoxes();
        }

        private void InitComboBoxes()
        {
            ComboboxDataUsedInInitialSNinfoWindow comboboxDataUsedInInitialSNinfoWindow = new ComboboxDataUsedInInitialSNinfoWindow();

            this.ModelSelectComboBox.ItemsSource = comboboxDataUsedInInitialSNinfoWindow.modelSelects;
            this.ModelSelectComboBox.DisplayMemberPath = "modelSelect";
            this.ModelSelectComboBox.SelectedIndex = 0;

            this.PCBASelectComboBox.ItemsSource = comboboxDataUsedInInitialSNinfoWindow.pCBASelecteds;
            this.PCBASelectComboBox.DisplayMemberPath = "pcbaSelected";
            this.PCBASelectComboBox.SelectedIndex = 0;

            this.YearSelectComboBox.ItemsSource = comboboxDataUsedInInitialSNinfoWindow.years;
            this.YearSelectComboBox.DisplayMemberPath = "year";
            this.YearSelectComboBox.SelectedIndex = 1;

            this.WeekSelectComboBox.ItemsSource = comboboxDataUsedInInitialSNinfoWindow.weekOfYears;
            this.WeekSelectComboBox.DisplayMemberPath = "weekofyear";
            this.WeekSelectComboBox.SelectedIndex = 1;
        }


        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }


        private void YearSelectComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            // 对输入的年份进行正确性分析
        }

        private void WeekSelectComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            // 对输入的周次进行正确性分析
        }

        private void OKButton_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
