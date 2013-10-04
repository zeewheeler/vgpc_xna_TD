using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Newtonsoft.Json;

namespace vgpc_tower_defense.GameObjects
{
    public class Tower : GameObject
    {
     
        //constructor
        public Tower(Texture2D default_texture, Texture2D projectile_texture)
            : base(default_texture)
        {
            //sounds
            sound_shoot     = null;
            sound_upgrade   = null;
            sound_build     = null;
            
            //textures
            texture_projectile = projectile_texture;

            //weapon metrics       
            current_weapon_damage               = 1;
            current_weapon_area_of_effect       = 10;
            current_weapon_attacks_per_second   = 1;
            current_weapon_range                = 100;

            damage_gained_per_level             = 10;
            area_gained_per_level               = 0;
            attacks_per_second_gained_per_level = 0;
            weapon_range_gained_per_level       = 20;
            
            projectile_speed = 2;
            max_projectiles = 20;

            //build and upgrade
            cost_to_build  = 1;
            current_cost_to_upgrade = 1;
            max_tower_level = 3;
           
            //Initializations
            projectiles = new List<Projectile>();
            status_effects = new List<string>();
            weapon_shoot_timer = TimeSpan.Zero;

            for (int i = 0; i < max_projectiles; i++)
            {
                Projectile new_projectile = new Projectile(null);
                new_projectile.speed = projectile_speed;
                projectiles.Add(new_projectile);
            }
        }



        public void load_projectile_texture(Texture2D projectile_texture)
        {
            texture_projectile = projectile_texture;
        }



        protected virtual bool level_up_tower()
        {
            if (globals.player_cash >= current_cost_to_upgrade && (current_tower_level < max_tower_level))
            {
                current_tower_level += 1;
                globals.player_cash -= current_cost_to_upgrade;

                current_weapon_damage               += damage_gained_per_level;
                current_weapon_area_of_effect       += area_gained_per_level;
                current_weapon_attacks_per_second   += attacks_per_second_gained_per_level;
                current_weapon_range                += weapon_range_gained_per_level;
                return true;
            }
            else
            {
                return false;
            }
        }



        protected virtual void update_projectiles()
        {
            foreach (Projectile projectile in projectiles)
            {

            }
        }



        protected virtual void fire_at_closest_mob(List<EnemyMob> enemy_mobs)
        {
            if(!is_disabled)
            {
                for (int i = 0; i < projectiles.Count; i++)
                {
                    if (!projectiles[i].is_active)
                    {
                        projectiles[i].is_active = true;
                        
                        Vector2 target_pos;
                        target_pos = Util.vgpc_math.find_nearest_mob(this.position, enemy_mobs);
                        projectiles[i].velocity *= projectile_speed;
                        break;
                    }
                }
            }
        }

        //
        protected virtual void damage_and_affect_mop(EnemyMob mob)
        {
            mob.damage_me((int)current_weapon_damage);

            
        }

        


        protected virtual void fire_point_blank_weapon(List<EnemyMob> enemy_mobs)
        {
            if (!is_disabled)
            {
                foreach (EnemyMob mob in enemy_mobs)
                {
                    float distance = Util.vgpc_math.get_distance_between(this.position, mob.position);
                    if (distance <= current_weapon_range)
                    {
                        damage_and_affect_mop(mob);
                    }
                }
            }
        }



        protected virtual void update_weapon_fire( List<EnemyMob> enemy_mobs )
        {
            if ((float)weapon_shoot_timer.Milliseconds > (1000f / current_weapon_attacks_per_second) )
            {
                weapon_shoot_timer = TimeSpan.Zero;
                if (!is_point_blank_area_damage_tower)
                {
                    fire_at_closest_mob(enemy_mobs);
                }
                else
                {
                    fire_point_blank_weapon(enemy_mobs);
                }
               
            }
        }
        




        //sounds
        protected SoundEffect sound_shoot;
        protected SoundEffect sound_upgrade;
        protected SoundEffect sound_build;

        //tower level 
        protected int max_tower_level;

      
       
            
        //projectiles
        protected static Texture2D texture_projectile; //texture
        
        //Weapon Numbers


        protected float current_weapon_damage;
        protected float current_weapon_area_of_effect;
        protected float current_weapon_attacks_per_second;
        protected float current_weapon_range;

        //Some towers may cause various effects, such as slow or damage over time. They will just be strings and will be copied over to mob
        // The mob will process it's own status effects
        protected List<string> status_effects;
        


            
        protected float damage_gained_per_level;
        protected float area_gained_per_level;
        protected float attacks_per_second_gained_per_level;
        protected float weapon_range_gained_per_level;

        public int cost_to_build { get; protected set; }
       
        protected int current_cost_to_upgrade;
     
        protected float projectile_speed;
            
        protected int max_projectiles;

       

        protected List<Projectile> projectiles;

        protected TimeSpan weapon_shoot_timer;

        //tower state variables
        
        //true if doesn't doesn't shoot projectile but rather damages a point blank area around it
        protected bool is_point_blank_area_damage_tower;
        //true if some effect is present that disables the tower(causes it not to fire)
        public bool is_disabled {get; set;}
        int current_tower_level;
        

           
    }
}
