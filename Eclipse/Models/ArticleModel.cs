using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Web;

namespace Eclipse.Models
{
    public class ArticleModel
    {
        public ArticleModel()
        {
            Articles = new Collection<ArticleItemModel>();
        }

        public ICollection<ArticleItemModel> Articles { get; set; }
    }
}