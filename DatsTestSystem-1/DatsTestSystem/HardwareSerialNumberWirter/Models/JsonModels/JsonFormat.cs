using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatsTestSystem.HardwareSerialNumberWirter.Models.JsonModels
{
    class JsonFormat
    {
        public string Model { get; set; }
        public string PCBAfactory { get; set; }
        public string Year { get; set; }
        public string Week { get; set; }
        public string SerialNumber { get; set; }
        public string HardWareNumber { get; set; }
        public string FirmWareNumber { get; set; }
        public string[] SnList { get; set; }

        public EachSNStatus[] eachSNStatuses { get; set; }
    }
}
