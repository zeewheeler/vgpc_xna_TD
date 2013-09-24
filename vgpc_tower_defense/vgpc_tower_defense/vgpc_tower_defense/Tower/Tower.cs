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
        public Tower(Texture2D default_tex, string Tower_Type)
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
            
            //texture_right       = null;
            //texture_up_right    = null;
            //texture_up          = null;
            //texture_up_left     = null;
            //texture_left        = null;
            //texture_down_left   = null;
            //texture_down        = null;
            //texture_down_right  = null;
 
            
        }

        //sounds
        protected SoundEffect sound_shoot;
        protected SoundEffect sound_upgrade;
        protected SoundEffect sound_build;

      
        //private Texture2D texture_default_tower;
        //private Texture2D texture_right;
        //private Texture2D texture_up_right;
        //private Texture2D texture_up;
        //private Texture2D texture_up_left;
        //private Texture2D texture_left; 
        //private Texture2D texture_down_left; 
        //private Texture2D texture_down;
        //private Texture2D texture_down_right;
            
        //projectiles
        protected Texture2D texture_projectile; //texture
        
        
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


        
        
        //bool
        private bool animated_tower; //if false, tower will only use default texture
        private bool rotating_tower;

              

            

           
    }
}
