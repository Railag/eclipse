﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Eclipse.Models
{
    public class ArticleModel
    {
        public ArticleModel()
        {
            this.Date = DateTime.Now;
            this.Author = "Author";
            this.Text = "Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat. Duis aute irure dolor in reprehenderit in voluptate velit esse cillum dolore eu fugiat nulla pariatur. Excepteur sint occaecat cupidatat non proident, sunt in culpa qui officia deserunt mollit anim id est laborum.";
            this.Title = "Title";
            this.Comments = new CommentModel();
            this.Categories = new string[2];
            this.Categories[0] = "Category1";
            this.Categories[1] = "Category2";
        }

        public ArticleModel(String Author, String Text, String Title, CommentModel Comments, String[] Categories)
        {
            Date = DateTime.Now;
            this.Author = Author;
            this.Text = Text;
            this.Title = Title;
            this.Comments = Comments;
            this.Categories = Categories;
        }

        public DateTime Date { get; set; }
        public String Author { get; set; }
        public String Text { get; set; }
        public String Title { get; set; }
        public CommentModel Comments { get; set; }
        public String[] Categories { get; set; }
        public AddCommentModel NewComment { get; set; }
    }
}