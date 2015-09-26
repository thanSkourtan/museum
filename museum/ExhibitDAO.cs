using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Collections;
using System.Configuration;
using System.Windows.Forms;

namespace museum
{
    class ExhibitDAO
    {
        public List<Exhibit> displayAll()
        {
            List<Exhibit> exhibitList = new List<Exhibit>();
            try
            {
                using (SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["museum.Properties.Settings.museumConnectionString"].ConnectionString))
                {
                    connection.Open();
                    string strSQL = "SELECT * FROM  info";
                    SqlCommand command = new SqlCommand(strSQL, connection);
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            /*for every row in the SqlDataReader we create an Exhibit object and then we fill in all its fields with the 
                              required data. Then we add the object to the ArrayList which is finally returned from the display method*/
                            Exhibit exhibit = new Exhibit();

                            exhibit.id = (int)reader["id"];
                            exhibit.type = (string)reader["type"];
                            exhibit.place = (string)reader["place"];
                            exhibit.timing = (string)reader["timing"];
                            exhibit.dimensions = (string)reader["dimensions"];
                            exhibit.area = (string)reader["area"];
                            exhibit.description = (string)reader["description"];
                            exhibit.image = (string)reader["image"];
                            exhibit.category = (int)reader["category"];

                            exhibitList.Add(exhibit);
                        }
                    }
                }
            }
            catch (SqlException sql)
            {
                MessageBox.Show("The exhibits could not be retrieved" + sql);
            }
            return exhibitList;
        }

        public List<Exhibit> displayfromCategory(int category)
        {
            List<Exhibit> exhibitList = new List<Exhibit>();
            try
            {
                using (SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["museum.Properties.Settings.museumConnectionString"].ConnectionString))
                {
                    connection.Open();
                    string sqlString = "SELECT * FROM info WHERE Category = " + category;
                    SqlCommand command = new SqlCommand(sqlString, connection);
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            /*for every row in the SqlDataReader we create an Exhibit object and then we fill in all its fields with the 
                              required data. Then we add the object to the ArrayList which is finally return from the display method*/
                            Exhibit exhibit = new Exhibit();

                            exhibit.id = (int)reader["id"];
                            exhibit.type = (string)reader["type"];
                            exhibit.place = (string)reader["place"];
                            exhibit.timing = (string)reader["timing"];
                            exhibit.dimensions = (string)reader["dimensions"];
                            exhibit.area = (string)reader["area"];
                            exhibit.description = (string)reader["description"];
                            exhibit.image = (string)reader["image"];
                            exhibit.category = (int)reader["category"];
                            exhibitList.Add(exhibit);
                        }
                    }
                }
            }
            catch (SqlException sql)
            {
                MessageBox.Show("The category exhibits could not be retrieved" + sql);
            }
            return exhibitList;
        }

        public Exhibit displayFromId(int id) 
        {
            Exhibit exhibit = new Exhibit();
            try
            {
                using (SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["museum.Properties.Settings.museumConnectionString"].ConnectionString))
                {
                    connection.Open();
                    string sqlString = "SELECT * FROM info WHERE id = " + id;
                    SqlCommand command = new SqlCommand(sqlString, connection);
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            exhibit.id = (int)reader["id"];
                            exhibit.type = (string)reader["type"];
                            exhibit.place = (string)reader["place"];
                            exhibit.timing = (string)reader["timing"];
                            exhibit.dimensions = (string)reader["dimensions"];
                            exhibit.area = (string)reader["area"];
                            exhibit.description = (string)reader["description"];
                            exhibit.image = (string)reader["image"];
                            exhibit.category = (int)reader["category"];
                        }
                    }
                }
            }
            catch (SqlException sql)
            {
                MessageBox.Show("The exhibit could not be retrieved!" + sql);
            }
            return exhibit;
        }



    
    }
}