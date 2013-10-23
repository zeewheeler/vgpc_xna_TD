using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace vgcpTowerDefense.GameObjects
{

    public class MobWayPoint
    {
        public Vector2 Position;    /*This position on the map the waypoint points to*/
        public int WayPointNumber;  /*Waypoint number. The mob will go to each waypoint, in order, forming a path*/
    }

    public class MobPathingInfo
    {
        public Vector2 MobSpawnLocation; /*location in which the mob spawns*/
        public List<MobWayPoint> PathWayPoints; /*List of successive mob way points*/
        public Rectangle MobEndZone; /*Bounding box in which a mob "scores" if the reach*/
    }


    /// <summary>
    /// Basic structure to define a signle mob spawn instance. Many of these comprise a "wave"
    /// </summary>
    public struct MobSpawnEntry
    {
        public String MobIdentifier; /*Name of mob to be spawned*/
        public int DelayAfter_ms; /*The amount of delay before another mob can be spawned after this one.
                            Used in the "Timed Mob Queue" mode.*/
        public String PathIdentifer; /*String identifier of the path the mob is to follow*/

    }

    public class MobWave
    {
        public List<MobSpawnEntry> MobSpawnEntries; /*Collection of mobs spawn entries.*/

        public MobWave()
        {
            MobSpawnEntries = new List<MobSpawnEntry>();
        }
    }
    
    
    public class EnemyMob : DrawableGameObject
    {
        public int Health;
        public List<MobWayPoint> MobPath;
        protected int CurrentWayPoint; /*Which WayPoint the mob is currently headed to.*/
        protected bool CurrentlyTravelingToWayPoint; /*True if the mob is heading tower a waypoint, false if not.*/
        public float Speed; /*The movementspeed of the mob*/
        protected List<Common.status_effect> CurrentStatusEffects; /*A list of status effects currently affecting this mob*/

        public Rectangle MobEndZone; /* Bounding box for the "end zone" of the mov. It the mob reaches this area, it "scores"*/
        
        
        /// <summary>
        /// Initializes a new EnemyMob at the moment it is created.
        /// </summary>
        /// <param name="loadedTexture"></param>
        public EnemyMob(Texture2D loadedTexture, String StringIdentifier)
                : base(loadedTexture)
        {
            
            Health = 100;
            Scale = 1.0f;
            CurrentWayPoint = 1;
            Speed = 1;
            CurrentlyTravelingToWayPoint = false;

            //initializations
            CurrentStatusEffects = new List<Common.status_effect>();
            MobEndZone = new Rectangle();
        }


        
        /// <summary>
        /// Resets object to intial values and "spawns" the mob at the given position
        /// </summary>
        /// <param name="position"></param>
        public virtual void Spawn(Vector2 position)
        {
            Health = 100;
            Scale = 1.0f;
            CurrentWayPoint = 1;
            Speed = 1;
            CurrentlyTravelingToWayPoint = false;
            CurrentStatusEffects.Clear();

            this.Position = position;
            this.IsActive = true;
        }

        public virtual void Spawn(MobPathingInfo mobPathInfo)
        {
            this.Spawn(mobPathInfo.MobSpawnLocation);
            this.MobEndZone = mobPathInfo.MobEndZone;
            this.MobPath = mobPathInfo.PathWayPoints;
            

        }


        /// <summary>
        /// Damages this mob by the amount passed in
        /// </summary>
        /// <param name="damage"></param>
        public void damage_me(int damage)
        {
            Health -= damage;
        }






        //removes status effects that have timed out
        protected void UpdateStatusEffects(GameTime GameTime)
        {

            // remove each status from the list which effect timer is zero or less
            CurrentStatusEffects.RemoveAll(x => x.StatusEffectTimeMS <= 0);

            //decrement the status effect timers
            foreach (Common.status_effect StatusEffect in CurrentStatusEffects)
            {
                StatusEffect.StatusEffectTimeMS -= (int)GameTime.ElapsedGameTime.TotalMilliseconds;
            }
            
          
        }


        //takes a list of incomming status effects, adds them to the mobs list of status effects, if not there already
        public void AddStatusEffects(List<Common.status_effect> incomingStatusEffects)
        {
            foreach (Common.status_effect incomingStatusEffect in incomingStatusEffects)
            {
                if(this.CurrentStatusEffects.FindIndex(item => item.StatusEffectName == incomingStatusEffect.StatusEffectName) != -1)
                {
                    //effect already exists, do nothing
                }
                else
                {
                   this.CurrentStatusEffects.Add(new Common.status_effect(incomingStatusEffect.StatusEffectName, incomingStatusEffect.StatusEffectTimeMS) );
                }
            }
        }


        /// <summary>
        /// Updates this mob. 
        /// </summary>
        /// <param name="gameTime"></param>
        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
            this.UpdateHealth();
            this.UpdateStatusEffects(gameTime);
            this.UpdatePathing();

            
        }

        /// <summary>
        /// Keeps mob moving on a path, if a path has been defined.
        /// </summary>
        protected virtual void UpdatePathing()
        {
            if (MobPath != null) /*Check if mob has a path defined*/
            {
                if (!CurrentlyTravelingToWayPoint )
                /*Mob has a path but is not heading towards a WayPoint, if one exists, find next waypoint and set mob velocity towards it*/
                {
                    if (this.CurrentWayPoint - 1 < this.MobPath.Count)
                    {
                        this.Velocity = Util.vgpc_math.CreateTargetUnitVector(this.Position, MobPath[CurrentWayPoint - 1].Position) * this.Speed;
                        this.CurrentlyTravelingToWayPoint = true;
                    }
                }
                else /*Mob is currently heading towards a waypoint. Check if mob has reached waypoint.*/
                {

                    Rectangle debug = this.GetBoundingRectangle();
                    debug.Width += 100;
                    debug.Height += 100;

                    debug.X -= 50;
                    debug.Y -= 50;
                    bool IsTrue = Util.vgpc_math.DoesRectangleContainVector(debug, MobPath[CurrentWayPoint - 1].Position);
                    if(IsTrue)
                    {
                        this.Velocity = Vector2.Zero;
                        this.CurrentlyTravelingToWayPoint = false;
                        this.CurrentWayPoint++;
                    }

                }
            }

            if (MobEndZone != null)
            {
                if (Util.vgpc_math.DoesRectangleContainVector(MobEndZone, Position))
                {
                    throw new Exception("Hey, you should implement the mob score thing");

                }
            }
        }

        /// <summary>
        /// Updates health related factors on mob, if health is zero, kills mob.
        /// </summary>
        protected virtual void UpdateHealth()
        {
            if (this.Health <= 0)
            {
                KillThisMob();
            }
        }


        /// <summary>
        /// Kills this mob.
        /// </summary>
        protected virtual void KillThisMob()
        {
            this.IsActive = false;

        }


     

      

    }
}
