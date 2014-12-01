using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Eclipse.Models
{
    public class LoginModel
    {
        public LoginModel()
        {
            IsLogged = false;
            Name = "User";
            ID = -1;
        }

        public LoginModel(bool IsLogged, string Name, int ID)
        {
            this.IsLogged = IsLogged;
            this.Name = Name;
            this.ID = ID;
        }

        public bool IsLogged { get; set; }
        public string Name { get; set; }
        public int ID { get; set; }
    }
}
