using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GMap.NET;
using GMap.NET.WindowsPresentation;
using TweetTrands.Utility;

namespace TweetTrends.DB
{
    class StatesPolygons
    {
        private static Dictionary<string, Data> states;
        private static List<GMapPolygonHole> polygons;

        private static StatesPolygons instance;



        private StatesPolygons()
        {
            FillPoints();
            FillPolygons();
        }

        public static StatesPolygons getInstance()
        {
            if (instance == null)
                instance = new StatesPolygons();
            return instance;
        }


        public List<GMapPolygonHole> PolygonsList
        {
            get
            {
                if (polygons == null) FillPoints();
                return polygons;
            }
        }
        public Dictionary<string, Data> Polygons
        {
            get
            {

                if (states == null) FillPolygons();
                return states;
            }
        }

        private void FillPoints()
        {

            List<PointLatLng> points = new List<PointLatLng>();
            polygons = new List<GMapPolygonHole>();
            foreach (string key in Polygons.Keys)
            {
                foreach (List<List<List<double>>> listOfLists in Polygons[key].Polygons)
                {
                    foreach (List<double> list in listOfLists[0])
                    {
                        points.Add(new PointLatLng(list[1], list[0]));
                    }
                    polygons.Add(new GMapPolygonHole(points));
                    points = new List<PointLatLng>();
                }
            }

        }

        private static void FillPolygons()
        {
            string jsonString = new StreamReader(@"D:\универ\4  сем\OOP\TweetTrends\Data\states.json").ReadToEnd();
            states = JsonConvert.DeserializeObject<Dictionary<string, Data>>(jsonString);
        }
    }
}
