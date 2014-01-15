using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Globalization;
using vgcpTowerDefense.GameObjects;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using FuncWorks.XNA.XTiled;

namespace vgcpTowerDefense.Managers
{
   
    //The AssetManager loads Art assets such as sound, songs and sprite and makes them available for use
    public class AssetManager
    {
        public Dictionary<string, SoundEffect> LoadedSounds;
        public Dictionary<string, Song> LoadedSongs;
        public Dictionary<string, Texture2D> LoadedSprites;
        public Dictionary<string, Map> LoadedMaps;

        protected Game Game;

        public AssetManager(Game game)
        {

            LoadedSprites = new Dictionary<string, Texture2D>();
            LoadedSongs = new Dictionary<string, Song>();
            LoadedSounds = new Dictionary<string, SoundEffect>();
            LoadedMaps = new Dictionary<string, Map>();

            Game = game;

        }

      

        public void LoadAllContent()
        {

            DirectoryInfo dir = new DirectoryInfo(Game.Content.RootDirectory);
            if (!dir.Exists)
                throw new DirectoryNotFoundException();

            FileInfo[] files = dir.GetFiles("*.*", SearchOption.AllDirectories);

            CultureInfo CultureInfo = new CultureInfo("en-US");

            foreach (FileInfo file in files)
            {
                string RootPath = Game.Content.RootDirectory;
                string Path = file.DirectoryName.ToString();
                Path = (Path.Split(new string[]{@"Content\"}, 20, StringSplitOptions.RemoveEmptyEntries))[1];

                string Name = file.Name.Split('.')[0].ToString();
                string PathPlusName = Path + @"\" + Name;

                if( (Path.StartsWith("Sounds", false, CultureInfo) )) 
                {
                    LoadSound(Name, PathPlusName);
                }
                else if ( (Path.StartsWith("Songs", false, CultureInfo)))
                {
                    LoadSong(Name, PathPlusName);
                }
                else if ( (Path.StartsWith("Sprites", false, CultureInfo)))
                {
                    LoadSprite(Name, PathPlusName);
                }
                else if ((Path.StartsWith("Maps", false, CultureInfo)))
                {
                    /*The FuncWorks Xtiled content processor creates addition files in a folder for each map it loads, we do NOT want to 
                     * load these files, so we exclude any folders after the Maps directory*/
                    if(!Path.Contains('\\') && !Path.Contains('/') )
                    {
                        LoadMap(Name, PathPlusName);
                    }
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

        public void LoadMap(String MapIdentifer, String mapPath)
        {
            LoadedMaps.Add(MapIdentifer, Game.Content.Load<Map>(mapPath));
        }




    }

}
