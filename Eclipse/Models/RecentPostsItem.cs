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
            this.CommentsNumber = 0;
        }

        public RecentPostsItem(string Title, int CommentsNumber)
        {
            this.Title = Title;
            this.CommentsNumber = CommentsNumber;
        }

        public string Title { get; set; }
        public int CommentsNumber { get; set; }
    }
}
