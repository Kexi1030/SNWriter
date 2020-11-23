﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatsTestSystem.HardwareSerialNumberWirter.Commands
{
    /// <summary>
    /// 解析状态返回帧数据 E0 03
    /// </summary>
    static class StatusFrameAnalysis
    {
        /// <summary>
        /// 串口接收上来的string和发送的sn对比
        /// </summary>
        /// <param name="snBack"></param>
        /// <param name="snSend"></param>
        /// <returns></returns>
        public static bool SnComparision(string snBack,string snSend)
        {
            string snBackProcessed = snBack.Substring(23, 33).Replace(" ","");
            bool EqualOr = snBackProcessed.Equals(snSend);

            return EqualOr;
        }
    }
}
