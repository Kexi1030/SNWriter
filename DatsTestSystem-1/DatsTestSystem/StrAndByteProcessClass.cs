using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatsTestSystem
{
    public static class StrAndByteProcessClass
    {
        /// <summary>
        /// bute[] To string
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public static string bytetoString(byte[] data)
        {
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < data.Length; i++)
            {
                sb.AppendFormat("{0:x2}" + " ", data[i]); // 向此实例追加通过处理复合格式字符串（包含零个或更多格式项）而返回的字符串。
            }

            string Result = sb.ToString().ToUpper();

            return Result;
        }

        /// <summary>
        /// 字符串转换16进制字节数组
        /// </summary>
        /// <param name="hexString"></param>
        /// <returns></returns>
        public static byte[] strToHexByte(string hexString)
        {
            hexString = hexString.Replace(" ", "");
            if ((hexString.Length % 2) != 0) hexString += " ";
            byte[] returnBytes = new byte[hexString.Length / 2];
            for (int i = 0; i < returnBytes.Length; i++)
                returnBytes[i] = Convert.ToByte(hexString.Substring(i * 2, 2).Replace(" ", ""), 16);
            return returnBytes;
        }
    }
}
