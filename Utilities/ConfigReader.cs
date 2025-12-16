using System.Text.Json;

namespace MarsAutomation.Utilities
{
    public static class ConfigReader
    {
        private static Dictionary<string, string> _config;

        // Static constructor
        static ConfigReader()
        {
            string jsonPath = Path.Combine(Directory.GetCurrentDirectory(), "TestData", "appsettings.json");
            string json = File.ReadAllText(jsonPath);
            _config = JsonSerializer.Deserialize<Dictionary<string, string>>(json);
        }

        public static string Get(string key)
        {
            return _config.ContainsKey(key) ? _config[key] : null;
        }

        // Shortcut properties
        public static string BaseUrl => Get("BaseUrl");
        public static string Browser => Get("Browser");
        public static string Username => Get("Username");
        public static string Password => Get("Password");
    }
}

