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
    public class GameObject
    {
        //Class variables. The class "glues" all of these variables together. 
        public Texture2D default_texture;
        public Vector2 position;
        public float direction;
        public Vector2 center;
        public Vector2 velocity;
        public bool is_active;


        //This is the class constructor, notice that is a function that is call the same thing as the
        //class name, which in this game is "GameObject". It is automatically called whenever you create an instance of this class.
        //This mechanism allows us to set up default values of it's variables.
       
        public GameObject(Texture2D loadedTexture)
        {
            direction = 0.0f;
            position = Vector2.Zero;

            default_texture = loadedTexture;

            position = new Vector2(0,0);
            velocity = new Vector2(0, 0);
            center = new Vector2((default_texture.Width / 2), (default_texture.Height / 2));

                
            
            velocity = Vector2.Zero;
            is_active = false;
        }

        //draws the game object
        public virtual void Draw(SpriteBatch spriteBatch)
        {
            if (is_active)
            {
                spriteBatch.Draw(default_texture, position, Color.White);
            }
        }

        //by adding the objects velocity to it's position every update cycle, we can make the object "move".
     

        public virtual void Update_Position()
        {
            if (is_active)
            {
                position.X += velocity.X;
                position.Y += velocity.Y;
            }
        }

        

        
 
    }
}
