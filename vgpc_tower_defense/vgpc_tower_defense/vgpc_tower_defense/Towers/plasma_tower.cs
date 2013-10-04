using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace vgpc_tower_defense.GameObjects
{


    public class plasma_tower : GameObjects.Tower
    {

        //constructor
        public plasma_tower(Texture2D default_tex, Texture2D projectile_texture)
            : base(default_tex, projectile_texture)
        {
           
        }

        //ref to point at the specific texture we want to draw
        public Texture2D texture_to_draw;
        //
        public Texture2D texture_default_tower;
        public Texture2D texture_right;
        public Texture2D texture_up_right;
        public Texture2D texture_up;
        public Texture2D texture_up_left;
        public Texture2D texture_left;
        public Texture2D texture_down_left;
        public Texture2D texture_down;
        public Texture2D texture_down_right;



       

      
    }
}
