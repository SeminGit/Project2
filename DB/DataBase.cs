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
        public Sentiments SentimentsData { get; set; }
        #endregion

        #region Static
        public static string TweetsDataPath { get; set; }
        public static Dictionary<string, string> TweetsFiles { get; set; }
        public static string SentimentsFile { get; set; }

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
            if(string.IsNullOrWhiteSpace(TweetsDataPath)) TweetsDataPath = Directory.GetParent(Directory.GetCurrentDirectory()).Parent.FullName + "\\Data";
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
            if(SentimentsFile == null )
            {
                SentimentsFile = "sentiments.txt";
            }

            TweetsData = ReadTweetsData(TweetsDataPath, TweetsFiles);
            SentimentsData = ReadSentiments(TweetsDataPath, SentimentsFile);
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
        public Sentiments ReadSentiments(string dataPath, string file)
        {
            if ( dataPath == null || file == null ) return new Sentiments();
            Sentiments sentimentsData = new Sentiments();

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
                return new Sentiments();
            }

            return sentimentsData;
        }
        #endregion
    }
}
