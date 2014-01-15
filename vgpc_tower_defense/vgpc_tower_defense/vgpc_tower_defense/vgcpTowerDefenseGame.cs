using System;
using System.Collections.Generic;
using System.Collections;
using System.ComponentModel;
using System.Linq;
using System.IO;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

using vgcpTowerDefense.GameObjects;
using vgcpTowerDefense.Managers;


using FuncWorks.XNA.XTiled;

 



namespace vgcpTowerDefense
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class vgcp_tower_defense_game : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        Managers.Game_Manager GameManager;

        public vgcp_tower_defense_game()
        {

            Config.LevelConfig.WriteExampleJsonLevelConfig();
         
            GameManager = new Game_Manager(this);
            Components.Add(GameManager);

            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";

            //define what screen resolution the game should run in
            graphics.PreferredBackBufferWidth = 1280;
            graphics.PreferredBackBufferHeight = 720;

            globals.viewport_rectangle.Width = 1280;
            globals.viewport_rectangle.Height = 720;
         }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            // TODO: Add your initialization logic here

            base.Initialize();
            this.IsMouseVisible = true;


        }


        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {     

            Config.TowerConfig.WriteExampleJsonTowerConfig();

            GameManager.LevelManager.LoadMap("Map2");
            GameManager.LevelManager.LoadMobWavesFromFile("Example_Json_Level_Definition.txt");

        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// all content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {

            KeyboardState KeyboardState = Keyboard.GetState();

            if (KeyboardState.IsKeyDown(Keys.Space))
            {
                GameManager.LevelManager.IsActive = true;
            }
     
            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            base.Draw(gameTime);
        }
    }
}
