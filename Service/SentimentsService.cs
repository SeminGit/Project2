using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TweetTrends.DB;

namespace TweetTrends.Service
{
    class SentimentsService
    {
        private static Sentiments sentiments;

        public static Sentiments Sentiments
        {
            get
            {
                if (sentiments == null) sentiments = TweetTrends.DB.Sentiments.getInstance();
                return sentiments;
            }
            
        } 
    }
}
