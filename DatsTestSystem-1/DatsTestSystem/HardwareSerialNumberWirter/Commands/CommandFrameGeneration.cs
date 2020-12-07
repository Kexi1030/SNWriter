using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using DatsTestSystem.CommandAggregationStatusDistribution;
using DatsTestSystem.HardwareSerialNumberWirter.Commands;

namespace DatsTestSystem.HardwareSerialNumberWirter.Commands
{
    /// <summary>
    /// 命令帧生成
    /// </summary>
    class CommandFrameGeneration
    {

        public string FwWriteString { get; set; }
        public string FwReadString { get; set; }

        public CommandFrameGeneration(string SnString)
        {
            this.FwWriteString = FWCommandFrameGenertation(SnString);

            this.FwReadString = "F500000000E00300FFFF5F";
        }

        /// <summary>
        /// 返回需要发送的烧写指令帧
        /// </summary>
        /// <param name="SnString"></param>
        /// <returns></returns>
        private string FWCommandFrameGenertation(string SnString)
        {
            string FwString = "00000000A00A120000";
            FwString += SnString;
            FwString += "000000000000FF";
            return ProtocolProcess.ProtocolProcessing(FwString);
        }
    }
}
