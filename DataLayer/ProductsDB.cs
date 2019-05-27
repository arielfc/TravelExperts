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
                    s.ProductId = Convert.ToInt32(reader["ProductId"].ToString());
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

        /*
        public static DataSet LoadProducts()
        {
            SqlConnection connection = TravelExpertsDB.GetConnection();
            DataSet products = new DataSet();
            try
            {
                string sql = "SELECT ProductId, ProdName FROM Products";
                SqlDataAdapter adapter = new SqlDataAdapter(sql, connection);

                products = new DataSet();
                adapter.Fill(products, "Products");
            }
            catch (Exception ex)
            {

            }
            finally
            {
                connection.Close();
            }
            return products;
        }
        */
    }
}
