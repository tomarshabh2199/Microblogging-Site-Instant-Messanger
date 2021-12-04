using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Mvc.Models 
{
    public class AnalyticsViewModel
    {
        public string MostTrendingHashTags { get; set; }
        public string TotalTweetstoday { get; set; }
        public string MostTweetByPerson {get;set;}
        
    }
}