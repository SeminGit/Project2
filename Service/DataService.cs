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

        public static StatePolygonsDAO GetPolygons => _dataBase.StatesPolygonsData;
        public static SentimentsDAO GetSentiments => _dataBase.SentimentsData;
    }
}
