using GMap.NET.WindowsPresentation;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using TweetTrands.Utility;
using TweetTrends.DB;
using TweetTrends.Model;
using TweetTrends.Service;


namespace TweetTrends
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private TweetsService service = TweetsService.getInstance();
        private Dictionary<GMapPolygonHole, double> sentimentRate;
        private DataBase _database = DataBase.GetInstance();

        public MainWindow()
        {
            InitializeComponent();
        }

        private void Map_Loaded(object sender, RoutedEventArgs e)
        {
            GMap.NET.GMaps.Instance.Mode = GMap.NET.AccessMode.ServerAndCache;
            Sentiments st = SentimentsService.Sentiments;
            Map.MapProvider = GMap.NET.MapProviders.GoogleMapProvider.Instance;
            Map.MinZoom = 2;
            Map.MaxZoom = 17;
            Map.Zoom = 2;
            Map.MouseWheelZoomType = GMap.NET.MouseWheelZoomType.MousePositionAndCenter;
            Map.CanDragMap = true;
            Map.DragButton = MouseButton.Left;
            sentimentRate = new Dictionary<GMapPolygonHole, double>();
            foreach (GMapPolygonHole pol in _database.StatesPolygonsData.PolygonsList)
            {
                sentimentRate.Add(pol, 0);
            }
            TweetName.ItemsSource = Names;
        }

        public List<string> Names
        {
            get => service.Names;

        }



        private Color StateColor(double sentimentValue)
        {
            sentimentValue *= 7;
            if (sentimentValue > 0)
            {
                //good = yellow
                if (145 + sentimentValue > 255)
                    sentimentValue = 255;
                else sentimentValue += 145;
                return Color.FromArgb(200, 255, (byte)(sentimentValue), 0);
            }
            else if (sentimentValue < 0)
            {
                // bad = red
                if (200 + sentimentValue < 0)
                    sentimentValue = 0;
                else sentimentValue *= 10;
                return Color.FromArgb(200, 255, (byte)(200 + sentimentValue), 0);
            }
            // no info
            return Color.FromArgb(200, 0, 0, 0);
        }

        private void TweetName_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            FillDictionary(TweetName.SelectedItem.ToString());
            foreach (GMapPolygonHole polygon in  _database.StatesPolygonsData.PolygonsList )
            {
                polygon.Shape = new Path();
                Map.RegenerateShape(polygon);
                (polygon.Shape as Path).Fill = new SolidColorBrush(StateColor(sentimentRate[polygon]));
                (polygon.Shape as Path).Stroke = Brushes.DarkBlue;
                (polygon.Shape as Path).StrokeThickness = 1.0;
                Map.Markers.Add(polygon);               
            }
            NullInDictionary();
        }

        private void FillDictionary(string name)
        {
            if (service.Tweets.ContainsKey(name))
            {
                foreach (Tweet tweet in service.Tweets[name])
                {
                    if (tweet == null || tweet.State == null) continue;
                    double value = sentimentRate[tweet.State] + tweet.Sentiment;
                    sentimentRate.Remove(tweet.State);
                    sentimentRate.Add(tweet.State, value);
                }
                return;
            }
            foreach (Tweet tweet in service.GetTweets(name))
            {
                if (tweet==null||tweet.State == null) continue;
                double value = sentimentRate[tweet.State] + tweet.Sentiment;
                sentimentRate.Remove(tweet.State);
                sentimentRate.Add(tweet.State,value);
            }
        }

        private void NullInDictionary()
        {
            foreach(GMapPolygonHole key in sentimentRate.Keys.ToList())
            {
                sentimentRate.Remove(key);
                sentimentRate.Add(key, 0);
            }
        }
    }
}
