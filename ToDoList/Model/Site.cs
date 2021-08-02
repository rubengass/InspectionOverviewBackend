using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;

namespace ToDoList.Model
{
    public class Site
    {
        public string SiteID { get; set; }
        public string SiteName { get; set; }
        public string CustomerID { get; set; }
        public string CustomerName { get; set; }
        public string ContractManagerID { get; set; }
        public string ContractManagerName { get; set; }
        public string DepartmentID { get; set; }
        public string DepartmentName { get; set; }

        public void GetAllSiteDataWithiNumber(int iNumber)
        {
            string connectionString = "datasource=127.0.0.1;port=3306;username=root;password=;database=inspectiondatabase;sslmode=none;";
            string query = "SELECT * FROM sites LIMIT "+ iNumber + ",1";
            MySqlConnection databaseConnection = new MySqlConnection(connectionString);
            MySqlCommand commandDatabase = new MySqlCommand(query, databaseConnection);
            commandDatabase.CommandTimeout = 60;
            MySqlDataReader reader;

            try
            {
                databaseConnection.Open();
                reader = commandDatabase.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        SiteID = reader.GetString(0);
                        SiteName = reader.GetString(4);
                        CustomerID = reader.GetString(1);
                        ContractManagerID = reader.GetString(3);
                        DepartmentID = reader.GetString(2);
                    }
                }
                else
                {
                    Console.WriteLine("No rows found");

                }
                databaseConnection.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Failed to retrieve a word from db, fallback word is selected.");
            }
            Department Dep = new Department();
            DepartmentName = Dep.GetDepartmentName(DepartmentID);
            Customer Cus = new Customer();
            CustomerName = Cus.GetCustomerName(CustomerID);
            ContractManager Con = new ContractManager();
            ContractManagerName = Con.GetContractManagerName(ContractManagerID);
        }

        public void GetSiteID(string SiteName)
        {
            string connectionString = "datasource=127.0.0.1;port=3306;username=root;password=;database=inspectiondatabase;sslmode=none;";
            string query = "SELECT * FROM sites WHERE Site_Name = '" + SiteName + "'";
            MySqlConnection databaseConnection = new MySqlConnection(connectionString);
            MySqlCommand commandDatabase = new MySqlCommand(query, databaseConnection);
            commandDatabase.CommandTimeout = 60;
            MySqlDataReader reader;

            try
            {
                databaseConnection.Open();
                reader = commandDatabase.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        SiteID = reader.GetString(0);
                    }
                }
                else
                {
                    Console.WriteLine("No rows found");

                }
                databaseConnection.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Failed to retrieve a word from db, fallback word is selected.");
            }
        }

        public void GetSiteName(string SiteID)
        {
            string connectionString = "datasource=127.0.0.1;port=3306;username=root;password=;database=inspectiondatabase;sslmode=none;";
            string query = "SELECT * FROM sites WHERE Site_Id ='" + SiteID + "'";
            MySqlConnection databaseConnection = new MySqlConnection(connectionString);
            MySqlCommand commandDatabase = new MySqlCommand(query, databaseConnection);
            commandDatabase.CommandTimeout = 60;
            MySqlDataReader reader;

            try
            {
                databaseConnection.Open();
                reader = commandDatabase.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        SiteName = reader.GetString(4);
                    }
                }
                else
                {
                    Console.WriteLine("No rows found");

                }
                databaseConnection.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Failed to retrieve a word from db, fallback word is selected.");
            }
        }

        public string CreateNewSite()
        {
            Department Dep = new Department();
            DepartmentID = Dep.GetDepartmentId(DepartmentName);
            int DepartmentId = Int32.Parse(DepartmentID);
            Customer Cus = new Customer();
            CustomerID = Cus.GetCustomerId(CustomerName);
            int CustomerId = Int32.Parse(CustomerID);
            ContractManager Con = new ContractManager();
            ContractManagerID = Con.GetContractManagerId(ContractManagerName);
            int ContractManagerId = Int32.Parse(ContractManagerID);

            string connectionString = "datasource=127.0.0.1;port=3306;username=root;password=;database=inspectiondatabase;sslmode=none;";
            string query = "INSERT INTO sites(Customer_Id,Department_Id,ContractManager_Id,Site_Name) values('" + CustomerId + "','" + DepartmentId + "','" + ContractManagerId + "','" + SiteName + "')";
            MySqlConnection databaseConnection = new MySqlConnection(connectionString);
            MySqlCommand commandDatabase = new MySqlCommand(query, databaseConnection);
            commandDatabase.CommandTimeout = 60;
            MySqlDataReader reader;

            try
            {
                databaseConnection.Open();
                reader = commandDatabase.ExecuteReader();
                databaseConnection.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Failed to create a new entry");
            }
            GetSiteID(SiteName);
            return SiteID;
        }
    }
}
