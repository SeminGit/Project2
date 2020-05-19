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
        public static string tweetPattern = @"\[[^\]]+\])(\s_\s[^\[]+",
                             coordinatesPattern = @"\[[\d.,\s-]+\]",
                             datePattern = @"\d{4}-\d{2}-\d{2}\s\d{2}:\d{2}:\d{2}",
                             textPattern = @"\[[^\]]+\]\s_\s\d{ 4}-\d{2}-\d{2}\s\d{2}:\d{2}:\d{2}\s";
        public static List<String> GetTweetAsString(String Tweets, string regexPattern)
        {
            Regex regex = new Regex(regexPattern);
            MatchCollection collection = regex.Matches(Tweets);
            List<string> tweets = new List<string>();
            foreach(Match tw in collection)
            {
                tweets.Add(tw.Value);
            }
            return tweets;
        }
        public static string FindString(string text, string regexPattern)
        {
            Regex regex = new Regex(regexPattern);
            Match match = regex.Match(text);
            return match.Value;
        }
        public static string GetTweetCoordinates(string tweet)
        {
            Regex regex = new Regex(coordinatesPattern);
            Match match = regex.Match(tweet);
            return match.Value;
        }
        public static string GetTweetDate(string tweet)
        {
            Regex regex = new Regex(datePattern);
            Match match = regex.Match(tweet);
            return match.Value;
        }
        public static string GetTweetText(string tweet)
        {
            Regex regex = new Regex(textPattern);
            Match match = regex.Match(tweet);
            return tweet.Replace(match.Value, "");
        }
    }
}
