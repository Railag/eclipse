using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace Eclipse.Models
{
    public class Db
    {
        public ArticleItemModel GetPostByTitle(string title)
        {
            using (var connection = new SqlConnection("Data Source=FIRRAEL\\SQLEXPRESS; Initial Catalog=Blog; Integrated Security=True"))
            {//ConfigurationManager.ConnectionStrings["mssql"].ConnectionString)) {
                connection.Open();
                using (var command = new SqlCommand(String.Format("SELECT Post.PostID, UserInfo.UserID, UserInfo.UserName, UserInfo.Email, UserInfo.Age, Comment.CommentDate, Comment.CommentText, Post.Date, Post.Text, Post.Title FROM Post INNER JOIN Comment ON Comment.PostID = Post.PostID INNER JOIN UserInfo ON UserInfo.UserID = Comment.UserID WHERE Post.Title = @title")))
                {
                    command.Connection = connection;
                    command.Parameters.Add(new SqlParameter("title", title));
                    using (var reader = command.ExecuteReader())
                    {
                        CommentModel commentModel = new CommentModel();
                        CommentItemModel commentItem = null;
                        UserModel user = null;
                        if (reader.Read())
                        {
                            user = new UserModel(Convert.ToInt32(reader["UserID"]), reader["UserName"].ToString(), reader["Email"].ToString(), Convert.ToInt32(reader["Age"]));
                            commentItem = new CommentItemModel(DateTime.Parse(reader["CommentDate"].ToString()), reader["CommentText"].ToString(), Convert.ToInt32(reader["PostID"]), user);
                            commentModel.Comments.Add(commentItem);
                        }
                        ArticleItemModel model = new ArticleItemModel();
                        model.Date = DateTime.Parse(reader["Date"].ToString());
                        model.User = user;
                        model.Text = reader["Text"].ToString();
                        model.Title = reader["Title"].ToString();
                        model.Comments = commentModel;
                        model.Categories = new string[2];
                        model.Categories[0] = "Category2352";
                        model.Categories[1] = "Category31231";
                        return model;
                    }
                }
            }
        }

        public void addComment(CommentItemModel comment)
        {
            addUser(comment.User);
            using (var connection = new SqlConnection("Data Source=FIRRAEL\\SQLEXPRESS; Initial Catalog=Blog; Integrated Security=True"))
            {//ConfigurationManager.ConnectionStrings["mssql"].ConnectionString)) {
                connection.Open();
                using (var command = new SqlCommand(String.Format("INSERT INTO Comment VALUES(@Post.PostID, UserInfo.UserName, UserInfo.Email, UserInfo.Age, Post.Date, Post.Text, Post.Title FROM Post INNER JOIN UserInfo ON UserInfo.UserID = Post.UserID ORDER BY Post.Date DESC")))
                {
                    command.Connection = connection;
                }
            }
        }

        public void addUser(UserModel user)
        {
            using (var connection = new SqlConnection("Data Source=FIRRAEL\\SQLEXPRESS; Initial Catalog=Blog; Integrated Security=True"))
            {//ConfigurationManager.ConnectionStrings["mssql"].ConnectionString)) {
                connection.Open();
                using (var command = new SqlCommand(String.Format("INSERT INTO UserInfo VALUES(@username, @age, @email)")))
                {
                    command.Parameters.Add(new SqlParameter("username", user.UserName));
                    command.Parameters.Add(new SqlParameter("age", user.Age));
                    command.Parameters.Add(new SqlParameter("email", user.Email));

                    command.Connection = connection;
                    command.ExecuteNonQuery();
                }
            }

        }

        public ArticleItemModel GetLastPost()
        {
            using (var connection = new SqlConnection("Data Source=FIRRAEL\\SQLEXPRESS; Initial Catalog=Blog; Integrated Security=True"))
            {//ConfigurationManager.ConnectionStrings["mssql"].ConnectionString)) {
                connection.Open();
                using (var command = new SqlCommand(String.Format("SELECT TOP 1 Post.PostID, UserInfo.UserID, UserInfo.UserName, UserInfo.Email, UserInfo.Age, Post.Date, Post.Text, Post.Title FROM Post INNER JOIN UserInfo ON UserInfo.UserID = Post.UserID ORDER BY Post.Date DESC")))
                {
                    command.Connection = connection;
                    using (var reader = command.ExecuteReader())
                    {
                        using (var connection2 = new SqlConnection("Data Source=FIRRAEL\\SQLEXPRESS; Initial Catalog=Blog; Integrated Security=True"))
                        {//ConfigurationManager.ConnectionStrings["mssql"].ConnectionString)) {
                            connection2.Open();
                            using (var command2 = new SqlCommand(String.Format("SELECT Post.PostID, UserInfo.UserID, UserInfo.UserName, UserInfo.Email, UserInfo.Age, Comment.CommentDate, Comment.CommentText FROM Post INNER JOIN Comment ON Post.PostID = Comment.PostID INNER JOIN UserInfo ON UserInfo.UserID = Comment.UserID WHERE Post.PostID = @id ORDER BY Comment.CommentDate DESC")))
                            {
                                command2.Connection = connection2;

                                UserModel user = null;
                                ArticleItemModel model = new ArticleItemModel();
                                if (reader.Read())
                                {
                                    int id = Convert.ToInt32(reader["PostID"]);
                                    command2.Parameters.Add(new SqlParameter("id", id));
                                    user = new UserModel(Convert.ToInt32(reader["UserID"]), reader["UserName"].ToString(), reader["Email"].ToString(), Convert.ToInt32(reader["Age"]));
                                    model.User = user;
                                    model.Date = DateTime.Parse(reader["Date"].ToString());
                                    model.Text = reader["Text"].ToString();
                                    model.Title = reader["Title"].ToString();
                                }
                                using (var reader2 = command2.ExecuteReader())
                                {
                                    CommentModel commentModel = new CommentModel();
                                    CommentItemModel commentItem = null;

                                    while (reader2.Read())
                                    {
                                        user = new UserModel(Convert.ToInt32(reader["UserID"]), reader2["UserName"].ToString(), reader2["Email"].ToString(), Convert.ToInt32(reader2["Age"]));
                                        commentItem = new CommentItemModel(DateTime.Parse(reader2["CommentDate"].ToString()), reader2["CommentText"].ToString(), Convert.ToInt32(reader2["PostID"]), user);
                                        commentModel.Comments.Add(commentItem);
                                    }


                                    model.Comments = commentModel;
                                    model.Categories = new string[2];
                                    model.Categories[0] = "Category2352";
                                    model.Categories[1] = "Category31231";
                                    return model;
                                }
                            }
                        }
                    }
                }
            }
        }

        public ArticleModel GetPosts()
        {
            using (var connection = new SqlConnection("Data Source=FIRRAEL\\SQLEXPRESS; Initial Catalog=Blog; Integrated Security=True"))
            {//ConfigurationManager.ConnectionStrings["mssql"].ConnectionString)) {
                connection.Open();
                ArticleModel articleModel = new ArticleModel();
                using (var command = new SqlCommand(String.Format("SELECT UserInfo.UserID, UserInfo.UserName, UserInfo.Email, UserInfo.Age, Post.Date, Post.Text, Post.Title FROM Post INNER JOIN UserInfo ON UserInfo.UserID = Post.UserID")))
                {
                    command.Connection = connection;
                    using (var reader = command.ExecuteReader())
                    {
                        using (var connection2 = new SqlConnection("Data Source=FIRRAEL\\SQLEXPRESS; Initial Catalog=Blog; Integrated Security=True"))
                        {//ConfigurationManager.ConnectionStrings["mssql"].ConnectionString)) {
                            connection2.Open();
                            using (var command2 = new SqlCommand(String.Format("SELECT Post.PostID, UserInfo.UserID, UserInfo.UserName, UserInfo.Email, UserInfo.Age, Comment.CommentDate, Comment.CommentText FROM Post INNER JOIN Comment ON Post.PostID = Comment.PostID INNER JOIN UserInfo ON UserInfo.UserID = Comment.UserID")))
                            {
                                command2.Connection = connection2;

                                while (reader.Read())
                                {
                                    UserModel user = null;
                                    ArticleItemModel model = new ArticleItemModel();
                                    user = new UserModel(Convert.ToInt32(reader["UserID"]), reader["UserName"].ToString(), reader["Email"].ToString(), Convert.ToInt32(reader["Age"]));
                                    model.Date = DateTime.Parse(reader["Date"].ToString());
                                    model.Text = reader["Text"].ToString();
                                    model.Title = reader["Title"].ToString();
                                    model.User = user;

                                    CommentModel commentModel = new CommentModel();
                                    using (var reader2 = command2.ExecuteReader())
                                    {

                                        CommentItemModel commentItem = null;
                                        while (reader2.Read())
                                        {
                                            user = new UserModel(Convert.ToInt32(reader["UserID"]), reader2["UserName"].ToString(), reader2["Email"].ToString(), Convert.ToInt32(reader2["Age"]));
                                            commentItem = new CommentItemModel(DateTime.Parse(reader2["CommentDate"].ToString()), reader2["CommentText"].ToString(), Convert.ToInt32(reader2["PostID"]), user);
                                            commentModel.Comments.Add(commentItem);
                                        }

                                        model.Categories = new string[2];
                                        model.Categories[0] = "Category2352";
                                        model.Categories[1] = "Category31231";
                                        model.Comments = commentModel;

                                    }
                                    articleModel.Articles.Add(model);
                                }

                                return articleModel;
                            }
                        }
                    }
                }
            }
        }


        public CommentModel getRecentComments()
        {
            using (var connection = new SqlConnection("Data Source=FIRRAEL\\SQLEXPRESS; Initial Catalog=Blog; Integrated Security=True"))
            {//ConfigurationManager.ConnectionStrings["mssql"].ConnectionString)) {
                connection.Open();
                using (var command = new SqlCommand(String.Format("SELECT TOP 3 Comment.PostID, UserInfo.UserID, UserInfo.UserName, UserInfo.Email, UserInfo.Age, Comment.CommentDate, Comment.CommentText FROM Comment INNER JOIN UserInfo ON UserInfo.UserID = Comment.UserID ORDER BY Comment.CommentDate DESC")))
                {
                    command.Connection = connection;
                    using (var reader = command.ExecuteReader())
                    {
                        CommentModel commentModel = new CommentModel();
                        CommentItemModel commentItem = null;

                        UserModel user = null;
                        while (reader.Read())
                        {
                            user = new UserModel(Convert.ToInt32(reader["UserID"]), reader["UserName"].ToString(), reader["Email"].ToString(), Convert.ToInt32(reader["Age"]));
                            commentItem = new CommentItemModel(DateTime.Parse(reader["CommentDate"].ToString()), reader["CommentText"].ToString(), Convert.ToInt32(reader["PostID"]), user);
                            commentModel.Comments.Add(commentItem);
                        }

                        return commentModel;
                    }
                }
            }
        }

        public RecentPostsModel getRecentPosts()
        {
            using (var connection = new SqlConnection("Data Source=FIRRAEL\\SQLEXPRESS; Initial Catalog=Blog; Integrated Security=True"))
            {//ConfigurationManager.ConnectionStrings["mssql"].ConnectionString)) {
                connection.Open();
                using (var command = new SqlCommand(String.Format("SELECT TOP 3 Post.Title, COUNT(Comment.CommentID) AS CommentsCount FROM Post LEFT OUTER JOIN Comment ON Post.PostID = Comment.PostID GROUP BY Post.Date, Post.Title ORDER BY Post.Date DESC")))
                {
                    command.Connection = connection;
                    using (var reader = command.ExecuteReader())
                    {
                        RecentPostsModel postsModel = new RecentPostsModel();
                        RecentPostsItem post = null;

                        while (reader.Read())
                        {
                            post = new RecentPostsItem(reader["Title"].ToString(), new Uri(reader["Title"].ToString(), UriKind.Relative), Convert.ToInt32(reader["CommentsCount"]));
                            postsModel.RecentPostItems.Add(post);
                        }

                        return postsModel;
                    }
                }
            }
        }

        public LoginModel getCredentials()
        {
            using (var connection = new SqlConnection("Data Source=FIRRAEL\\SQLEXPRESS; Initial Catalog=Blog; Integrated Security=True"))
            {//ConfigurationManager.ConnectionStrings["mssql"].ConnectionString)) {
                connection.Open();
                using (var command = new SqlCommand(String.Format("SELECT * FROM Credentials")))
                {
                    command.Connection = connection;
                    using (var reader = command.ExecuteReader())
                    {
                        LoginModel model = new LoginModel();

                        if (reader.Read())
                        {
                            model.Name = reader["Login"].ToString();
                            model.Password = reader["Password"].ToString();
                        }

                        return model;
                    }
                }
            }
        }



    }
}