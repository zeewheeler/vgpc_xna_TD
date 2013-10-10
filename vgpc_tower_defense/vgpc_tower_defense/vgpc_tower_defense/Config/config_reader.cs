using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;

namespace vgpc_tower_defense.Config
{
    public class ContentConfigEntry
    {
        public string ContentItemType;
        public string ContentStringIdentifier;
        public string ContentPath;

        public ContentConfigEntry(String configItemType, String key, String value)
        {
            ContentItemType = configItemType;
            ContentStringIdentifier = key;
            ContentPath = value;
        }


        public ContentConfigEntry()
        {
        }
    }
    

   
    public static class config_reader
    {
        const string ContentPath = @"\config\content.txt";
        
        //reads the config file and returns a string containing all lines which begin with the contentPrefix parameter
        static public List<ContentConfigEntry> ReadContentConfig()
        {
            string CurDir = Directory.GetCurrentDirectory();
            string Line = "";
            string [] SplitLine;

             List<ContentConfigEntry> ReturnList = new List<ContentConfigEntry>();

            System.IO.StreamReader file = new System.IO.StreamReader(CurDir + @"\config\content.txt");

            while ((Line = file.ReadLine()) != null)
            {

                //remove all whitespace from beginning and end of line
                Line.Trim();

                //ignore line if it starts with a "#" or "//"(Treat line as comment), else read it as a configuration entry. 
                if (!Line.StartsWith("#") && !Line.StartsWith(@"//"))
                {
                    SplitLine = Line.Split(new char[] { ' ', ',' }, StringSplitOptions.RemoveEmptyEntries);

                    
                    //Validate config entry, it should be 3 values: <ConfigItemType> <Key> <Value>
                    if (SplitLine.Length == 3)
                    {
                        ReturnList.Add(new ContentConfigEntry(SplitLine[0], SplitLine[1], SplitLine[2]));

                    }
                   
                }

                   
             
             
            }

            return ReturnList;

        }

               
        }
    
}
