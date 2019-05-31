using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data;
using BusinessLayer;

namespace DataLayer
{
    public class ProductsDB
    {
        public static List<Product> GetProducts()
        {
            SqlConnection connection = TravelExpertsDB.GetConnection();
            List<Product> results = new List<Product>();
            try
            {
                string sql = "SELECT ProductId, ProdName FROM Products";
                SqlCommand command = new SqlCommand(sql, connection);
                SqlDataReader reader =
                    command.ExecuteReader(System.Data.CommandBehavior.CloseConnection);
                while (reader.Read())
                {
                    Product s = new Product();
                    s.ProductId = Convert.ToInt32(reader["ProductId"]);
                    s.ProdName = reader["ProdName"].ToString();
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

        public static Product GetProductByID(int id)
        {

            SqlConnection connection = TravelExpertsDB.GetConnection();
            Product result = new Product();
            try
            {
                string sql = "SELECT ProductId, ProdName FROM Products " +
                    " WHERE ProductId=" + id;
                SqlCommand command = new SqlCommand(sql, connection);
                SqlDataReader reader =
                    command.ExecuteReader(System.Data.CommandBehavior.CloseConnection);
                reader.Read();

                result.ProductId = Convert.ToInt32(reader["ProductId"]);
                result.ProdName = reader["ProdName"].ToString();
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

        public static int GetProductId(string name)
        {

            SqlConnection connection = TravelExpertsDB.GetConnection();
            int result = 0;
            try
            {
                string sql = "SELECT ProductId FROM Products " +
                    " WHERE ProdName = '" + name + "'";
                SqlCommand command = new SqlCommand(sql, connection);
                SqlDataReader reader =
                    command.ExecuteReader(System.Data.CommandBehavior.CloseConnection);
                reader.Read();
                result = Convert.ToInt32(reader["ProductId"]);
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

        public static bool UpdateProduct(int id, string name)
        {
            SqlConnection connection = TravelExpertsDB.GetConnection();
            int rowAffected = 0;
            try
            {
                string sql = "UPDATE Products SET ProdName=@name " +
                    " WHERE ProductId=@id";
                SqlCommand command = new SqlCommand(sql, connection);

                command.Parameters.AddWithValue("@name", name);
                command.Parameters.AddWithValue("@id", id);
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

        public static bool AddProduct(string name)
        {
            SqlConnection connection = TravelExpertsDB.GetConnection();
            int rowAffected = 0;
            try
            {
                string sql = "INSERT INTO Products (ProdName) " +
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
