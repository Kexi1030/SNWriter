using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace Test
{
    class Program
    {
        delegate void EventHandler();

        static void Main(string[] args)
        {
            Console.WriteLine("当前的线程是" + Thread.CurrentThread.ManagedThreadId.ToString());
            CommandAggregate commandAggregate = new CommandAggregate("F500000000E00300FFFF5F");
            Console.ReadLine();
        }

    }
}
