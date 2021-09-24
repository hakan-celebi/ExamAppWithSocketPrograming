using System;
using System.IO;
using System.Text.Json;

namespace ExamApp.DataProviders
{
    public class JsonDataProvider
    {
        public static string FilePath { get; set; } = AppDomain.CurrentDomain.BaseDirectory + "Application.config.json";
        private StreamReader Reader { get; set; }
        public JsonConfig Config { get => GetJsonConfig(); }
        private JsonConfig GetJsonConfig()
        {
            Reader = new StreamReader(FilePath);
            string json = Reader.ReadToEnd();
            Reader.Dispose();
            return JsonSerializer.Deserialize<JsonConfig>(json);
        }
        public void UpdateJsonConfig(JsonConfig config)
        {
            string json = JsonSerializer.Serialize<JsonConfig>(config);
            File.WriteAllText(FilePath, json);
        }
    }
}