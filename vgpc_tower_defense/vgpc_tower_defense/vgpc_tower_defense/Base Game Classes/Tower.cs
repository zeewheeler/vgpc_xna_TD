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

            rate_of_fire_level_1 = 1;
            rate_of_fire_level_2 = 2;
            rate_of_fire_level_3 = 3;

            cost_to_build  = 1;
            cost_upgrade_1 = 2;
            cost_upgrade_2 = 3;

            projectile_speed = 2;

            max_projectiles = 20;

            for (int i = 0; i < max_projectiles; i++)
            {
                Projectile new_projectile = new Projectile(null);
                new_projectile.speed = projectile_speed;
                projectiles.Add(new Projectile(null));
            }

          
           
 
            
        }

        public void load_projectile_texture(Texture2D projectile_texture)
        {
            texture_projectile = projectile_texture;
        }

        private void update_projectiles()
        {
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

        protected float rate_of_fire_level_1;
        protected float rate_of_fire_level_2;
        protected float rate_of_fire_level_3;

        protected float cost_to_build;
        protected float cost_upgrade_1;
        protected float cost_upgrade_2;

        protected float projectile_speed;

        protected int max_projectiles;

        protected List<Projectile> projectiles;



        
        
        //bool
        private bool animated_tower; //if false, tower will only use default texture
        private bool rotating_tower;

              

            

           
    }
}
