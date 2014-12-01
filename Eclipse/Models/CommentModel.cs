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
            Comments = new Collection<AddCommentModel>();
            //Comments.Add(new CommentItemModel());
            First = new CommentItemModel();
        }

        public ICollection<AddCommentModel> Comments { get; set; }
        public CommentItemModel First { get; set; }
    }
}