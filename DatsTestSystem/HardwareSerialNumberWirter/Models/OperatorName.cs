using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatsTestSystem.HardwareSerialNumberWirter.Models
{
    public class OperatorName : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

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

        private void OnPropertyChanged(string propertyName)
        {
            PropertyChangedEventHandler handler = this.PropertyChanged;
            if (handler != null)
            {
                handler(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }
}
