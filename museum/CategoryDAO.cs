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
    /*We have build one DAO class for the two tables:1) General Category, which is the first level of classification, and 2) Category, which is the second level
      We have chosen to get a connection to the database in every separate function instead of getting one in the constructor and then leave it open throughout the 
      application's lifespan.*/
    class CategoryDAO
    {
        /*gets ALL data from the General Category table*/
        public List<GeneralCategory> getGeneralCategory() 
        {
            List<GeneralCategory> genCatList = new List<GeneralCategory>();
            try
            {
                using (SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["museum.Properties.Settings.museumConnectionString"].ConnectionString))
                {
                    connection.Open();
                    string sqlQuery = "SELECT * FROM GeneralCategory";
                    SqlCommand command = new SqlCommand(sqlQuery, connection);
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            var generalCategory = new GeneralCategory();
                            generalCategory.id = (int)reader["id"];
                            generalCategory.name = (string)reader["name"];
                            generalCategory.text = (string)reader["text"];
                            generalCategory.image = (string)reader["image"];
                            genCatList.Add(generalCategory);
                        }
                    }
                }
            }
            catch (SqlException sql)
            {
                MessageBox.Show("The general category could not be retrieved" + sql);
            }
            return genCatList;
        }
        /*gets data for only one row from General Category table*/
        public GeneralCategory getSingleGeneralCategory(string id) 
        {
            var generalCategory = new GeneralCategory();
            try
            {
                using (SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["museum.Properties.Settings.museumConnectionString"].ConnectionString))
                {
                    connection.Open();
                    string sqlQuery = "SELECT * FROM GeneralCategory WHERE id = " + id;
                    SqlCommand command = new SqlCommand(sqlQuery, connection);
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            generalCategory.id = (int)reader["id"];
                            generalCategory.name = (string)reader["name"];
                            generalCategory.text = (string)reader["text"];
                            generalCategory.image = (string)reader["image"];
                        }
                    }
                }
            }
            catch (SqlException sql)
            {
                MessageBox.Show("The single general category could not be retrieved" + sql);
            }
            return generalCategory;
        }

        /*gets data from the Categories table for rows that belong to a specific General Category*/
        public List<Category> getCategory(string id)
        {
            List<Category> catList = new List<Category>();
            try
            {
                using (SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["museum.Properties.Settings.museumConnectionString"].ConnectionString))
                {
                    connection.Open();
                    string sqlQuery = "SELECT * FROM Categories WHERE generalCategory = " + id;
                    SqlCommand command = new SqlCommand(sqlQuery, connection);
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            var category = new Category();
                            category.id = (int)reader["id"];
                            category.name = (string)reader["name"];
                            category.generalCategory = (int)reader["generalCategory"];
                            category.text = (string)reader["text"];
                            catList.Add(category);
                        }
                    }
                }
            }
            catch (SqlException sql)
            {
                MessageBox.Show("The categories could not be retrieved" + sql);
            }
            return catList;
        }

        /*gets data for only one row from Categories table*/
        public Category getSingleCategory(string id)
        {
            Category category = new Category();
            try
            {
                using (SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["museum.Properties.Settings.museumConnectionString"].ConnectionString))
                {
                    connection.Open();
                    string sqlQuery = "SELECT * FROM Categories WHERE id = " + id;
                    SqlCommand command = new SqlCommand(sqlQuery, connection);
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            category.id = (int)reader["id"];
                            category.name = (string)reader["name"];
                            category.generalCategory = (int)reader["generalCategory"];
                            category.text = (string)reader["text"];
                        }
                    }
                }
            }
            catch (SqlException sql)
            {
                MessageBox.Show("The single category could not be retrieved" + sql);
            }
            return category;
        }


        public List<Category> getAllCategories() {
            List<Category> catList = new List<Category>();
            try {    
                using (SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["museum.Properties.Settings.museumConnectionString"].ConnectionString))
                {
                    connection.Open();
                    string sqlString = "SELECT * FROM categories";
                    SqlCommand command = new SqlCommand(sqlString, connection);
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Category category = new Category();
                            category.id = (int)reader["id"];
                            category.name = (string)reader["name"];
                            category.generalCategory = (int)reader["generalCategory"];
                            category.text = (string)reader["text"];
                            catList.Add(category);
                        }
                    }
                }
            }
            catch (SqlException sql) 
            {
                MessageBox.Show("The categories could not be retrieved " + sql);
            }
            return catList;
        }




    }
}
