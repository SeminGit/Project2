using GMap.NET;
using System.Collections.Generic;
using System.Linq;

namespace TweetTrands.Utility
{
    public class GMapPolygonHole : GMap.NET.WindowsPresentation.GMapPolygon
    {
        //protected GraphicsPath graphicsPath;
        /// <summary>
        /// How many holes does the Polygon have
        /// </summary>
        public int NumberOfHoles
        {
            get
            {
                return LstPointHoles.Count;
            }
        }

        /// <summary>
        /// The Minimum latitude
        /// </summary>
        public double MinLat { get; private set; }

        /// <summary>
        /// The Maximum latitude
        /// </summary>
        public double MaxLat { get; private set; }

        /// <summary>
        /// The Minimum longitude
        /// </summary>
        public double MinLng { get; private set; }

        /// <summary>
        /// The Maximum longitude
        /// </summary>
        public double MaxLng { get; private set; }

        /// <summary>
        /// The List with the coordinates of all the Holes
        /// </summary>
        public List<List<PointLatLng>> LstPointHoles = new List<List<PointLatLng>>();


        /// <summary>
        /// The List with the local coordinates of all the Holes
        /// </summary>
        public List<List<GPoint>> LstPointsLocalHoles = new List<List<GPoint>>();


        /// <summary>
        /// Constructor
        /// </summary>
        public GMapPolygonHole(List<GMap.NET.PointLatLng> outerPoints)
            : base(outerPoints)
        {
            CalcBoundingBox();
        }

        /// <summary>
        /// Calculate the Min and Max X and y (lat/Long) values
        /// </summary>
        public void CalcBoundingBox()
        {
            if (this.Points.Count == 0)
            {
                return;
            }
            MinLat = this.Points.Min(x => x.Lat);
            MaxLat = this.Points.Max(x => x.Lat);

            MinLng = this.Points.Min(x => x.Lng);
            MaxLng = this.Points.Max(x => x.Lng);
        }


        public void AddNormalHole(List<PointLatLng> points)
        {
            this.LstPointHoles.Add(points);
        }


        /// <summary>
        /// Returns true if the point is Inside of the Polygon, also considers Holes!
        /// </summary>
        /// <param name="p">Point to check</param>
        /// <returns>True if Point is inside, false otherwise</returns>
        public bool IsPointInsidePolygon(PointLatLng p, bool borderIsPartOfPolygon)
        {
            //Border belongs to Polygon!
            if (borderIsPartOfPolygon && Points.Contains(p))
            {
                return true;
            }

            //Check if point is within the Bounding Box
            if (p.Lng < MinLng || p.Lng > MaxLng || p.Lat < MinLat || p.Lng > MaxLat)
            {
                // Definitely not within the polygon!
                return false;
            }

            //Ray-cast algorithm is here onward
            int k, j = Points.Count - 1;
            bool oddNodes = false; //to check whether number of intersections is odd
            for (k = 0; k < Points.Count; k++)
            {
                //fetch adjacent points of the polygon
                PointLatLng polyK = Points[k];
                PointLatLng polyJ = Points[j];

                //check the intersections
                if (polyJ.Lat < p.Lat && polyK.Lat >= p.Lat || polyK.Lat < p.Lat && polyJ.Lat >= p.Lat)
                {
                    if (polyJ.Lng + (p.Lat - polyJ.Lat) / (polyK.Lat - polyJ.Lat) * (polyK.Lng - polyJ.Lng) < p.Lng)
                    {
                        oddNodes = !oddNodes;
                    }
                }

                j = k;
            }

            //Now Test all Holes:
            foreach (var hole in this.LstPointHoles)
            {
                j = hole.Count - 1;
                for (k = 0; k < hole.Count; k++)
                {
                    //fetch adjacent points of the polygon
                    PointLatLng polyK = hole[k];
                    PointLatLng polyJ = hole[j];

                    //check the intersections
                    if (polyJ.Lat < p.Lat && polyK.Lat >= p.Lat || polyK.Lat < p.Lat && polyJ.Lat >= p.Lat)
                    {
                        if (polyJ.Lng + (p.Lat - polyJ.Lat) / (polyK.Lat - polyJ.Lat) * (polyK.Lng - polyJ.Lng) < p.Lng)
                        {
                            oddNodes = !oddNodes;
                        }
                    }
                    j = k;
                }
            }

            return oddNodes;
        }
    }
}
