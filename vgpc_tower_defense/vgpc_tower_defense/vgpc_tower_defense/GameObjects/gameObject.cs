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
        private Texture2D   texture;
        private Vector2     position;
        private float       direction;
        private Vector2     center;
        private Vector2     velocity;
        private bool        is_active;


        //This is the class constructor, notice that is a function that is call the same thing as the
        //class name, which in this game is "GameObject". It is automatically called whenever you create an instance of this class.
        //This mechanism allows us to set up default values of it's variables.
       
        public GameObject(Texture2D loadedTexture)
        {
            direction = 0.0f;
            position = Vector2.Zero;

            texture = loadedTexture;
            center = new Vector2(
                texture.Width / 2, texture.Height / 2);
            velocity = Vector2.Zero;
            is_active = false;
        }

        //draws the game object
        public void Draw(SpriteBatch spriteBatch)
        {
            if (is_active)
            {
                spriteBatch.Draw(texture, position, Color.White);
            }
        }

        //by adding the objects velocity to it's position, we can make the object "move".
        //Note, you don't have to use every variable every time. 

        public void Update_Position()
        {
            if (is_active)
            {
                position.X += velocity.X;
                position.Y += velocity.Y;
            }
        }
        




        //Accessor functions. Private class varibles are not accessible to anything outside the class unless
        //you specificaly allow it. These functions allow outside code to get and/or set class variables. This 
        //might not make sense at first, but the whole idea of a class is to "encapsulate" and "abstract", we want 
        //the class to act like a game object, and not just a pile of variables.
        public float PositionX { get { return position.X; } set { position.X = value; } }
        public float PositionY { get { return position.Y; } set { position.Y = value; } }
        public float VelocityX { get { return velocity.X; } set { velocity.X = value; } }
        public float VelocityY { get { return velocity.Y; } set { velocity.Y = value; } }
        public bool Is_active { get { return is_active; } set { is_active = value; } }
        public Texture2D GetTexture() { return texture; }
        public Vector2 GetPos() { return position; }
        public Vector2 GetVel() { return velocity; }
        public void SetPosition(Vector2 pos) { position = pos; }
        public void SetVelocity(Vector2 vel) { velocity = vel; }
        public Vector2 GetCenter() { return center; }
        public float Direction { get { return direction; } set { direction = value; } }
        public int GetWidth() { return texture.Width; }
        public int GetHeight() { return texture.Height; }
    }
}
