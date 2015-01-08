using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Web;

namespace Eclipse.Models
{
    public class ArticleModel
    {
        public static int POSTS_PER_PAGE = 10;
        public ArticleModel()
        {
            Articles = new Collection<ArticleItemModel>();
            CurrentLastPage = 0;
        }

        public ICollection<ArticleItemModel> Articles { get; set; }

        public int CurrentLastPage { get; set; } 
        
    }
}