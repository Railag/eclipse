using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Eclipse.Models
{
    public class LoginModel
    {
        public bool IsLogged { get; set; }
        public string Name { get; set; }
        public string Password { get; set; }
    }
}
