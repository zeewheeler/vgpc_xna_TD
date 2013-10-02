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
        public Tower(Texture2D default_tex)
            : base(default_tex)
        {
            //sounds
            sound_shoot     = null;
            sound_upgrade   = null;
            sound_build     = null;
          
            
            //projectiles
            texture_projectile  = null;

            damage_level_1 = 1;
            damage_level_2 = 2;
            damage_level_3 = 3;

            area_of_effect_level_1 = 1;
            area_of_effect_level_2 = 1;
            area_of_effect_level_3 = 1;

            //
            rate_of_fire_level_shoots_per_second_1 = 3;
            rate_of_fire_level_shoots_per_second_2 = 2;
            rate_of_fire_level_shoots_per_second_3 = 1;

            cost_to_build  = 1;
            cost_upgrade_1 = 2;
            cost_upgrade_2 = 3;

            weapon_range_level_1 = 20;
            weapon_range_level_2 = 30;
            weapon_range_level_3 = 40;

            projectile_speed = 2;

            max_projectiles = 20;

            weapon_shoot_timer = TimeSpan.Zero;

            projectiles = new List<Projectile>();

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

        protected virtual void update_projectiles()
        {
            foreach (Projectile projectile in projectiles)
            {

            }
        }

        protected virtual void update_weapon_fire( List<EnemyMob> enemy_mobs )
        {
            if ((float)weapon_shoot_timer.Seconds > rate_of_fire_level_shoots_per_second_1)
            {
                weapon_shoot_timer = TimeSpan.Zero;

                for (int i = 0; i < projectiles.Count; i++)
                {
                    if (!projectiles[i].is_active)
                    {
                        Vector2 target_pos;
                        projectiles[i].is_active = true;
                        target_pos = Util.vgpc_math.find_nearest_mob(this.position, enemy_mobs);
                        projectiles[i].velocity = 
                    }

                }
            }
        }
        




        //sounds
        protected SoundEffect sound_shoot;
        protected SoundEffect sound_upgrade;
        protected SoundEffect sound_build;

      
       
            
        //projectiles
        protected static Texture2D texture_projectile; //texture
        
        //Weapon Numbers
        
        protected float damage_level_1;
        protected float damage_level_2;
        protected float damage_level_3;

        protected float area_of_effect_level_1;
        protected float area_of_effect_level_2;
        protected float area_of_effect_level_3;

        protected float rate_of_fire_level_shoots_per_second_1;
        protected float rate_of_fire_level_shoots_per_second_2;
        protected float rate_of_fire_level_shoots_per_second_3;

        protected float weapon_range_level_1;
        protected float weapon_range_level_2;
        protected float weapon_range_level_3;

        protected float cost_to_build;
        protected float cost_upgrade_1;
        protected float cost_upgrade_2;

        protected float projectile_speed;

        protected int max_projectiles;

        protected List<Projectile> projectiles;

        protected TimeSpan weapon_shoot_timer;



        
        
        //bool
        private bool animated_tower; //if false, tower will only use default texture
        private bool rotating_tower;

              

            

           
    }
}
