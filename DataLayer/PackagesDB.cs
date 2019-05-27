using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessLayer;
using System.Data.SqlClient;

namespace DataLayer
{
    public class PackagesDB
    {
        public List<Package> GetPackage()
        {

            List<Package> result = new List<Package>();

            //Create the SQL Query for returning all the articles
            string sqlQuery = String.Format("select * from Packages");

            //Create and open a connection to SQL Server 
            SqlConnection connection = TravelExpertsDB.GetConnection();
            if (connection.State != System.Data.ConnectionState.Open)
                connection.Open();

            SqlCommand command = new SqlCommand(sqlQuery, connection);

            //Create DataReader for storing the returning table into server memory
            SqlDataReader dataReader = command.ExecuteReader();

            Package package = null;

            //load into the result object the returned row from the database
            if (dataReader.HasRows)
            {
                while (dataReader.Read())
                {
                    package = new Package();

                    package.PackageID = Convert.ToInt32(dataReader["PackageID"]);
                    package.PkgName = dataReader["PkgName"].ToString();
                    package.PkgStartDate = Convert.ToDateTime(dataReader["PkgStartDate"]);
                    package.PkgEndDate = Convert.ToDateTime(dataReader["PkgEndDate"]);
                    package.PkgDesc = dataReader["PkgDesc"].ToString();
                    package.PkgBasePrice = Convert.ToInt32(dataReader["PkgBasePrice"]);
                    package.PkgAgencyCommission = Convert.ToInt32(dataReader["PkgAgencyCommission"]);

                    result.Add(package);
                }
            }
            return result;

        }

        public Package GetPackageById(int packageId)
        {
            Package result = new Package();

            //Create the SQL Query for returning a package based on its primary key
            string sqlQuery = String.Format("select * from Packages where PackageId={0}", packageId);

            //Create and open a connection to SQL Server 
            SqlConnection connection = TravelExpertsDB.GetConnection();
            if (connection.State != System.Data.ConnectionState.Open)
                connection.Open();

            SqlCommand command = new SqlCommand(sqlQuery, connection);

            SqlDataReader dataReader = command.ExecuteReader();

            //load into the result object the returned row from the database
            if (dataReader.HasRows)
            {
                while (dataReader.Read())
                {
                    result.PackageID = Convert.ToInt32(dataReader["PackageID"]);
                    result.PkgName = dataReader["PkgName"].ToString();
                    result.PkgStartDate = Convert.ToDateTime(dataReader["PkgStartDate"]);
                    result.PkgEndDate = Convert.ToDateTime(dataReader["PkgEndDate"]);
                    result.PkgDesc = dataReader["PkgDesc"].ToString();
                    result.PkgBasePrice = Convert.ToInt32(dataReader["PkgBasePrice"]);
                    result.PkgAgencyCommission = Convert.ToInt32(dataReader["PkgAgencyCommission"]);
                }
            }

            return result;
        }

        public int SavePackage(Package package)
        {
            //Create the SQL Query for inserting a package
            string createQuery = String.Format("Insert into Packages (PkgName, PkgStartDate , PkgEndDate, PkgDesc, PkgBasePrice, PkgAgencyCommission) Values('{0}', '{1}', '{2}', '{3}', '{4}', {5});"
            + "Select @@Identity", package.PkgName, package.PkgStartDate, package.PkgEndDate, package.PkgDesc, package.PkgBasePrice, package.PkgAgencyCommission);

            //Create the SQL Query for updating a package
            string updateQuery = String.Format("Update Articles SET PkgName='{0}', PkgStartDate = '{1}', PkgEndDate ='{2}', PkgDesc = '{3}', PkgBasePrice = '{4}', PkgAgencyCommission = {5} Where PackageID = {6};",
                package.PkgName, package.PkgStartDate, package.PkgEndDate, package.PkgDesc, package.PkgBasePrice, package.PkgAgencyCommission, package.PackageID);

            //Create and open a connection to SQL Server 
            SqlConnection connection = TravelExpertsDB.GetConnection();
            if (connection.State != System.Data.ConnectionState.Open)
                connection.Open();

            //Create a Command object
            SqlCommand command = null;

            if (package.PackageID != 0)
                command = new SqlCommand(updateQuery, connection);
            else
                command = new SqlCommand(createQuery, connection);

            int savedPackageID = 0;
            try
            {
                //Execute the command to SQL Server and return the newly created ID
                var commandResult = command.ExecuteScalar();
                if (commandResult != null)
                {
                    savedPackageID = Convert.ToInt32(commandResult);
                }
                else
                {
                    //the update SQL query will not return the primary key but if doesn't throw exception 
                    //then we will take it from the already provided data
                    savedPackageID = package.PackageID;
                }
            }
            catch (Exception ex)
            {
                //there was a problem executing the script
            }

            //Close and dispose
            command.Dispose();
            connection.Close();
            connection.Dispose();

            return savedPackageID;
        }
        //public List<Package> GetPackages()
        //{
        //    SqlConnection connection = TravelExpertsDB.GetConnection();
        //    List<Package> results = new List<Package>();
        //    try
        //    {
        //        string sql = "SELECT PackageId, PkgName, PkgStartDate, PkgEndDate, PkgDesc, PkgBasePrice, PkgAgencyCommission " +
        //            "FROM Packages ";
        //        SqlCommand command = new SqlCommand(sql, connection);
        //        SqlDataReader reader = command.ExecuteReader(System.Data.CommandBehavior.CloseConnection);

