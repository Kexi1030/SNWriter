using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SNWrite.Models;
using System.Web.Script.Serialization;
using System.IO;

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

        public void CreateSNFromJsonFile(string FileLoad)
        {
            string JsonString = File.ReadAllText(FileLoad);

            JavaScriptSerializer ser = new JavaScriptSerializer();
            SNinitalize sNinitalize = ser.Deserialize<SNinitalize>(JsonString);

        }

        private void CreateSNFromsNinitalize(SNinitalize sNinitalize)
        {
            string SerialNumber = sNinitalize.SerialNumber;
            List<string> list = new List<string>(SerialNumber.Split(','));
            int length = 0;

            foreach(var eachstring in list)
            {
                bool containOr = eachstring.Contains("-");
                if(containOr)
                {
                    List<string> listContainsTwo = new List<string>(eachstring.Split('-'));
                    length += (Convert.ToInt32(listContainsTwo[1]) - Convert.ToInt32(listContainsTwo[0]));
                }
                else
                {
                    length += 1;
                }
            }

            for(int i=0;i<length;i++)
            {

            }

        }
    }
}
