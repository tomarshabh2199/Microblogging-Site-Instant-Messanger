using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity.Validation;
using System.Data.Entity.Infrastructure;

namespace Mvc.Models
{
    public class DetailViewModel
    {
        [Required(ErrorMessage = "This is Invalid")]

        [Key]
        public int Id { get; }

        [StringLength(10)]
        public string Firstname { get; set; }

        [StringLength(10)]
        public string Lastname { get; set; }

        [EmailAddress]
        [Required]
        //[Remote("IsEmailExist", "Detail", AdditionalFields = "Id",
        //        ErrorMessage = "Email ID already exists")]
        public string Email { get; set; }

        [Required]
        [MaxLength(10),MinLength(6)]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Required]
        [MaxLength(10), MinLength(6)]
        [DataType(DataType.Password)]
        public string Confirmpassword { get; set; }

        [MaxLength(10)]
        [Phone]
        public string Phone { get; set; }

        [StringLength(8)]
        public string Country { get; set; }

        [Required(ErrorMessage = "Please select file.")]
        //[RegularExpression(@"([a-zA-Z0-9\s_\\.\-:])+(.png|.jpg|.gif)$", ErrorMessage = "Only Image files allowed.")]
        public string Image { get; set; }

    }
    }