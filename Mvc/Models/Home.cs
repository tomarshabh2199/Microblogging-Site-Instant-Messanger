using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Web;

namespace Mvc.Models
{
    public class Home
    {
        public string userImage { get; set; }
        public int totalFollowerCount { get; set; }
        public int totalFollowingCount { get; set; }
        public List<ComposePost> ComposePosts { get; set; }
    }
}