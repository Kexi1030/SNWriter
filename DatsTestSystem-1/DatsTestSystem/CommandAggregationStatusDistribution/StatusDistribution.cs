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
        List<string> AllusefulFrameStart;

        private object _msgLock;
        private List<byte[]> msgList;
        Thread _distributeThread; // 状态帧分发的线程
        private bool _start;

        public delegate void DataBackHandler(byte[] data);
        public event DataBackHandler DataDistrubution;

        AutoResetEvent _event = new AutoResetEvent(false);

        public StatusDistribution()
        {
            AllusefulFrameStart = new List<string>() { "E0 " };

            _msgLock = new object();
            msgList = new List<byte[]>();
            //_distributeThread = new Thread(StartDistributeThread);
            //_distributeThread.Start();
        }

        public void OpenDisThread()
        {
            // 线程打开
            //Console.WriteLine("OpenDisThread\n");
            _start = true;
            _distributeThread = new Thread(StartDistributeThread);
            _distributeThread.Start();
            lock (_msgLock)
            {
                msgList.Clear();
            }
        }

        public void CloseDisThread()
        {
            _start = false;
        }

        public void AddMsg(byte[] msg)
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
            byte[] FrameBack = new byte[] { };
            while(_start)
            {
                byte[] curr_msg = null; // 当前要处理的数据
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
                    //curr_msg.CopyTo(FrameBack, FrameBack.Length);
                    var m = new List<byte>();
                    m.AddRange(FrameBack);
                    m.AddRange(curr_msg);
                    FrameBack = m.ToArray();
                    //Console.WriteLine(FrameBack.Length);
                    if (FrameBack.Length >= 106)
                    {
                        if (AllusefulFrameStart.Contains(StrAndByteProcessClass.bytetoString(FrameBack.Take(1).ToArray()))) // 如果帧头匹配成功
                        {
                            //Console.WriteLine("匹配成功！！！！！！！！！！！！！！！！");
                            DataDistrubution(FrameBack.Take(106).ToArray());
                            //Console.WriteLine("DataDistrubution");
                            Array.Clear(FrameBack, 0, FrameBack.Length);
                            Console.WriteLine("FrameBack已清空");
                        }
                        else
                        {
                            Console.WriteLine("匹配失败！！！！！！！！！！！！！！");

                            // 当前帧无用 丢弃 未完成
                        }
                    }
                }
                else // 当前接收的列表为空
                {
                    _event.WaitOne();
                }
            }
        }
    }
}


