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
    /// The game manager "glues" together all of the other managers, and provides one interface to manager the game
    /// </summary>
    public class Game_Manager : GameComponent
    {
        public LevelManager LevelManager;
        public AssetManager AssetManager;

        public List<EnemyMob> Mobs;
        public List<Tower> Towers;

        string State;

        public Game_Manager(Game game)
            : base(game)
        {
            AssetManager = new Managers.AssetManager(game);
            LevelManager = new LevelManager(AssetManager);

             Mobs = new List<EnemyMob>();
             Towers = new List<Tower>();
        }

        public override void Update(GameTime gameTime)
        {
 	         base.Update(gameTime);

             LevelManager.Update(gameTime);

             foreach (EnemyMob mob in Mobs)
             {
                 mob.Update(gameTime);
             }

             foreach (Tower Tower in Towers)
             {
                 Tower.Update(gameTime);
             }
        }
        
    }
}
