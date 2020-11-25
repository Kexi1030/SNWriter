using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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

                ToPortManagement += new EventHandler(serialPortManagement.QCDataReceived);

                Console.WriteLine("开始写");
                ToPortManagement(stringget);
            }
        }

    }

}
