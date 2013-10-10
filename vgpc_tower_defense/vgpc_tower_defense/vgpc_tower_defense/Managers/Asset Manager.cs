using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using vgpc_tower_defense.GameObjects;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace vgpc_tower_defense.Managers
{
   
    //The AssetManager loads Art assets such as sound, songs and sprite and makes them available for use
    public class AssetManager
    {
        public Dictionary<string, SoundEffect> LoadedSounds;
        public Dictionary<string, Song> LoadedSongs;
        public Dictionary<string, Texture2D> LoadedSprites;

        protected Game Game;

        public AssetManager(Game game)
        {

            LoadedSprites = new Dictionary<string, Texture2D>();
            LoadedSongs = new Dictionary<string, Song>();
            LoadedSounds = new Dictionary<string, SoundEffect>();

            Game = game;

        }

        public void LoadContentFromConfig(List<Config.ContentConfigEntry> configEntries)
        {

            Config.ContentConfigEntry TempEntry = new Config.ContentConfigEntry();
            foreach (Config.ContentConfigEntry Entry in configEntries)
            {

                TempEntry.ContentPath = Entry.ContentPath;
                TempEntry.ContentStringIdentifier = Entry.ContentStringIdentifier;
                TempEntry.ContentItemType = Entry.ContentItemType;


                TempEntry.ContentPath = TempEntry.ContentPath.Trim();
                TempEntry.ContentStringIdentifier = TempEntry.ContentStringIdentifier.Trim();
                TempEntry.ContentStringIdentifier = TempEntry.ContentStringIdentifier.Trim();

                switch (TempEntry.ContentItemType.ToLower())
                {
                    case globals.ContentConfigSOUNDIdentifier:
                        LoadedSounds.Add(TempEntry.ContentStringIdentifier, Game.Content.Load<SoundEffect>(TempEntry.ContentPath));
                        break;

                    case globals.ContentConfigSONGIdentifier:
                        LoadedSongs.Add(TempEntry.ContentStringIdentifier, Game.Content.Load<Song>(TempEntry.ContentPath));
                        break;

                    case globals.ContentConfigSPRITEIdentifier:
                        LoadedSprites.Add(TempEntry.ContentStringIdentifier, Game.Content.Load<Texture2D>(TempEntry.ContentPath));
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

}
