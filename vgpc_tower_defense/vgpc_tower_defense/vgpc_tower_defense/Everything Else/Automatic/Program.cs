using System;

namespace vgcpTowerDefense
{
#if WINDOWS || XBOX
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        static void Main(string[] args)
        {
            using (vgcp_tower_defense_game game = new vgcp_tower_defense_game())
            {
                game.Run();
            }
        }
    }
#endif
}

