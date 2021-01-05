using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Collections.ObjectModel;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace ListBoxTest
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        ObservableCollection<Model> listNumber = new ObservableCollection<Model>();
        int k = 1;
        public MainWindow()
        {
            InitializeComponent();
            for (int i = 0; i < 99; i++)
            {
                listNumber.Add(new Model() { Number = i });
            }
            listBox.ItemsSource = listNumber;
        }
        private List<Model> GetNumber()
        {
            List<Model> listNumber = new List<Model>();
            for(int i = 0;i<99;i++)
            {
                listNumber.Add(new Model() { Number = i });
            }
            return listNumber;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            int temp = listNumber[k].Number;
            listNumber[k] = new Model() { Number =temp,mark = 1};
            k += 2;
            //this.listBox.ItemsSource = listNumber;
        }
    }
    public class Model
    {
        public int Number { get; set; }
        public int mark { get; set; }
    }
}
