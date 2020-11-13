using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace SNWrite.Models
{
    class InitializationWindowDataUsed
    {

    }

    class ModelSelect
    {
        public string modelSelect { get; set; }
    }

    class PCBASelected
    {
        public string pcbaSelected { get; set; }
    }

    class Year
    {
        public string year { get; set; }
    }

    class WeekOfYear
    {
        public string weekofyear { get; set; }
    }

    public class OperatorName: INotifyPropertyChanged
    {
        private string _operatorname;
        public string operatorname
        {
            get
            {
                return _operatorname;
            }
            set
            { 
                _operatorname = value;
                OnPropertyChanged("operatorname");
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged(string propertyName)
        {
            PropertyChangedEventHandler handler = this.PropertyChanged;
            if (handler != null)
            {
                handler(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }

    public class SNStringInListBox
        /*
         * 列表中的sn的数据类
         */
    {
        private string Snstring;

        public string snstring
        {
            get
            {
                return Snstring;
            }
            set
            {
                Snstring = value;
            }
        }


    }
}

