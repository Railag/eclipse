using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Eclipse.Models
{
    public class AddCommentModel
    {
      //  public readonly ICollection<string> Countries = new Collection<string>() { "Russia", "Belarus", "Ukraine" };

      //  public string Country { get; set; }
        [Required(ErrorMessage="Заполните поле {0}")]
        [Display(Name="Комментарий")]
        public string Text { get; set; }
      //  public int Age { get; set; }
      //  public HttpPostedFileWrapper Avatar { get; set; }

      //  public string UserName { get; set; }
        public UserModel User { get; set; }
    }
}