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
        public int WayPointNumber;  /*Waypoint order number. The mob will go to each waypoint, in order, forming a path*/
    }
    
    
    public class EnemyMob : DrawableGameObject
    {
        public int Health;
        public List<MobWayPoint> MobPath;
        protected int CurrentWayPoint; /*Which WayPoint the mob is currently headed to. It starts at 1 then heads to the next*/
        protected bool CurrentlyTravelingToWayPoint; /*True if the mob is heading tower a waypoint, false if not. 
* Used so the mob does not have recalculate a direction vector each update cycle*/
        public float Speed; /*The movementspeed of the mob*/
        
        
        public EnemyMob(Texture2D loadedTexture)
                : base(loadedTexture)
        {
            
            Health = 1000;
            Scale = .8f;
            CurrentWayPoint = 1;
            Speed = 3;
            CurrentlyTravelingToWayPoint = false;

            //initializations
            CurrentStatusEffects = new List<Common.status_effect>();
        }


        public EnemyMob()
            : base()
        {

            Health = 100;

            //initializations
            CurrentStatusEffects = new List<Common.status_effect>();
        }




        public void damage_me(int damage)
        {
            Health -= damage;
        }


        protected List<Common.status_effect> CurrentStatusEffects;



        //removes status effects that have timed out
        protected void UpdateStatusEffects(GameTime GameTime)
        {

            // remove each status from the list which effect timer is zero or less
            CurrentStatusEffects.RemoveAll(x => x.StatusEffectTimeMS <= 0);

            //decrement the status effect timers
            foreach (Common.status_effect StatusEffect in CurrentStatusEffects)
            {
                StatusEffect.StatusEffectTimeMS -= GameTime.ElapsedGameTime.Milliseconds;
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


        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
            this.UpdateHealth();
            this.UpdateStatusEffects(gameTime);
            this.UpdatePathing();

            
        }


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
                    bool IsTrue = Util.vgpc_math.DoesRectangleContainVector(debug, MobPath[CurrentWayPoint].Position);
                    if(IsTrue)
                    {
                        this.Velocity = Vector2.Zero;
                        this.CurrentlyTravelingToWayPoint = false;
                        this.CurrentWayPoint++;
                    }

                }
            }
        }

        protected virtual void UpdateHealth()
        {
            if (this.Health <= 0)
            {
                KillThisMob();
            }
        }

        protected virtual void KillThisMob()
        {
            this.IsActive = false;

        }


     

      

    }
}
