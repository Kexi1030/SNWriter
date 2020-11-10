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
using System.Windows.Navigation;
using System.Windows.Shapes;

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
            InitializeComponent(); // 

            SNList.ItemsSource = new List<SNinList>
            {
                new SNinList("0301 2032 0017 5152")
                ,new SNinList("0301 2032 0018 5152")
            };

            SNList.DisplayMemberPath = "sn";
        }
    }


}
