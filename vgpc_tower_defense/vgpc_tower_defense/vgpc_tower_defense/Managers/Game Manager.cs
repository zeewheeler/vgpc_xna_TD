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
    public class Game_Manager : DrawableGameComponent
    {
        protected SpriteBatch SpriteBatch;
        
        protected LevelManager LevelManager;
        protected AssetManager AssetManager;
        

        //A dictionary of lists that will hold instantiated Mobs grouped by type
        public Dictionary<String,  List<EnemyMob>> Mobs;

        //A dictionary of lists that will hold instantiaed towers grouped by type
        public Dictionary<String, List<Tower>> Towers;

        // Dictopnary containing defined mob way points. These describe the spawning point, the "end-zone" and a path connecting the two
        public Dictionary<String, MobPathingInfo> MobPaths;
           

        enum GameState { Run, Pause, Menu, Etc };

        public Game_Manager(Game game)
            : base(game)
        {
            SpriteBatch = new SpriteBatch(Game.GraphicsDevice);

            
            AssetManager = new Managers.AssetManager(game);
            LevelManager = new LevelManager(AssetManager);

            Mobs    = new Dictionary<string, List<EnemyMob>>();
            Towers = new Dictionary<string, List<Tower>>();
        }

        public override void Initialize()
        {
            base.Initialize();
        }

        protected override void LoadContent()
        {
            base.LoadContent();

            AssetManager.LoadAllContent();
        }

        public override void Draw(GameTime gameTime)
        {
            base.Draw(gameTime);

            //for each instantiated mob type
            foreach (var mobList in Mobs)
            {
                //for each instantiated mob in each mob type
                foreach (EnemyMob mob in mobList.Value)
                {
                    mob.Draw(SpriteBatch);
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

        

        public override void Update(GameTime gameTime)
        {
 	         base.Update(gameTime);

             LevelManager.Update(gameTime);

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
        
    }
}
