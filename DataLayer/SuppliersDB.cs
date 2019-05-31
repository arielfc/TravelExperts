using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using BusinessLayer;

namespace DataLayer
{
    public  class SuppliersDB
    {
        public static List<Supplier> GetSuppliers()
        {
            SqlConnection connection = TravelExpertsDB.GetConnection();
            List<Supplier> results = new List<Supplier>();
            try
            {
                string sql = "SELECT SupplierId, SupName FROM Suppliers";
                SqlCommand command = new SqlCommand(sql, connection);
                SqlDataReader reader =
                    command.ExecuteReader(System.Data.CommandBehavior.CloseConnection);
                while (reader.Read())
                {
                    Supplier s = new Supplier();
                    s.SupplierId = Convert.ToInt32(reader["SupplierId"]);
                    s.SupName = reader["SupName"].ToString();
                    results.Add(s);
                }

            }
            catch (Exception ex)
            {

            }
            finally
            {
                connection.Close();
            }
            return results;
        }
        public Supplier GetSupplierByID(int id)
        {
            SqlConnection connection = TravelExpertsDB.GetConnection();
            Supplier result = new Supplier();
            try
            {
                string sql = "SELECT SupplierId, SupName FROM Suppliers " +
                    " WHERE SupplierId=" + id;
                SqlCommand command = new SqlCommand(sql, connection);
                SqlDataReader reader =
                    command.ExecuteReader(System.Data.CommandBehavior.CloseConnection);
                reader.Read();

                result.SupplierId = Convert.ToInt32(reader["SupplierId"]);
                result.SupName = reader["SupName"].ToString();
            }
            catch (Exception ex)
            {

            }
            finally
            {
                connection.Close();
            }
            return result;

        }
        public static List<Supplier> GetSupplierListByID(int id)
        {
            SqlConnection connection = TravelExpertsDB.GetConnection();
            List<Supplier> results = new List<Supplier>();
            try
            {
                string sql = "SELECT SupplierId, SupName FROM Suppliers " +
                    " WHERE SupplierId=" + id;
                SqlCommand command = new SqlCommand(sql, connection);
                SqlDataReader reader =
                    command.ExecuteReader(System.Data.CommandBehavior.CloseConnection);
                while(reader.Read())
                {
                    Supplier s = new Supplier();
                    s.SupplierId = Convert.ToInt32(reader["SupplierId"]);
                    s.SupName = reader["SupName"].ToString();
                    results.Add(s);
                }

            }
            catch (Exception ex)
            {

            }
            finally
            {
                connection.Close();
            }
            return result;

        }

        public bool UpdateSupplier (int id, string name)
        {
            SqlConnection connection = TravelExpertsDB.GetConnection();
            Product result = new Product();
            int rowAffected = 0;
            try
            {
                string sql = "UPDATE Supplier SET SupName=@name " +
                    " WHERE ProductId=@id";
                SqlCommand command = new SqlCommand(sql, connection);

                command.Parameters.AddWithValue("@name", name);
                rowAffected = command.ExecuteNonQuery();
            }
            catch (Exception ex)
            {

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

        public bool AddSupplier(string name)
        {
            SqlConnection connection = TravelExpertsDB.GetConnection();
            Product result = new Product();
            int rowAffected = 0;
            try
            {
                string sql = "INSERT INTO Suppliers (SupName) " +
                    " VALUES (@name);";
                SqlCommand command = new SqlCommand(sql, connection);

                command.Parameters.AddWithValue("@name", name);
                rowAffected = command.ExecuteNonQuery();
            }
            catch (Exception ex)
            {

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
