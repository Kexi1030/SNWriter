using System;
using System.IO;
using System.Web.Script.Serialization;
using DatsTestSystem.HardwareSerialNumberWirter.Models.JsonModels;
using Newtonsoft.Json;


namespace DatsTestSystem.HardwareSerialNumberWirter.Commands
{
    class JsonCreate
    {
        /*
        public void CreateJson(JsonFormat jsonFormat)
        {
            JavaScriptSerializer ser = new JavaScriptSerializer();
            string outputJson = ser.Serialize(jsonFormat);
            File.WriteAllText("OUTPUT.json", outputJson);
        }
        */

        public void CreateJson(JsonFormat jsonFormat)
        {
            string outputJson = JsonConvert.SerializeObject(jsonFormat, new JsonSerializerSettings() { Formatting = Formatting.Indented });
            File.WriteAllText(DateTime.Now.ToString("yyyy-MM-dd") + ".json", outputJson); // 保存到
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
