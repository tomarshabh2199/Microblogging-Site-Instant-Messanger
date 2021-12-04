using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Mvc.Models
{
    public class FollowerDetailsView
    {
        public string userImage { get; set; }
        public string Firstname { get; set; }
        public int Id { get; set; }
        public bool isUserFollow { get; set; }
    }
}