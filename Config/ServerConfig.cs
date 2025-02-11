using System;
using System.IO;
using System.Text.Json;
using System.Collections.Generic;

namespace GAMEBR0GAMES.Config
{
    public class ModInfo
    {
        public required string ModPath { get; set; }
        public required string ModName { get; set; }
    }

    public class ServerConfig
    {
        public string DiagExePath { get; set; } = string.Empty;
        public string ServerExePath { get; set; } = string.Empty;
        public string WorkbenchExePath { get; set; } = string.Empty;
        public List<ModInfo> Mods { get; set; } = new List<ModInfo>();

        private static readonly string ConfigPath = Path.Combine(
            Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location) ?? AppDomain.CurrentDomain.BaseDirectory,
            "server_config.json"
        );

        public static ServerConfig Load()
        {
            if (File.Exists(ConfigPath))
            {
                try
                {
                    string jsonString = File.ReadAllText(ConfigPath);
                    var config = JsonSerializer.Deserialize<ServerConfig>(jsonString);
                    if (config == null)
                    {
                        return new ServerConfig();
                    }
                    config.Mods ??= new List<ModInfo>();
                    return config;
                }
                catch
                {
                    return new ServerConfig();
                }
            }
            return new ServerConfig();
        }

        public void Save()
        {
            string jsonString = JsonSerializer.Serialize(this, new JsonSerializerOptions { WriteIndented = true });
            File.WriteAllText(ConfigPath, jsonString);
        }

        public void AddMod(string path, string name)
        {
            Mods.Add(new ModInfo { ModPath = path, ModName = name });
            Save();
        }
    }
}
