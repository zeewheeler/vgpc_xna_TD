using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace vgcpTowerDefense.Managers
{
    /// <summary>
    ///     The game manager "glues" together all of the other managers, and provides one interface to manage the game
    /// </summary>
    public class Game_Manager : DrawableGameComponent
    {
        public AssetManager AssetManager;
        public LevelManager LevelManager;
        public SpriteBatch SpriteBatch;
        public UnitManager UnitManager;

        public Rectangle ViewportRectangle;


        public Game Game;
        
        
        public Game_Manager(Game game)
            : base(game)
        {
           


            AssetManager = new AssetManager(game);
            UnitManager = new UnitManager(AssetManager);
            LevelManager = new LevelManager(AssetManager, UnitManager);

           
           

            this.Game = game;
        }

        public override void Initialize()
        {
            base.Initialize();
            LevelManager.MapView = Game.GraphicsDevice.Viewport.Bounds;
        }

        protected override void LoadContent()
        {

            SpriteBatch = new SpriteBatch(Game.GraphicsDevice);

            ViewportRectangle = new Rectangle(0, 0, 
                Game.GraphicsDevice.Viewport.Width,
                Game.GraphicsDevice.Viewport.Width);

            AssetManager.LoadAllContent();


            base.LoadContent();


        }

        public override void Draw(GameTime gameTime)
        {
            base.Draw(gameTime);
            
            SpriteBatch.Begin();
                LevelManager.Draw(SpriteBatch);    
                UnitManager.Draw(SpriteBatch);
                
            SpriteBatch.End();
            
        }


        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);

            LevelManager.Update(gameTime);
            UnitManager.Update(gameTime);
        }

        private enum GameState
        {
            Run,
            Pause,
            Menu,
            Etc
        };
    }
}