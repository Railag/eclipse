using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Eclipse.Models
{
    public class RecentPostsItem
    {
        public RecentPostsItem()
        {
            this.Text = "Text";
            this.PostUri = new Uri("http://google.com");
            this.CommentsNumber = 33;
        }

        public RecentPostsItem(string Text, Uri PostUri, int CommentsNumber)
        {
            this.Text = Text;
            this.PostUri = PostUri;
            this.CommentsNumber = CommentsNumber;
        }

        public string Text { get; set; }
        public Uri PostUri { get; set; }
        public int CommentsNumber { get; set; }
    }
}
