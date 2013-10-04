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
    public class EnemyMob : GameObject
    {
        public EnemyMob(Texture2D loadedTexture)
                : base(loadedTexture)
        {
            
            mob_health = 100;

            //initializations
            current_status_effects = new List<Common.status_effect>();
        }

        protected int mob_health;

        public void damage_me(int damage)
        {
            mob_health -= damage;
        }


        protected List<Common.status_effect> current_status_effects;



        //removes status effects that have timed out
        protected void update_status_effects(GameTime game_time)
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
        protected void add_status_effects(List<Common.status_effect> incomming_status_effects)
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

        protected virtual void update_health()
        {
            if (this.mob_health <= 0)
            {
                kill_this_mob();
            }
        }

        protected virtual void kill_this_mob()
        {
            this.is_active = false;

        }

    }
}
