using MySqlConnector;
using System.Diagnostics;
using System.Net;
namespace api.Controllers
{
    class DatabaseLogin { 
    private static string connectionString = "Server=localhost;Database=BlogPosts;User ID=root;password=neurit159;";
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
                                Id = (int)reader["Id"],
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
        public static List<BlogPost> GetPost(int id)
        {
            try
            {
                using (MySqlConnection connection = new MySqlConnection(connectionString))
                {
                    
                    connection.Open();
                    MySqlCommand command = new MySqlCommand("SELECT * FROM BlogPosts WHERE id = @id", connection);
                    command.Parameters.AddWithValue("@id", id);
                    List<BlogPost> blogPosts = new List<BlogPost>();
                    using (MySqlDataReader reader = command.ExecuteReader())
                    {
                        int i = 0;
                        while (reader.Read())
                        {
                            blogPosts.Add(new BlogPost
                            {
                                Id = (int)reader["Id"],
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
        public static bool? DeletePost(int id)
        {
            try
            {
                using (MySqlConnection connection = new MySqlConnection(connectionString))
                {
                    
                    connection.Open();
                    MySqlCommand command = new MySqlCommand("delete from BlogPosts where id = @id;", connection);
                    command.Parameters.AddWithValue("@id", id);
                    command.ExecuteNonQuery();
                    connection.Close();
                    return true;
                }
                
            }
            catch (Exception e)
            {
            }
            return null;
        }
        public static bool? PatchPost(int id, string Content)
        {
            try
            {
                using (MySqlConnection connection = new MySqlConnection(connectionString))
                {
                    
                    connection.Open();
                    MySqlCommand command = new MySqlCommand("UPDATE BlogPosts SET Content = @Content, CreationDate = @CreationDate WHERE id = @id;", connection);
                    command.Parameters.AddWithValue("@id", id);
                    command.Parameters.AddWithValue("@Content", Content);
                    command.Parameters.AddWithValue("@creationDate", DateTime.Now);
                    command.ExecuteNonQuery();
                    connection.Close();
                    return true;
                }
                
            }
            catch (Exception e)
            {
            }
            return null;
        }
    }
}