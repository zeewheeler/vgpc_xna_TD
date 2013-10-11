using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace vgpc_tower_defense.Config
{

    public class ConfigEntry
    {
        public string ConfigItemType;
        public string ConfigStringIdentifier;
        public string ConfigItemValue;

        public ConfigEntry(String configItemType, String key, String value)
        {
            ConfigItemType = configItemType;
            ConfigStringIdentifier = key;
            ConfigItemValue = value;
        }

    }

    public static class JsonConfigOperations
    {
        public static List<ConfigEntry> ReadJsonConfigFile()
        {
            string JsonFromFile = File.ReadAllText(@"config\config.txt");

            List<ConfigEntry> ConfigEntries = new List<ConfigEntry>();
            ConfigEntries = Newtonsoft.Json.JsonConvert.DeserializeObject<List<ConfigEntry>>(JsonFromFile);
            return ConfigEntries;
        }


        public static void CreateExampleJsonConfigFile()
        {
            List<ConfigEntry> ConfigEntries = new List<ConfigEntry>();

            ConfigEntries.Add(new ConfigEntry("ConfigItemType1", "Key1", "value1"));
            ConfigEntries.Add(new ConfigEntry("ConfigItemType2", "Key2", "value2"));
            ConfigEntries.Add(new ConfigEntry("ConfigItemType3", "Key3", "value3"));
            ConfigEntries.Add(new ConfigEntry("ConfigItemType4", "Key4", "value4"));
            ConfigEntries.Add(new ConfigEntry("ConfigItemType5", "Key5", "value5"));

            string JsonString = Newtonsoft.Json.JsonConvert.SerializeObject(ConfigEntries, Formatting.Indented);

            File.WriteAllText(@"config\Example_Json_Config.txt", JsonString);

        }
    }

    //Reads a json game configuration file and returns a list of configuration entries


    
    
}
