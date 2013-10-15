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


    /// <summary>
    /// Struct used to store config definable tower properties.
    /// </summary>
    public class ConfigTowerVars
    {

        //Constructor that initializes all reference objects;
        public ConfigTowerVars()
        {
            StatusEffects = new List<Common.status_effect>();
        }


        public string TowerName; /*String to hold the name of this tower*/

        //Sounds
        public String SoundShoot; /*SoundEffect to be played when the tower shoots*/
        public String SoundUpgrade; /*SoundEffect, if any, to be played when tower is upgraded*/
        public String SoundBuild; /*SoundEffect to be played when tower is built*/

        //Textures
        public String TextureProjectile; /*The texture of the tower's projectile*/
        public String TextureTower;     /*The Texture of the Tower*/

        //Weapon Numbers
        public float CurrentWeaponDamage; /*Starting weapon damage of tower*/
        public float CurrentWeaponAreaOfEffect; /*Starting weapon area of effect of the tower's weapon */
        public float CurrentWeaponAttacksPerSecond; /*Starting weapon attacks per second*/
        public float CurrentWeaponRange;  /*Starting weapon range of the weapon*/

        //Some towers may cause various effects, such as slow or damage over time. They will just be strings and will be copied over to mob.
        // The mob will process it's own status effects
        public List<Common.status_effect> StatusEffects;

        public float DamageGainedPerLevel; /*The amount of damage gained per level*/
        public float AttacksPerSecondGainedPerLevel; /*The amount of attacks per second gained per level*/
        public float WeaponRangeGainedPerLevel; /*The amount of weapon ranged gained per level*/
        public float ProjectileSpeed; /*The speed of the tower's projectiles*/

        //Build Cost and Level Related
        public int CostToBuild; /*Cost to build the tower*/
        public int CurrentCostToUpgrade; /*Cost to upgrade the tower*/
        public int UpgradeCostIncreasePerLevel; /*The amount the cost to upgrade the tower increase each level*/
        public int MaxTowerLevel; /* The maximum allowed level of tower*/

        //tower state variables

        public bool is_point_blank_area_damage_tower; /*True if tower is a point-blank area effect type, false if not*/

        //Tower graphic options
        public float scale;/*How big the towers graphic will be*/
        public float rotation; /*What rotation, if any, that tower's graphic will be drawed with*/
    }

    /// <summary>
    /// Defines methods to easily set towerConfigValues using values from Json config files
    /// </summary>
    static public class TowerConfig
    {


        /// <summary>
        /// Reads values from a properly structured json file and sets this TowerConfig accordingly. The filename parameter
        /// is the name of the json tower config file assumed to be in \Definitions\Towers.
        /// </summary>
        /// <param name="filename"></param>
        static public ConfigTowerVars GetTowerConfigFromJsonFile(string filename)
        {
            string JsonFromFile = File.ReadAllText(@"\Definitions\Towers\" + filename);
            return Newtonsoft.Json.JsonConvert.DeserializeObject<ConfigTowerVars>(JsonFromFile);
        }



        /// <summary>
        /// Writes out a json file with the same Json structure which SetTowerConfigFromJsonFile requires. 
        /// </summary>
        static public void WriteExampleJsonTowerConfig()
        {
            ConfigTowerVars TowerConfigVars = new ConfigTowerVars();


            TowerConfigVars.AttacksPerSecondGainedPerLevel = 2;
            TowerConfigVars.CostToBuild = 3;
            TowerConfigVars.UpgradeCostIncreasePerLevel = 10;
            TowerConfigVars.CurrentCostToUpgrade = 4;
            TowerConfigVars.CurrentWeaponAreaOfEffect = 5;
            TowerConfigVars.CurrentWeaponAttacksPerSecond = 6;
            TowerConfigVars.CurrentWeaponDamage = 7;
            TowerConfigVars.CurrentWeaponRange = 8;
            TowerConfigVars.DamageGainedPerLevel = 10;
            TowerConfigVars.is_point_blank_area_damage_tower = false;
            TowerConfigVars.MaxTowerLevel = 11;
            TowerConfigVars.ProjectileSpeed = 12;
            TowerConfigVars.WeaponRangeGainedPerLevel = 13;

            TowerConfigVars.scale = 1.0f;
            TowerConfigVars.rotation = 0.0f;

            TowerConfigVars.TextureProjectile = "TextureProjectile";
            TowerConfigVars.SoundShoot = "SoundShoot";
            TowerConfigVars.SoundBuild = "soundBuild";
            TowerConfigVars.SoundUpgrade = "SoundUpdate";
            TowerConfigVars.TowerName = "ExampleJsonDefinedTower";

            TowerConfigVars.StatusEffects.Add(new Common.status_effect("Slow", 10000));
            TowerConfigVars.StatusEffects.Add(new Common.status_effect("ArmorReduce", 10000));

            String JsonOutput = Newtonsoft.Json.JsonConvert.SerializeObject(TowerConfigVars, Formatting.Indented);

            File.WriteAllText(Directory.GetCurrentDirectory() + @"\Definitions\Towers\Example_Json_Tower_Definition.txt", JsonOutput, Encoding.ASCII);
        }




    }
    
}
