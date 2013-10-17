using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace vgcpTowerDefense.GameObjects
{
   
        public class Projectile : DrawableGameObject
        {
           
            public Projectile(Texture2D loaded_texture)
                : base(loaded_texture)
            {
                speed = 1;
            }


           
            public float speed;

           
            

          
            
           
        }
    
}
