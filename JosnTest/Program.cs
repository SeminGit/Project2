using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;

namespace JosnTest
{
    class Program
    {

        private static Dictionary<string, Data> states;
        static void Main(string[] args)
        {
            string jsonString = new StreamReader(@"D:\универ\4  сем\OOP\TweetTrends\Data\states.json").ReadToEnd();
            states = JsonConvert.DeserializeObject<Dictionary<string, Data>>(jsonString);
        }
    }



    class Data
    {
        [JsonProperty("polygons")]
        public List<List<List<List<double>>>> polygons { get; set; }
    }
}
