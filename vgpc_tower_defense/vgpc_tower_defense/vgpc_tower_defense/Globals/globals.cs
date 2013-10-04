using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace vgpc_tower_defense
{
    class globals
    {
        //player
        //cash earned by killing mobs and used for building and upgrading towers
        public static int player_cash { get; set; }
        //health is reduced when a mob "gets through" and the game ends when health is reduce to 0
        public static int player_health { get; set; }


        //game state
        public static bool is_game_paused { get; set; }
        public static bool is_game_over { get; set; }

    }

    
}
