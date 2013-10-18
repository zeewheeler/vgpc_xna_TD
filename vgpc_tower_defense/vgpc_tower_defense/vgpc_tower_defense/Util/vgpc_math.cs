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

namespace vgcpTowerDefense.Util
{
    class vgpc_math
    {
        
        //returns a unit vector pointing to target, a unit vector has a length of 1
        public static Vector2 CreateTargetUnitVector(Vector2 position_from, Vector2 position_to)
        {

            Vector2 target_vector = new Vector2();

            target_vector = position_to - position_from;
            target_vector.Normalize();

            return target_vector;
        }


        public class FindNearestMobResult
        {
            public GameObjects.EnemyMob EnemyMob;
            public float Distance;
            
        }

        //returns the position of the closest enemy mob to the given position
        public static List<FindNearestMobResult> FindNearestMob(Vector2 towerCenter, List<GameObjects.EnemyMob> enemyMobs)
        {
            List<FindNearestMobResult> ReturnClass = new List<FindNearestMobResult>();
            //if no enemies in list, return a null via default keyword
            if (enemyMobs.Count == 0)
            {
                return ReturnClass;
            }
             
            float distance;
            float MinDistance = float.MaxValue;


           
            //iterate through each mob, if it has a lower distance that current min, update it to the new closest mob
            foreach(GameObjects.EnemyMob BadGuy in enemyMobs)
            {
                if(BadGuy.IsActive)
                {
                    //utilize the build in vector class do the the math for us
                    if (0 == ReturnClass.Count)
                    {
                        FindNearestMobResult Result = new FindNearestMobResult();
                        ReturnClass.Add(Result);
                    }
                   
                    distance = Vector2.Distance(towerCenter, BadGuy.GetCenter());
                
                    if (distance < MinDistance)
                    {
                        ReturnClass[0].EnemyMob = BadGuy;
                        ReturnClass[0].Distance = distance;
                    }
                }
            }

            return ReturnClass;
            
        }


        //returns the distance between two points
        public static float GetDistanceBetweenTwoVectors(Vector2 PointA, Vector2 PointB)
        {
                Vector2 difference = new Vector2();
                difference = PointB - PointA;

                return (float)difference.Length();
        }

        public static bool DoesRectangleContainVector(Rectangle rectangle, Vector2 vectorPosition)
        {
            Point point = new Point();

            point.X = (int)vectorPosition.X;
            point.Y = (int)vectorPosition.Y;
            return rectangle.Contains(point);

        }

    }
}
