using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace vgcpTowerDefense.Managers
{


    /// <summary>
    /// Manages a game level, which consists of multiple "waves". The level manager will spawn multiple waves in a 
    /// time series, and the waves will spawn multiple EnemyMobs in a time series.
    /// </summary>
    class LevelManager
    {
        public List<String> EnemyMobs; /*A list containing the identifier for each mob type that may be spawned for this level
                                 used to preload resources as well as ensure there is a definition for each supplied
                                 mon identifier*/
        
        public List<GameObjects.MobWave> MobWaves;

        int CurrentMobInWave;   /*Denotes which mob is next to be spawned in the current active wave */
        int CurrentWaveInLevel;  /* Denotes which wave is currently active*/
        public bool IsActive; /*Denotes where a level is actively spawning mobs or not*/

        TimeSpan TimeSinceLastSpawn; /*Time Since last mob Spawned*/
        TimeSpan TimeToNextSpawn;   /*Amount of time that must pass until next spawn*/
        AssetManager AssetManager;
        
        public LevelManager(AssetManager assetManager)
        {
            AssetManager = assetManager;
            Reset();
        }

        public LevelManager(AssetManager assetManager, List<GameObjects.MobWave> mobWaves)
        {
            this.MobWaves = mobWaves;
            AssetManager = assetManager;
            Reset();

        }

        /// <summary>
        /// Resets values to starting state
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
        /// Spawns the next mob in a wave or advances the leve to the next level if an end of wave is reached. 
        /// </summary>
        private void SpawnNextMob()
        {
            String MobIdentifer;
            String MobPathIdentifier;
            

            if (MobWaves.Count > CurrentWaveInLevel - 1) /*Check if wave is in bounds*/
            {
                if ( (CurrentMobInWave - 1) < MobWaves[CurrentWaveInLevel - 1].MobSpawnEntries.Count ) /*Check if mob is in bounds*/
                {
                    MobIdentifer = MobWaves[CurrentWaveInLevel - 1].MobSpawnEntries[CurrentMobInWave - 1].MobIdentifier;
                    MobPathIdentifier = MobWaves[CurrentWaveInLevel - 1].MobSpawnEntries[CurrentMobInWave - 1].PathIdentifer;

                    /*Find an enemy mob that is currently not being used of the appropriate type.
                    * If one is not available, create a new one.*/
                    for (int i = 0; i < globals.Mobs.Count; i++)
                    {
                        if (MobIdentifer == globals.Mobs[i].IdentifierString &&
                            (false == globals.Mobs[i].IsActive))
                        {
                            //Inactive yet instantiated mob of the proper type found, spawn it
                            globals.Mobs[i].Spawn(globals.MobPaths["MobPathIdentifier"]);

                            //Reset the Timer
                            TimeToNextSpawn = TimeSpan.FromMilliseconds(
                                (double)MobWaves[CurrentWaveInLevel - 1].MobSpawnEntries[CurrentMobInWave - 1].DelayAfter_ms);
                            CurrentMobInWave++;
                            return;

                        }
                    }
                    //no available mobs of proper type, we'll have to add a new one
                    GameObjects.EnemyMob NewMob = new GameObjects.EnemyMob(AssetManager.LoadedSprites[MobIdentifer],
                        MobIdentifer);
                    NewMob.Spawn(globals.MobPaths[MobPathIdentifier]);
                    globals.Mobs.Add(NewMob);
                    
                    //Reset the Timer
                    TimeToNextSpawn = TimeSpan.FromMilliseconds(
                               (double)MobWaves[CurrentWaveInLevel - 1].MobSpawnEntries[CurrentMobInWave - 1].DelayAfter_ms);
                    CurrentMobInWave++;
                    return;

                }
                else // Last mob in wave. Advance to Next wave, if there is one
                {
                    CurrentWaveInLevel++; //increment wave
                    CurrentMobInWave = 1; //set current mob to first in new wave
                    IsActive = false; /* Stop the spawning. After each wave, the spawning stops until the player triggers
                                       * the next wave. To start again, set isActive to true. */
                }
            }
            else // no more mobs or waves, level is done!
            {
                throw new Exception("What happens when you finish all mobs and waves?"); //reminder to implement some sort of end of level
            }
        }



    }// End Level Manager Class
}
