using Microsoft.Extensions.Configuration;
using System;
using System.IO;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace PolygonStats.Configuration
{
    class ConfigurationManager
    {
        private string JsonSource { get; set; } = $"{Directory.GetCurrentDirectory()}{Path.DirectorySeparatorChar}Config.json";
        private static readonly Lazy<ConfigurationManager> _shared = new(() => new ConfigurationManager());
        public static ConfigurationManager Shared => _shared.Value;

        /* Global Json Deserializer Options */
        public static JsonSerializerOptions JsonDeserializerOptions;

        public Config Config { get; set; }

        private ConfigurationManager()
        {
            ConfigurationBuilder configurationBuilder = new();
            _ = configurationBuilder.SetBasePath(Directory.GetCurrentDirectory());
            _ = configurationBuilder.AddJsonFile("Config.json", true, false);
            IConfiguration builtConfig = configurationBuilder.Build();

            Config = new Config();
            builtConfig.Bind(Config);

            if (!File.Exists(JsonSource))
            {
                Save();
            }
            Config.Encounter.DiscordWebhooks.RemoveAt(0);

            JsonDeserializerOptions = new JsonSerializerOptions();
            JsonDeserializerOptions.Converters.Add(new NumberToStringConverter());

            Console.WriteLine("Config was loaded!");
        }

        public void Save()
        {
            JsonSerializerOptions options = new()
            {
                WriteIndented = true
            };
            // open config file
            string json = JsonSerializer.Serialize(Config, options);
            //write string to file
            File.WriteAllText(JsonSource, json);

            Console.WriteLine("Config was created!");
        }

        private class NumberToStringConverter : JsonConverter<string>
        {
            public override string Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
            {
                return reader.TokenType == JsonTokenType.Number ? reader.GetInt64().ToString() : reader.GetString();
            }

            public override void Write(Utf8JsonWriter writer, string value, JsonSerializerOptions options)
            {
                writer.WriteStringValue(value);
            }
        }
    }
}
