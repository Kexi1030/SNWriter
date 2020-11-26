using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Test
{

    delegate string EventHandler(string demo);

    class CommandAggregate
    {
        SerialportConfigurationInformation serialportConfigurationInformation = new SerialportConfigurationInformation()
        {
            PortName = "COM4",
            BaudRate = 9600,
            DataBits = 8,
            StopBits = "1",
        };

        public event EventHandler ToPortManagement;

        private string commandStringGet;

        public CommandAggregate(string stringget)
        {
            commandStringGet = stringget;

            if(stringget.Substring(0,2)=="F5")
            {
                SerialPortManagement serialPortManagement = new SerialPortManagement(serialportConfigurationInformation);
                ToPortManagement += new EventHandler(serialPortManagement.SendData);

                ToPortManagement += new EventHandler(serialPortManagement.FWDataReceived);

                ToPortManagement(stringget);
            }
        }

        public void callback()
        {
            Console.WriteLine("这是commandAggregate的回调函数的ThreadID");
            Console.WriteLine(Thread.CurrentThread.ManagedThreadId.ToString());
            Console.ReadLine();
        }

    }

}
