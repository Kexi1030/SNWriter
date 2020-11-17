using DatsTestSystem.HardwareSerialNumberWirter.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Test
{
    class Program
    {
        static void Main(string[] args)
        {
            ReportCreation reportCreation = new ReportCreation(@"F:\Code\硬件序列号烧写\Test\OUTPUT.json");
            reportCreation.CreateReport();
        }
    }
}
