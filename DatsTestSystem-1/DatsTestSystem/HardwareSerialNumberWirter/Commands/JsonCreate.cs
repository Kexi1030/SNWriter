using System;
using System.IO;
using System.Web.Script.Serialization;
using DatsTestSystem.HardwareSerialNumberWirter.Models.JsonModels;
using Newtonsoft.Json;


namespace DatsTestSystem.HardwareSerialNumberWirter.Commands
{
    class JsonCreate
    {
        public void CreateJson(JsonFormat jsonFormat,string filename)
        {
            string outputJson = JsonConvert.SerializeObject(jsonFormat, new JsonSerializerSettings() { Formatting = Formatting.Indented });
            File.WriteAllText(filename, outputJson); // 保存
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
