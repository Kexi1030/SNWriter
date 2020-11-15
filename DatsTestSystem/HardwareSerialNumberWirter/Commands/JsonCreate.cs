using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Script.Serialization;
using DatsTestSystem.HardwareSerialNumberWirter.Models.JsonModels;

namespace DatsTestSystem.HardwareSerialNumberWirter.Commands
{
    class JsonCreate
    {
        public void CreateJson(JsonFormat jsonFormat)
        {
            JavaScriptSerializer ser = new JavaScriptSerializer();
            string outputJson = ser.Serialize(jsonFormat);
            File.WriteAllText("OUTPUT.json", outputJson);
        }

        public JsonFormat CreateSNFromJsonFile(string FileLoad)
        {
            string JsonString = File.ReadAllText(FileLoad);

            JavaScriptSerializer ser = new JavaScriptSerializer();
            JsonFormat Jsonreturn  = ser.Deserialize<JsonFormat>(JsonString);

            return Jsonreturn;
        }
    }
}
