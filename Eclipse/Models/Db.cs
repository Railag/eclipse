using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace Eclipse.Models
{
    public class Db
    {
        public ArticleItemModel GetPostByTitle(string Title)
        {
            using (var connection = new SqlConnection(ConfigurationManager.ConnectionStrings["mssql"].ConnectionString))
            {
                connection.Open();
                using (var command = new SqlCommand(String.Format("SELECT Post.PostID, UserInfo.UserID, UserInfo.UserName, UserInfo.Email, UserInfo.Age, Post.Date, Post.Text, Post.Title FROM Post INNER JOIN UserInfo ON UserInfo.UserID = Post.UserID WHERE @title = Post.Title")))
                {
                    command.Connection = connection;
                    command.Parameters.Add(new SqlParameter("title", Title));
                    using (var reader = command.ExecuteReader())
                    {
                        using (var connection2 = new SqlConnection(ConfigurationManager.ConnectionStrings["mssql"].ConnectionString))
                        {
                            connection2.Open();
                            using (var command2 = new SqlCommand(String.Format("SELECT Post.PostID, UserInfo.UserID, UserInfo.UserName, UserInfo.Email, UserInfo.Age, Comment.CommentDate, Comment.CommentText FROM Post INNER JOIN Comment ON Post.PostID = Comment.PostID INNER JOIN UserInfo ON UserInfo.UserID = Comment.UserID WHERE Post.PostID = @id ORDER BY Comment.CommentDate DESC")))
                            {
                                command2.Connection = connection2;

                                UserModel user = null;
                                ArticleItemModel model = new ArticleItemModel();
                                if (reader.Read())
                                {
                                    int id = Convert.ToInt32(reader["PostID"]);
                                    if (id == 0)
                                        return null;
                                    model.PostID = id;
                                    command2.Parameters.Add(new SqlParameter("id", id));
                                    user = new UserModel(Convert.ToInt32(reader["UserID"]), reader["UserName"].ToString(), reader["Email"].ToString(), Convert.ToInt32(reader["Age"]));
                                    model.User = user;
                                    model.Date = DateTime.Parse(reader["Date"].ToString());
                                    model.Text = reader["Text"].ToString();
                                    model.Title = reader["Title"].ToString();
                                    model.NewComment = new CommentItemModel();
                                    model.NewComment.PostID = id;
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

        public void addPost(ArticleItemModel post)
        {
            int UserID = addUser(post.User);
            using (var connection = new SqlConnection(ConfigurationManager.ConnectionStrings["mssql"].ConnectionString))
            {
                connection.Open();
                using (var command = new SqlCommand(String.Format("INSERT INTO Post(UserID, Text, Title, Date) VALUES(@UserID, @Text, @Title, @Date)")))    // Post.PostID, UserInfo.UserName, UserInfo.Email, UserInfo.Age, Post.Date, Post.Text, Post.Title FROM Post INNER JOIN UserInfo ON UserInfo.UserID = Post.UserID ORDER BY Post.Date DESC")))
                {
                    command.Connection = connection;
                    command.Parameters.Add(new SqlParameter("UserID", UserID));
                    command.Parameters.Add(new SqlParameter("Text", post.Text));
                    command.Parameters.Add(new SqlParameter("Title", post.Title));
                    command.Parameters.Add(new SqlParameter("Date", post.Date));
                    command.ExecuteNonQuery();
                }
            }
        }

        public void addComment(CommentItemModel comment)
        {
            int UserID = addUser(comment.User);
            using (var connection = new SqlConnection(ConfigurationManager.ConnectionStrings["mssql"].ConnectionString))
            {
                connection.Open();
                using (var command = new SqlCommand(String.Format("INSERT INTO Comment(UserID, PostID, CommentText, CommentDate) VALUES(@UserID, @PostID, @CommentText, @CommentDate)")))
                {
                    command.Connection = connection;
                    command.Parameters.Add(new SqlParameter("UserID", UserID));
                    command.Parameters.Add(new SqlParameter("PostID", comment.PostID));
                    command.Parameters.Add(new SqlParameter("CommentText", comment.Text));
                    command.Parameters.Add(new SqlParameter("CommentDate", comment.Date));
                    command.ExecuteNonQuery();
                }
            }
        }

        public int addUser(UserModel user)
        {
            using (var connection = new SqlConnection(ConfigurationManager.ConnectionStrings["mssql"].ConnectionString))
            {
                connection.Open();
                using (var command = new SqlCommand(String.Format("INSERT INTO UserInfo(UserName, Age, Email) VALUES(@username, @age, @email); SELECT SCOPE_IDENTITY()")))
                {
                    command.Parameters.Add(new SqlParameter("username", user.UserName));
                    command.Parameters.Add(new SqlParameter("age", user.Age));
                    command.Parameters.Add(new SqlParameter("email", user.Email)); 

                    command.Connection = connection;
                    int id = Convert.ToInt32(command.ExecuteScalar());
                    return id;
                }
            }

        }

        public void deletePost(int PostID)
        {
            using (var connection = new SqlConnection(ConfigurationManager.ConnectionStrings["mssql"].ConnectionString))
            {
                connection.Open();
                using (var command = new SqlCommand(String.Format("DELETE FROM Comment WHERE @PostID = Comment.PostID")))
                {
                    command.Connection = connection;
                    command.Parameters.Add(new SqlParameter("PostID", PostID));
                    command.ExecuteNonQuery();
                }
                using (var command = new SqlCommand(String.Format("DELETE FROM Post WHERE @PostID = Post.PostID")))
                {
                    command.Connection = connection;
                    command.Parameters.Add(new SqlParameter("PostID", PostID));
                    command.ExecuteNonQuery();
                }
            }
        }

        public void deleteComment(int CommentID)
        {
            using (var connection = new SqlConnection(ConfigurationManager.ConnectionStrings["mssql"].ConnectionString))
            {
                connection.Open();
                using (var command = new SqlCommand(String.Format("DELETE FROM Comment WHERE @CommentID = Comment.CommentID")))
                {
                    command.Connection = connection;
                    command.Parameters.Add(new SqlParameter("CommentID", CommentID));
                    command.ExecuteNonQuery();
                }
            }
        }
        
        public ArticleItemModel GetLastPost()
        {
            using (var connection = new SqlConnection(ConfigurationManager.ConnectionStrings["mssql"].ConnectionString))
            {
                connection.Open();
                using (var command = new SqlCommand(String.Format("SELECT TOP 1 Post.PostID, UserInfo.UserID, UserInfo.UserName, UserInfo.Email, UserInfo.Age, Post.Date, Post.Text, Post.Title FROM Post INNER JOIN UserInfo ON UserInfo.UserID = Post.UserID ORDER BY Post.Date DESC")))
                {
                    command.Connection = connection;
                    using (var reader = command.ExecuteReader())
                    {
                        using (var connection2 = new SqlConnection(ConfigurationManager.ConnectionStrings["mssql"].ConnectionString))
                        {
                            connection2.Open();
                            using (var command2 = new SqlCommand(String.Format("SELECT Post.PostID, UserInfo.UserID, UserInfo.UserName, UserInfo.Email, UserInfo.Age, Comment.CommentDate, Comment.CommentText, Comment.CommentID FROM Post INNER JOIN Comment ON Post.PostID = Comment.PostID INNER JOIN UserInfo ON UserInfo.UserID = Comment.UserID WHERE Post.PostID = @id ORDER BY Comment.CommentDate DESC")))
                            {
                                command2.Connection = connection2;

                                UserModel user = null;
                                ArticleItemModel model = new ArticleItemModel();
                                if (reader.Read())
                                {
                                    int id = Convert.ToInt32(reader["PostID"]);
                                    model.PostID = id;
                                    command2.Parameters.Add(new SqlParameter("id", id));
                                    user = new UserModel(Convert.ToInt32(reader["UserID"]), reader["UserName"].ToString(), reader["Email"].ToString(), Convert.ToInt32(reader["Age"]));
                                    model.User = user;
                                    model.Date = DateTime.Parse(reader["Date"].ToString());
                                    model.Text = reader["Text"].ToString();
                                    model.Title = reader["Title"].ToString();
                                    model.NewComment = new CommentItemModel();
                                    model.NewComment.PostID = id;
                                }
                                using (var reader2 = command2.ExecuteReader())
                                {
                                    CommentModel commentModel = new CommentModel();
                                    CommentItemModel commentItem = null;

                                    while (reader2.Read())
                                    {
                                        user = new UserModel(Convert.ToInt32(reader["UserID"]), reader2["UserName"].ToString(), reader2["Email"].ToString(), Convert.ToInt32(reader2["Age"]));
                                        commentItem = new CommentItemModel(DateTime.Parse(reader2["CommentDate"].ToString()), reader2["CommentText"].ToString(), Convert.ToInt32(reader2["PostID"]), user);
                                        commentItem.CommentID = Convert.ToInt32(reader2["CommentID"]);
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
            using (var connection = new SqlConnection(ConfigurationManager.ConnectionStrings["mssql"].ConnectionString))
            {
                connection.Open();
                ArticleModel articleModel = new ArticleModel();
                using (var command = new SqlCommand(String.Format("SELECT UserInfo.UserID, UserInfo.UserName, UserInfo.Email, UserInfo.Age, Post.PostID, Post.Date, Post.Text, Post.Title FROM Post INNER JOIN UserInfo ON UserInfo.UserID = Post.UserID ORDER BY Post.Date DESC")))
                {
                    command.Connection = connection;
                    using (var reader = command.ExecuteReader())
                    {
                        using (var connection2 = new SqlConnection(ConfigurationManager.ConnectionStrings["mssql"].ConnectionString))
                        {
                            connection2.Open();
                            using (var command2 = new SqlCommand(String.Format("SELECT Comment.PostID, UserInfo.UserID, UserInfo.UserName, UserInfo.Email, UserInfo.Age, Comment.CommentDate, Comment.CommentText FROM Post INNER JOIN Comment ON Post.PostID = Comment.PostID INNER JOIN UserInfo ON UserInfo.UserID = Comment.UserID WHERE @PostID = Comment.PostID ORDER BY Comment.CommentDate")))
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
                                    int postID = Convert.ToInt32(reader["PostID"]);
                                    if (command2.Parameters.Contains("PostID"))
                                        command2.Parameters.RemoveAt("PostID");
                                    command2.Parameters.Add(new SqlParameter("postID", postID));

                                    CommentModel commentModel = new CommentModel();
                                    using (var reader2 = command2.ExecuteReader())
                                    {

                                        CommentItemModel commentItem = null;
                                        while (reader2.Read())
                                        {
                                            int commentPostID = Convert.ToInt32(reader2["PostID"]);
                                            if (postID == commentPostID)
                                            {
                                                user = new UserModel(Convert.ToInt32(reader2["UserID"]), reader2["UserName"].ToString(), reader2["Email"].ToString(), Convert.ToInt32(reader2["Age"]));
                                                commentItem = new CommentItemModel(DateTime.Parse(reader2["CommentDate"].ToString()), reader2["CommentText"].ToString(), postID, user);
                                                commentModel.Comments.Add(commentItem);
                                            }
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
            using (var connection = new SqlConnection(ConfigurationManager.ConnectionStrings["mssql"].ConnectionString))
            {
                connection.Open();
                using (var command = new SqlCommand(String.Format("SELECT TOP 3 Post.Title, Comment.PostID, UserInfo.UserID, UserInfo.UserName, UserInfo.Email, UserInfo.Age, Comment.CommentDate, Comment.CommentText FROM Comment INNER JOIN Post ON Post.PostID = Comment.PostID INNER JOIN UserInfo ON UserInfo.UserID = Comment.UserID ORDER BY Comment.CommentDate DESC")))
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
                            commentItem = new CommentItemModel(DateTime.Parse(reader["CommentDate"].ToString()), reader["CommentText"].ToString(), Convert.ToInt32(reader["PostID"]), user, reader["Title"].ToString());
                            commentModel.Comments.Add(commentItem);
                        }

                        return commentModel;
                    }
                }
            }
        }

        public RecentPostsModel getRecentPosts()
        {
            using (var connection = new SqlConnection(ConfigurationManager.ConnectionStrings["mssql"].ConnectionString))
            {
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
                            post = new RecentPostsItem(reader["Title"].ToString(), Convert.ToInt32(reader["CommentsCount"]));
                            postsModel.RecentPostItems.Add(post);
                        }

                        return postsModel;
                    }
                }
            }
        }

        public ArticleItemModel GetPostById(int ID)
        {
            using (var connection = new SqlConnection(ConfigurationManager.ConnectionStrings["mssql"].ConnectionString))
            {
                connection.Open();
                using (var command = new SqlCommand(String.Format("SELECT Post.PostID, UserInfo.UserID, UserInfo.UserName, UserInfo.Email, UserInfo.Age, Post.Date, Post.Text, Post.Title FROM Post INNER JOIN UserInfo ON UserInfo.UserID = Post.UserID WHERE @ID = Post.PostID")))
                {
                    command.Connection = connection;
                    command.Parameters.Add(new SqlParameter("ID", ID));
                    using (var reader = command.ExecuteReader())
                    {
                        using (var connection2 = new SqlConnection(ConfigurationManager.ConnectionStrings["mssql"].ConnectionString))
                        {
                            connection2.Open();
                            using (var command2 = new SqlCommand(String.Format("SELECT Post.PostID, UserInfo.UserID, UserInfo.UserName, UserInfo.Email, UserInfo.Age, Comment.CommentDate, Comment.CommentText FROM Post INNER JOIN Comment ON Post.PostID = Comment.PostID INNER JOIN UserInfo ON UserInfo.UserID = Comment.UserID WHERE Post.PostID = @id ORDER BY Comment.CommentDate DESC")))
                            {
                                command2.Connection = connection2;

                                UserModel user = null;
                                ArticleItemModel model = new ArticleItemModel();
                                if (reader.Read())
                                {
                                    int id = Convert.ToInt32(reader["PostID"]);
                                    if (id == 0)
                                        return null;
                                    model.PostID = id;
                                    command2.Parameters.Add(new SqlParameter("id", id));
                                    user = new UserModel(Convert.ToInt32(reader["UserID"]), reader["UserName"].ToString(), reader["Email"].ToString(), Convert.ToInt32(reader["Age"]));
                                    model.User = user;
                                    model.Date = DateTime.Parse(reader["Date"].ToString());
                                    model.Text = reader["Text"].ToString();
                                    model.Title = reader["Title"].ToString();
                                    model.NewComment = new CommentItemModel();
                                    model.NewComment.PostID = id;
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

        public void updatePost(ArticleItemModel post)
        {
            updateUser(post.User);
            using (var connection = new SqlConnection(ConfigurationManager.ConnectionStrings["mssql"].ConnectionString))
            {
                connection.Open();
                using (var command = new SqlCommand(String.Format("UPDATE Post SET Text = @Text, Title = @Title WHERE PostID = @PostID")))    
                {
                    command.Connection = connection;
                    command.Parameters.Add(new SqlParameter("Text", post.Text));
                    command.Parameters.Add(new SqlParameter("Title", post.Title));
                    command.Parameters.Add(new SqlParameter("Date", post.Date));
                    command.Parameters.Add(new SqlParameter("PostID", post.PostID));
                    command.ExecuteNonQuery();
                }
            }
        }

        public void updateUser(UserModel user)
        {
            using (var connection = new SqlConnection(ConfigurationManager.ConnectionStrings["mssql"].ConnectionString))
            {
                connection.Open();
                using (var command = new SqlCommand(String.Format("UPDATE UserInfo SET UserName = @username, Age = @age, Email = @email WHERE UserID = @userId")))
                {
                    command.Parameters.Add(new SqlParameter("username", user.UserName));
                    command.Parameters.Add(new SqlParameter("age", user.Age));
                    command.Parameters.Add(new SqlParameter("email", user.Email));
                    command.Parameters.Add(new SqlParameter("userId", user.UserID));

                    command.Connection = connection;
                    command.ExecuteScalar();
                }
            }

        }

        public LoginModel getCredentials()
        {
            using (var connection = new SqlConnection(ConfigurationManager.ConnectionStrings["mssql"].ConnectionString))
            {
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