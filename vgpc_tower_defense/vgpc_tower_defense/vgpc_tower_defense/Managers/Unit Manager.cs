using System;
using System.Collections;
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
    public class UnitManager
    {

          //A dictionary of lists that will hold instantiated Mobs grouped by type
          public Dictionary<String, List<EnemyMob>> Mobs;

          //A dictionary of lists that will hold instantiaed towers grouped by type
          public Dictionary<String, List<Tower>> Towers;

          protected AssetManager AssetManager;
          
        public UnitManager(AssetManager assetManager)
        {
            Mobs = new Dictionary<string, List<EnemyMob>>();
            Towers = new Dictionary<string, List<Tower>>();

            AssetManager = assetManager;
        }

        public void Update(GameTime gameTime)
        {

            //for each instantiated mob type
            foreach (var mobList in Mobs)
            {
                //for each instantiated mob in each mob type
                foreach (EnemyMob mob in mobList.Value)
                {
                    mob.Update(gameTime);
                }
            }

            //for each instantiated tower type
            foreach (var towerList in Towers)
            {
                //for each instantiated tower in each tower type
                foreach (Tower tower in towerList.Value)
                {
                    tower.Update(gameTime);
                }
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            //for each instantiated mob type
            foreach (var mobList in Mobs)
            {
                //for each instantiated mob in each mob type
                foreach (EnemyMob mob in mobList.Value)
                {
                    mob.Draw(spriteBatch);
                }
            }

            //for each instantiated tower type
            foreach (var TowerType in Towers)
            {
                //for each instantiated tower in each tower type
                foreach (Tower tower in TowerType.Value)
                {
                    tower.Draw(spriteBatch);
                }
            }
        }

        

    }
}
