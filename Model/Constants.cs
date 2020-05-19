using GMap.NET;
using System.Windows.Input;

namespace TweetTrends.Model
{
    class Constants
    {
        #region Patterns
        public readonly static string tweetPattern = @"(\[[^\]]+\])(\s_\s[^\[]+)";
        public readonly static string coordinatesPattern = @"(\[[\d.,\s-]+\])";
        public readonly static string datePattern = @"(\d{4}-\d{2}-\d\d\s\d\d:\d\d:\d\d)";
        public readonly static string textPattern = @"(\[[^\]]+\])\s_\s(\d{4}-\d{2}-\d\d\s\d\d:\d\d:\d\d)\s";
        #endregion

        #region MapConsts
        public readonly static bool MaxCanDrop = true;
        public readonly static int MinMapZoom = 2;
        public readonly static int MaxMapZoom = 17;
        public readonly static int MapZoom = 2;
        public readonly static AccessMode MapAccessMode = AccessMode.ServerAndCache;
        public readonly static MouseWheelZoomType MapMouseWheelZoomType = MouseWheelZoomType.MousePositionAndCenter;
        public readonly static MouseButton MapDragButton = MouseButton.Left;
        #endregion

    }
}
