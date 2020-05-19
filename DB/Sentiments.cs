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
        private static Sentiments instance;
        public List<Sentiment> SentimentsList
        {
            get;set;
        }



        private Sentiments()
        {
            SentimentsList = new List<Sentiment>();
            FillSentimenst();
        }

        public static Sentiments getInstance()
        {
            if (instance == null)
                instance = new Sentiments();
            return instance;
        }

        private void FillSentimenst()
        {
            string rootDirectory = Directory.GetParent(Directory.GetCurrentDirectory()).Parent.FullName;

            StreamReader reader = new StreamReader(rootDirectory + "\\Data\\sentiments.txt");
            string[] sentiments = reader.ReadToEnd().Split("\n".ToCharArray());
            reader.Close();
            foreach(string sentiment in sentiments)
            {
                if(sentiment!="")
                this.SentimentsList.Add(new Sentiment(sentiment.Split(',')[0], Double.Parse(sentiment.Split(',')[1].Replace('.', ','))));
            }
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
