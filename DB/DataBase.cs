using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TweetTrends.DB
{
    class DataBase
    {
        

        [JsonProperty("polygons")]
        public List<List<List<List<double>>>> Polygons { get; set; }
        public Tweets TweetsData { get; set; }

        #region Static
        public static string TweetsDataPath { get; set; }
        public static Dictionary<string, string> Files { get; set; }
        private static DataBase _instance;
        public static DataBase GetInstance()
        {
            if ( _instance == null ) _instance = new DataBase();
            return _instance;
        }
        #endregion

        public DataBase()
        {
            if(string.IsNullOrWhiteSpace(TweetsDataPath)) TweetsDataPath = Directory.GetParent(Directory.GetCurrentDirectory()).Parent.FullName + "\\Data";
            if ( Files == null )
            {
                Files = new Dictionary<string, string>();
                Files.Add("family_tweets2014.txt", "Family");
                Files.Add("football_tweets2014.txt", "Football");
                Files.Add("high_school_tweets2014.txt", "High School");
                Files.Add("movie_tweets2014.txt", "Movie");
                Files.Add("shopping_tweets2014.txt", "Shopping");
                Files.Add("texas_tweets2014.txt", "Texas");
                Files.Add("weekend_tweets2014.txt", "Weekend");
            }

            TweetsData = Tweets.getInstance();
            ReadTweetsData(TweetsDataPath, Files);
        }

        public void ReadTweetsData(string dataPath, Dictionary<string, string> files)
        {
            if ( files == null || dataPath == null ) return;

            StreamReader reader;
            foreach (KeyValuePair<string, string> fileInfo in files )
            {
                try
                {
                    string fullDataPath = dataPath + "\\" + fileInfo.Key;
                    reader = new StreamReader(fullDataPath);
                    TweetsData.Names.Add(fileInfo.Value, reader.ReadToEnd());
                    reader.Close();
                }
                catch
                {
                    continue;
                }
            }
        }
    }
}
