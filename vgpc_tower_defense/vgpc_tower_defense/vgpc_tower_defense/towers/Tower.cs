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
        public Tower(string Tower_Type)
            : base(null)
        {
            sound_shoot = null;
            sound_upgrade = null;
            texture_projectile = null;
            texture_tower = null;
            tower_type = Tower_Type;

           
            
        }

        //sounds
        private SoundEffect sound_shoot;
        private SoundEffect sound_upgrade;

        //textures
        private Texture2D texture_projectile;
        private Texture2D texture_tower;

        //type
        private string tower_type;

        //containers
        List<Projectile> tower_projectiles;


      

            

           
    }
}
