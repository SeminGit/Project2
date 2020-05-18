using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;

namespace TweetTrends.Service
{
    //(\[[^\]]+\])(\s_\s[^\[]+) - Tweet
    //(\[[^\]]+\]) - cordinates
    //(\d{4}-\d{2}-\d\d\s\d\d:\d\d:\d\d) - date
    // (\[[^\]]+\])\s_\s(\d{4}-\d{2}-\d\d\s\d\d:\d\d:\d\d)\s - split to get text

    class Parser
    {
        private static Regex tweetRegex = new Regex(@"(\[[^\]]+\])(\s_\s[^\[]+)");
        private static Regex coordinatesRegex = new Regex(@"(\[[\d.,\s-]+\])");
        private static Regex dateRegex = new Regex(@"(\d{4}-\d{2}-\d\d\s\d\d:\d\d:\d\d)");
        private static Regex textRegex = new Regex(@"(\[[^\]]+\])\s_\s(\d{4}-\d{2}-\d\d\s\d\d:\d\d:\d\d)\s");
        public static List<String> GetTweetAsString(String Tweets)
        {
            MatchCollection collection = tweetRegex.Matches(Tweets);
            List<string> tweets = new List<string>();
            foreach(Match tw in collection)
            {
                tweets.Add(tw.Value);
            }
            return tweets;
        }
        public static string GetTweetCoordinates(string tweet)
        {
            Match match = coordinatesRegex.Match(tweet);
            return match.Value;
        }
        public static string GetTweetDate(string tweet)
        {
            Match match = dateRegex.Match(tweet);
            return match.Value;
        }
        public static string GetTweetText(string tweet)
        {
            Match match = textRegex.Match(tweet);
            return tweet.Replace(match.Value, "");
        }
    }
}
