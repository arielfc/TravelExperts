// Created by Yue Yang 20190527
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using BusinessLayer;

namespace DataLayer
{
    public class Products_SuppliersDB
    {
        public static List<Product_Supplier> GetProducts_Suppliers()
        {
            SqlConnection connection = TravelExpertsDB.GetConnection();
            List<Product_Supplier> results = new List<Product_Supplier>();
            try
            {
                string sql = "SELECT ProductSupplierID, ProductId, " +
                    " SupplierId FROM Products_Suppliers";
                SqlCommand command = new SqlCommand(sql, connection);
                SqlDataReader reader =
                    command.ExecuteReader(System.Data.CommandBehavior.CloseConnection);
                while (reader.Read())
                {
                    Product_Supplier s = new Product_Supplier();

                    s.ProductSupplierId = Convert.ToInt32(reader["ProductSupplierId"]);
                    s.ProductId = Convert.ToInt32(reader["ProductId"]);
                    s.SupplierId = Convert.ToInt32(reader["SupplierId"]);
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
        public static int GetPSID_By_P_SID(int pid, int sid)
        {

            SqlConnection connection = TravelExpertsDB.GetConnection();
            int result = 0;

            try
            {
                string sql = "SELECT ProductSupplierId " +
                    " FROM Products_Suppliers " +
                    " WHERE ProductId=" + pid + " AND SupplierId=" + sid;

                SqlCommand command = new SqlCommand(sql, connection);
                SqlDataReader reader =
                    command.ExecuteReader(System.Data.CommandBehavior.CloseConnection);
                reader.Read();

                result = Convert.ToInt32(reader["ProductSupplierId"]);
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
        public static List<Supplier> GetSuppliersByProductID(int pid)
        {
            SqlConnection connection = TravelExpertsDB.GetConnection();
            List<Supplier> results = new List<Supplier>();
            try
            {
                string sql = "SELECT SupplierId, SupName FROM Suppliers " +
                    " WHERE SupplierId = ANY (SELECT SupplierId " +
                    " FROM Products_Suppliers " +
                    " WHERE ProductId =" + pid + ")";
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

        public static bool UpdatePS(int psid, int pid, int sid)
        {
            SqlConnection connection = TravelExpertsDB.GetConnection();
            Product_Supplier result = new Product_Supplier();
            int rowAffected = 0;
            try
            {
                string sql = "UPDATE Products_Suppliers SET ProductId=@pid," +
                    " SupplierId=@sid " +
                    " WHERE ProductSupplierId=@psid";
                SqlCommand command = new SqlCommand(sql, connection);

                command.Parameters.AddWithValue("@psid", psid);
                command.Parameters.AddWithValue("@pid", pid);
                command.Parameters.AddWithValue("@sid", sid);
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

        public static bool AddPS(int pid, int sid)
        {
            SqlConnection connection = TravelExpertsDB.GetConnection();
            Product_Supplier result = new Product_Supplier();
            int rowAffected = 0;
            try
            {
                string sql = "INSERT INTO Products_Suppliers (ProductId, " +
                    " SupplierId) " +
                    " VALUES (@pid, @sid);";
                SqlCommand command = new SqlCommand(sql, connection);

                command.Parameters.AddWithValue("@pid", pid);
                command.Parameters.AddWithValue("@sid", sid);
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
