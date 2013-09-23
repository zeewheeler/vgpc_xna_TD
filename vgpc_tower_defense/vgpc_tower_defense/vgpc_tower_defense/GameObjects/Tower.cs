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
    public class Tower : GameObject
    {
     
        public Tower(Texture2D loaded_texture, SoundEffect soundBuild, SoundEffect soundShoot, SoundEffect soundUpgrade, Vector2 pos)
            : base(loaded_texture)
        {
            sound_shoot = soundShoot;
            sound_upgrade = soundUpgrade;
            soundBuild.Play();
            PositionX = pos.X;
            PositionY = pos.Y;
        }

        private SoundEffect sound_shoot;
        private SoundEffect sound_upgrade;
            

           
    }
}
