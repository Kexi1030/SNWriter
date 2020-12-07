using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatsTestSystem.HardwareSerialNumberWirter.Commands
{
    /// <summary>
    /// 协议处理的静态类 F5 5F
    /// </summary>
    static class ProtocolProcess
    {
        public static string ProtocolProcessing(string unprocessedString)
        {
            string ProcessedString;

            List<char> ProcessedChars = new List<char>();

            for (int i = 0; i < unprocessedString.Length; i++)
            {
                if (unprocessedString[i] == 'F')
                {
                    if (unprocessedString[i + 1] == 'F' || unprocessedString[i + 1] == '5')
                    {
                        ProcessedChars.Add('F');
                        ProcessedChars.Add('F');
                        ProcessedChars.Add(unprocessedString[i]);
                        ProcessedChars.Add(unprocessedString[i + 1]);
                        i += 1;
                    }
                }
                else if (unprocessedString[i] == '5' && unprocessedString[i + 1] == 'F')
                {
                    ProcessedChars.Add('F');
                    ProcessedChars.Add('F');
                    ProcessedChars.Add(unprocessedString[i]);
                    ProcessedChars.Add(unprocessedString[i + 1]);
                    i += 1;
                }
                else
                {
                    ProcessedChars.Add(unprocessedString[i]);
                }
            }

            ProcessedString = string.Concat<char>(ProcessedChars);  // 字符数组转换为字符串

            ProcessedString = ProcessedString.Insert(0, "F5");
            ProcessedString = ProcessedString.Insert(ProcessedString.Length, "5F");
            return ProcessedString;
        }
    }
}
