using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SNWrite.Models;
using System.Web.Script.Serialization;
using System.IO;
using System.Windows.Forms;

namespace SNWrite.Commands
{
    class CreatJsonFromInitalizationWindow
    {
        public void CreateJson(SNinitalize sNinitalize)
        {
            JavaScriptSerializer ser = new JavaScriptSerializer();
            string outputJson = ser.Serialize(sNinitalize);
            File.WriteAllText("OUTPUT.json", outputJson);
        }

        public SNinitalize CreateSNFromJsonFile(string FileLoad)
        {
            string JsonString = File.ReadAllText(FileLoad);

            JavaScriptSerializer ser = new JavaScriptSerializer();
            SNinitalize sNinitalize = ser.Deserialize<SNinitalize>(JsonString);

            return sNinitalize;
        }

        public SN CreateSNFromsNinitalize(SNinitalize sNinitalize)
        {
            string SerialNumber = sNinitalize.SerialNumber;
            List<string> list = new List<string>(SerialNumber.Split(','));

            string DoneSerialNumber = createShortSNSerialNumber(sNinitalize);
            SN sN = new SN();

            var SNnumber = new List<Snnumber>();
            // C#中的数组是不支持动态添加元素的，只能创建固定大小的数组
            // 使用泛型list< T >,先将元素存入list中，最后使用ToArray()转成数组
            foreach (string eachstring in list)
            {
                bool containOr = eachstring.Contains("-");
                if (containOr)
                {
                    List<string> listContainsTwo = new List<string>(eachstring.Split('-'));
                    int startNum = Convert.ToInt32(listContainsTwo[0]);
                    int endNum = Convert.ToInt32(listContainsTwo[1]);
                    for (int i = startNum; i < endNum + 1; i++)
                    {
                        string CurrentNumber = string.Format("{0:D4}", i);
                        string temp = DoneSerialNumber.Insert(12, CurrentNumber);

                        Snnumber Temp = new Snnumber();
                        Temp.number = temp;
                        bool c = Temp == null;
                        //MessageBox.Show(Temp.number);
                        SNnumber.Add(Temp);
                    }
                }
                else
                {
                    string temp = DoneSerialNumber.Insert(12, string.Format("{0:D4}", int.Parse(eachstring)));
                    Snnumber Temp = new Snnumber();
                    Temp.number = temp;
                    SNnumber.Add(Temp);
                }
            }

            sN.SNnumber = SNnumber.ToArray();

            return sN;
        }

        private string createShortSNSerialNumber(SNinitalize sNinitalize)
        {
            string shortSNSerialNumber = "0082";

            switch (sNinitalize.Model)
            {
                case "单路 0x02":
                    shortSNSerialNumber += "02";
                    break;
                case "双路6.125m道间距版本 0x03":
                    shortSNSerialNumber += "03";
                    break;
                case "双路12.5m道间距版本 0x04":
                    shortSNSerialNumber += "04";
                    break;
                case "双路3.125m道间距版本 0x05":
                    shortSNSerialNumber += "05";
                    break;
            }

            switch (sNinitalize.PCBAfactory)
            {
                case "元森快捷制版 / 无锡鸿睿焊接 0x00":
                    shortSNSerialNumber += "00";
                    break;
                case "崇达制版/凌华焊接 0x01":
                    shortSNSerialNumber += "01";
                    break;
            }

            shortSNSerialNumber += sNinitalize.Year.Substring(2);
            shortSNSerialNumber += sNinitalize.Week;

            shortSNSerialNumber += sNinitalize.HardWareNumber;
            shortSNSerialNumber += sNinitalize.FirmWareNumber;

            return shortSNSerialNumber;
        }
    }


}

