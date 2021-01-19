using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO.Ports;
using DatsTestSystem.SerialPortManagement.Models;
using System.Windows.Forms;
using System.Threading;
using System.Diagnostics;
using DatsTestSystem.HardwareSerialNumberWirter;

namespace DatsTestSystem.SerialPortManagement
{
    public class SerialPortManagementClass
    {
        public delegate void DataReadyHandler(byte[] data);
        public event DataReadyHandler DataReadyEvent; // 接收到的msg的处理事件

        public Thread _writeThread, _readThread;
        private List<byte[]> _writeList;
        object _writeObj;
        private bool _Running;
        AutoResetEvent _event = new AutoResetEvent(false);

        public SerialPortManagementClass()
        {
            _writeObj = new object();

            _writeList = new List<byte[]>();
            _writeList.Clear();

            _Running = true;

            _writeThread = new Thread(WriteThread);
            _writeThread.Name = "_writeThread";
            _writeThread.Start();
            _readThread = new Thread(ReadThread);
            _readThread.Name = "_readThread";
            //_readThread.Start();
        }

        public void inifPort(SerialportConfigurationInformation portinfo)
        {
            if(!serialPort.IsOpen)
            {
                serialPort.PortName = portinfo.PortName;
                serialPort.BaudRate = portinfo.BaudRate;
                serialPort.DataBits = portinfo.DataBits;
                serialPort.Parity = (System.IO.Ports.Parity)Convert.ToInt32(portinfo.Parity.ToString());
                serialPort.StopBits = (System.IO.Ports.StopBits)Convert.ToInt32(portinfo.StopBits);
            }
            else
            {
            }
        }

        public void Virtualframegeneration()
        {
            byte[] FrameGeneration = new byte[] { 0, 0 };

            while (_Running)
            {
                byte[] temp = new byte[] { 1, 1, 1, 1 };

                var m = new List<byte>();
                m.AddRange(FrameGeneration);
                m.AddRange(temp);
                //temp.CopyTo(FrameGeneration, FrameGeneration.Length);

                FrameGeneration = m.ToArray();
                Thread.Sleep(100);

                DataReadyEvent(FrameGeneration);
                Thread.Sleep(100);
            }
        }

        private void ReadThread() // 读线程
        {
            while (_Running)
            {
                byte[] buf = new byte[128];
                try
                {
                    int ret = serialPort.Read(buf, 0, buf.Length);
                    if (ret > 0)
                    {
                        byte[] tmp = new byte[ret];

                        for (int i = 0; i < ret; i++)
                            tmp[i] = buf[i];
                        DataReadyEvent(tmp); // 对读取上来的数据处理
                    }
                }
                catch (InvalidOperationException e)
                {
                    // Console.WriteLine("Open serial port first, error {0}", e.ToString());
                    Console.WriteLine("当前串口无法操作，请尝试进行串口配置");
                    return;
                }
            }
        }

        public void AddData(byte[] cmd) // 待写入的指令加入到指令列表中去
        {
            lock (_writeObj)
            {
                _writeList.Add(cmd);
            }
            _event.Set();
        }

        private void WriteThread() // 对串口数据进行写入的线程
        {
            //Virtualframegeneration();

            while (_Running)
            {
                byte[] buf = null;
                lock (_writeObj)
                {
                    if (_writeList.Count > 0)
                    {
                        buf = _writeList[0];
                        _writeList.RemoveAt(0);
                    }
                }
                if (buf != null)
                {
                    try
                    {
                        serialPort.Write(buf, 0, buf.Length);
                    }
                    catch (InvalidOperationException e)
                    {
                        //Console.WriteLine("{0}", e.ToString());
                        Console.WriteLine("当前串口无法操作，请尝试进行串口配置");
                        return;
                    }
                }
                else
                {
                    _event.WaitOne();
                }
            }

        }

        public void Open() // 打开串口
        {
            // Debug.Assert((serialPort != null) && (!serialPort.IsOpen));
            if(serialPort.IsOpen)
            {
                serialPort.Close();
                // 关闭现在正在运行的线程
            }
            try
            {
                serialPort.Open();
                lock (_writeObj)
                {
                    _writeList.Clear();
                }
                _Running = true;
                _event.Reset();
                //_readThread.Start();
                //_writeThread.Start();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "串口打开错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public void Close() // 关闭串口
        {
            try
            {
                serialPort.Close();
            }
            catch(Exception e)
            {
                MessageBox.Show(e.Message, "串口关闭错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        Task Task;
        public string StringBack { get; set; }

        private SerialPort serialPort = new SerialPort();

        /// <summary>
        /// 发送数据
        /// </summary>
        /// <param name="data"></param>
        /// <param name="serialportConfigurationInformation"> 表示选择的串口配置信息</param>
        /// <returns></returns>
        public bool SendData(string data)
        {
            byte[] dataSend = StrAndByteProcessClass.strToHexByte(data);

            if (!serialPort.IsOpen)
            {
                try
                {
                    serialPort.Open();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "错误1", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }

            if (serialPort.IsOpen)
            {
                try
                {
                    serialPort.Write(dataSend, 0, dataSend.Length);

                    Thread.Sleep(500);

                    //StringBack = DataReceived();

                    Task.Start();

                    return true;
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "错误2", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                MessageBox.Show("串口未打开", "错误3", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            return false;
        }

        /// <summary>
        /// 关闭串口
        /// </summary>
        public void ClearSelf()
        {
            if (serialPort.IsOpen)
            {
                serialPort.Close();

                _Running = false;
                _writeThread.Join();
                _readThread.Join();
            }
        }
    }
}
