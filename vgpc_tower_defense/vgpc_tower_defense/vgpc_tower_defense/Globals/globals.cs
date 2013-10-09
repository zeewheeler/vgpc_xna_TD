using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace vgpc_tower_defense
{
    public static class globals
    {
        //player
        //cash earned by killing mobs and used for building and upgrading towers
        public static int PlayerCash;
        //Lives are reduced when a mob "gets through" and the game ends when lives is reduced to 0
        public static int PlayerLives;

        //game state
        public static bool IsGamePaused;
        public static bool IsGameOver;


        //config
        //items in config.txt file will be lable with the following prefixes
        public  const string ContentConfigSOUNDIdentifier = "contentSound";
        public  const string ContentConfigSONGIdentifier = "contentSong";
        public  const string ContentConfigSPRITEIdentifier = "contentSprite";

    }

    
}
