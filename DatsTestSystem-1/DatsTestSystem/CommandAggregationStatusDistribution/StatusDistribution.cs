using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace DatsTestSystem.CommandAggregationStatusDistribution
{
    public class StatusDistribution
    {
        private object _msgLock;
        private List<string> msgList;
        Thread _distributeThread; // 状态帧分发的线程
        private bool _start;

        AutoResetEvent _event = new AutoResetEvent(false);

        public StatusDistribution()
        {
            _msgLock = new object();
            msgList = new List<string>();
            _distributeThread = new Thread(StartDistributeThread);
            _distributeThread.Start();
        }

        public void AddMsg(string msg)
        {
            lock (_msgLock)
            {
                msgList.Add(msg); // 将串口读取的数据添加到msgList
            }
            _event.Set(); // 唤醒线程
        }
        
        /// <summary>
        /// 用来分发状态帧的线程
        /// </summary>
        public void StartDistributeThread()
        {
            _start = true; // 标志位使得循环一直进行
            while(_start)
            {
                string curr_msg = null; // 当前要处理的数据
                lock(_msgLock)
                {
                    if(msgList.Count>0)
                    {
                        curr_msg = msgList[0];
                        msgList.RemoveAt(0); // 移除索引0处的元素
                    }
                }

                if(curr_msg != null)
                {
                    // 分发
                }
                else // 当前接收的列表为空
                {
                    _event.WaitOne(); // 挂起当前的线程
                }
            }
        }
    }
}


