using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TweetTrends.DB
{
    class Polygon
    {
        [JsonProperty("polygons")]
        public List<List<List<List<double>>>> Polygons { get; set; }
    }
}
