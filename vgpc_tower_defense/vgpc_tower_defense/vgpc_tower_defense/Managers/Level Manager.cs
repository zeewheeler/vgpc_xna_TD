using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using vgcpTowerDefense.GameObjects;
using FuncWorks.XNA.XTiled;


namespace vgcpTowerDefense.Managers
{
    /// <summary>
    ///     Manages a game level, which consists of multiple "waves". The level manager will spawn multiple waves in a
    ///     time series, and the waves will spawn multiple EnemyMobs in a time series.
    /// </summary>
    public class LevelManager
    {
        private readonly AssetManager AssetManager;
        private readonly UnitManager  UnitManager;
        private readonly Game         Game;

        private int CurrentMobInWave; /*Denotes which mob is next to be spawned in the current active wave */
        private int CurrentWaveInLevel; /* Denotes which wave is currently active*/

        public List<String> EnemyMobs;
            /*A list containing the identifier for each mob type that may be spawned for this level
                                 used to preload resources as well as ensure there is a definition for each supplied
                                 mon identifier*/

        public bool IsActive; /*Denotes where a level is actively spawning mobs or not*/
        public Dictionary<String, MobPathingInfo> MobPaths; /* Stores loaded mob paths*/
        public List<MobWave> MobWaves; /*Stores loaded mob waves*/

        private TimeSpan TimeSinceLastSpawn; /*Time Since last mob Spawned*/
        private TimeSpan TimeToNextSpawn; /*Amount of time that must pass until next spawn*/
        
        public Rectangle MapView;
        private Map Map;

        private bool MapLoaded; //true if a map is currently loaded
        private string CurrentMapStringId;

        


      


        public LevelManager(AssetManager assetManager, UnitManager unitManager)
        {
            AssetManager = assetManager;
            UnitManager = unitManager;
            
            MobPaths = new Dictionary<string, MobPathingInfo>();
            MobWaves = new List<MobWave>();
            MapLoaded = false;
            Reset();
        }
        
        //draws the map to screen, if a map is loaded
        public void Draw(SpriteBatch spriteBatch)
        {
            if (MapLoaded && CurrentMapStringId != "")
            {
                AssetManager.LoadedMaps[CurrentMapStringId].Draw(spriteBatch, MapView);
            }

        }
        
        public void LoadMobWavesFromFile(string fileName)
        {
            MobWaves = Config.LevelConfig.GetLevelConfigFromJsonFile(fileName).LevelMobWaves;
            Reset();
        }

       //Reads the objects from a .tmx map and creates dict of mobPath(s)
        public void LoadMap(String mapIdentifier)
        {
            MapLoaded = true;
            CurrentMapStringId = mapIdentifier;

            if (AssetManager.LoadedMaps.ContainsKey(mapIdentifier))
            {
                Map = AssetManager.LoadedMaps[mapIdentifier];
                MobWaves.Clear();
                MobPaths.Clear();
                Reset();

                //add waypoints defined in the .tmx map to asset manager
                var ObjLayerList = Map.ObjectLayers;

                foreach (var ObjLayer in ObjLayerList)
                {
                    Console.WriteLine(ObjLayer.Name);

                    foreach (var mapObject in ObjLayer.MapObjects)
                    {
                        
                        
                        //Console.WriteLine(Obj.Name);
                        if (mapObject.Type.StartsWith("path_", true, null))
                        {
                            //Console.WriteLine(Obj.Type);
                            
                            
                            if (!MobPaths.ContainsKey(mapObject.Type))
                            { 
                                //create new dict entry for path 
                                MobPaths.Add(mapObject.Type, new MobPathingInfo());
                            }
                           
                            if (mapObject.Name.Equals("EndZone", StringComparison.OrdinalIgnoreCase))
                            {
                                MobPaths[mapObject.Type].MobEndZone = mapObject.Bounds;
                            }
                            else if (mapObject.Name.StartsWith("WayPoint", true, null))
                            {
                                var wayPoint = new MobWayPoint{};
                                wayPoint.Position.X = mapObject.Bounds.Center.X;
                                wayPoint.Position.Y = mapObject.Bounds.Center.Y;
                                wayPoint.WayPointNumber = Int32.Parse(mapObject.Properties["WayPoint"].Value);
                                MobPaths[mapObject.Type].PathWayPoints.Add(wayPoint);
                            }
                            else if (mapObject.Name.Equals("Spawn", StringComparison.OrdinalIgnoreCase))
                            {
                                MobPaths[mapObject.Type].MobSpawnLocation.X = mapObject.Bounds.Center.X;
                                MobPaths[mapObject.Type].MobSpawnLocation.Y = mapObject.Bounds.Center.Y;
                            }
                        }
                    }
                }
            }
            else
            {
                throw new Exception("Map: " + mapIdentifier + " does not exist!\n");
            }
        }

