using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Eclipse.Models
{
    public class UserModel
    {
        [Range(1, 120)]
        public int? Age { get; set; }
        [RegularExpression(@"\w+@\w+\.\w+")]
        public string Email { get; set; }
        [Required]
        public string UserName { get; set;  }
    }
}