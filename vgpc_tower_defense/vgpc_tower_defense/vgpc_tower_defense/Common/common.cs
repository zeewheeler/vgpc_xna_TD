using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace vgcpTowerDefense.Common
{
    public class status_effect
    {
        public status_effect()
        {
        }

        public status_effect(string Status_name, int Status_effect_time_ms)
        {
            status_effect_name = Status_name;
            status_effect_time_ms = Status_effect_time_ms;
        }
        
        //name of the status effect, ie "slowed", "poisoned", etc
        public string status_effect_name { get; set; }
        //amount of time the status effect will last in miliseconds.
        public int status_effect_time_ms { get; set; }
    }
}
