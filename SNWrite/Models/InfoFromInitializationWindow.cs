using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SNWrite.Models
{
    public class SNinitalize
    {
        public string Model { get; set; }
        public string PCBAfactory { get; set; }
        public string Year { get; set; }
        public string Week { get; set; }
        public string SerialNumber { get; set; }
        public string HardWareNumber { get; set; }
        public string FirmWareNumber { get; set; }
        public SN SN { get; set; }
    }

    public class InfoFromInitializationWindow
    {
    }

    public class SN
    {
        public Snnumber[] SNnumber { get; set; }
    }

    public class Snnumber
    {
        public string number { get; set; }
        public bool Done { get; set; }
    }

}
