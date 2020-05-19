using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace TweetTrends.DB
{
    public class Tweets
    {
        
        public  Dictionary<string,string> Names;

        #region Constructors
        public Tweets()
        {
            Names = new Dictionary<string, string>();
        }
        #endregion
    }
}
