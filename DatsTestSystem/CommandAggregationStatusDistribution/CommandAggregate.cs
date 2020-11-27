using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DatsTestSystem.SerialPortManagement;
using DatsTestSystem.SerialPortManagement.Models;
using System.Threading;
using System.Windows;
using DatsTestSystem.HardwareSerialNumberWirter.Commands;

namespace DatsTestSystem.CommandAggregationStatusDistribution
{
    public delegate bool FWDelegate(string CommandString, ref string stringGet);
    /// <summary>
    /// 根据发来的指令 分为烧写还是QC 执行不同的操作
    /// </summary>
    class CommandAggregate
    {
        private SerialportConfigurationInformation SerialportConfigurationInformation = new SerialportConfigurationInformation() { PortName = "COM1", BaudRate = 9600, DataBits = 8, StopBits = "1" };

        public string StringBack { get; set; }

        SerialPortManagementClass serialPortManagement;

        public CommandAggregate()
        {
            serialPortManagement = new SerialPortManagementClass(SerialportConfigurationInformation);

        }

        public void FWSend(string CommandString,ref string stringGet)
        { 
            FWDelegate fWDelegate = new FWDelegate(serialPortManagement.SendData);
            var result = fWDelegate.BeginInvoke(CommandString, ref stringGet,null, null);

            fWDelegate.EndInvoke(ref stringGet, result);
        }

        private string FWReadCommand() => StringBack = serialPortManagement.DataReceived();


        private void QCCommand(string QCString)
        {

        }

    }
}
