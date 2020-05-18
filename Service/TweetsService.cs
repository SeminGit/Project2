using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
        private TweetBase twBase;
        private List<Tweet> tweetsList;

        public static TweetsService getInstance()
        {
            if (instance == null)
                instance = new TweetsService();
            return instance;
        }

        public List<Tweet> GetTweets(string name)
        {
           
            tweetsList = new List<Tweet>();
            List<string> tweetsAsString = Parser.GetTweetAsString(twBase.Names[name]);
            Thread[] threads = new Thread[tweetsAsString.Count/20+1];
            for (int i = 0; i < threads.Length; i++)
            {
                int start = (tweetsAsString.Count / 10) * i;
                int end = (tweetsAsString.Count / 10) * (i + 1);
                threads[i] = new Thread(() => { Parsing(tweetsAsString,start, end); });
            }           
            for (int i = 0; i < threads.Length; i++)
            {
                threads[i].Start();
            }

            for (int i = 0; i < threads.Length; i++)
            {
                threads[i].Join();
            }


            this.tweets.Add(name, tweetsList);
            return tweetsList;
        }

        private void Parsing(List<string> tweetsAsString,int start,int end)
        {
            string tweet;
            if (start > tweetsAsString.Count) return;
            for (int i = start; i < end&&i<tweetsAsString.Count; i++)
            {
                string text;
                string buf;
                double c1, c2;
                tweet = tweetsAsString[i];
                text = Parser.GetTweetText(tweet);
                buf = Parser.GetTweetCoordinates(tweet).Replace("[", "");
                buf = buf.Replace("]", "");
                c1 = Double.Parse(buf.Split(',')[0].Replace('.', ','));
                c2 = Double.Parse(buf.Split(',')[1].Replace('.', ','));
                tweetsList.Add(new Tweet(text, c1, c2));
            }
        }

        private TweetsService()
        {
            twBase = TweetBase.getInstance();
        }

        public List<string> Names
        {
            get => twBase.Names.Keys.ToList<string>();
        }

        public Dictionary<string, List<Tweet>> Tweets
        {
            get => tweets;
        }
    }
}
