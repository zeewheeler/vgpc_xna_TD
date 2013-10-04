using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace vgpc_tower_defense.Util
{
    class vgpc_math
    {
        
        //returns a unit vector pointing to target
        public static Vector2 create_target_vector(Vector2 position_from, Vector2 position_to, float projectile_speed)
        {

            Vector2 target_vector = new Vector2();

            //target_vector.X = projectile_speed *
            //             (float)((position_to.X - position_from.X) /
            //             (Math.Sqrt((Math.Pow((double)position_to.X, 2) +
            //             (Math.Pow((double)position_from.X, 2))))));

            target_vector = position_to - position_from;
            target_vector.Normalize();

            //target_vector.Y = projectile_speed *
            //             (float)((position_to.Y - position_from.Y) /
            //             (Math.Sqrt((Math.Pow((double)position_to.Y, 2) +
            //             (Math.Pow((double)position_from.Y, 2))))));


            return target_vector;
        }



        //returns the position of the closest enemy mob to the given position
        public static Vector2 find_nearest_mob(Vector2 tower_position, List<GameObjects.EnemyMob> enemy_mobs)
        {
            //if no enemies in list, return a null via default keyword
            if (enemy_mobs.Count == 0)
            {
                return default(Vector2);
            }
            
            float Xdiff;
            float Ydiff;
            float distance;
            float min_distance = float.MaxValue;

            Vector2 pos_of_closest_mob = new Vector2();
            
            //iterate through each mob, if it has a lower distance that current min, update it to the new closest mob
            foreach(GameObjects.EnemyMob bad_guy in enemy_mobs)
            {
                Xdiff = (bad_guy.position.X - tower_position.X);
                Ydiff = (bad_guy.position.Y - tower_position.Y);
                distance = (float)Math.Sqrt((Math.Pow((double)Xdiff, 2) + Math.Pow((double)Ydiff, 2)));

                if (distance < min_distance)
                {
                    pos_of_closest_mob.X = tower_position.X;
                    pos_of_closest_mob.Y = tower_position.Y;
                }

            }
            return pos_of_closest_mob;
        }


        //returns the distance between two points
        public static float get_distance_between(Vector2 point_a, Vector2 point_b)
        {
                Vector2 difference = new Vector2();
                difference = point_b - point_a;

                return (float)difference.Length();
        }

    }
}
