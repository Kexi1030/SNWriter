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
    public class CommandAggregate
    {

        public string CommandFromFW { get; set; }
        public Task task;

        public CommandAggregate()
        {


        }

        public void GetFWString(string CommandString)
        {

            CommandFromFW = CommandString;

            task.Start();
        }

        private void QCCommand(string QCString)
        {

        }

    }
}
