using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessLayer;
using System.Data.SqlClient;

namespace DataLayer
{
    public static class PackagesDB
    {
        public static List<Package> GetPackages()
        {
            SqlConnection connection = TravelExpertsDB.GetConnection();
            List<Package> results = new List<Package>();
            try
            {
                string sql = "SELECT PackageId, PkgName, PkgStartDate, PkgEndDate," +
                    " PkgDesc, PkgBasePrice, PkgAgencyCommission " +
                    " FROM Packages";
                SqlCommand command = new SqlCommand(sql, connection);
                SqlDataReader reader = command.ExecuteReader(System.Data.CommandBehavior.CloseConnection);

                while (reader.Read())
                {
                    Package p = new Package();

                    p.PackageID = Convert.ToInt32(reader["PackageId"].ToString());
                    p.PkgName = reader["PkgName"].ToString();
                    p.PkgStartDate = Convert.ToDateTime(reader["PkgStartDate"].ToString());
                    p.PkgEndDate = Convert.ToDateTime(reader["PkgEndDate"].ToString());
                    p.PkgDesc = reader["PkgDesc"].ToString();
                    p.PkgBasePrice = Convert.ToDecimal(reader["PkgBasePrice"].ToString());
                    p.PkgAgencyCommission = Convert.ToDecimal(reader["PkgAgencyCommission"].ToString());
                    results.Add(p);
                }
            }
            catch { }
            finally
            {
                connection.Close();
            }
            return results;
        }

        public static void AddPackage(string Name, DateTime StartDate, DateTime EndDate, string Description, decimal BasePrice, decimal Commission)
        {
            string sql = "INSERT INTO Packages" +
                " (PkgName, PkgStartDate, PkgEndDate, PkgDesc, PkgBasePrice, PkgAgencyCommission) " +
                " VALUES " +
                "(@PkgName, @PkgStartDate, @PkgEndDate, @PkgDesc, @PkgBasePrice, @PkgAgencyCommission)";
            SqlConnection connection = DataLayer.TravelExpertsDB.GetConnection();
            SqlCommand command = new SqlCommand(sql, connection);

            command.Parameters.AddWithValue("@PkgName", Name);
            command.Parameters.AddWithValue("@PkgStartDate", StartDate);
            command.Parameters.AddWithValue("@PkgEndDate", EndDate);
            command.Parameters.AddWithValue("@PkgDesc", Description);
            command.Parameters.AddWithValue("@BasePrice", BasePrice);
            command.Parameters.AddWithValue("@PkgAgencyComission", Commission);

            command.ExecuteNonQuery();
        }
    }
}
