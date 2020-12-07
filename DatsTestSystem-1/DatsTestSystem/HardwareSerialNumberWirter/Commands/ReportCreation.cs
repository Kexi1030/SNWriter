using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DatsTestSystem.HardwareSerialNumberWirter.Models.JsonModels;
using DatsTestSystem.HardwareSerialNumberWirter.Commands;
using iTextSharp.text;
using iTextSharp.text.pdf;
using System.IO;

namespace DatsTestSystem.HardwareSerialNumberWirter.Commands
{
    static class ReportCreation
    {
        public static void CreateReport(string FileLoad)
        {
            JsonCreate jsonCreate = new JsonCreate();

            JsonFormat JsonData = jsonCreate.CreateSNFromJsonFile(FileLoad);

            List<string> contentText = new List<string>();

            foreach (var i in JsonData.eachSNStatuses)
            {
                string temp = "";
                temp += i.SnString;
                temp += "    ";
                temp += i.Done.ToString();
                temp += "    ";
                temp += i.OperateTime;
                temp += "    ";
                temp += i.OperatorName;

                contentText.Add(temp);
            }

            create(contentText,FileLoad.Split('.')[0]);
        }

        private static void create(List<string> contentText,string fileload)
        {
            Document document = new Document(); // 创建一个文件

            PdfWriter.GetInstance(document, new FileStream(fileload+".pdf", FileMode.Create)); // pdf的实例化到当前文件夹
            BaseFont baseFont = BaseFont.CreateFont(@"C:\Windows\Fonts\SIMSUN.TTC,1", BaseFont.IDENTITY_H, BaseFont.NOT_CACHED);//找到基础字体
            Font contentFont = new Font(baseFont, 12);//创建一个新的字体

            document.Open();
            document.Add(new Paragraph("      序列号          是否成功              操作时间               操作人", contentFont));
            foreach (string eachstring in contentText)
            {
                document.Add(new Paragraph(eachstring, contentFont));
            }
            document.Close();
        }
    }
}
