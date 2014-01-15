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
using vgcpTowerDefense.GameObjects;

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
        public static List<FindNearestMobResult> FindNearestMob(Vector2 towerCenter, Dictionary<String, List<EnemyMob>> Mobs)
        {
            List<FindNearestMobResult> ReturnVal = new List<FindNearestMobResult>();
           
             
            float distance;
            float MinDistance = float.MaxValue;

            foreach (var mobList in Mobs)
            {
                //for each instantiated mob in each mob type
                foreach (EnemyMob BadGuy in mobList.Value)
                {
                    if (BadGuy.IsActive)
                    {

                        if (0 == ReturnVal.Count)
                        {
                            FindNearestMobResult Result = new FindNearestMobResult();
                            ReturnVal.Add(Result);
                        }
                        //utilize the XNA vector class do the the math for us
                        distance = Vector2.Distance(towerCenter, BadGuy.GetCenter());

                        if (distance < MinDistance)
                        {
                            ReturnVal[0].EnemyMob = BadGuy;
                            ReturnVal[0].Distance = distance;

                            MinDistance = distance;
                        }
                    }
                }
            }

           
            //iterate through each mob, if it has a lower distance that current min, update it to the new closest mob
            //foreach(GameObjects.EnemyMob BadGuy in enemyMobs)
            //{
            //    if(BadGuy.IsActive)
            //    {
                    
            //        if (0 == ReturnVal.Count)
            //        {
            //            FindNearestMobResult Result = new FindNearestMobResult();
            //            ReturnVal.Add(Result);
            //        }
            //        //utilize the XNA vector class do the the math for us
            //        distance = Vector2.Distance(towerCenter, BadGuy.GetCenter());
                
            //        if (distance < MinDistance)
            //        {
            //            ReturnVal[0].EnemyMob = BadGuy;
            //            ReturnVal[0].Distance = distance;

            //            MinDistance = distance;
            //        }
            //    }
            //}

            return ReturnVal;
            
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
