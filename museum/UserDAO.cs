using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Configuration;
using System.Windows.Forms;

namespace museum
{
    class UserDAO
    {
            public List<User> getAllUserData() 
            {
                    List<User> userList = new List<User>();
                    try
                    {
                        using (SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["museum.Properties.Settings.museumConnectionString"].ConnectionString))
                        {
                            connection.Open();
                            string sqlQuery = "SELECT * FROM users";
                            SqlCommand command = new SqlCommand(sqlQuery, connection);
                            using (SqlDataReader reader = command.ExecuteReader())
                            {
                                while (reader.Read())
                                {
                                    var user = new User();
                                    user.id = (int)reader["id"];
                                    user.username = (string)reader["username"];
                                    user.name = (string)reader["name"];
                                    user.last = (string)reader["last"];
                                    user.password = (string)reader["password"];
                                    user.email = (string)reader["email"];
                                    userList.Add(user);
                                }
                            }
                        }
                    }
                    catch (SqlException sql) 
                    {
                        MessageBox.Show("The user data could not be retrieved!" + sql);
                        return null;
                    }
                    return userList;
            }


            public User getSingleUser(int id)
            {
                var user = new User();
                try
                {
                    using (SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["museum.Properties.Settings.museumConnectionString"].ConnectionString))
                    {
                        connection.Open();
                        string sqlQuery = "SELECT * FROM users WHERE id = " + id;
                        SqlCommand command = new SqlCommand(sqlQuery, connection);
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                user.id = (int)reader["id"];
                                user.username = (string)reader["username"];
                                user.name = (string)reader["name"];
                                user.last = (string)reader["last"];
                                user.password = (string)reader["password"];
                                user.email = (string)reader["email"];
                            }
                        }
                    }
                }
                catch (SqlException sql)
                {
                    MessageBox.Show("The single user could not be retrieved" + sql);
                }
                return user;
            }


            public bool addUser(User user)
            {
                try{
                    using (SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["museum.Properties.Settings.museumConnectionString"].ConnectionString))
                    {
                        connection.Open();
                        string sqlQuery = "INSERT INTO users (username,name,last,email,password) VALUES (@username,@name,@last,@email,@password)";
                        SqlCommand command = new SqlCommand(sqlQuery, connection);
                        command.Parameters.AddWithValue("@username", user.username);
                        command.Parameters.AddWithValue("@name", user.name);
                        command.Parameters.AddWithValue("@last", user.last);
                        command.Parameters.AddWithValue("@email", user.email);
                        command.Parameters.AddWithValue("@password", user.password);
                        int oe = command.ExecuteNonQuery();
                    }
                }catch(SqlException sql){
                    MessageBox.Show("The user could not be inserted" + sql);
                }
                return true;
            }


            public bool updateUser(User user)
            {
                try
                {
                    using (SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["museum.Properties.Settings.museumConnectionString"].ConnectionString))
                    {
                        connection.Open();
                        string sqlQuery = "UPDATE users SET username = @username,name = @name,last = @last,email = @email,password =@password  WHERE id = " + user.id;
                        SqlCommand command = new SqlCommand(sqlQuery, connection);
                        command.Parameters.AddWithValue("@username", user.username);
                        command.Parameters.AddWithValue("@name", user.name);
                        command.Parameters.AddWithValue("@last", user.last);
                        command.Parameters.AddWithValue("@email", user.email);
                        command.Parameters.AddWithValue("@password", user.password);
                        command.ExecuteNonQuery();
                    }
                }
                catch (SqlException sql)
                {
                    MessageBox.Show("The user could not be updated" + sql);
                    return false;
                }
                return true;
            }

            public User getLastUser()
            { 
               var user = new User();
               try
               {
                   using (SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["museum.Properties.Settings.museumConnectionString"].ConnectionString))
                   {
                       connection.Open();
                       string sqlQuery = "SELECT TOP 1 * FROM users ORDER BY id DESC";
                       SqlCommand command = new SqlCommand(sqlQuery, connection);
                       using (SqlDataReader reader = command.ExecuteReader())
                       {
                           while (reader.Read())
                           {
                               user.id = (int)reader["id"];
                               user.username = (string)reader["username"];
                               user.name = (string)reader["name"];
                               user.last = (string)reader["last"];
                               user.password = (string)reader["password"];
                               user.email = (string)reader["email"];
                           }
                       }
                   }
               }
               catch (SqlException sql) 
               {
                   MessageBox.Show("The user could not be retrieved" + sql);
               }
                return user;
            }





        }

}
