using System;
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
    public class TowerConfig
    {

        public TowerConfig()
        {
            StatusEffects = new List<Common.status_effect>();

            
        }

        public String/*SoundEffect*/ SoundShoot;
        public String/*SoundEffect*/ SoundUpgrade;
        public String/*SoundEffect*/ SoundBuild;

        //tower level 
        public int MaxTowerLevel;

        

        //projectiles
        public String/*Texture2D*/ TextureProjectile; //texture

        //Weapon Numbers


        public float CurrentWeaponDamage;
        public float CurrentWeaponAreaOfEffect;
        public float CurrentWeaponAttacksPerSecond;
        public float CurrentWeaponRange;

        //Some towers may cause various effects, such as slow or damage over time. They will just be strings and will be copied over to mob
        // The mob will process it's own status effects
        public List<Common.status_effect> StatusEffects;




        public float DamageGainedPerLevel;
        public float AreaOfEffectGainedPerLevel;
        public float AttacksPerSecondGainedPerLevel;
        public float WeaponRangeGainedPerLevel;

        public int CostToBuild;

        public int CurrentCostToUpgrade;

        public float ProjectileSpeed;

        //public int MaxProjectiles;



        //public List<Projectile> Projectiles;

        //public TimeSpan WeaponShootTimer;

        //tower state variables

        //true if doesn't doesn't shoot projectile but rather damages a point blank area around it
        public bool is_point_blank_area_damage_tower;
        //true if some effect is present that disables the tower(causes it not to fire)
        //public bool IsDisabled { get; set; }
        //int current_tower_level;

        //base gameObject values
        public float scale;
        public float rotation;

    }
}
