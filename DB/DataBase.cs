using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TweetTrends.Model;

namespace TweetTrends.DB
{
    class DataBase
    {
        #region Properties
        public Tweets TweetsData { get; set; }
        public SentimentsDAO SentimentsData { get; set; }
        public StatePolygonsDAO StatesPolygonsData { get; set; }
        #endregion

        #region Static
        public static string DataPath { get; set; }
        public static Dictionary<string, string> TweetsFiles { get; set; }
        public static string SentimentsFile { get; set; }
        public static string StatesFile { get; set; }

        private static DataBase _instance;
        public static DataBase GetInstance()
        {
            if ( _instance == null ) _instance = new DataBase();
            return _instance;
        }
        #endregion

        #region Constructors
        public DataBase()
        {
            if(string.IsNullOrWhiteSpace(DataPath) ) DataPath = Directory.GetParent(Directory.GetCurrentDirectory()).Parent.FullName + "\\Data";
            if ( TweetsFiles == null )
            {
                TweetsFiles = new Dictionary<string, string>();
                TweetsFiles.Add("family_tweets2014.txt", "Family");
                TweetsFiles.Add("football_tweets2014.txt", "Football");
                TweetsFiles.Add("high_school_tweets2014.txt", "High School");
                TweetsFiles.Add("movie_tweets2014.txt", "Movie");
                TweetsFiles.Add("shopping_tweets2014.txt", "Shopping");
                TweetsFiles.Add("texas_tweets2014.txt", "Texas");
                TweetsFiles.Add("weekend_tweets2014.txt", "Weekend");
            }
            if( SentimentsFile == null )
            {
                SentimentsFile = "sentiments.txt";
            }
            if(StatesFile == null )
            {
                StatesFile = "states.json";
            }

            TweetsData = ReadTweetsData(DataPath, TweetsFiles);
            SentimentsData = ReadSentiments(DataPath, SentimentsFile);
            StatesPolygonsData = ReadStates(DataPath, StatesFile);
        }
        #endregion

        #region Methods
        public Tweets ReadTweetsData(string dataPath, Dictionary<string, string> files)
        {
            if ( files == null || dataPath == null ) return new Tweets();
            Tweets tweets = new Tweets();

            StreamReader reader;
            foreach (KeyValuePair<string, string> fileInfo in files )
            {
                try
                {
                    string fullDataPath = dataPath + "\\" + fileInfo.Key;
                    reader = new StreamReader(fullDataPath);
                    tweets.Names.Add(fileInfo.Value, reader.ReadToEnd());
                    reader.Close();
                }
                catch
                {
                    continue;
                }
            }

            return tweets;
        }
        public SentimentsDAO ReadSentiments(string dataPath, string file)
        {
            if ( dataPath == null || file == null ) return new SentimentsDAO();
            SentimentsDAO sentimentsData = new SentimentsDAO();

            try
            {
                StreamReader reader = new StreamReader(dataPath + "\\" + file);
                string[] sentiments = reader.ReadToEnd().Split("\n".ToCharArray());
                reader.Close();
                foreach ( string sentiment in sentiments )
                {
                    if ( sentiment != "" )
                        sentimentsData.SentimentsList.Add(new Sentiment(sentiment.Split(',')[0], Double.Parse(sentiment.Split(',')[1].Replace('.', ','))));
                }
            }
            catch
            {
                return new SentimentsDAO();
            }

            return sentimentsData;
        }
        public StatePolygonsDAO ReadStates(string dataPath, string file)
        {
            if ( string.IsNullOrWhiteSpace(dataPath) || string.IsNullOrWhiteSpace(file) ) return new StatePolygonsDAO();

            StatesPolygonsData = new StatePolygonsDAO();
            string fullPath = dataPath + "\\" + file;
            try
            {
                string jsonString = new StreamReader(fullPath).ReadToEnd();
                StatesPolygonsData.Polygons = JsonConvert.DeserializeObject<Dictionary<string, Polygon>>(jsonString);
            }
            catch
            {
                return new StatePolygonsDAO();
            }

            StatesPolygonsData.FillPoints();
            return StatesPolygonsData;
        }
        #endregion
    }
}
