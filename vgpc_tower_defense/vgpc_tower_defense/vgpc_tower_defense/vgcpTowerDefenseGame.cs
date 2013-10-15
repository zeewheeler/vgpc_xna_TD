using System;
using System.Collections.Generic;
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

using vgpc_tower_defense.GameObjects;
using vgpc_tower_defense.Managers;
 



namespace vgpc_tower_defense
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class vgcp_tower_defense_game : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        Managers.AssetManager AssetManager;
       


       
        public vgcp_tower_defense_game()
        {
            AssetManager = new Managers.AssetManager(this);
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";

            globals.Mobs = new List<GameObjects.EnemyMob>();
            globals.Towers = new List<GameObjects.Tower>();

           

            //define what screen resolution the game should run in
            graphics.PreferredBackBufferWidth = 1280;
            graphics.PreferredBackBufferHeight = 720;
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
            

           //initialize global variables

        }


        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            globals.viewport_rectangle = new Rectangle(0, 0,
              graphics.GraphicsDevice.Viewport.Width,
              graphics.GraphicsDevice.Viewport.Height);


            //AssetManager.LoadContentFromConfig(Config.config_reader.ReadContentConfig());
            AssetManager.LoadAllContent();

            Config.JsonConfigOperations.CreateExampleJsonConfigFile();
            
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);
    
            globals.Mobs.Add( new EnemyMob(AssetManager.LoadedSprites["enemy2Down"]) );
            globals.Mobs[0].Position.X = globals.viewport_rectangle.Width - 100;
            globals.Mobs[0].Position.Y = globals.viewport_rectangle.Center.Y;
            globals.Mobs[0].IsActive = true;
            globals.Mobs[0].Velocity.X = -1f;



            globals.Towers.Add(new Tower(AssetManager.LoadedSprites["PlasmaRight"],
                AssetManager.LoadedSprites["cannonball"]));

            globals.Towers[0].Position.X = globals.viewport_rectangle.Center.X / 3;
            globals.Towers[0].Position.Y = globals.viewport_rectangle.Center.Y;
            globals.Towers[0].IsActive = true;

            globals.Towers.Add(new Tower(AssetManager.LoadedSprites["PlasmaRight"],
               AssetManager.LoadedSprites["cannonball"]));

            globals.Towers[1].Position.X = globals.viewport_rectangle.Center.X / 3;
            globals.Towers[1].Position.Y = globals.viewport_rectangle.Center.Y + 200;
            globals.Towers[1].IsActive = true;

            globals.Towers.Add(new Tower(AssetManager.LoadedSprites["PlasmaRight"],
               AssetManager.LoadedSprites["cannonball"]));

            globals.Towers[2].Position.X = globals.viewport_rectangle.Center.X / 3;
            globals.Towers[2].Position.Y = globals.viewport_rectangle.Center.Y - 200;
            globals.Towers[2].IsActive = true;




            globals.Towers[0].CreateExampleJsonTowerConfigFile();
            


           

            // TODO: use this.Content to load your game content here
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
            // Allows the game to exit
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed)
                this.Exit();

            
            //if(!(Util.vgpc_math.DoesRectangleContainVector(globals.viewport_rectangle, globals.Mobs[0].Position)))
            //{
            //    globals.Mobs[0].Velocity.Y *= -1;
            //}
            
            
            foreach (EnemyMob Mob in globals.Mobs)
            {
                Mob.Update(gameTime);
            }

            if (!globals.Mobs[0].IsActive)
            {
                globals.Mobs[0].Position.X = globals.viewport_rectangle.Width - 100;
                globals.Mobs[0].Position.Y = globals.viewport_rectangle.Center.Y;
                globals.Mobs[0].Health = 100;
                globals.Mobs[0].IsActive = true;
            }

            foreach (Tower Tower in globals.Towers)
            {
                Tower.Update(gameTime);
               
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

            // TODO: Add your drawing code here
            spriteBatch.Begin();

            foreach (EnemyMob Mob in globals.Mobs)
            {
                Mob.draw(spriteBatch);
            }

            foreach (Tower Tower in globals.Towers)
            {
                Tower.draw(spriteBatch);
            }

            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
