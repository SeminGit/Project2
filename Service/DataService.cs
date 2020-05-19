using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TweetTrends.DB;

namespace TweetTrends.Service
{
    class DataService
    {
        private static DataBase _dataBase = DataBase.GetInstance();

        public static StatesPolygons GetPolygons => _dataBase.StatesPolygonsData;
        public static Sentiments GetSentiments => _dataBase.SentimentsData;
    }
}
