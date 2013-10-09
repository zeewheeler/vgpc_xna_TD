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
   
    
    class AssetManager
    {
        public GraphicsDeviceManager Graphics;
        public SpriteBatch SpriteBatch;

      
        public Dictionary<string, SoundEffect>   LoadedSounds;
        public Dictionary<string, Song> LoadedSongs;
        public Dictionary<string, Texture2D> LoadedSprites;

        protected Game Game;

        public AssetManager(Game game) 
        {
           
            Graphics = new GraphicsDeviceManager(game);

            Graphics.PreferredBackBufferWidth = 1280;
            Graphics.PreferredBackBufferHeight = 720;


            

            LoadedSprites = new Dictionary<string, Texture2D>();
            LoadedSongs = new Dictionary<string, Song>();
            LoadedSounds = new Dictionary<string, SoundEffect>();
            

            Game = game;
            

            //todo read Prefbufferwidth/height from somewhere
        }

        public void LoadContentFromConfig(List<Config.ContentConfigEntry> configEntries)
        {
            foreach (Config.ContentConfigEntry Entry in configEntries)
            {
                switch (Entry.ContentItemType.ToLower())
                {
                    case globals.ContentConfigSOUNDIdentifier:
                        LoadedSounds.Add(Entry.ContentStringIdentifier, Game.Content.Load<SoundEffect>(Entry.ContentPath));
                        break;

                    case globals.ContentConfigSONGIdentifier:
                        LoadedSongs.Add(Entry.ContentStringIdentifier, Game.Content.Load<Song>(Entry.ContentPath));
                        break;

                    case globals.ContentConfigSPRITEIdentifier:
                        LoadedSprites.Add(Entry.ContentStringIdentifier, Game.Content.Load<Texture2D>(Entry.ContentPath));
                        break;

                    default:
                        throw new Exception("Error in LoadContentFromConfig: Unrecognized content type");
                }
            }
        }

        public void LoadSound(String SoundIdentifer, String soundPath)
        {
            LoadedSounds.Add(SoundIdentifer, Game.Content.Load<SoundEffect>(soundPath));
        }

        public void LoadSong(String SongIdentifer, String songPath)
        {
            LoadedSongs.Add(SongIdentifer, Game.Content.Load<Song>(songPath));
        }

        public void LoadSprite(String SpriteIdentifer, String spritePath)
        {
            LoadedSprites.Add(SpriteIdentifer, Game.Content.Load<Texture2D>(spritePath));
        }

       


       

    }

    class EnemyMobManager : DrawableGameComponent
    {
        public GraphicsDeviceManager Graphics;
        public SpriteBatch spriteBatch;

        public EnemyMobManager(Game game) :
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
