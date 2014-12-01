using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;

namespace Eclipse.Models
{
    public class RecentPostsModel
    {
        public RecentPostsModel()
        {
            RecentPostItems = new Collection<RecentPostsItem>();
            //RecentPostItems.Add(new RecentPostsItem(new Uri("http://google.com"), 15));
            RecentPostItems.Add(new RecentPostsItem());
            RecentPostItems.Add(new RecentPostsItem());
            RecentPostItems.Add(new RecentPostsItem());
            RecentPostItems.Add(new RecentPostsItem());
            First = new RecentPostsItem();
        }

        public ICollection<RecentPostsItem> RecentPostItems { get; set; }
        public RecentPostsItem First { get; set; }
    }
}
