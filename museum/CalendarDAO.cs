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
    class CalendarDAO
    {
        public List<Calendar> getAllCalendarData()
        {
            List<Calendar> calendarList = new List<Calendar>();
            try
            {
                using (SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["museum.Properties.Settings.museumConnectionString"].ConnectionString))
                {
                    connection.Open();
                    string sqlQuery = "SELECT * FROM calendar";
                    SqlCommand command = new SqlCommand(sqlQuery, connection);
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Calendar calendar = new Calendar();
                            calendar.id = (int)reader["id"];
                            calendar.eventTitle = (string)reader["EventTitle"];
                            calendar.date = (DateTime)reader["Date"];
                            calendar.text = (string)reader["Text"];
                            calendar.image = (string)reader["image"];
                            calendarList.Add(calendar);
                        }
                    }
                }
            }
            catch (SqlException sql)
            {
                MessageBox.Show("The calendar data could not be retrieved!" + sql);
                return null;
            }
            return calendarList;
        }

        public Calendar getSingleCalendarData(int id) 
        {
            Calendar cal = new Calendar();
            try
            {
                using (SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["museum.Properties.Settings.museumConnectionString"].ConnectionString))
                {
                    connection.Open();
                    string sqlQuery = "SELECT * FROM calendar WHERE id = " + id;
                    SqlCommand command = new SqlCommand(sqlQuery, connection);
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            cal.id = (int)reader["id"];
                            cal.eventTitle = (string)reader["EventTitle"];
                            cal.date = (DateTime)reader["Date"];
                            cal.text = (string)reader["Text"];
                            cal.image = (string)reader["image"];
                        }
                    }
                }
            }
            catch (SqlException sql)
            {
                MessageBox.Show("The single calendar data could not be retrieved!" + sql);
                return null;
            }
            return cal;
        }
    }
}
