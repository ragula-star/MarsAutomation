using System.Text.Json;

namespace MarsAutomation.Utilities
{
    public static class JsonReader
    {
        public static T ReadJson<T>(string filePath)
        {
            string json = File.ReadAllText(filePath);
            return JsonSerializer.Deserialize<T>(json);
        }
    }
}
