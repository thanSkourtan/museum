using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Windows.Forms;
using System.Configuration;
using System.IO;
using System.Data;
using System.Runtime.Serialization.Formatters.Binary;

namespace museum
{
    class SessionListObjectDAO
    {

        public void InsertSessionListObject(SessionListObject slo)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["museum.Properties.Settings.museumConnectionString"].ConnectionString))
                {
                    connection.Open();
                    /******************************************************************/
                    MemoryStream memStream = new MemoryStream();
                    //StreamWriter sw = new StreamWriter(memStream);
                    BinaryFormatter bf = new BinaryFormatter();
                    bf.Serialize(memStream,slo.sessionList);
                    

                    //sw.Write(slo.sessionList);
                    /************************************************************************/
                    string query = "INSERT INTO SessionList (UserId,Session,SessionName) VALUES(@userId,@session,@sessionname)";
                    SqlCommand command = new SqlCommand(query,connection);
                    command.Parameters.AddWithValue("@userId", slo.userId);
                    command.Parameters.AddWithValue("@sessionname", slo.sessionName);


                    //command.Parameters.Add("@session", SqlDbType.VarBinary, Int32.MaxValue);

                    command.Parameters.AddWithValue("@session", memStream.GetBuffer());

                    command.ExecuteNonQuery();
                }
            }
            catch (SqlException sql) 
            {
                MessageBox.Show("The SessionList could not be entered " + sql);
            }

        }


        public List<SessionListObject> getAllSessionListsForUser(int userId)
        {
            List<SessionListObject> myList = new List<SessionListObject>();
            try
            {
                using (SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["museum.Properties.Settings.museumConnectionString"].ConnectionString))
                {
                    connection.Open();
                    string query = "SELECT * FROM SessionList WHERE userId = " + userId;
                    SqlCommand command = new SqlCommand(query, connection);
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            var sessionListObject = new SessionListObject();
                            sessionListObject.id = (int)reader["id"];
                            sessionListObject.userId = (int)reader["UserId"];
                            sessionListObject.sessionName = (string)reader["SessionName"];

                            byte[] tempArray = (byte[])reader["Session"];
                            //sessionListObject.sessionList = (List<SessionObject>)reader["Session"];
                            if (tempArray != null)
                            {
                                MemoryStream ms = new MemoryStream(tempArray);
                                BinaryFormatter binForm = new BinaryFormatter();
                                sessionListObject.sessionList = (List<SessionObject>)binForm.Deserialize(ms);
                            }
                            myList.Add(sessionListObject);
                        }
                    }
                }
            }
            catch (SqlException sql)
            {
                MessageBox.Show("The SessionList could not be entered " + sql);
            }
            return myList;
        }


        public SessionListObject getSingleSessionListObject(int id)
        {
            SessionListObject mySessionObject = new SessionListObject();
            try
            {
                using (SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["museum.Properties.Settings.museumConnectionString"].ConnectionString))
                {
                    connection.Open();
                    string query = "SELECT * FROM SessionList WHERE id = " + id;
                    SqlCommand command = new SqlCommand(query, connection);
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            mySessionObject.id = (int)reader["id"];
                            mySessionObject.userId = (int)reader["UserId"];
                            mySessionObject.sessionName = (string)reader["SessionName"];

                            byte[] tempArray = (byte[])reader["Session"];
                            //sessionListObject.sessionList = (List<SessionObject>)reader["Session"];
                            if (tempArray != null)
                            {
                                MemoryStream ms = new MemoryStream(tempArray);
                                BinaryFormatter binForm = new BinaryFormatter();
                                mySessionObject.sessionList = (List<SessionObject>)binForm.Deserialize(ms);
                            }
                           
                        }
                    }
                }
            }
            catch (SqlException sql)
            {
                MessageBox.Show("The SessionObject could not be entered " + sql);
            }
            return mySessionObject;
        
        }


    }
}
