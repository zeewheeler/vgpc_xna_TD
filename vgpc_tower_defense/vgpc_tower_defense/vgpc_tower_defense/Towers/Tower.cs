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

        public TowerConfig TowerVars;

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
            CurrentWeaponDamage = 1;
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

        public Tower(TowerConfig TowerConfig)
            : base(null)
        {
            TowerVars = TowerConfig;
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
                    if (!Util.vgpc_math.does_rectangle_contain(globals.viewport_rectangle, Projectile.Position))
                    {
                        Projectile.IsActive = false;
                        continue;
                    }

                    Rectangle projectileBoundingBox = new Rectangle((int)Projectile.Position.X, (int)Projectile.Position.Y,
                          this.TextureProjectile.Width, this.TextureProjectile.Height);

                    //check if projectile has collided with a mob
                    foreach (EnemyMob mob in globals.Mobs)
                    {
                        Rectangle mob_bounding_box = new Rectangle((int)mob.Position.X, (int)mob.Position.Y,
                            this.CurrentTexture.Width, this.CurrentTexture.Height);

                        //if mob is 
                        if (mob_bounding_box.Intersects(projectileBoundingBox))
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


                Vector2 TargetPosition = Util.vgpc_math.find_nearest_mob(this.Position, enemyMobs);


                //find the nearest mob to this tower
                float distance_to_closest_mob = Util.vgpc_math.get_distance_between(this.GetCenter(), TargetPosition);

                if (TargetPosition != null && (distance_to_closest_mob <= this.CurrentWeaponRange))
                {
                    for (int i = 0; i < this.Projectiles.Count; i++)
                    {
                        if (!Projectiles[i].IsActive)
                        {
                            Projectiles[i].IsActive = true;

                            //create a vector from this tower to the nearest mob
                            Projectiles[i].Velocity = Util.vgpc_math.create_target_unit_vector(this.GetCenter(), TargetPosition);

                            //since the function creates a unit vecor(lenth, which in this case is the speed portion of the vector), we need to multiply the vectoy
                            //by the turrent projectile speed
                            Projectiles[i].Velocity *= ProjectileSpeed;

                            //set the projectile position equal to this tower's position
                            Projectiles[i].Position = this.GetCenter();
                            break;
                        }
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
                    float distance = Util.vgpc_math.get_distance_between(this.Position, mob.Position);
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
