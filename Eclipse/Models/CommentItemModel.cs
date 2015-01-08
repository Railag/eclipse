using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Eclipse.Models
{
    public class CommentItemModel
    {
        public CommentItemModel()
        {
            this.Text = "Text";
            this.User = new UserModel();
            this.Date = DateTime.Now;
        }

        public CommentItemModel(DateTime Date, String Text, int PostID, UserModel User, string PostTitle = "")
        {
            this.User = User;
            this.Text = Text;
            this.PostID = PostID;
            this.Date = Date;
            this.PostTitle = PostTitle;
        }

        public CommentItemModel(String Text, int PostID, UserModel User)
        {
            this.User = User;
            this.Text = Text;
            this.PostID = PostID;
            this.Date = DateTime.Now;
        }

        public CommentItemModel(int CommentID, String Text, int PostID, UserModel User, string PostTitle)
        {
            this.CommentID = CommentID;
            this.User = User;
            this.Text = Text;
            this.PostID = PostID;
            this.Date = DateTime.Now;
            this.PostTitle = PostTitle;
        }

        public int CommentID { get; set; }

        [Required(ErrorMessage="Заполните поле {0}")]
        [Display(Name="Комментарий")]
        public string Text { get; set; }
        public DateTime Date { get; set; }
        public int PostID { get; set; }
        public string PostTitle { get; set; }

        public UserModel User { get; set; }
    }
}