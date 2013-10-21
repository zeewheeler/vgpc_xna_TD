using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

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
        List<String> EnemyMobs; /*A list containing the identifier for each mob type that may be spawned for this level*/

    }
}
