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
    /// <summary>
    /// 根据发来的指令 分为烧写还是QC 执行不同的操作
    /// </summary>
    public class CommandAggregate
    {
        public delegate void CommandAggregateHandler(byte[] data);
        public event CommandAggregateHandler CommandDataToPort;
        private bool _start;
        Thread _CommandDataToPortThread;

        private object _commandlock;
        private List<string> commandstringList;

        AutoResetEvent _event = new AutoResetEvent(false);

        public CommandAggregate()
        {
            _commandlock = new object();
            commandstringList = new List<string>();
            _CommandDataToPortThread = new Thread(CommandDataToPortThread);
            _CommandDataToPortThread.Start();
        }

        public void AddMsg(string msg)
        {
            lock (_commandlock)
            {
                commandstringList.Add(msg);
            }
            _event.Set();
        }

        private void CommandDataToPortThread()
        {
            _start = true;
            while(_start)
            {
                string curr_data = null;
                lock(_commandlock)
                {
                    if (commandstringList.Count > 0)
                    {
                        curr_data = commandstringList[0];
                        commandstringList.RemoveAt(0);
                    }
                }
                if(curr_data!=null)
                {
                    CommandDataToPort(StrAndByteProcessClass.strToHexByte(curr_data));
                }
                else 
                {
                    _event.WaitOne();
                }
            }
        }


    }
}
