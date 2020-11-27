using DatsTestSystem.CommandAggregationStatusDistribution;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatsTestSystem.HardwareSerialNumberWirter.Commands
{
    public delegate void AsyncMethodCaller(string stringsend, ref string stringget);

    public static class FW
    {
        public static string Send(string CurrentString)
        {
            string StringGet = "";

            // 生成需要发送的数据 包括查询帧和配置序列号帧
            CommandFrameGeneration commandFrameGeneration = new CommandFrameGeneration(CurrentString);

            CommandAggregate commandAggregate = new CommandAggregate();
            AsyncMethodCaller caller = new AsyncMethodCaller(commandAggregate.FWSend);


            IAsyncResult result = caller.BeginInvoke(commandFrameGeneration.FwReadString, ref StringGet, null, null);

            caller.EndInvoke(ref StringGet, result);

            if(StringGet == "")
            {
                IAsyncResult result2 = caller.BeginInvoke(commandFrameGeneration.FwReadString, ref StringGet, null, null);

                caller.EndInvoke(ref StringGet, result2);
                if (StringGet == "")
                    return "False";
            }
            IAsyncResult result3 = caller.BeginInvoke(commandFrameGeneration.FwWriteString, ref StringGet, null, null);
            caller.EndInvoke(ref StringGet, result3);

            IAsyncResult result4 = caller.BeginInvoke(commandFrameGeneration.FwReadString, ref StringGet, null, null);

            caller.EndInvoke(ref StringGet, result4);

            bool EqualOr = StatusFrameAnalysis.SnComparision(StringGet,CurrentString);
            if(EqualOr)
            {
                return StringGet;
            }
            else
            {
                return "False";
            }
        }

        /*
        public static void callBackMethod(IAsyncResult ar)
        {
            FWDelegate asyncMethodCaller = ar.AsyncState as FWDelegate;

            asyncMethodCaller.EndInvoke(ref string stringGet, ar);
        }
        */
    }
}
