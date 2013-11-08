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


using FuncWorks.XNA.XTiled;

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
        protected UnitManager UnitManager;
        

       

        enum GameState { Run, Pause, Menu, Etc };


           

        public Game_Manager(Game game)
            : base(game)
        {
            SpriteBatch = new SpriteBatch(Game.GraphicsDevice);

            
            AssetManager = new Managers.AssetManager(game);
            UnitManager = new UnitManager(AssetManager);
            LevelManager = new LevelManager(AssetManager);

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
            UnitManager.Draw(SpriteBatch);
          
        }

        

        public override void Update(GameTime gameTime)
        {
 	         base.Update(gameTime);

             LevelManager.Update(gameTime);
             UnitManager.Update(gameTime);

            
        }
        
    }
}
