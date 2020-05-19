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
        private Dictionary<string, string> _tweetsDataFiles;
        public static string TweetsDataPath { get; set; }
        private static DataBase _instance;
        public static DataBase GetInstance()
        {
            if ( _instance == null ) _instance = new DataBase();
            return _instance;
        }
        public static bool AddTweetsFile(string fileType, string filePath)
        {
            return true;
        }
        #endregion

        public DataBase()
        {
            if(string.IsNullOrEmpty(TweetsDataPath)) TweetsDataPath = Directory.GetParent(Directory.GetCurrentDirectory()).Parent.FullName + "\\Data";
            TweetsData = Tweets.getInstance();
            ReadTweetsData(TweetsDataPath);
        }

        public bool ReadTweetsData(string dataPath)
        {
            string data = "";
            StreamReader reader;

            reader = new StreamReader(dataPath + "\\family_tweets2014.txt");
            data = reader.ReadToEnd();
            reader.Close();
            TweetsData.Names.Add("Family", data);

            reader = new StreamReader(dataPath + "\\football_tweets2014.txt");
            data = reader.ReadToEnd();
            reader.Close();
            TweetsData.Names.Add("Football", data);

            reader = new StreamReader(dataPath + "\\high_school_tweets2014.txt");
            data = reader.ReadToEnd();
            reader.Close();
            TweetsData.Names.Add("High School", data);

            reader = new StreamReader(dataPath + "\\movie_tweets2014.txt");
            data = reader.ReadToEnd();
            reader.Close();
            TweetsData.Names.Add("Movie", data);

            reader = new StreamReader(dataPath + "\\shopping_tweets2014.txt");
            data = reader.ReadToEnd();
            reader.Close();
            TweetsData.Names.Add("ShoppingTweets", data);

            reader = new StreamReader(dataPath + "\\texas_tweets2014.txt");
            data = reader.ReadToEnd();
            reader.Close();
            TweetsData.Names.Add("Texas", data);

            reader = new StreamReader(dataPath + "\\weekend_tweets2014.txt");
            data = reader.ReadToEnd();
            reader.Close();
            TweetsData.Names.Add("Weekend", data);

            return true;
        }
    }
}
