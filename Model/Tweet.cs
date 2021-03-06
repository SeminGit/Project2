﻿using GMap.NET;
using GMap.NET.WindowsPresentation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using TweetTrands.Utility;
using TweetTrends.Service;

namespace TweetTrends.Model
{
    class Tweet
    {
        private string _text;

        public Tweet(string text,double longitude,double latitude)
        {
            Text = text;
            Longitude = longitude;
            Latitude = latitude;
            SetState();
        }

        public GMapPolygonHole State { get; set; }
        public string Date { get; set; }
        public double Longitude { get; set; }
        public double Latitude { get; set; }
        public double Sentiment { get; set; }
        public string Text
        {
            get => _text;
            set
            {
                _text = value;
                SetSentiment();
            }
        }
       
        private void SetState()
        {            
            foreach(GMapPolygonHole polygon in DataService.GetPolygons.PolygonsList)
            {
                if(polygon.IsPointInsidePolygon(new PointLatLng(Longitude, Latitude),true))
                {
                    State = polygon;
                    return;
                }                
            }
        }
        private void SetSentiment()
        {
            MatchCollection collection = new Regex(@"\w+").Matches(Text);
            Double value;
            Sentiment = 0;
            foreach(Match word in collection)
            {
                value = DataService.GetSentiments.SentimentValue(word.Value);
                if (Double.IsNaN(value)) continue;
                Sentiment += value;
            }
        }
        

    }
}
