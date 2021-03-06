﻿using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace vgcpTowerDefense.Config
{



    ////////GENERAL CONFIG UTILS

    /// <summary>
    /// Struct-like class the holds a configuration entry.
    /// </summary>
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


    /// <summary>
    /// Reads configuration settings from a json formated file. Also writes out an example config file
    /// </summary>
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
    /// Reads tower config data from json formatted files, also creates example config files
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
            string JsonFromFile = File.ReadAllText(@"Definitions\Towers\" + filename);
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
            TowerConfigVars.CurrentWeaponRange = 10000;
            TowerConfigVars.DamageGainedPerLevel = 10;
            TowerConfigVars.is_point_blank_area_damage_tower = false;
            TowerConfigVars.MaxTowerLevel = 11;
            TowerConfigVars.ProjectileSpeed = 12;
            TowerConfigVars.WeaponRangeGainedPerLevel = 13;

            TowerConfigVars.scale = 1.0f;
            TowerConfigVars.rotation = 0.0f;

            TowerConfigVars.TextureProjectile = "cannonball";
            TowerConfigVars.TextureTower = "PlasmaRight";
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

    /// <summary>
    /// Holds mob related vars
    /// </summary>
    public class ConfigMobVars
    {
        public int Health;
        public float Speed; /*The movementspeed of the mob*/
        public string MobIdentifier;

 
    }
    
    /// <summary>
    /// Reads EnemyMob vars from a file
    /// </summary>
    static public class MobConfig
    {
        //public struct 
    }



    public class ConfigLevelVars
    {
        public List<GameObjects.MobWave> LevelMobWaves;
        public string LevelIdentifier;

        public ConfigLevelVars()
        {
            LevelMobWaves = new List<GameObjects.MobWave>();
        }
    }



    /// <summary>
    /// Reads Level vars from a file
    /// </summary>
    public static class LevelConfig
    {

        static public void WriteExampleJsonLevelConfig()
        {
            //List<GameObjects.MobWave> LevelMobWaves = new List<GameObjects.MobWave>();
            ConfigLevelVars ExampleLevel = new ConfigLevelVars();

            ExampleLevel.LevelIdentifier = "Example Level Json Definition";


            GameObjects.MobWave MobWave1 = new GameObjects.MobWave();
            GameObjects.MobWave MobWave2 = new GameObjects.MobWave();
            GameObjects.MobWave MobWave3 = new GameObjects.MobWave();

            GameObjects.MobSpawnEntry MobEntry1 = new GameObjects.MobSpawnEntry();
            GameObjects.MobSpawnEntry MobEntry2 = new GameObjects.MobSpawnEntry();
            GameObjects.MobSpawnEntry MobEntry3 = new GameObjects.MobSpawnEntry();
           

            MobEntry1.DelayAfter_ms = 1000;
            MobEntry1.MobIdentifier = "EvilRobotRight";
            MobEntry1.PathIdentifer = "Path_1";

            MobEntry2.DelayAfter_ms = 2000;
            MobEntry2.MobIdentifier = "EvilRobotRight";
            MobEntry2.PathIdentifer = "Path_1";

            MobEntry3.DelayAfter_ms = 3000;
            MobEntry3.MobIdentifier = "EvilRobotRight";
            MobEntry3.PathIdentifer = "Path_1";
            
          
            MobWave1.MobSpawnEntries.Add(MobEntry1);
            MobWave1.MobSpawnEntries.Add(MobEntry2);
            MobWave1.MobSpawnEntries.Add(MobEntry3);

            MobWave2.MobSpawnEntries.Add(MobEntry3);
            MobWave2.MobSpawnEntries.Add(MobEntry2);
            MobWave2.MobSpawnEntries.Add(MobEntry1);

            MobWave3.MobSpawnEntries.Add(MobEntry2);
            MobWave3.MobSpawnEntries.Add(MobEntry3);
            MobWave3.MobSpawnEntries.Add(MobEntry1);

            ExampleLevel.LevelMobWaves.Add(MobWave1);
            ExampleLevel.LevelMobWaves.Add(MobWave2);
            ExampleLevel.LevelMobWaves.Add(MobWave3);


            String JsonOutput = Newtonsoft.Json.JsonConvert.SerializeObject(ExampleLevel, Formatting.Indented);

            File.WriteAllText(Directory.GetCurrentDirectory() + @"\Definitions\Levels\Example_Json_Level_Definition.txt", JsonOutput, Encoding.ASCII);
        }

        static public ConfigLevelVars GetLevelConfigFromJsonFile(string filename)
        {
            string JsonFromFile = File.ReadAllText(@"Definitions\Levels\" + filename);
            return Newtonsoft.Json.JsonConvert.DeserializeObject<ConfigLevelVars>(JsonFromFile);
        }
    }

    
    
}
