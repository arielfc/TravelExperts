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
            string ConnectionString = "Data Source=DESKTOP-KCKTTB9\\SQLEXPRESS;Initial Catalog=TravelExperts;Integrated Security=true";
            connection.ConnectionString = ConnectionString;
            connection.Open();
            return connection;
        }
    }
}
