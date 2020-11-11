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
using SNWrite.Models;

namespace SNWrite
{
    /// <summary>
    /// InitializationInput.xaml 的交互逻辑
    /// </summary>
    public partial class InitializationInput : Window
    {
        public InitializationInput()
        {
            InitializeComponent();
            this.WindowStartupLocation = WindowStartupLocation.CenterScreen;

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
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
