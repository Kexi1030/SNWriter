using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatsTestSystem.HardwareSerialNumberWirter.Commands
{
    class CommandFrameGeneration
    {

        public string FwWriteString { get; set; }
        public string FwReadString { get; set; }

        CommandFrameGeneration(string SnString)
        {
            this.FwWriteString = FWCommandFrameGenertation(SnString);

            this.FwReadString = "FW00000000E00300FF";
        }

        /// <summary>
        /// 返回需要发送的烧写指令帧 前面附加了FW
        /// </summary>
        /// <param name="SnString"></param>
        /// <returns></returns>
        private string FWCommandFrameGenertation(string SnString)
        {
            string FwString = "FW00000000A00A120000";
            FwString += SnString;
            FwString += "000000000000FF";
            return FwString;
        }
        




    }
}