        //        while (reader.Read())
        //        {
        //            Package p = new Package();

        //            p.PackageID = Convert.ToInt32(reader["PackageId"].ToString());
        //            p.PkgName = reader["PkgName"].ToString();
        //            p.PkgStartDate = Convert.ToDateTime(reader["PkgStartDate"].ToString());
        //            p.PkgEndDate = Convert.ToDateTime(reader["PkgEndDate"].ToString());
        //            p.PkgDesc = reader["PkgDesc"].ToString();
        //            p.PkgBasePrice = Convert.ToDecimal(reader["PkgBasePrice"].ToString());
        //            p.PkgAgencyCommission = Convert.ToDecimal(reader["PkgAgencyCommission"].ToString());
        //            results.Add(p);
        //        }
        //    }
        //    catch { }
        //    finally
        //    {
        //        connection.Close();
        //    }
        //    return results;
        //}
        //public int InsertPackage(Package package)
        //{

        //    //Create the SQL Query for inserting an article
        //    string sqlQuery = String.Format("Insert into Packages (PkgName, PkgStartDate , PkgEndDate, PkgDesc, PkgBasePrice, PkgAgencyCommission) Values('{0}', '{1}', '{2}', '{3}', '{4}', {5});"
        //    + "Select @@Identity", package.PkgName, package.PkgStartDate, package.PkgEndDate, package.PkgDesc, package.PkgBasePrice, package.PkgAgencyCommission);

        //    //Create and open a connection to SQL Server 
        //    SqlConnection connection = TravelExpertsDB.GetConnection();
        //    if (connection.State != System.Data.ConnectionState.Open)
        //        connection.Open();

        //    //Create a Command object
        //    SqlCommand command = new SqlCommand(sqlQuery, connection);

        //    //Execute the command to SQL Server and return the newly created ID
        //    int newPackageID = Convert.ToInt32((decimal)command.ExecuteScalar());

        //    //Close and dispose
        //    command.Dispose();
        //    connection.Close();
        //    connection.Dispose();

        //    // Set return value
        //    return newPackageID;
        //}

        //public static void AddPackage(string Name, DateTime StartDate, DateTime EndDate, string Description, decimal BasePrice, decimal Commission)
        //{
        //    string sql = "INSERT INTO Packages" +
        //        " (PkgName, PkgStartDate, PkgEndDate, PkgDesc, PkgBasePrice, PkgAgencyCommission) " +
        //        " VALUES " +
        //        "(@PkgName, @PkgStartDate, @PkgEndDate, @PkgDesc, @PkgBasePrice, @PkgAgencyCommission)";
        //    SqlConnection connection = DataLayer.TravelExpertsDB.GetConnection();
        //    SqlCommand command = new SqlCommand(sql, connection);

        //    command.Parameters.AddWithValue("@PkgName", Name);
        //    command.Parameters.AddWithValue("@PkgStartDate", StartDate);
        //    command.Parameters.AddWithValue("@PkgEndDate", EndDate);
        //    command.Parameters.AddWithValue("@PkgDesc", Description);
        //    command.Parameters.AddWithValue("@BasePrice", BasePrice);
        //    command.Parameters.AddWithValue("@PkgAgencyComission", Commission);

        //    command.ExecuteNonQuery();
        //}
    }
}
