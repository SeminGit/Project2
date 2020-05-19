namespace TweetTrends.Model
{
    class Constants
    {
        public readonly static string tweetPattern = @"\[[^\]]+\])(\s_\s[^\[]+",
                                     coordinatesPattern = @"\[[\d.,\s-]+\]",
                                     datePattern = @"\d{4}-\d{2}-\d{2}\s\d{2}:\d{2}:\d{2}",
                                     textPattern = @"\[[^\]]+\]\s_\s\d{ 4}-\d{2}-\d{2}\s\d{2}:\d{2}:\d{2}\s";
    }
}
