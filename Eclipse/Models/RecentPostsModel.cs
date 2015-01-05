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
            //RecentPostItems.Add(new RecentPostsItem());
            //RecentPostItems.Add(new RecentPostsItem());
            //RecentPostItems.Add(new RecentPostsItem());
            //RecentPostItems.Add(new RecentPostsItem());
        }

        public ICollection<RecentPostsItem> RecentPostItems { get; set; }
    }
}
