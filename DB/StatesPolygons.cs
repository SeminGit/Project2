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
        public Dictionary<string, Polygon> Polygons { get; set; }
        public List<GMapPolygonHole> PolygonsList { get; set; }

        public void FillPoints()
        {

            List<PointLatLng> points = new List<PointLatLng>();
            PolygonsList = new List<GMapPolygonHole>();
            foreach (string key in Polygons.Keys)
            {
                foreach (List<List<List<double>>> listOfLists in Polygons[key].Polygons)
                {
                    foreach (List<double> list in listOfLists[0])
                    {
                        points.Add(new PointLatLng(list[1], list[0]));
                    }
                    PolygonsList.Add(new GMapPolygonHole(points));
                    points = new List<PointLatLng>();
                }
            }
        }
    }
}
