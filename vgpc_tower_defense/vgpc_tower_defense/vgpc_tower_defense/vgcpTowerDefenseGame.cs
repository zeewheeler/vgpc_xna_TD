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

        //GameObjects.Tower ptower;
        //GameObjects.Tower ptower2;
        //GameObjects.Tower ptower3;
        //GameObjects.Tower ptower4;
      
        //GameObjects.EnemyMob badguy;

        

        //List<GameObjects.EnemyMob> active_badguys = new List<GameObjects.EnemyMob>();
        

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            globals.viewport_rectangle = new Rectangle(0, 0,
              graphics.GraphicsDevice.Viewport.Width,
              graphics.GraphicsDevice.Viewport.Height);

           


            AssetManager.LoadContentFromConfig(Config.config_reader.ReadContentConfig());
            
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);


           
            globals.Mobs.Add(new EnemyMob(AssetManager.LoadedSprites["badGuy1"]));


            globals.Mobs[0].Position.X = globals.viewport_rectangle.Center.X;
            globals.Mobs[0].Position.Y = globals.viewport_rectangle.Center.Y;
            globals.Mobs[0].IsActive = true;


            //AssetManager.LoadSprite("test1", @"Sprites\Towers\Plasma\Plasma_Right");
            //AssetManager.LoadContentFromConfig(Config.config_reader.ReadContentConfig());
            //AssetManager.SpriteBatch = new SpriteBatch(this.GraphicsDevice);

            //ptower = new GameObjects.Tower(Content.Load<Texture2D>("Sprites\\Towers\\Plasma\\Plasma_Right"), Content.Load<Texture2D>("Sprites\\Projectiles\\cannonball"));
            //ptower.Position.X = graphics.GraphicsDevice.Viewport.Width / 8;
            //ptower.Position.Y = graphics.GraphicsDevice.Viewport.Height / 10;
            //ptower.IsActive = true;


            //ptower2 = new GameObjects.Tower(Content.Load<Texture2D>("Sprites\\Towers\\Plasma\\Plasma_Right"), Content.Load<Texture2D>("Sprites\\Projectiles\\cannonball"));
            //ptower2.Position.X = graphics.GraphicsDevice.Viewport.Width / 8;
            //ptower2.Position.Y = graphics.GraphicsDevice.Viewport.Height / 6;
            //ptower2.IsActive = true;

            //ptower3 = new GameObjects.Tower(Content.Load<Texture2D>("Sprites\\Towers\\Plasma\\Plasma_Right"), Content.Load<Texture2D>("Sprites\\Projectiles\\cannonball"));
            //ptower3.Position.X = graphics.GraphicsDevice.Viewport.Width / 8;
            //ptower3.Position.Y = graphics.GraphicsDevice.Viewport.Height / 4;
            //ptower3.IsActive = true;

            //ptower4 = new GameObjects.Tower(Content.Load<Texture2D>("Sprites\\Towers\\Plasma\\Plasma_Right"), Content.Load<Texture2D>("Sprites\\Projectiles\\cannonball"));
            //ptower4.Position.X = graphics.GraphicsDevice.Viewport.Width / 8;
            //ptower4.Position.Y = graphics.GraphicsDevice.Viewport.Height / 2;
            //ptower4.IsActive = true;

          

            //badguy = new GameObjects.EnemyMob(Content.Load<Texture2D>("Sprites\\Bad guys\\enemy 2 - 1"));
            //badguy.Position.X = graphics.GraphicsDevice.Viewport.Width - 500;
            //badguy.Position.Y = graphics.GraphicsDevice.Viewport.Height / 1.2f;
            //badguy.IsActive = true;
            //badguy.Velocity.Y = -3f;


            //active_badguys.Add(badguy);


           


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
        protected override void Update(GameTime GameTime)
        {
            // Allows the game to exit
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed)
                this.Exit();


            foreach (EnemyMob Mob in globals.Mobs)
            {
                Mob.Update(GameTime);
              
            }

            foreach (Tower Tower in globals.Towers)
            {
                Tower.Update(GameTime);
               
            }
            // TODO: Add your update logic here

            //foreach (GameObjects.EnemyMob badguy in active_badguys)
            //{
            //    badguy.update_position();


            //    if (!Util.vgpc_math.does_rectangle_contain(Common.display.viewport_rectangle, badguy.Position))
            //    {
            //        badguy.Velocity.Y *= -1f;
            //    }
            //}

            //ptower.update_tower(game_time, active_badguys, Common.display.viewport_rectangle);
            //ptower2.update_tower(game_time, active_badguys, Common.display.viewport_rectangle);
            //ptower3.update_tower(game_time, active_badguys, Common.display.viewport_rectangle);
            //ptower4.update_tower(game_time, active_badguys, Common.display.viewport_rectangle);
         
           
          

            base.Update(GameTime);
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

            //ptower.draw(spriteBatch);
            //ptower2.draw(spriteBatch);
            //ptower3.draw(spriteBatch);
            //ptower4.draw(spriteBatch);
         

            //AssetManager.SpriteBatch.Begin();
            //AssetManager.SpriteBatch.Draw(AssetManager.LoadedSprites["test1"], new Vector2(AssetManager.Graphics.GraphicsDevice.Viewport.Width / 2 ,
            //    AssetManager.Graphics.GraphicsDevice.Viewport.Height /2 ), Color.White);
            //AssetManager.SpriteBatch.End();

            //foreach (GameObjects.EnemyMob badguy in active_badguys)
            //{
            //    badguy.draw(spriteBatch);
            //}
           

            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
