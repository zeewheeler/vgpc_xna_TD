using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace vgpc_tower_defense.GameObjects
{
    public class TowerConfig /*This class holds the variables required for a tower and methods to easy set the values*/
    {
        
        public String SoundShoot; /*SoundEffect to be played when the tower shoots*/
        public String SoundUpgrade; /*SoundEffect, if any, to be played when tower is upgraded*/
        public String SoundBuild; /*SoundEffect to be played when tower is built*/

        //tower level 
        public int MaxTowerLevel; /* The maximum allowed level of tower*/

        //projectiles
        public String TextureProjectile; /*The texture of the tower's projectile*/

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

        //Costs
        public int CostToBuild; /*Cost to build the tower*/
        public int CurrentCostToUpgrade; /*Cost to upgrade the tower*/
        public int UpgradeCostIncreasePerLevel; /*The amount the cost to upgrade the tower increase each level*/

        //tower state variables

        public bool is_point_blank_area_damage_tower; /*True if tower is a point-blank area effect type, false if not*/

        //Tower graphic options
        public float scale;/*How big the towers graphic will be*/
        public float rotation; /*What rotation, if any, that tower's graphic will be drawed with*/
        
        
        public TowerConfig()
        {
            StatusEffects = new List<Common.status_effect>();
        }

        /// <summary>
        /// Reads values from a properly structured json file and sets this TowerConfig accordingly. The filename parameter
        /// is the name of the json tower config file assumed to be in \Definitions\Towers.
        /// </summary>
        /// <param name="filename"></param>
        public void SetTowerConfigFromJsonFile(string filename)
        {
             string JsonFromFile = File.ReadAllText(@"\Definitions\Towers\" + filename);
             this = Newtonsoft.Json.JsonConvert.DeserializeObject<TowerConfig>(JsonFromFile);
        }


        
        /// <summary>
        /// Writes out a json file with the same Json structure which SetTowerConfigFromJsonFile requires. 
        /// </summary>
        public void WriteExampleJsonTowerConfig()
        {
               TowerConfig TowerConfig = new TowerConfig();
            
            TowerConfig.AttacksPerSecondGainedPerLevel = 2;
            TowerConfig.CostToBuild = 3;
            TowerConfig.UpgradeCostIncreasePerLevel = 10;
            TowerConfig.CurrentCostToUpgrade = 4;
            TowerConfig.CurrentWeaponAreaOfEffect = 5;
            TowerConfig.CurrentWeaponAttacksPerSecond = 6;
            TowerConfig.CurrentWeaponDamage = 7;
            TowerConfig.CurrentWeaponRange = 8;
            TowerConfig.DamageGainedPerLevel = 10;
            TowerConfig.is_point_blank_area_damage_tower = false;
            TowerConfig.MaxTowerLevel = 11;
            TowerConfig.ProjectileSpeed = 12;
            TowerConfig.WeaponRangeGainedPerLevel = 13;

            TowerConfig.scale = 1.0f;
            TowerConfig.rotation = 0.0f;

            TowerConfig.TextureProjectile = "TextureProjectile";
            TowerConfig.SoundShoot = "SoundShoot";
            TowerConfig.SoundBuild = "soundBuild";
            TowerConfig.SoundUpgrade = "SoundUpdate";

            TowerConfig.StatusEffects.Add(new Common.status_effect("Slow", 10000));
            TowerConfig.StatusEffects.Add(new Common.status_effect("ArmorReduce", 10000));

            String JsonOutput = Newtonsoft.Json.JsonConvert.SerializeObject(TowerConfig, Formatting.Indented);

            File.WriteAllText(Directory.GetCurrentDirectory() + @"\Definitions\Towers\Example_Json_Tower_Definition.txt", JsonOutput, Encoding.ASCII);
        }


        

    }
}
