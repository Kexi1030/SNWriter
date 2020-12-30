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
            _start = true;
            if(_distributeThread!=null)
            {
                if(_distributeThread.ThreadState != ThreadState.Running)
                {
                    _distributeThread.Resume();
                    Console.WriteLine(_distributeThread.ThreadState);
                    Console.WriteLine("线程已经resume");
                }
            }
            else
            {
                _distributeThread = new Thread(StartDistributeThread);
                _distributeThread.Start();
                Console.WriteLine("线程Start了");
            }
            lock (_msgLock)
            {
                msgList.Clear();
            }
        }

        public void CloseDisThread()
        {
            _distributeThread.Suspend();
            Console.WriteLine(_distributeThread.ThreadState);
            Console.WriteLine("分发线程已经挂起");
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
                    var m = new List<byte>();
                    m.AddRange(FrameBack);
                    m.AddRange(curr_msg);
                    FrameBack = m.ToArray();
                    if (FrameBack.Length >= 106)
                    {
                        if (AllusefulFrameStart.Contains(StrAndByteProcessClass.bytetoString(FrameBack.Take(1).ToArray()))) // 如果帧头匹配成功
                        {
                            //Console.WriteLine("匹配成功！！！！！！！！！！！！！！！！");
                            Console.WriteLine("当前FrameBack{0}", StrAndByteProcessClass.bytetoString(FrameBack.Take(106).ToArray()));
                            DataDistrubution(FrameBack.Take(106).ToArray());
                            // 已经分发给其他模块将其删除
                            FrameBack = new byte[] { };
                            curr_msg = null;
                            //Console.WriteLine("FrameBack已清空");
                        }
                        else
                        {
                            Console.WriteLine("当前收到的帧为无用帧 结束");

                            // 当前帧无用 丢弃
                            FrameBack = new byte[] { };
                            curr_msg = null;
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


