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
				throw ex;
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
				throw ex;
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
				throw ex;
            }
            finally
            {
                connection.Close();
            }
            return results;

        }

		public static int GetMaxId()
		{

			SqlConnection connection = TravelExpertsDB.GetConnection();
			int result = 0;
			try
			{
				string sql = "SELECT MAX(SupplierId) AS MaxId FROM Suppliers";
				SqlCommand command = new SqlCommand(sql, connection);
				SqlDataReader reader =
					command.ExecuteReader(System.Data.CommandBehavior.CloseConnection);
				reader.Read();
				result = Convert.ToInt32(reader["MaxId"]);
			}
			catch (Exception ex)
			{
				throw ex;
			}
			finally
			{
				connection.Close();
			}
			return result;

		}


		public static bool UpdateSupplier (int id, string name)
        {
            SqlConnection connection = TravelExpertsDB.GetConnection();
            int rowAffected = 0;
            try
            {
                string sql = "UPDATE Suppliers SET SupName=@name " +
					" WHERE SupplierId=@id";
                SqlCommand command = new SqlCommand(sql, connection);

                command.Parameters.AddWithValue("@name", name);
				command.Parameters.AddWithValue("@id", id);
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

        public static bool AddSupplier(int id, string name)
        {
            SqlConnection connection = TravelExpertsDB.GetConnection();
            int rowAffected = 0;
            try
            {
                string sql = "INSERT INTO Suppliers (SupplierId, SupName) " +
                    " VALUES (@id, @name);";
                SqlCommand command = new SqlCommand(sql, connection);

				command.Parameters.AddWithValue("@id", id);
				command.Parameters.AddWithValue("@name", name);
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
