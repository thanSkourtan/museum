using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Windows.Forms;
using System.Configuration;

namespace museum
{
    class VisitorBooksEntryDAO
    {
        public List<VisitorBooksEntry> getAllVisitorsBookData() {

            List<VisitorBooksEntry> visitorsBookEntryList = new List<VisitorBooksEntry>();
            try
            {
                using(SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["museum.Properties.Settings.museumConnectionString"].ConnectionString))
                {
                    connection.Open();
                    string sqlString = "SELECT * FROM Visitorsbook";
                    SqlCommand command = new SqlCommand(sqlString, connection);
                    SqlDataReader reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        var vbe = new VisitorBooksEntry();
                        vbe.id = (int)reader["id"];
                        vbe.name = (string)reader["name"];
                        vbe.text = (string)reader["text"];
                        visitorsBookEntryList.Add(vbe);
                    }
                }
            }
            catch (SqlException sql){
                MessageBox.Show("The data from the visitors book could not be collected." + sql);
            }

            return visitorsBookEntryList;
        }


        public bool addVisitorMessage(VisitorBooksEntry entry) 
        { 
            try{
                    using (SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["museum.Properties.Settings.museumConnectionString"].ConnectionString))
                    {
                        connection.Open();
                        string sqlQuery = "INSERT INTO visitorsbook (text,name) VALUES (@text,@name)";
                        SqlCommand command = new SqlCommand(sqlQuery, connection);
                        command.Parameters.AddWithValue("@text", entry.text);
                        command.Parameters.AddWithValue("@name", entry.name);
                        int oe = command.ExecuteNonQuery();
                    }
                }catch(SqlException sql){
                    MessageBox.Show("The entry could not be inserted" + sql);
                    return false;
                }
                return true;
        
        }   



    }
}
