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


namespace vgpc_tower_defense
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class Game1 : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        


        

      
       
        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";

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

            //using (StreamReader file_reader = File.OpenText("towers.json"))
            //{
            //    JsonTextReader json_reader = new JsonTextReader(file_reader);
            //    while (json_reader.Read())
            //    {
            //        if (json_reader.Value != null)
            //            Console.WriteLine("Token: {0}, Value: {1}", json_reader.TokenType, json_reader.Value);
            //        else
            //            Console.WriteLine("Token: {0}", json_reader.TokenType);
            //    }
            //}


            Common.display.viewport_rectangle = new Rectangle(0, 0,
                graphics.GraphicsDevice.Viewport.Width,
                graphics.GraphicsDevice.Viewport.Height);
        }

        GameObjects.Tower ptower;
        GameObjects.Tower ptower2;
        GameObjects.Tower ptower3;
        GameObjects.Tower ptower4;
        GameObjects.Tower ptower5;
        GameObjects.Tower ptower6;
        GameObjects.Tower ptower7;
        GameObjects.Tower ptower8;
        GameObjects.EnemyMob badguy;

        List<GameObjects.EnemyMob> active_badguys = new List<GameObjects.EnemyMob>();
        

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);

            ptower = new GameObjects.Tower(Content.Load<Texture2D>("Sprites\\Towers\\Plasma\\Plasma_Right"), Content.Load<Texture2D>("Sprites\\Projectiles\\cannonball"));
            ptower.position.X = graphics.GraphicsDevice.Viewport.Width / 10;
            ptower.position.Y = graphics.GraphicsDevice.Viewport.Height / 10;
            ptower.is_active = true;


            ptower2 = new GameObjects.Tower(Content.Load<Texture2D>("Sprites\\Towers\\Plasma\\Plasma_Right"), Content.Load<Texture2D>("Sprites\\Projectiles\\cannonball"));
            ptower2.position.X = graphics.GraphicsDevice.Viewport.Width / 8;
            ptower2.position.Y = graphics.GraphicsDevice.Viewport.Height / 8;
            ptower2.is_active = true;

            ptower3 = new GameObjects.Tower(Content.Load<Texture2D>("Sprites\\Towers\\Plasma\\Plasma_Right"), Content.Load<Texture2D>("Sprites\\Projectiles\\cannonball"));
            ptower3.position.X = graphics.GraphicsDevice.Viewport.Width / 7;
            ptower3.position.Y = graphics.GraphicsDevice.Viewport.Height / 6;
            ptower3.is_active = true;

            ptower4 = new GameObjects.Tower(Content.Load<Texture2D>("Sprites\\Towers\\Plasma\\Plasma_Right"), Content.Load<Texture2D>("Sprites\\Projectiles\\cannonball"));
            ptower4.position.X = graphics.GraphicsDevice.Viewport.Width / 6;
            ptower4.position.Y = graphics.GraphicsDevice.Viewport.Height / 5;
            ptower4.is_active = true;

            ptower5 = new GameObjects.Tower(Content.Load<Texture2D>("Sprites\\Towers\\Plasma\\Plasma_Right"), Content.Load<Texture2D>("Sprites\\Projectiles\\cannonball"));
            ptower5.position.X = graphics.GraphicsDevice.Viewport.Width / 7;
            ptower5.position.Y = graphics.GraphicsDevice.Viewport.Height / 4;
            ptower5.is_active = true;

            ptower6 = new GameObjects.Tower(Content.Load<Texture2D>("Sprites\\Towers\\Plasma\\Plasma_Right"), Content.Load<Texture2D>("Sprites\\Projectiles\\cannonball"));
            ptower6.position.X = graphics.GraphicsDevice.Viewport.Width / 10;
            ptower6.position.Y = graphics.GraphicsDevice.Viewport.Height / 3;
            ptower6.is_active = true;

            ptower7 = new GameObjects.Tower(Content.Load<Texture2D>("Sprites\\Towers\\Plasma\\Plasma_Right"), Content.Load<Texture2D>("Sprites\\Projectiles\\cannonball"));
            ptower7.position.X = graphics.GraphicsDevice.Viewport.Width / 10;
            ptower7.position.Y = graphics.GraphicsDevice.Viewport.Height / 2;
            ptower7.is_active = true;

            badguy = new GameObjects.EnemyMob(Content.Load<Texture2D>("Sprites\\Bad guys\\enemy 2 - 1"));
            badguy.position.X = graphics.GraphicsDevice.Viewport.Width  - 500;
            badguy.position.Y = graphics.GraphicsDevice.Viewport.Height / 2;
            badguy.is_active = true;
            badguy.velocity.Y = -2f;
            

            active_badguys.Add(badguy);
               

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
        protected override void Update(GameTime game_time)
        {
            // Allows the game to exit
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed)
                this.Exit();

            // TODO: Add your update logic here

            foreach (GameObjects.EnemyMob badguy in active_badguys)
            {
                badguy.update_position();


                if (!Util.vgpc_math.does_rectangle_contain(Common.display.viewport_rectangle, badguy.position))
                {
                    badguy.velocity.Y *= -1f;
                }
            }

            ptower.update_tower(game_time, active_badguys, Common.display.viewport_rectangle);
            ptower2.update_tower(game_time, active_badguys, Common.display.viewport_rectangle);
            ptower3.update_tower(game_time, active_badguys, Common.display.viewport_rectangle);
            ptower4.update_tower(game_time, active_badguys, Common.display.viewport_rectangle);
            ptower5.update_tower(game_time, active_badguys, Common.display.viewport_rectangle);
            ptower6.update_tower(game_time, active_badguys, Common.display.viewport_rectangle);
            ptower7.update_tower(game_time, active_badguys, Common.display.viewport_rectangle);
           
          

            base.Update(game_time);
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

           
            ptower.draw(spriteBatch);
            ptower2.draw(spriteBatch);
            ptower3.draw(spriteBatch);
            ptower4.draw(spriteBatch);
            ptower5.draw(spriteBatch);
            ptower6.draw(spriteBatch);
            ptower7.draw(spriteBatch);

            foreach (GameObjects.EnemyMob badguy in active_badguys)
            {
                badguy.draw(spriteBatch);
            }
           

            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
