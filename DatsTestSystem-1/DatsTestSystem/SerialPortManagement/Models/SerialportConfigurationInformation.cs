using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatsTestSystem.SerialPortManagement.Models
{
    public class SerialportConfigurationInformation
    {
        public string PortName { get; set; }
        public int BaudRate { get; set; }
        public object Parity { get; set; }
        public int DataBits { get; set; }
        public object StopBits { get; set; }
    }
}
