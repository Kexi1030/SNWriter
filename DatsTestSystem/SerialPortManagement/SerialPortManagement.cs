using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO.Ports;
using DatsTestSystem.SerialPortManagement.Models;
using System.Windows.Forms;

namespace DatsTestSystem.SerialPortManagement
{
    class SerialPortManagementClass
    {
        private SerialPort serialPort = new SerialPort();

        public SerialPortManagementClass(SerialportConfigurationInformation serialportConfigurationInformation)
        {

            serialPort.PortName = serialportConfigurationInformation.PortName;
            serialPort.BaudRate = serialportConfigurationInformation.BaudRate;
            serialPort.Parity = (Parity)Convert.ToInt32(serialportConfigurationInformation.Parity);
            serialPort.DataBits = serialportConfigurationInformation.DataBits;
            serialPort.StopBits = (StopBits)Convert.ToInt32(serialportConfigurationInformation.StopBits);
        }

        private static SerialportConfigurationInformation DefaultSerialPortInfo = new SerialportConfigurationInformation() 
        {
            PortName="COM1",BaudRate=9600,DataBits=8,StopBits="1",Parity="None"
        }; // 这里需要默认端口信息


        /// <summary>
        /// 发送数据
        /// </summary>
        /// <param name="data"></param>
        /// <param name="serialportConfigurationInformation"> 表示选择的串口配置信息</param>
        /// <returns></returns>
        public bool SendData(string data)
        {
            byte[] dataSend = strToHexByte(data);

            try
            {
                serialPort.Open();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            if (serialPort.IsOpen)
            {
                try
                {
                    serialPort.Write(dataSend, 0, data.Length);
                    return true;
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                MessageBox.Show("串口未打开", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
            }
        }

        /// <summary>
        /// 字符串转换16进制字节数组
        /// </summary>
        /// <param name="hexString"></param>
        /// <returns></returns>
        private byte[] strToHexByte(string hexString)
        {
            hexString = hexString.Replace(" ", "");
            if ((hexString.Length % 2) != 0) hexString += " ";
            byte[] returnBytes = new byte[hexString.Length / 2];
            for (int i = 0; i < returnBytes.Length; i++)
                returnBytes[i] = Convert.ToByte(hexString.Substring(i * 2, 2).Replace(" ", ""), 16);
            return returnBytes;
        }

        /// <summary>
        /// 接收数据
        /// </summary>
        /// <returns></returns>
        public string DataReceived()
        {
            byte[] ReDatas = new byte[serialPort.BytesToRead];
            serialPort.Read(ReDatas, 0, ReDatas.Length);

            string dataReceived = bytetoString(ReDatas);

            return dataReceived;

        }

        /// <summary>
        /// bute[] To string
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        private string bytetoString(byte[] data)
        {
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < data.Length; i++)
            {
                sb.AppendFormat("{0:x2}" + " ", data[i]); // 向此实例追加通过处理复合格式字符串（包含零个或更多格式项）而返回的字符串。
            }

            string Result = sb.ToString().ToUpper();

            return Result;
        }
    }
}
