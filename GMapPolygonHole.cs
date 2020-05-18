using GMap.NET;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TweetTrands.Utility
{
    public class GMapPolygonHole : GMap.NET.WindowsPresentation.GMapPolygon
    {
        protected GraphicsPath graphicsPath;
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


        /*/// <summary>
        /// Set the Color for the polygon and its Border
        /// </summary>
        /// <param name="col"></param>
        public void SetPolygonColor(Color colFill, Color colBorder)
        {
            this.Stroke.Color = colBorder;
            ((SolidBrush)this.Fill).Color = colFill;
        }*/

        /*/// <summary>
        /// Update the Graphics path
        /// </summary>
        public virtual void UpdateGraphicsPath()
        {

            if (graphicsPath == null)
            {
                graphicsPath = new GraphicsPath();
            }
            else
            {
                graphicsPath.Reset();
            }
            //Add Main Polygon first:
            Point[] pnts = new Point[LocalPoints.Count];
            for (int i = 0; i < LocalPoints.Count; i++)
            {
                Point p2 = new Point((int)LocalPoints[i].X, (int)LocalPoints[i].Y);
                pnts[pnts.Length - 1 - i] = p2;
            }

            if (pnts.Length > 2)
            {
                graphicsPath.AddPolygon(pnts);

                //Add All Holes:
                foreach (var holeLocalPoints in LstPointsLocalHoles)
                {
                    pnts = new Point[holeLocalPoints.Count];
                    for (int i = 0; i < holeLocalPoints.Count; i++)
                    {
                        Point p2 = new Point((int)holeLocalPoints[i].X, (int)holeLocalPoints[i].Y);
                        pnts[pnts.Length - 1 - i] = p2;
                    }
                    if (pnts.Length > 2)
                    {
                        graphicsPath.AddPolygon(pnts);
                    }
                }
            }
            else if (pnts.Length > 0)
            {
                graphicsPath.AddLines(pnts);
            }
        }*/




        /// <summary>
        /// Add a hole 
        /// </summary>
        /// <param name="holePoly"></param>
        public void AddNormalHole(GMap.NET.WindowsForms.GMapPolygon holePoly)
        {
            this.LstPointHoles.Add(holePoly.Points);
        }


        /// <summary>
        /// Add a hole 
        /// </summary>
        /// <param name="holePoly"></param>
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
