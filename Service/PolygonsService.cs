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
        private static StatesPolygons polygons;

        public static StatesPolygons Polygons
        {
            get
            {
                if (polygons == null) polygons = TweetTrends.DB.StatesPolygons.getInstance();
                return polygons;
            }

        }
    }
}
