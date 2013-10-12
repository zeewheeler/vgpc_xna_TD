using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace vgpc_tower_defense.GameObjects
{
    public class EnemyMob : DrawableGameObject
    {
        public EnemyMob(Texture2D loadedTexture)
                : base(loadedTexture)
        {
            
            Health = 100;
            Scale = .8f;

            //initializations
            current_status_effects = new List<Common.status_effect>();
        }


        public EnemyMob()
            : base()
        {

            Health = 100;

            //initializations
            current_status_effects = new List<Common.status_effect>();
        }


        protected int Health;

        public void damage_me(int damage)
        {
            Health -= damage;
        }


        protected List<Common.status_effect> current_status_effects;



        //removes status effects that have timed out
        protected void UpdateStatusEffects(GameTime game_time)
        {

            // remove each status from the list which effect timer is zero or less
            current_status_effects.RemoveAll(x => x.status_effect_time_ms <= 0);

            //decrement the status effect timers
            foreach (Common.status_effect status_effect in current_status_effects)
            {
                status_effect.status_effect_time_ms -= game_time.ElapsedGameTime.Milliseconds;
            }
            
          
        }


        //takes a list of incomming status effects, adds them to the mobs list of status effects, if there are not there already
        public void AddStatusEffects(List<Common.status_effect> incomming_status_effects)
        {
            foreach (Common.status_effect inc_status_effect in incomming_status_effects)
            {
                if(this.current_status_effects.FindIndex(item => item.status_effect_name == inc_status_effect.status_effect_name) != -1)
                {
                    //effect already exists, do nothing
                }
                else
                {
                    incomming_status_effects.Add(new Common.status_effect(inc_status_effect.status_effect_name, inc_status_effect.status_effect_time_ms) );
                }
            }
        }

        public virtual void Update(GameTime GameTime)
        {
            this.update_position();
            this.UpdateHealth();
            this.UpdateStatusEffects(GameTime);
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
