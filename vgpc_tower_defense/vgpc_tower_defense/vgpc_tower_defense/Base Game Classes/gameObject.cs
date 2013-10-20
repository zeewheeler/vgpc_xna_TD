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
    
    //The DrawableGameObject defines a set of variables and functions that can define a basic game object such as a tower, enemy mob, projectile, etc 
    public class DrawableGameObject
    {

        protected Texture2D TextureCurrent; /*The current texture to be drawn of this object*/
        public Vector2 Position;            /*The position on the viewport in which this object will be drawn. Centered on Top Left of object*/
        public Vector2 Velocity;            /*The magnitude and direction the object moves each update*/
        public bool IsActive;               /*Flag, update with update each update if true, will not update if false*/
        public float Rotation;              /*Describes the rotation to be applied to object's draw*/
        public float AngularVelocity;       /* The magnitude and direction that the object's rotation changes each update*/
        public float Scale;                 /*The amount the object's graphic and hit box are scaled.*/
        public Color Color { get; set; }
  
        //This is the class constructor, notice that is a function that is call the same thing as the
        //class name, which in this game is "GameObject". It is automatically called whenever you create an instance of this class.
        //This mechanism allows us to set up default values of it's variables.

        /// <summary>
        /// Constructor. Initializes the the object to default values the moment it is instantiated.
        /// </summary>
        /// <param name="loadedTexture"></param>
 
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

        /// <summary>
        /// Returns the offset the represents the distance between the center of the object and the "origin" of the object. The origin of the object is the point
        /// which the object rotates about.
        /// </summary>
        /// <returns></returns>
        public Vector2 GetOriginOffset()
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
                (int)(this.Position.X - this.TextureCurrent.Width * this.Scale / 2),
                (int)(this.Position.Y - this.TextureCurrent.Height * this.Scale / 2),
                (int)(this.TextureCurrent.Width * this.Scale),
                (int)(this.TextureCurrent.Height * this.Scale));

            return BoundingRec;

        }

        //draws the game object with default texture
        public virtual void Draw(SpriteBatch spriteBatch)
        {
            if (IsActive)
            {
                //spriteBatch.Draw(CurrentTexture, Position, Color.White);
                spriteBatch.Draw(TextureCurrent, Position, null, Color, Rotation, this.GetOriginOffset(), Scale, SpriteEffects.None, 1);
                    
            }
        }

        //draws game object with specified texture
        public virtual void Draw(SpriteBatch spriteBatch, Texture2D texture)
        {
            if (IsActive)
            {
                //spriteBatch.Draw(texture, this.GetCenter(), Color.White);
                spriteBatch.Draw(texture, Position, null, Color, Rotation, this.GetOriginOffset(), Scale, SpriteEffects.None, 1);
                
            }
        }

  
        //by adding the objects velocity to it's position every update cycle, we can make the object "move".
        public virtual void Update(GameTime gameTime)
        {
            if (IsActive)
            {
                this.Position += Velocity;
                this.Rotation += AngularVelocity;
  
            }
        }

    }
}
