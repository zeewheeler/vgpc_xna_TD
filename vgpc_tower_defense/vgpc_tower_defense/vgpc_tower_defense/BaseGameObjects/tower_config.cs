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
            //config_texture_right        = ""; 
            //config_texture_right_up     = ""; 
            //config_texture_up           = ""; 
            //config_texture_up_left      = ""; 
            //config_texture_left         = ""; 
            //config_texture_left_down    = ""; 
            //config_texture_down         = "";
            //config_texture_down_right   = ""; 
        }
        
        //game objects might want something like this 
        //System.Collections.Generic.Dictionary<string, Texture2D>;

        private string config_texture_default { get; set; }
        //private string config_texture_right         { get; set; }
        //private string config_texture_right_up      { get; set; }
        //private string config_texture_up            { get; set; }
        //private string config_texture_up_left       { get; set; }
        //private string config_texture_left          { get; set; }
        //private string config_texture_left_down     { get; set; }
        //private string config_texture_down          { get; set; }
        //private string config_texture_down_right    { get; set; }

        private string projectile_default { get; set; }
        //private string projectile_1 { get; set; }
        //private string projectile_2 { get; set; }
        //private string projectile_3 { get; set; }


        private string fire_sound { get; set; }
                
    }
}
