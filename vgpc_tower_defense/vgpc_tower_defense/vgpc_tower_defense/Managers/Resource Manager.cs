using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace vgpc_tower_defense.Managers
{
   
    
    class ResourceManager
    {
        public GraphicsDeviceManager Graphics;
        public SpriteBatch SpriteBatch;

      
        protected Dictionary<string, SoundEffect>   LoadedSounds;
        protected Dictionary<string, Song>          LoadedSongs;
        protected Dictionary<string, Texture2D>     LoadedSprites;

        protected Game Game;

        public ResourceManager(Game game) 
        {
           
            Graphics = new GraphicsDeviceManager(game);
            SpriteBatch = new SpriteBatch(game.GraphicsDevice);
          
            

            LoadedSprites = new Dictionary<string, Texture2D>();
            LoadedSongs = new Dictionary<string, Song>();
            LoadedSounds = new Dictionary<string, SoundEffect>();
            

            Game = game;
            

            //todo read Prefbufferwidth/height from somewhere
        }

        
                
        dsasdas
        public void LoadContentFromConfig(List<Config.ContentConfigEntry> configEntries)
        {
            foreach (Config.ContentConfigEntry Entry in configEntries)
            {
                switch (Entry.ContentItemType.ToLower())
                {
                    case globals.ContentConfigSOUNDIdentifier:


                       
                        break;

                    case globals.ContentConfigSONGIdentifier:
                      
                        LoadedSongs.Add(Entry.ContentStringIdentifier, Game.Content.Load<Song>(Entry.ContentPath));
                        break;

                    case globals.ContentConfigSPRITEIdentifier:
                      
                        break;
                    
                    default: 
                        break;
                }
            }
        }


       

    }

    class EnenmyMobManager : DrawableGameComponent
    {
        public GraphicsDeviceManager Graphics;
        public SpriteBatch spriteBatch;

        public EnenmyMobManager(Game game) :
            base(game)
        {
            Graphics = new GraphicsDeviceManager(game);

            //todo read Prefferbufferwidth/height from somewhere

        }


        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
        }

        public override void Draw(GameTime gameTime)
        {
            base.Draw(gameTime);
        }

    }
}
