using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace TweetTrends.DB
{
    class TweetBase
    {
        private static TweetBase instance;
        public  Dictionary<string,string> Names;

        #region
        public string CaliTweets
        {
            get;set;
        }
        public string FamilyTweets
        {
            get;set;
        }
        public string FootballTweets
        {
            get;set;
        }
        public string HighSchoolTweets
        {
            get;set;
        }
        public string MovieTweets
        {
            get;set;
        }
        public string ShoppingTweets
        {
            get;set;
        }
        public string SnowTweets
        {
            get;set;
        }
        public string TexasTweets
        {
            get;set;
        }
        public string WeekendTweets
        {
            get;set;
        }
        #endregion

        private TweetBase()
        {
            FillTweets();
        }

        public static TweetBase getInstance()
        {
            if (instance == null)
                instance = new TweetBase();
            return instance;
        }

        public void FillTweets()
        {
            string rootDirectory = Directory.GetParent(Directory.GetCurrentDirectory()).Parent.FullName;
            Names = new Dictionary<string, string>();
            StreamReader reader;

            //reader = new StreamReader(rootDirectory + "\\Data\\cali_tweets2014.txt");
            //Names.Add("Cali", reader.ReadToEnd());
            //CaliTweets = reader.ReadToEnd();
            //reader.Close();          

            reader = new StreamReader(rootDirectory + "\\Data\\family_tweets2014.txt");
            FamilyTweets = reader.ReadToEnd();
            reader.Close();
            Names.Add("Family",FamilyTweets);

            reader = new StreamReader(rootDirectory + "\\Data\\football_tweets2014.txt");
            FootballTweets = reader.ReadToEnd();
            reader.Close();
            Names.Add("Football",FootballTweets);

            reader = new StreamReader(rootDirectory + "\\Data\\high_school_tweets2014.txt");
            HighSchoolTweets = reader.ReadToEnd();
            reader.Close();
            Names.Add("High School",HighSchoolTweets);

            reader = new StreamReader(rootDirectory + "\\Data\\movie_tweets2014.txt");
            MovieTweets = reader.ReadToEnd();
            reader.Close();
            Names.Add("Movie",MovieTweets);

            reader = new StreamReader(rootDirectory + "\\Data\\shopping_tweets2014.txt");
            ShoppingTweets = reader.ReadToEnd();
            reader.Close();
            Names.Add("ShoppingTweets",ShoppingTweets);

            reader = new StreamReader(rootDirectory + "\\Data\\texas_tweets2014.txt");
            TexasTweets = reader.ReadToEnd();
            reader.Close();
            Names.Add("Texas",TexasTweets);

            reader = new StreamReader(rootDirectory + "\\Data\\weekend_tweets2014.txt");
            WeekendTweets = reader.ReadToEnd();
            reader.Close();
            Names.Add("Weekend",WeekendTweets);
        }
    }
}
