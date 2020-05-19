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

    class RegexParser
    {
        public static string tweetPattern = @"\[[^\]]+\])(\s_\s[^\[]+",
                             coordinatesPattern = @"\[[\d.,\s-]+\]",
                             datePattern = @"\d{4}-\d{2}-\d{2}\s\d{2}:\d{2}:\d{2}",
                             textPattern = @"\[[^\]]+\]\s_\s\d{ 4}-\d{2}-\d{2}\s\d{2}:\d{2}:\d{2}\s";
        public static List<String> GetListOfMatchedStrigns(String text, string regexPattern)
        {
            Regex regex = new Regex(regexPattern);
            MatchCollection collection = regex.Matches(text);
            List<string> list = new List<string>();
            foreach(Match tw in collection)
            {
                list.Add(tw.Value);
            }
            return list;
        }
        public static string FindString(string text, string regexPattern)
        {
            Regex regex = new Regex(regexPattern);
            Match match = regex.Match(text);
            return match.Value;
        }
        public static string FindAndDeleteString(string text, string regexPattern)
        {
            Regex regex = new Regex(regexPattern);
            Match match = regex.Match(text);
            return text.Replace(match.Value, "");
        }   
    }
}
