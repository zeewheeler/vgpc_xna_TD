using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using vgcpTowerDefense.GameObjects;

namespace vgcpTowerDefense.Managers
{
    /// <summary>
    /// Manages instantiated units, such as towers and mobs
    /// </summary>
    public class Unit_Manager
    {
          public static List<EnemyMob> Mobs;
          public static List<Tower> Towers;
          
        public Unit_Manager()
        {
            Mobs = new List<EnemyMob>();
            Towers = new List<Tower>();
        }

        public void Update(GameTime gameTime)
        {
            for
        }

    }
}
