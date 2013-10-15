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

        protected Texture2D TextureCurrent;
        public Vector2 Position;
        public Vector2 Velocity;
        public bool IsActive;
        public float Rotation;
        public float Scale;
        public Color Color { get; set; }
  
        //This is the class constructor, notice that is a function that is call the same thing as the
        //class name, which in this game is "GameObject". It is automatically called whenever you create an instance of this class.
        //This mechanism allows us to set up default values of it's variables.


        public DrawableGameObject()
        {

            Position = new Vector2(0, 0);
            Velocity = new Vector2(0, 0);
            
            Scale = 1f;
            Rotation = 0f;
            Position = Vector2.Zero;
            Velocity = Vector2.Zero;
            IsActive = false;
            Color = Color.White;
        }
        
        public DrawableGameObject(Texture2D loadedTexture)
        {

            Position = new Vector2(0, 0);
            Velocity = new Vector2(0, 0);
            
            Scale = 1f;
            Rotation = 0f;
            Position = Vector2.Zero;
            Velocity = Vector2.Zero;
            IsActive = false;
            Color = Color.White;
            TextureCurrent = loadedTexture;
        }


        //Public Functions. These funcations can be called "outside" the class. They provide an interface with which to interact with the class.
        public Vector2 GetCenter()
        {
            if (TextureCurrent == null)
            {
                throw new Exception("A gameObject tried to do texture operations without a texture defined");
            }

            return new Vector2(this.Position.X + ((this.TextureCurrent.Width * this.Scale) / 2), 
                this.Position.Y + ((this.TextureCurrent.Width * this.Scale) / 2));
        }

        public Vector2 GetOrigin()
        {
            if (TextureCurrent == null)
            {
                throw new Exception("A gameObject tried to do texture operations without a texture defined");
            }

            return new Vector2( (this.TextureCurrent.Width * this.Scale) / 2, (this.TextureCurrent.Height * this.Scale) / 2);
        }

        public virtual Rectangle GetBoundingRectangle()
        {
            Rectangle BoundingRec = new Rectangle(
                (int)this.Position.X,
                (int)this.Position.Y,
                (int)(this.TextureCurrent.Width * this.Scale),
                (int)(this.TextureCurrent.Height * this.Scale));
            
            return BoundingRec;

        }

        //draws the game object with default texture
        public virtual void draw(SpriteBatch spriteBatch)
        {
            if (IsActive)
            {
                //spriteBatch.Draw(CurrentTexture, Position, Color.White);
                spriteBatch.Draw(TextureCurrent, Position, null, Color, Rotation, this.GetOrigin(), Scale, SpriteEffects.None, 1);
                    
            }
        }

        //draws game object with specified texture
        public virtual void draw(SpriteBatch spriteBatch, Texture2D texture)
        {
            if (IsActive)
            {
                //spriteBatch.Draw(texture, this.GetCenter(), Color.White);
                spriteBatch.Draw(texture, Position, null, Color, Rotation, this.GetOrigin(), Scale, SpriteEffects.None, 1);
                
            }
        }

  
        //by adding the objects velocity to it's position every update cycle, we can make the object "move".
        public virtual void update_position()
        {
            if (IsActive)
            {
                this.Position += Velocity;
                
            }
        }

    }
}
