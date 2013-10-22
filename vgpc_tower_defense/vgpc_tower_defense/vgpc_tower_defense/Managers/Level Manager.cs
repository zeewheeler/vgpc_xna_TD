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
    struct MobSpawnEntry
    {
        String MobIdentifier; /*Name of mob to be spawned*/
        int DelayAfter_ms; /*The amount of delay before another mob can be spawned after this one.
                            Used in the "Timed Mob Queue" mode.*/

    }

    public class MobWave
    {
        List<MobSpawnEntry> MobSpawnEntries; /*Collection of mobs spawn entries.*/
    }






    /// <summary>
    /// Manages a game level, which consists of multiple "waves". The level manager will spawn multiple waves in a 
    /// time series, and the waves will spawn multiple EnemyMobs in a time series.
    /// </summary>
    class LevelManager
    {
        List<String> EnemyMobs; /*A list containing the identifier for each mob type that may be spawned for this level
                                 used to preload resources as well as ensure there is a definition for each supplied
                                 mon identifier*/
        List<MobWave> MobWaves;

        int WaveProgress; /*Denotes which mob is next to be spawned in the current active wave */
        int LevelProgress; /* Denotes which wave is currently active*/

        GameTime Gametime;
        AssetManager AssetManager;
        
        public LevelManager(GameTime gameTime, AssetManager assetManager)
        {
            GameTime = gameTime;
            AssetManager = assetManager;

        }

    }
}
