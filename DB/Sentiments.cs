using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TweetTrends.Model;

namespace TweetTrends.DB
{
    class Sentiments
    {
        public List<Sentiment> SentimentsList { get; set; }

        public Sentiments()
        {
            SentimentsList = new List<Sentiment>();
        }

        public Double SentimentValue(string word)
        {           
            foreach(Sentiment sentiment in SentimentsList)
            {
                if (sentiment.isThatSentiment(word)) return sentiment.Value;
            }
            return Double.NaN;
        }
    }
}
