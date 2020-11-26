using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Test
{
    class CallBack
    {
        int temp
        {
            get; set;
        }

        public CallBack()
            {
            temp = 0;
            }
        public void callback()
        {
            temp = 1;
            Console.WriteLine("这是CallBack的回调函数的ThreadID");
            Console.WriteLine(Thread.CurrentThread.ManagedThreadId.ToString());
            Console.ReadLine();
        }
    }
}
