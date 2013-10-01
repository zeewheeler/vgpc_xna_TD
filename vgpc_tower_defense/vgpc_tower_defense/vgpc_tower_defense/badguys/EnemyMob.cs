using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace vgpc_tower_defense.GameObjects
{
    public class EnemyMob : GameObject
    {
        public EnemyMob(Texture2D loadedTexture)
                : base(loadedTexture)
        {
            mob_health = 100;
        }

        protected int mob_health;

        public void damage_me(int damage)
        {
            mob_health -= damage;
        }
       

    }
}
