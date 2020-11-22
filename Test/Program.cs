using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DatsTestSystem.SerialPortManagement;
using DatsTestSystem.SerialPortManagement.Models;
using System.Threading;

namespace Test
{
    class Program
    {
        static void Main(string[] args)
        {
            SerialPortManagement serialPortManagement = new SerialPortManagement();
            var data = serialPortManagement.strToHexByte("F500000000A00A120000008203012034432151520AFF5F0C0D0E0FFFFF5F");

            
            SerialportConfigurationInformation serialportConfigurationInformation = new SerialportConfigurationInformation() { PortName ="COM4",BaudRate=9600,DataBits=8,StopBits="1"};
            /*bool result = serialPortManagement.SendData(data,serialportConfigurationInformation);
            Console.WriteLine(result);
            */

            //Thread.Sleep(1000);
            var data2 = serialPortManagement.strToHexByte("F500000000E00300FFFF5F");
            bool result2 = serialPortManagement.SendData(data2, serialportConfigurationInformation);
            Console.WriteLine(result2);

            Thread.Sleep(1000);
            string data3 = serialPortManagement.DataReceived();
            Console.WriteLine(data3);

            Console.ReadLine();

        }
    }
}
