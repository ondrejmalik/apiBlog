using MySqlConnector;
using System.Diagnostics;
using System.Net;
namespace api.Controllers
{
    class DatabaseLogin { 
    private static string connectionString = "Server=localhost;Database=BlogPosts;User ID=root;Password=student;";
        public static void InsertPost(BlogPost post)
        {
            try
            {
                using (MySqlConnection connection = new MySqlConnection(connectionString))
                {
                    connection.Open();
                    string query = "INSERT INTO BlogPosts (Content, CreationDate,Author) VALUES (@Content, @CreationDate, @Author)";
                    MySqlCommand command = new MySqlCommand(query, connection);
                    command.Parameters.AddWithValue("@Content", post.Content);
                    command.Parameters.AddWithValue("@CreationDate", post.CreationDate);
                    command.Parameters.AddWithValue("@Author", post.Author);
                    command.ExecuteNonQuery();
                    connection.Close();
                }
            }
            catch (Exception e)
            {
            }
        }
        public static List<BlogPost> GetPost()
        {
            try
            {
                using (MySqlConnection connection = new MySqlConnection(connectionString))
                {
                    
                    connection.Open();
                    MySqlCommand command = new MySqlCommand("SELECT * FROM BlogPosts", connection);
                    List<BlogPost> blogPosts = new List<BlogPost>();
                    using (MySqlDataReader reader = command.ExecuteReader())
                    {
                        int i = 0;
                        while (reader.Read())
                        {
                            blogPosts.Add(new BlogPost
                            {
                                Content = (string)reader["Content"],
                                CreationDate = (DateTime)reader["CreationDate"],
                                Author = (string)reader["Author"],
                            });
                            i++;
                        }
                    }

                    connection.Close();
                    return blogPosts;
                }
                
            }
            catch (Exception e)
            {
            }
            return null;
        }
    }
}