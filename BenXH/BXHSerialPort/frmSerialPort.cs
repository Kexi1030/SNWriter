/*****************************************************
 * ProjectName:  BXHSerialPort
 * Description:
 * ClassName:    Class1
 * CLRVersion:   4.0.30319.42000
 * Author:       JiYF
 * NameSpace:    BXHSerialPort
 * MachineName:  JIYF_PC
 * CreateTime:   2017/3/25 16:46:14
 * UpdatedTime:  2017/3/25 16:46:14
*****************************************************/
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace BXHSerialPort
{
    public partial class frmSerialPort : Form
    {
        private SerialPort ComDevice = new SerialPort();  // 实例化一个SerialPort
        public frmSerialPort()
        {
            InitializeComponent();
            this.init();
        }
        public void init() // 初始化参数绑定接受数据
        {
            btnSend.Enabled = false;  // 发送按钮对用户的点击不作出回应
            cbbComList.Items.AddRange(SerialPort.GetPortNames()); // 端口下拉菜单  GetPortNames 获取当前计算机的串行端口名的数组
            if (cbbComList.Items.Count > 0)
            {
                cbbComList.SelectedIndex = 0;
            }
            cbbBaudRate.SelectedIndex = 5; // 波特率
            cbbDataBits.SelectedIndex = 0; // 数据位
            cbbParity.SelectedIndex = 0; // 校验位
            cbbStopBits.SelectedIndex = 0; // 停止位
            pictureBox1.BackgroundImage = Properties.Resources.red; // 设置当前的图片框的背景图片为红色图片

            ComDevice.DataReceived += new SerialDataReceivedEventHandler(Com_DataReceived);//绑定事件 指示已通过由 SerialPort 对象表示的端口接收了数据

        }

        /// <summary>
        /// 打开串口
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnOpen_Click(object sender, EventArgs e)
        {
            if (cbbComList.Items.Count <= 0)
            {
                MessageBox.Show("没有发现串口,请检查线路！");
                return;
            }

            if (ComDevice.IsOpen == false) // 如果当前串口还没有打开
            {
                ComDevice.PortName = cbbComList.SelectedItem.ToString();
                ComDevice.BaudRate = Convert.ToInt32(cbbBaudRate.SelectedItem.ToString());
                ComDevice.Parity = (Parity)Convert.ToInt32(cbbParity.SelectedIndex.ToString());
                ComDevice.DataBits = Convert.ToInt32(cbbDataBits.SelectedItem.ToString());
                ComDevice.StopBits = (StopBits)Convert.ToInt32(cbbStopBits.SelectedItem.ToString());
                try
                {
                    ComDevice.Open();
                    btnSend.Enabled = true; // 当串口连接上了之后对用户的点击做出回应
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                btnOpen.Text = "关闭串口";
                pictureBox1.BackgroundImage = Properties.Resources.green;
            }
            else  // 如果当前的串口已经被打开了
            {
                try
                {
                    ComDevice.Close();
                    btnSend.Enabled = false;
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                btnOpen.Text = "打开串口";
                pictureBox1.BackgroundImage = Properties.Resources.red;
            }

            cbbComList.Enabled = !ComDevice.IsOpen;
            cbbBaudRate.Enabled = !ComDevice.IsOpen;
            cbbParity.Enabled = !ComDevice.IsOpen;
            cbbDataBits.Enabled = !ComDevice.IsOpen;
            cbbStopBits.Enabled = !ComDevice.IsOpen;
        }

        /// <summary>
        /// 关闭串口
        /// </summary>
        public void ClearSelf()
        {
            if (ComDevice.IsOpen)
            {
                ComDevice.Close();
            }
        }

        /// <summary>
        /// 发送数据
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="data"></param>
        /// 
        public bool SendData(byte[] data)
        {
            if (ComDevice.IsOpen)
            {
                try
                {
                    ComDevice.Write(data, 0, data.Length);//发送数据
                    MessageBox.Show("发送成功", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
        /// 发送数据button事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSend_Click(object sender, EventArgs e)
        {
            byte[] sendData = null;

            if (rbtnSendHex.Checked)
            {
                sendData = strToHexByte(txtSendData.Text.Trim());
            }
            else if (rbtnSendASCII.Checked)
            {
                sendData = Encoding.ASCII.GetBytes(txtSendData.Text.Trim());
            }
            else if (rbtnSendUTF8.Checked)
            {
                sendData = Encoding.UTF8.GetBytes(txtSendData.Text.Trim());
            }
            else if (rbtnSendUnicode.Checked)
            {
                sendData = Encoding.Unicode.GetBytes(txtSendData.Text.Trim());
            }
            else
            {
                sendData = Encoding.ASCII.GetBytes(txtSendData.Text.Trim());
            }

            if (this.SendData(sendData))//发送数据成功计数
            {
                lblSendCount.Invoke(new MethodInvoker(delegate
                {
                    lblSendCount.Text = (int.Parse(lblSendCount.Text) + txtSendData.Text.Length).ToString();
                }));
            }
            else
            {

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
            returnBytes[i] = Convert.ToByte(hexString.Substring(i * 2, 2).Replace(" ",""), 16);
            return returnBytes;
        }

        /// <summary>
        /// 接收数据
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Com_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            byte[] ReDatas = new byte[ComDevice.BytesToRead];
            ComDevice.Read(ReDatas, 0, ReDatas.Length);//读取数据
            this.AddData(ReDatas);//输出数据
        }
       
        /// <summary>
        /// 添加数据
        /// </summary>
        /// <param name="data">字节数组</param>
        public void AddData(byte[] data)
        {
            if (rbtnHex.Checked)
            {
                StringBuilder sb = new StringBuilder();
                for (int i = 0; i < data.Length; i++)
                {
                    sb.AppendFormat("{0:x2}" + " ", data[i]); // 向此实例追加通过处理复合格式字符串（包含零个或更多格式项）而返回的字符串。 
                }
                AddContent(sb.ToString().ToUpper());
            }
            else if (rbtnASCII.Checked)
            {
                AddContent(new ASCIIEncoding().GetString(data));
            }
            else if (rbtnUTF8.Checked)
            {
                AddContent(new UTF8Encoding().GetString(data));
            }
            else if (rbtnUnicode.Checked)
            {
                AddContent(new UnicodeEncoding().GetString(data));
            }
            else
            {}
          
            lblRevCount.Invoke(new MethodInvoker(delegate // 更改接收字节数长度
            {
                lblRevCount.Text = (int.Parse(lblRevCount.Text) + data.Length).ToString();
            }));
        }


        /// <summary>
        /// 输入到显示区域
        /// </summary>
        /// <param name="content"></param>
        private void AddContent(string content)
        {
            this.BeginInvoke(new MethodInvoker(delegate // MethodInvoker 委托 该委托可执行托管代码中声明为 void 且不接受任何参数的任何方法
            {
                if(chkAutoLine.Checked && txtShowData.Text.Length>0) // 自动换行被勾选并且数字接收区长度大于0
                {
                    txtShowData.AppendText("\r\n");
                }
                txtShowData.AppendText(content);
            }));
        }

       
        /// <summary>
        /// 清空接收区
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnClearRev_Click(object sender, EventArgs e)
        {
            txtShowData.Clear();
        }

        /// <summary>
        /// 清空发送区
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnClearSend_Click(object sender, EventArgs e)
        {
            txtSendData.Clear();
        }

    }
}
