using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Web;

namespace Eclipse.Models
{
    public class CommentModel
    {
        public CommentModel()
        {
            Comments = new Collection<CommentItemModel>();
            //Comments.Add(new CommentItemModel());
        }

        public ICollection<CommentItemModel> Comments { get; set; }
    }
}