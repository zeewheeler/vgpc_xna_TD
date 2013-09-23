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
                //m_Active = false;
            }
    }
}
