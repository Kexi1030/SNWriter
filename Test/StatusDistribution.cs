using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Test
{
    class StatusDistribution
    {
        private string FWstringBack { get; set; }
        private string QCstringBack { get; set; }

        public void getFWString(string demo)
        {
            FWstringBack = demo;
            Console.WriteLine("这是烧写的返回值");
            Console.WriteLine(demo);
            Console.ReadLine();
        }

        public void getQCString(string demo )
        {
            QCstringBack = demo;
            Console.WriteLine(demo);
            Console.WriteLine("这是QC的返回值");
            Console.ReadLine();
        }
    }
}
