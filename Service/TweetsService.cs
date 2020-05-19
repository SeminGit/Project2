using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using TweetTrends.DB;
using TweetTrends.Model;

namespace TweetTrends.Service
{
    class TweetsService
    {
        private static TweetsService instance;
        private Dictionary<string, List<Tweet>> tweets = new Dictionary<string, List<Tweet>>();
        private Tweets twBase;
        private List<Tweet> tweetsList;

        private DataBase _dataBase;

        public static TweetsService getInstance()
        {
            if (instance == null)
                instance = new TweetsService();
            return instance;
        }
        public List<Tweet> GetTweets(string name)
        {
            tweetsList = new List<Tweet>();
            List<string> allDataTweets = RegexParser.GetListOfMatchedStrings(twBase.Names[name], Constants.tweetPattern);
            Task[] parsingTweetsTasks = new Task[4];
            for (int i = 0; i < parsingTweetsTasks.Length; i++)
            {
                int start = (allDataTweets.Count / 10) * i;
                int end = (allDataTweets.Count / 10) * (i + 1);
                parsingTweetsTasks[i] = new Task(() => { Parsing(allDataTweets, start, end); });
            }
            for (int i = 0; i < parsingTweetsTasks.Length; i++)
            {
                parsingTweetsTasks[i].Start();
            }

            Task.WaitAll(parsingTweetsTasks);
            this.tweets.Add(name, tweetsList);
            return tweetsList;
        }

        private void Parsing(List<string> tweetsAsString,int start,int end)
        {
            string tweet;
            if (start > tweetsAsString.Count) return;
            for (int i = start; i < end&&i<tweetsAsString.Count; i++)
            {
                string text, buf;
                double longitude, latitude;
                tweet = tweetsAsString[i];
                //text = RegexParser.GetTweetText(tweet);
                text = RegexParser.FindAndDeleteString(tweet, Constants.textPattern);
                buf = RegexParser.FindString(tweet,Constants.coordinatesPattern).Replace("[", "");
                buf = buf.Replace("]", "");
                longitude = Double.Parse(buf.Split(',')[0].Replace('.', ','));
                latitude = Double.Parse(buf.Split(',')[1].Replace('.', ','));
                tweetsList.Add(new Tweet(text, longitude, latitude));
            }
        }

        private TweetsService()
        {
            _dataBase = DataBase.GetInstance();
            twBase = _dataBase.TweetsData;
        }

        public List<string> Names => twBase.Names.Keys.ToList<string>();

        public Dictionary<string, List<Tweet>> Tweets => tweets;
    }
}
