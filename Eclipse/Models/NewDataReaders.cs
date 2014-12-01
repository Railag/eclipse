/*using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace Eclipse.Models
{
    public class NewDataReaders
    {
        public ArticleModel GetArticleModel(string title)
        {
            //using = try + finally
            using() {

            }
             using(var connection = new SqlConnection(ConfigurationManager.ConnectionStrings["mssql"].ConnectionString)) {
                  connection.Open();
                  using(var command = new SqlCommand(String.Format("SELECT * FROM Post WHERE Title = @title"))) {
                      
                  }
             }
            var connection = new SqlConnection(ConfigurationManager.ConnectionStrings["mssql"].ConnectionString);
            connection.Open();

            var command = new SqlCommand(String.Format("SELECT * FROM Post WHERE Title = '{0}", title));
           
            command.Connection = connection;
            command.Parameters.Add(new SqlParameter("title", title));
          

            var reader = command.ExecuteReader();

            PostModel postModel = null;

            if (reader.Read())
            {
                postModel = new PostModel(reader["Title".ToString(), reader["Body"].ToString(), DateTime.Parse(reader["DateCreated"].ToString());
            }
            reader.Dispose();
            command.Dispose();
            connection.Dispose();
        
            return new ArticleModel(postModel, null);
        }
    }
} */