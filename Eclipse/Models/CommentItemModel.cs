using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Eclipse.Models
{
    public class CommentItemModel
    {
        public CommentItemModel()
        {
            this.Author = "Author";
            this.Text = "Text";
            this.Date = DateTime.Now;
        }
        
        public CommentItemModel(DateTime Date, String Author="Author", String Text="Text") 
        {
            this.Author = Author;
            this.Text = Text;
            this.Date = Date;
        }

        public string Author { get; set; }
        public string Text { get; set; }
        public DateTime Date { get; set; }
    }
}
