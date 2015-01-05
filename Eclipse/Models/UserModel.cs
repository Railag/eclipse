using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Eclipse.Models
{
    public class UserModel
    {
        public UserModel()
        {
            this.UserName = "Name";
            this.Email = "Mail";
            this.Age = 0;
        }

        public UserModel(string UserName, string Email = "", int Age = 0)
        {
            this.UserName = UserName;
            this.Email = Email;
            this.Age = Age;
        }

        [Range(1, 120)]
        public int? Age { get; set; }
        [RegularExpression(@"\w+@\w+\.\w+")]
        public string Email { get; set; }
        [Required]
        public string UserName { get; set;  }
    }
}