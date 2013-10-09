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
        
        //returns a unit vector pointing to target, a unit vector has a length of 1
        public static Vector2 create_target_unit_vector(Vector2 position_from, Vector2 position_to)
        {

            Vector2 target_vector = new Vector2();

            target_vector = position_to - position_from;
            target_vector.Normalize();

            return target_vector;
        }



        //returns the position of the closest enemy mob to the given position
        public static Vector2 find_nearest_mob(Vector2 tower_center, List<GameObjects.EnemyMob> enemy_mobs)
        {
            //if no enemies in list, return a null via default keyword
            if (enemy_mobs.Count == 0)
            {
                return default(Vector2);
            }
             
            float distance;
            float min_distance = float.MaxValue;
            Vector2 position_of_closest_mob = new Vector2();

            //iterate through each mob, if it has a lower distance that current min, update it to the new closest mob
            foreach(GameObjects.EnemyMob bad_guy in enemy_mobs)
            {
                //utilize the build in vector class do the the math for us
                distance = Vector2.Distance(tower_center, bad_guy.GetCenter());
                
                if (distance < min_distance)
                {
                    position_of_closest_mob = bad_guy.GetCenter();
                }
            }

            return position_of_closest_mob;
        }


        //returns the distance between two points
        public static float get_distance_between(Vector2 point_a, Vector2 point_b)
        {
                Vector2 difference = new Vector2();
                difference = point_b - point_a;

                return (float)difference.Length();
        }

        public static bool does_rectangle_contain(Rectangle rectangle, Vector2 vector_position)
        {
            Point point = new Point();

            point.X = (int)vector_position.X;
            point.Y = (int)vector_position.Y;
            return rectangle.Contains(point);

        }

    }
}
