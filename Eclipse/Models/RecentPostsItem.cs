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
            this.Title = "Title";
            this.PostUri = new Uri("http://google.com");
            this.CommentsNumber = 33;
        }

        public RecentPostsItem(string Title, Uri PostUri, int CommentsNumber)
        {
            this.Title = Title;
            this.PostUri = PostUri;
            this.CommentsNumber = CommentsNumber;
        }

        public string Title { get; set; }
        public Uri PostUri { get; set; }
        public int CommentsNumber { get; set; }
    }
}
