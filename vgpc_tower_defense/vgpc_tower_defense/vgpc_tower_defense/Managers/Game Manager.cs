using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace vgcpTowerDefense.Managers
{
    /// <summary>
    /// The game manager "glues" together all of the other managers, and provides one interface to manager the game
    /// </summary>
    public class Game_Manager
    {
        public LevelManager LevelManager;
        public AssetManager AssetManager;

        string State;

        public Game_Manager(LevelManager levelManager, AssetManager assetManager)
        {
            this.LevelManager = levelManager;
            this.AssetManager = assetManager;
        }
    }
}
