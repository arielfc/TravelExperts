using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using BusinessLayer;

namespace DataLayer
{
    class Packages_Products_SuppliersDB
    {
        public List<Packages_Products_Supplier> GetPackages_Products_Suppliers()
        {
            SqlConnection connection = TravelExpertsDB.GetConnection();
            List<Packages_Products_Supplier> results = new List<Packages_Products_Supplier>();
            try
            {
                string sql = "SELECT * FROM Packages_Products_Suppliers";
                SqlCommand command = new SqlCommand(sql, connection);
                SqlDataReader reader =
                    command.ExecuteReader(System.Data.CommandBehavior.CloseConnection);
                while (reader.Read())
                {
                    Packages_Products_Supplier s = new Packages_Products_Supplier();

                    s.PackageId = Convert.ToInt32(reader["PackageId"]);
                    s.ProductSupplierId = Convert.ToInt32(reader["ProductId"]);
                    results.Add(s);
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                connection.Close();
            }
            return results;
        }

        public bool UpdatePPS(int pid, int psid)
        {
            SqlConnection connection = TravelExpertsDB.GetConnection();
            int rowAffected = 0;
            try
            {
                string sql = "UPDATE Packages_Products_Suppliers SET ProductSupplierId=@psid " +
                    " WHERE PackageId=@pid";
                SqlCommand command = new SqlCommand(sql, connection);

                command.Parameters.AddWithValue("@psid", psid);
                command.Parameters.AddWithValue("@pid", pid);
                rowAffected = command.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                connection.Close();
            }
            if (rowAffected > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool AddPPS(int pid, int psid)
        {
            SqlConnection connection = TravelExpertsDB.GetConnection();
            int rowAffected = 0;
            try
            {
                string sql = "INSERT INTO Packages_Products_Suppliers (PackageId, " +
                    " ProductSupplierId) VALUES (@pid, @psid);";
                SqlCommand command = new SqlCommand(sql, connection);

                command.Parameters.AddWithValue("@pid", pid);
                command.Parameters.AddWithValue("@psid", psid);
                rowAffected = command.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                connection.Close();
            }
            if (rowAffected > 0)
            {
                return true;
            }
            else
            {
                return false;
            }

        }
    }
}
