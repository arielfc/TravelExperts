using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;

namespace DataLayer
{
    class TravelExpertsDB
    {
        public static SqlConnection GetConnection()
        {
            // Establish Connection to DB
            
            SqlConnection connection = new SqlConnection();
            string ConnectionString = "Data Source=DESKTOP-JE6DIRU;" +
                "Initial Catalog=TravelExperts;Integrated Security=true";
            connection.ConnectionString = ConnectionString;
            /*connection.Open();
            return connection;
            */
            // Remote DB
            /*
            SqlConnectionStringBuilder builder = new SqlConnectionStringBuilder();
            builder.DataSource = "softdev0523.database.windows.net";
            builder.UserID = "softdevlogin";
            builder.Password = "softDevma3$";
            builder.InitialCatalog = "TravelExperts";
            SqlConnection connection = new SqlConnection(builder.ConnectionString);
            */
            try
            {
                connection.Open();
            }
            catch (Exception ex)
            {

            }
            finally
            {

            }
            return connection;
        }
    }
}
