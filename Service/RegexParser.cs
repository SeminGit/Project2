using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;

namespace TweetTrends.Service
{
    class RegexParser
    {
        public static List<String> GetListOfMatchedStrings(String text, string regexPattern)
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