        /// <summary>
        ///     Resets values to starting state
        /// </summary>
        public void Reset()
        {
            TimeToNextSpawn = TimeSpan.Zero;
            TimeSinceLastSpawn = TimeSpan.Zero;
            IsActive = false;
            CurrentMobInWave = 1;
            CurrentWaveInLevel = 1;
        }

        public void Update(GameTime gameTime)
        {
            if (IsActive && (MobWaves != null))
            {
                TimeSinceLastSpawn += gameTime.ElapsedGameTime;
                if (TimeSinceLastSpawn > TimeToNextSpawn)
                {
                    TimeSinceLastSpawn = TimeSpan.Zero;
                    SpawnNextMob();
                }
            }
        }


        /// <summary>
        ///     Spawns the next mob in a wave or advances the leve to the next level if an end of wave is reached.
        /// </summary>
        private void SpawnNextMob()
        {
            String MobIdentifer;
            String MobPathIdentifier;


            if (MobWaves.Count > CurrentWaveInLevel - 1) /*Check if wave is in bounds*/
            {
                if ((CurrentMobInWave - 1) < MobWaves[CurrentWaveInLevel - 1].MobSpawnEntries.Count)
                    /*Check if mob is in bounds*/
                {
                    MobIdentifer = MobWaves[CurrentWaveInLevel - 1].MobSpawnEntries[CurrentMobInWave - 1].MobIdentifier;
                    MobPathIdentifier =
                        MobWaves[CurrentWaveInLevel - 1].MobSpawnEntries[CurrentMobInWave - 1].PathIdentifer;

                    /*Find an enemy mob that is currently not being used of the appropriate type.
                    * If one is not available, create a new one.*/
                    
                    //UnitManager.Mobs[MobIdentifer]

                    if (UnitManager.Mobs.ContainsKey(MobIdentifer))
                    {
                        foreach (var mob in UnitManager.Mobs[MobIdentifer])
                        {
                            if (!mob.IsActive)
                            {
                                mob.Spawn(MobPaths[MobPathIdentifier]);

                                //Reset the Timer
                                TimeToNextSpawn = TimeSpan.FromMilliseconds(
                                    MobWaves[CurrentWaveInLevel - 1].MobSpawnEntries[CurrentMobInWave - 1].DelayAfter_ms);
                                CurrentMobInWave++;
                            }
                        }
                    }
                    else
                    {
                        var newMob = new EnemyMob(AssetManager.LoadedSprites[MobIdentifer],
                       MobIdentifer);
                        newMob.Spawn(MobPaths[MobPathIdentifier]);
                        UnitManager.Mobs[MobIdentifer].Add(newMob);
                    }

                    TimeToNextSpawn = TimeSpan.FromMilliseconds(
                               MobWaves[CurrentWaveInLevel - 1].MobSpawnEntries[CurrentMobInWave - 1].DelayAfter_ms);
                    CurrentMobInWave++;
                    return;

                    //for (int i = 0; i < globals.Mobs.Count; i++)
                    //{
                    //    if (MobIdentifer == globals.Mobs[i].IdentifierString &&
                    //        (false == globals.Mobs[i].IsActive))
                    //    {
                    //        //Inactive yet instantiated mob of the proper type found, spawn it
                    //        globals.Mobs[i].Spawn(globals.MobPaths["MobPathIdentifier"]);

                    //        //Reset the Timer
                    //        TimeToNextSpawn = TimeSpan.FromMilliseconds(
                    //            MobWaves[CurrentWaveInLevel - 1].MobSpawnEntries[CurrentMobInWave - 1].DelayAfter_ms);
                    //        CurrentMobInWave++;
                    //        return;
                    //    }
                    //}
                    ////no available mobs of proper type, we'll have to add a new one
                    //var newMob = new EnemyMob(AssetManager.LoadedSprites[MobIdentifer],
                    //    MobIdentifer);
                    //newMob.Spawn(globals.MobPaths[MobPathIdentifier]);
                    //globals.Mobs.Add(newMob);

                    ////Reset the Timer
                    //TimeToNextSpawn = TimeSpan.FromMilliseconds(
                    //    MobWaves[CurrentWaveInLevel - 1].MobSpawnEntries[CurrentMobInWave - 1].DelayAfter_ms);
                    //CurrentMobInWave++;
                }
                CurrentWaveInLevel++; //increment wave
                CurrentMobInWave = 1; //set current mob to first in new wave
                IsActive = false; /* Stop the spawning. After each wave, the spawning stops until the player triggers
                                       * the next wave. To start again, set isActive to true. */
            }
            else // no more mobs or waves, level is done!
            {
                throw new Exception("What happens when you finish all mobs and waves?");
                    //reminder to implement some sort of end of level
            }
        }
    } // End Level Manager Class
}