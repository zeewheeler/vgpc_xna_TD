using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Newtonsoft.Json;
using System.Diagnostics;

namespace vgpc_tower_defense.GameObjects
{
    
    
    public class Tower : DrawableGameObject
    {


        //sounds
        protected SoundEffect SoundShoot;
        protected SoundEffect SoundUpgrade;
        protected SoundEffect SoundBuild;

        //tower level 
        protected int MaxTowerLevel;




        //projectiles
        protected Texture2D TextureProjectile; //texture

        //Weapon Numbers


        protected float CurrentWeaponDamage;
        protected float CurrentWeaponAreaOfEffect;
        protected float CurrentWeaponAttacksPerSecond;
        protected float CurrentWeaponRange;

        //Some towers may cause various effects, such as slow or damage over time. They will just be strings and will be copied over to mob
        // The mob will process it's own status effects
        protected List<Common.status_effect> StatusEffects;




        protected float DamageGainedPerLevel;
        protected float AreaOfEffectGainedPerLevel;
        protected float AttacksPerSecondGainedPerLevel;
        protected float WeaponRangeGainedPerLevel;

        public int CostToBuild { get; protected set; }

        protected int CurrentCostToUpgrade;

        protected float ProjectileSpeed;

        protected int MaxProjectiles;



        protected List<Projectile> Projectiles;

        protected TimeSpan WeaponShootTimer;

        //tower state variables

        //true if doesn't doesn't shoot projectile but rather damages a point blank area around it
        protected bool is_point_blank_area_damage_tower;
        //true if some effect is present that disables the tower(causes it not to fire)
        public bool IsDisabled { get; set; }
        int current_tower_level;
        
        
        
        //constructor
        public Tower(Texture2D defaultTexture, Texture2D textureProjectile)
            : base(defaultTexture)
        {
            //sounds
            SoundShoot = null;
            SoundUpgrade = null;
            SoundBuild = null;

            //textures
            TextureProjectile = textureProjectile;

            //weapon metrics       
            CurrentWeaponDamage = 10;
            CurrentWeaponAreaOfEffect = 10;
            CurrentWeaponAttacksPerSecond = 1;
            CurrentWeaponRange = 10000;

            DamageGainedPerLevel = 10;
            AreaOfEffectGainedPerLevel = 0;
            AttacksPerSecondGainedPerLevel = 0;
            WeaponRangeGainedPerLevel = 20;

            ProjectileSpeed = 25;
            MaxProjectiles = 20;

            //build and upgrade
            CostToBuild = 1;
            CurrentCostToUpgrade = 1;
            MaxTowerLevel = 3;

            Scale = .8f;

            //Initializations
            Projectiles = new List<Projectile>();
            StatusEffects = new List<Common.status_effect>();
            WeaponShootTimer = TimeSpan.Zero;

            for (int i = 0; i < MaxProjectiles; i++)
            {
                Projectile NewProjectile = new Projectile(this.TextureProjectile);
                NewProjectile.speed = ProjectileSpeed;
                Projectiles.Add(NewProjectile);
            }
        }

        public Tower(String config_file_path)
            : base(null)
        {

        }

        
        public virtual bool TowerConfigReader(String configString)
        {
            return false;
        }

        
        public void load_projectile_texture(Texture2D projectile_texture)
        {
            TextureProjectile = projectile_texture;
        }


        public void CreateExampleJsonTowerConfigFile()
        {
            TowerConfig TowerConfig = new TowerConfig();

            TowerConfig.AreaOfEffectGainedPerLevel = 1;
            TowerConfig.AttacksPerSecondGainedPerLevel = 2;
            TowerConfig.CostToBuild = 3;
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

            File.WriteAllText(Directory.GetCurrentDirectory() + @"\config\Example_Json_Tower_Definition.txt", JsonOutput, Encoding.ASCII);
            //string JsonFromFile = File.ReadAllText("JsonInput.txt");
            //TowerConfig = Newtonsoft.Json.JsonConvert.DeserializeObject<TowerConfig>(JsonFromFile);
           
            
     

        }



        protected virtual bool level_up_tower()
        {
            if (globals.PlayerCash >= CurrentCostToUpgrade && (current_tower_level < MaxTowerLevel))
            {
                current_tower_level += 1;
                globals.PlayerCash -= CurrentCostToUpgrade;

                CurrentWeaponDamage += DamageGainedPerLevel;
                CurrentWeaponAreaOfEffect += AreaOfEffectGainedPerLevel;
                CurrentWeaponAttacksPerSecond += AttacksPerSecondGainedPerLevel;
                CurrentWeaponRange += WeaponRangeGainedPerLevel;
                return true;
            }
            else
            {
                return false;
            }
        }



