using System;
using System.Collections.Generic;
using System.Collections;
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
        Managers.AssetManager AssetManager;
        Managers.LevelManager LevelManager;
        Managers.Game_Manager GameManager;


        Rectangle mapView;
        Map map;

       

        Util.DrawRectangle RectangleDrawer;

        MouseState PreviousMouseState;
        KeyboardState PreviousKeyboardState;


        int MobDeathCounter = 0;

        Random random;




       
        public vgcp_tower_defense_game()
        {
            //AssetManager = new Managers.AssetManager(this);
            Config.LevelConfig.WriteExampleJsonLevelConfig();
            //LevelManager = new Managers.LevelManager(AssetManager,
            //    Config.LevelConfig.GetLevelConfigFromJsonFile("Example_Json_Level_Definition.txt").LevelMobWaves);

            //LevelManager = new Managers.LevelManager(this);
            //LevelManager.IsActive = true;
            GameManager = new Game_Manager(this);


            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";

            //globals.Mobs = new List<GameObjects.EnemyMob>();
            //globals.Towers = new List<GameObjects.Tower>();
            //globals.MobPath = new List<MobWayPoint>();

            RectangleDrawer = new Util.DrawRectangle(this);
            RectangleDrawer.Visible = false;



            Components.Add(RectangleDrawer);

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

            mapView = graphics.GraphicsDevice.Viewport.Bounds;

            //Load All content that is added to content project. This MUST be called before you try to use any content.
            
            AssetManager.LoadAllContent();

            map = AssetManager.LoadedMaps["Map2"];
           // map.Orientation = MapOrientation.Isometric;

            //Config.JsonConfigOperations.CreateExampleJsonConfigFile();
            //Config.TowerConfig.WriteExampleJsonTowerConfig();
            
            
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);





            random = new Random();

          


            IEnumerable<MapObject> MapObjects = map.GetObjectsInRegion(mapView);

            List<MobWayPoint> MobPathWayPoints = new List<MobWayPoint>();
            Vector2 MobSpawn = new Vector2();
            Rectangle EndZoneRect = new Rectangle();

            foreach (MapObject MapObj in MapObjects)
            {

               if(MapObj.Properties.ContainsKey("WayPoint"))
                {
                   MobWayPoint wayPoint = new MobWayPoint();

                   wayPoint.Position.X = MapObj.Bounds.Center.X;
                   wayPoint.Position.Y = MapObj.Bounds.Center.Y;
                   wayPoint.WayPointNumber = Int32.Parse(MapObj.Properties["WayPoint"].Value);
                   MobPathWayPoints.Add(wayPoint);
                }

               if (MapObj.Properties.ContainsKey("spawn"))
               {
                   MobSpawn = new Vector2();
                   MobSpawn.X = MapObj.Bounds.Center.X;
                   MobSpawn.Y = MapObj.Bounds.Center.Y;
               }

               if (MapObj.Properties.ContainsKey("EndZone"))
               {
                   EndZoneRect = new Rectangle(
                       MapObj.Bounds.X,
                       MapObj.Bounds.Y,
                       MapObj.Bounds.Width,
                       MapObj.Bounds.Height);
               }
            }

            MobPathingInfo MobPath = new MobPathingInfo();
            MobPath.PathWayPoints = MobPathWayPoints;
            MobPath.MobSpawnLocation = MobSpawn;
            MobPath.MobEndZone = EndZoneRect;
            globals.MobPaths.Add("Path1", MobPath);



            //globals.Mobs.Add(new EnemyMob(AssetManager.LoadedSprites["EvilRobotRight"],
            //    "EvilRobotRight"));
            //globals.Mobs[0].Position.X = MobSpawn.X - 200;
            //globals.Mobs[0].Position.Y = MobSpawn.Y;
            //globals.Mobs[0].IsActive = true;
            //globals.Mobs[0].MobPath = globals.MobPath;



            /*globals.Towers.Add(new Tower(AssetManager.LoadedSprites["PlasmaRight"],
                AssetManager.LoadedSprites["cannonball"]));*/

            //globals.Towers.Add(new Tower("Example_Json_Tower_Definition.txt", AssetManager));

            //globals.Towers[0].Position.X = globals.viewport_rectangle.Center.X / 3;
            //globals.Towers[0].Position.Y = globals.viewport_rectangle.Center.Y;
            //globals.Towers[0].IsActive = true;

            //globals.Towers.Add(new Tower(AssetManager.LoadedSprites["Ninja"],
            //   AssetManager.LoadedSprites["rocket"]));

            //globals.Towers[1].Position.X = globals.viewport_rectangle.Center.X / 3;
            //globals.Towers[1].Position.Y = globals.viewport_rectangle.Center.Y + 200;
            //globals.Towers[1].IsActive = true;

            //globals.Towers.Add(new Tower(AssetManager.LoadedSprites["Ninja"],
            //   AssetManager.LoadedSprites["starcharge"]));

            //globals.Towers[2].Position.X = globals.viewport_rectangle.Center.X / 3;
            //globals.Towers[2].Position.Y = globals.viewport_rectangle.Center.Y - 200;
            //globals.Towers[2].IsActive = true;

            //foreach (Projectile projectile in globals.Towers[2].Projectiles)
            //{
            //    projectile.AngularVelocity = .1f;
            //}


            

            //Debug: Visualize the enemies bounding rectangle
            //RectangleDrawer.Visible = true;
            RectangleDrawer.SetColor(Color.Red);


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



            KeyboardState KeyboardState = Keyboard.GetState();

            if (KeyboardState.IsKeyDown(Keys.Space))
            {
                LevelManager.IsActive = true;
            }
     

            var MouseState = Mouse.GetState();
            var MousePosition = new Vector2(MouseState.X, MouseState.Y);

            if (MouseState.LeftButton == ButtonState.Pressed && !(PreviousMouseState.LeftButton == ButtonState.Pressed))
            {

                Tower newTower = new Tower(AssetManager.LoadedSprites["Laser001"],
            AssetManager.LoadedSprites["LaserBlue"]);
                //Tower newTower = new Tower("laser_tower.txt", AssetManager);

                globals.Towers.Add(newTower);
             
               

                globals.Towers[globals.Towers.Count - 1].Position.X = MousePosition.X;
                globals.Towers[globals.Towers.Count - 1].Position.Y = MousePosition.Y;
                globals.Towers[globals.Towers.Count - 1].IsActive = true;
            }

            if (MouseState.RightButton == ButtonState.Pressed && !(PreviousMouseState.RightButton == ButtonState.Pressed))
            {

                globals.Towers.Add(new Tower(AssetManager.LoadedSprites["Ninja"],
    AssetManager.LoadedSprites["LaserRed"]));



                globals.Towers[globals.Towers.Count - 1].Position.X = MousePosition.X;
                globals.Towers[globals.Towers.Count - 1].Position.Y = MousePosition.Y;
                globals.Towers[globals.Towers.Count - 1].IsActive = true;
            }



          
            
            foreach (EnemyMob Mob in globals.Mobs)
            {
                Mob.Update(gameTime);
            }


            //for (int i = 0; i < globals.Mobs.Count; i++)
            //{
            //    if (!globals.Mobs[i].IsActive)
            //    {
            //        globals.Mobs[i].Spawn(MobSpawn);
            //        MobDeathCounter++;

            //        if ((MobDeathCounter % 2 == 0) && (globals.Mobs.Count < 100 ) )
            //        {
            //            globals.Mobs.Add(new EnemyMob(AssetManager.LoadedSprites["EvilRobotRight"],
            //                "EvilRobotRight"));
            //            globals.Mobs[globals.Mobs.Count - 1].Spawn(MobSpawn);
            //            globals.Mobs[globals.Mobs.Count - 1].MobPath = globals.MobPath;
            //        }
            //    }

                
            //}

            if (MobDeathCounter % 10 == 0)
            {
            }
           

            foreach (Tower Tower in globals.Towers)
            {
                Tower.Update(gameTime);
               
            }


            PreviousMouseState = MouseState;
            PreviousKeyboardState = KeyboardState;

            LevelManager.Update(gameTime);
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

            //draw background
            map.Draw(spriteBatch, mapView);
 
            
            //Draw Units
            foreach (EnemyMob Mob in globals.Mobs)
            {
                Mob.Draw(spriteBatch);
            }

            foreach (Tower Tower in globals.Towers)
            {
                Tower.Draw(spriteBatch);
            }

            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
