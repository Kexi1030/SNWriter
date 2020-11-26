using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace Test
{
    delegate void Handler();
    class StatusDistribution
    {
        private string FWstringBack { get; set; }
        private string QCstringBack { get; set; }

        public event Handler goCommand;

        public void getFWString(string demo)
        {
            Console.WriteLine("分发的getFWString的ThreadID为" + Thread.CurrentThread.ManagedThreadId.ToString());

            FWstringBack = demo;
            Console.WriteLine("这是烧写的返回值");
            Console.WriteLine(demo);

            CallBack callBack = new CallBack();
            goCommand += new Handler(callBack.callback);

            Action action = callBack.callback;
            action.BeginInvoke(null, null);
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
