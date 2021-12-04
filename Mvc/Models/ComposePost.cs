using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Mvc.Models
{
    public class ComposePost
    {
        [Required]
        [Key]
        public int Id { get; set; }


        [Required]
        [StringLength(300)]
        public string Content { get; set; }


        public string Name { get; set; }

        public string userImage { get; set; }

        public string User_id { get; set; }

        public Boolean isUserTweet { get; set; }

        [Required]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:MM/dd/yyyy}")]
        public string Created_date { get; set; }
    }
}