using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DatsTestSystem.SerialPortManagement;
using DatsTestSystem.SerialPortManagement.Models;
using System.Threading;
using System.Windows;

namespace DatsTestSystem.CommandAggregationStatusDistribution
{
    /// <summary>
    /// 根据发来的指令 分为烧写还是QC 执行不同的操作
    /// </summary>
    class CommandAggregate
    {
        private SerialportConfigurationInformation SerialportConfigurationInformation = new SerialportConfigurationInformation() { PortName = "COM1", BaudRate = 9600, DataBits = 8, StopBits = "1" };

        private string CommandPending { get; set; }
        public string StringBack { get; set; }

        SerialPortManagementClass serialPortManagement;

        public CommandAggregate()
        {
            serialPortManagement = new SerialPortManagementClass(SerialportConfigurationInformation);

        }

        public void FWSend(string CommandString)
        {
            if (CommandString == "FWF500000000E00300FFFF5F")
            {
                FWReadCommand();
            }
            else
            {
                serialPortManagement.SendData(CommandString.Remove(0, 2));


                Thread.Sleep(800);
            }
        }

        private string FWReadCommand() => StringBack = serialPortManagement.DataReceived();


        private void QCCommand(string QCString)
        {

        }

    }
}
