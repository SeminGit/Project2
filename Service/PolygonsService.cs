using GMap.NET.WindowsPresentation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TweetTrends.DB;

namespace TweetTrends.Service
{
    class PolygonsService
    {
        private static DataBase _database = DataBase.GetInstance();

        public static StatesPolygons Polygons => _database.StatesPolygonsData;
    }
}
