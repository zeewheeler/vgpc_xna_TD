using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using vgcpTowerDefense.GameObjects;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace vgcpTowerDefense.Managers
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

      

        public void LoadAllContent()
        {

            DirectoryInfo dir = new DirectoryInfo(Game.Content.RootDirectory);
            if (!dir.Exists)
                throw new DirectoryNotFoundException();

            FileInfo[] files = dir.GetFiles("*.*", SearchOption.AllDirectories);

            foreach (FileInfo file in files)
            {
                string RootPath = Game.Content.RootDirectory;
                string Path = file.DirectoryName.ToString();
                Path = (Path.Split(new string[]{@"Content\"}, 20, StringSplitOptions.RemoveEmptyEntries))[1];

                string Name = file.Name.Split('.')[0].ToString();
                string PathPlusName = Path + @"\" + Name;

                if( (Path.Contains("Song")) && !(Path.Contains("Sprite")) && !(Path.Contains("Sounds")) ) 
                {
                    LoadSound(Name, PathPlusName);
                }
                else if ( (Path.Contains("Sprite")) && !(Path.Contains("Song")) && !(Path.Contains("Sounds")) )
                {
                    LoadSprite(Name, PathPlusName);
                }
                else if ( (Path.Contains("Sounds")) && !(Path.Contains("Song")) && !(Path.Contains("Sprite")) )
                {
                    LoadSound(Name, PathPlusName);
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
