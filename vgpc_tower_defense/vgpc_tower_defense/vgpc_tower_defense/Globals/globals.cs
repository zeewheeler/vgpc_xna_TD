using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using vgcpTowerDefense.GameObjects;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace vgcpTowerDefense
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
        public  const string ContentConfigSOUNDIdentifier = "content_sound";
        public const string ContentConfigSONGIdentifier = "content_song";
        public const string ContentConfigSPRITEIdentifier = "content_sprite";

        //Lists to hold instantiated units
        public static List<EnemyMob> Mobs;
        public static List<Tower> Towers;

        //display related stuff
        public static Rectangle viewport_rectangle;

        public static List<MobWayPoint> MobPath;
        //Pathing Related Stuff, TODO Move to pathing related class


    }

    
}