        protected virtual void UpdateProjectiles(GameTime gameTime)
        {
            foreach (Projectile Projectile in Projectiles)
            {
                if (Projectile.IsActive)
                {

                    //check if projectile is off the screen, if so, mark as inactive
                    if (!Util.vgpc_math.DoesRectangleContainVector(globals.viewport_rectangle, Projectile.Position))
                    {
                        Projectile.IsActive = false;
                        continue;
                    }

                    Rectangle ProjectileBoundingBox = Projectile.GetBoundingRectangle();

                    //check if projectile has collided with a mob
                    foreach (EnemyMob mob in globals.Mobs)
                    {

                        Rectangle mob_bounding_box = mob.GetBoundingRectangle();

                        //if mob is 
                        if (mob_bounding_box.Intersects(ProjectileBoundingBox))
                        {
                            Projectile.IsActive = false;
                            DamageAndAffectMob(mob);
                            break;
                        }
                    }

                    //finally,  update the position of each active projectile
                    Projectile.update_position();



                }
            }
        }

        public virtual void Update(GameTime gameTime)
        {
            UpdateWeapon(gameTime);
            UpdateProjectiles(gameTime);
            UpdateAnimation(gameTime);

        }



        protected virtual void UpdateAnimation(GameTime game_time)
        {
            //todo
        }

        protected virtual void FireAtClosestMob(List<EnemyMob> enemyMobs)
        {
            if (!IsDisabled && (enemyMobs.Count > 0))
            {

                List<Util.vgpc_math.FindNearestMobResult> Results = Util.vgpc_math.FindNearestMob(this.Position, enemyMobs);
                if (1 != Results.Count)
                {
                    return;
                }
                if (Results[0].Distance > this.CurrentWeaponRange)
                {
                    return;
                }


                for (int i = 0; i < this.Projectiles.Count; i++)
                {
                    if (!Projectiles[i].IsActive)
                    {
                        Projectiles[i].IsActive = true;

                        //create a vector from this tower to the nearest mob
                        Projectiles[i].Velocity = Util.vgpc_math.create_target_unit_vector(this.Position, Results[0].EnemyMob.Position);

                        //since the function creates a unit vecor(lenth, which in this case is the speed portion of the vector), we need to multiply the vectoy
                        //by the turrent projectile speed
                        Projectiles[i].Velocity *= ProjectileSpeed;

                        //set the projectile position equal to this tower's position
                        Projectiles[i].Position = this.Position;
                        break;
                    }
                }
            }
        }

        //
        protected virtual void DamageAndAffectMob(EnemyMob mob)
        {
            mob.damage_me((int)CurrentWeaponDamage);
            mob.AddStatusEffects(StatusEffects);

        }




        protected virtual void fire_point_blank_weapon(List<EnemyMob> enemy_mobs)
        {
            if (!IsDisabled)
            {
                foreach (EnemyMob mob in enemy_mobs)
                {
                    float distance = Util.vgpc_math.GetDistanceBetweenTwoVectors(this.Position, mob.Position);
                    if (distance <= CurrentWeaponRange)
                    {
                        DamageAndAffectMob(mob);
                    }
                }
            }
        }



        protected virtual void UpdateWeapon(GameTime game_time)
        {
            WeaponShootTimer += game_time.ElapsedGameTime;

            TimeSpan attack_interval = new TimeSpan(0, 0, 0, 0, (int)(1000 / CurrentWeaponAttacksPerSecond));

            if (WeaponShootTimer > attack_interval)
            {
                WeaponShootTimer = TimeSpan.Zero;
                if (!is_point_blank_area_damage_tower)
                {
                    FireAtClosestMob(globals.Mobs);
                }
                else
                {
                    fire_point_blank_weapon(globals.Mobs);
                }

            }
        }

        //the base gameobject class just draws the default texture, but we the tower class to draw all if it's associated projectiles as well
        //thus we will override the base draw function and add this functionality
        public override void draw(SpriteBatch spriteBatch)
        {
            base.draw(spriteBatch);
            foreach (Projectile projectile in this.Projectiles)
            {
                projectile.draw(spriteBatch, this.TextureProjectile);
            }
        }
    }
}
