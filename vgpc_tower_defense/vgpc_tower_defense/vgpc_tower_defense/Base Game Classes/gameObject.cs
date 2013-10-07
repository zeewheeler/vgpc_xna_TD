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
    
    //The DrawableGameObject defines a set of variables and functions that can define a basic game object such as a tower, enemy mob, projectile, etc 
    public class DrawableGameObject
    {
        
        public Texture2D current_texture;
       
        public  Vector2 position;
        public Vector2 get_center()
        {
            if (current_texture == null)
            {
                throw new Exception("A gameObject tried to do texture operations without a texture defined");
            } 

            return new Vector2(this.position.X + this.current_texture.Width / 2, this.position.Y + this.current_texture.Height / 2);
            
        }
       
        public float direction;
        public Vector2 velocity;
        public bool is_active;

        public float rotation;
        public float scale;
       


        //This is the class constructor, notice that is a function that is call the same thing as the
        //class name, which in this game is "GameObject". It is automatically called whenever you create an instance of this class.
        //This mechanism allows us to set up default values of it's variables.

        public DrawableGameObject(Texture2D loadedTexture)
        {
            direction = 0.0f;
            position = Vector2.Zero;

           

            position = new Vector2(0,0);
            velocity = new Vector2(0, 0);

            if (loadedTexture != null)
            {
                current_texture = loadedTexture;
            }
                
            
            velocity = Vector2.Zero;
            is_active = false;
        }



        //Public Functions. These funcations can be called "outside" the class. They provide an interface with which to interact with the class.


        //draws the game object with default texture
        public virtual void draw(SpriteBatch spriteBatch)
        {
            if (is_active)
            {
                spriteBatch.Draw(current_texture, position, Color.White);
            }
        }

        


        //draws game object with specified texture
        public virtual void draw(SpriteBatch spriteBatch, Texture2D texture)
        {
            if (is_active)
            {
                spriteBatch.Draw(texture, this.get_center(), Color.White);
            }
        }
                


       

        //by adding the objects velocity to it's position every update cycle, we can make the object "move".
        public virtual void update_position()
        {
            if (is_active)
            {
                position.X += velocity.X;
                position.Y += velocity.Y;
            }
        }

       

        

        
 
    }
}
