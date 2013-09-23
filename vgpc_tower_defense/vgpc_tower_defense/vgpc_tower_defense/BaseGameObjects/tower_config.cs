using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace vgpc_tower_defense.GameObjects
{
    class tower_config : game_object_config
    {
        //default constructor sets all values to proper default values
        tower_config(string tower_type) : base()
        {
            config_texture_default = "";   
            
        }
        
        //game objects might want something like this 
        //System.Collections.Generic.Dictionary<string, Texture2D>;
        private string config_texture_default;
        public string Config_texture_default { get; set; }
       

        private string projectile_default;
        public string Projectile_default { get; set; }
       
        private string sound_fire { get; set; }
        private string sound_upgrade { get; set; }
                
    }
}
