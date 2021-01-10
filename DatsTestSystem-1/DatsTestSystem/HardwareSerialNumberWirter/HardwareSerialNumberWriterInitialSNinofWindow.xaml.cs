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
using DatsTestSystem.HardwareSerialNumberWirter.Models.JsonModels;
using DatsTestSystem.HardwareSerialNumberWirter.Commands;
using DatsTestSystem.HardwareSerialNumberWirter.Models;
using System.Collections.ObjectModel;

namespace DatsTestSystem.HardwareSerialNumberWirter
{
    /// <summary>
    /// HardwareSerialNumberWriterInitialSNinofWindow.xaml 的交互逻辑
    /// </summary>
    /// 

    public partial class HardwareSerialNumberWriterInitialSNinofWindow : Window
    {

        public ObservableCollection<ListBoxItems> observableCollection
        {
            get;
            set;
        }


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
            // this.YearSelectComboBox.IsEditable = true;

            this.WeekSelectComboBox.ItemsSource = comboboxDataUsedInInitialSNinfoWindow.weekOfYears;
            this.WeekSelectComboBox.DisplayMemberPath = "weekofyear";
            this.WeekSelectComboBox.SelectedIndex = 1;
            // this.WeekSelectComboBox.IsEditable = true;
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
            // 对所有输入的信息进行正确性分析 未完成

            JsonFormat jsonFormat = new JsonFormat();

            jsonFormat.Model = ModelSelectComboBox.Text;
            jsonFormat.PCBAfactory = PCBASelectComboBox.Text;
            jsonFormat.Year = YearSelectComboBox.Text;
            jsonFormat.Week = WeekSelectComboBox.Text;
            jsonFormat.SerialNumber = SerialNumberTextBox.Text;
            jsonFormat.HardWareNumber = HardWareNumberTextBox.Text;
            jsonFormat.FirmWareNumber = FirmWareNumberTextBox.Text;

            try
            {
                jsonFormat.SnList = CreateSnListinJsonFormat(jsonFormat);

                ObservableCollection<ListBoxItems> temp = new ObservableCollection<ListBoxItems>();

                foreach (string i in jsonFormat.SnList)
                {
                    temp.Add(new ListBoxItems() { snstring = i });
                }
                observableCollection = temp;

                // 保存在本地
                JsonCreate jsonCreate = new JsonCreate();
                jsonCreate.CreateJson(jsonFormat);

                this.Close();
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
                MessageBox.Show("输入有误请检查");
            }
        }

        private string[] CreateSnListinJsonFormat(JsonFormat sNinitalize)
        {
            string SerialNumber = sNinitalize.SerialNumber;

            char[] SplitChars = { ',', '，' };

            List<string> list = new List<string>(SerialNumber.Split(SplitChars));

            string DoneSerialNumber = createShortSNSerialNumber(sNinitalize);

            List<string> snstringlist = new List<string>();
            // C#中的数组是不支持动态添加元素的，只能创建固定大小的数组
            // 使用泛型list< T >,先将元素存入list中，最后使用ToArray()转成数组

            foreach (string eachstring in list)
            {
                bool containOr = eachstring.Contains("-");
                if (containOr)
                {
                    List<string> listContainsTwo = new List<string>(eachstring.Split('-'));
                    int startNum = Convert.ToInt32(listContainsTwo[0]);
                    int endNum = Convert.ToInt32(listContainsTwo[1]);
                    for (int i = startNum; i < endNum + 1; i++)
                    {
                        string CurrentNumber = string.Format("{0:D4}", i);
                        string temp = DoneSerialNumber.Insert(12, CurrentNumber);

                        snstringlist.Add(temp);
                    }
                }
                else
                {
                    string temp = DoneSerialNumber.Insert(12, string.Format("{0:D4}", int.Parse(eachstring)));
                    snstringlist.Add(temp);
                }
            }

            string[] SnList = snstringlist.ToArray();

            return SnList;
        }

        private string createShortSNSerialNumber (JsonFormat sNinitalize)
        {
            string shortSNSerialNumber = "0082";

            switch (sNinitalize.Model)
            {
                case "单路 0x02":
                    shortSNSerialNumber += "02";
                    break;
                case "双路6.125m道间距版本 0x03":
                    shortSNSerialNumber += "03";
                    break;
                case "双路12.5m道间距版本 0x04":
                    shortSNSerialNumber += "04";
                    break;
                case "双路3.125m道间距版本 0x05":
                    shortSNSerialNumber += "05";
                    break;
            }

            switch (sNinitalize.PCBAfactory)
            {
                case "元森快捷制版 / 无锡鸿睿焊接 0x00":
                    shortSNSerialNumber += "00";
                    break;
                case "崇达制版/凌华焊接 0x01":
                    shortSNSerialNumber += "01";
                    break;
            }

            shortSNSerialNumber += sNinitalize.Year.Substring(2);
            shortSNSerialNumber += sNinitalize.Week.PadLeft(2,'0');
            Console.WriteLine(sNinitalize.Week.PadLeft(2, '0'));

            shortSNSerialNumber += sNinitalize.HardWareNumber;
            Console.WriteLine(sNinitalize.HardWareNumber);
            shortSNSerialNumber += sNinitalize.FirmWareNumber;

            return shortSNSerialNumber;
        }
    }
}
