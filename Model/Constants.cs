namespace TweetTrends.Model
{
    class Constants
    {
        public readonly static string tweetPattern = @"(\[[^\]]+\])(\s_\s[^\[]+)";
        public readonly static string coordinatesPattern = @"(\[[\d.,\s-]+\])";
        public readonly static string datePattern = @"(\d{4}-\d{2}-\d\d\s\d\d:\d\d:\d\d)";
        public readonly static string textPattern = @"(\[[^\]]+\])\s_\s(\d{4}-\d{2}-\d\d\s\d\d:\d\d:\d\d)\s";
    }
}
