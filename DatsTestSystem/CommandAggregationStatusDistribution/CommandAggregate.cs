using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatsTestSystem.CommandAggregationStatusDistribution
{
    /// <summary>
    /// 根据发来的指令 分为烧写还是QC 执行不同的操作
    /// </summary>
    class CommandAggregate
    {
        private string CommandPending { get; set; }

        CommandAggregate(string CommandString)
        {
            if(CommandString.Substring(0,2) == "FW")
            {
                FWCommand(CommandString);
            }
            else
            {
                QCCommand(CommandString);
            }
        }

        private void FWCommand(string FwString)
        {

        }

        private void QCCommand (string QCString)
        {

        }
    }
}
