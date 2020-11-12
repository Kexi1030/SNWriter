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

        }
    }
}
