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
                

            
        }

        GameObjects.Tower ptower;
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

            ptower = new GameObjects.Tower(Content.Load<Texture2D>("Sprites\\Towers\\Plasma\\Plasma_Right"), Content.Load<Texture2D>("Sprites\\Projectiles\\starcharge"));
            ptower.position.X = graphics.GraphicsDevice.Viewport.Width / 10;
            ptower.position.Y = graphics.GraphicsDevice.Viewport.Height / 2;
            ptower.is_active = true;

            badguy = new GameObjects.EnemyMob(Content.Load<Texture2D>("Sprites\\Towers\\Plasma\\Plasma_Right"));
            badguy.position.X = graphics.GraphicsDevice.Viewport.Width / 2;
            badguy.position.Y = graphics.GraphicsDevice.Viewport.Height / 2;
            badguy.is_active = true;
            badguy.velocity.X = -5;

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
        protected override void Update(GameTime gameTime)
        {
            // Allows the game to exit
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed)
                this.Exit();

            // TODO: Add your update logic here

            foreach (GameObjects.EnemyMob badguy in active_badguys)
            {
                badguy.Update_Position();
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

            spriteBatch.Draw(GameObjects.plasma_tower.default_texture, ptower.position, Color.White);
            foreach (GameObjects.EnemyMob badguy in active_badguys)
            {
                badguy.Draw(spriteBatch);
            }
           

            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
