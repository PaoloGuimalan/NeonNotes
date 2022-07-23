using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace NeonNotesOnline.Models
{
    public class Register
    {
        [Required]
        public string userName { get; set; }
        [Required]
        public string firstName { get; set; }
        [Required]
        public string lastName { get; set; }
        [Required]
        [RegularExpression(".+\\@.+\\..+", ErrorMessage = "Please enter a valid Email")]
        public string email { get; set; }
        [Required]
        public string password { get; set; }
    }
}