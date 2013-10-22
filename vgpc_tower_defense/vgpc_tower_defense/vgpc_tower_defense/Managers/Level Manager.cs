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
    /// Basic structure to define a signle mob spawn instance. Many of these comprise a "wave"
    /// </summary>
    public struct MobSpawnEntry
    {
        public String MobIdentifier; /*Name of mob to be spawned*/
        public int DelayAfter_ms; /*The amount of delay before another mob can be spawned after this one.
                            Used in the "Timed Mob Queue" mode.*/
        public String PathIdentifer; /*String identifier of the path the mob is to follow*/

    }

    public class MobWave
    {
        public List<MobSpawnEntry> MobSpawnEntries; /*Collection of mobs spawn entries.*/
    }






    /// <summary>
    /// Manages a game level, which consists of multiple "waves". The level manager will spawn multiple waves in a 
    /// time series, and the waves will spawn multiple EnemyMobs in a time series.
    /// </summary>
    class LevelManager
    {
        public List<String> EnemyMobs; /*A list containing the identifier for each mob type that may be spawned for this level
                                 used to preload resources as well as ensure there is a definition for each supplied
                                 mon identifier*/
        
        public List<MobWave> MobWaves;

        int MobMarker;   /*Denotes which mob is next to be spawned in the current active wave */
        int WaveMarker;  /* Denotes which wave is currently active*/
        public bool IsActive; /*Denotes where a level is actively spawning mobs or not*/

        TimeSpan TimeSinceLastSpawn; /*Time Since last mob Spawned*/
        TimeSpan TimeToNextSpawn;   /*Amount of time that must pass until next spawn*/
        AssetManager AssetManager;
        
        public LevelManager(AssetManager assetManager)
        {
            AssetManager = assetManager;
            Reset();
        }

        /// <summary>
        /// Resets values to starting state
        /// </summary>
        public void Reset()
        {
            TimeSinceLastSpawn = TimeSpan.Zero;
            IsActive = false;
            MobMarker = 1;
            WaveMarker = 1;
        }

        public void Update(GameTime gameTime)
        {
            if (IsActive && (MobWaves != null))
            {
                TimeSinceLastSpawn.Add( gameTime.ElapsedGameTime);

                if (TimeSinceLastSpawn > TimeToNextSpawn)
                {
                    TimeSinceLastSpawn = TimeSpan.Zero;
                    SpawnNextMob();
                }
            }

        }


        private void SpawnNextMob()
        {
            String MobIdentifer;
            String MobPathIdentifier;

            
            if (MobWaves.Count > WaveMarker) /*Check if wave is in bounds*/
            {
                if (MobWaves[WaveMarker - 1].MobSpawnEntries.Count > /*Check if mob is in bounds*/
                    MobMarker)
                {
                    MobIdentifer = MobWaves[WaveMarker - 1].MobSpawnEntries[MobMarker].MobIdentifier;
                    MobPathIdentifier = MobWaves[WaveMarker - 1].MobSpawnEntries[MobMarker].PathIdentifer;
                    
                    /*Find an enemy mob that is currently not being used of the appropriate type.
                    * If one is not available, create a new one.*/
                    for (int i = 0; i < globals.Mobs.Count; i++)
                    {
                        if(MobIdentifer == globals.Mobs[i].IdentifierString && 
                            (false == globals.Mobs[i].IsActive) )
                        {
                            //Inactive yet instantiated mob of the proper type found, spawn it
                            globals.Mobs[i].Spawn(globals.MobPaths["MobPathIdentifier"].MobSpawnLocation,
                                );

                        }
                    }
                }
            }
        }



    }
}
