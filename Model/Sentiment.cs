using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TweetTrends.Service;

namespace TweetTrends.Model
{
    class Sentiment
    {
        public Sentiment(string text,double value)
        {
            Text = text;
            Value = value;
        }
        public string Text
        {
            get;set;
        }
        public double Value
        {
            get;set;
        }
        
        public bool isThatSentiment(string word)
        {
            return Text.Equals(word);
        }
    }
}
