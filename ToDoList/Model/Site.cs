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
        public Customer Customer { get; set; }
        public ContractManager ContractManager { get; set; }
        public Department Department { get; set; }
        public string cusID;
        public string conID;
        public string depID;
        public string cusName;
        public string conName;
        public string depName;

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
                        cusID = reader.GetString(1);
                        conID = reader.GetString(3);
                        depID = reader.GetString(2);
                    }
                }
                else
                {
                    Console.WriteLine("No rows found");

                }
                databaseConnection.Close();
            }
            catch (Exception)
            {
                Console.WriteLine("Failed to retrieve Sites");
            }
            Department = new Department();
            Department.DepartmentID = depID;
            Department.GetDepartmentName(depID);
            ContractManager = new ContractManager();
            ContractManager.ContractManagerID = conID;
            ContractManager.GetContractManagerName(conID);
            Customer = new Customer();
            Customer.CustomerID = cusID;
            Customer.GetCustomerName(cusID);
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
            catch (Exception)
            {
                Console.WriteLine("Failed to retrieve SiteID");
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
            catch (Exception)
            {
                Console.WriteLine("Failed to retrieve SiteName");
            }
        }

        public string CreateNewSite()
        {
            depID = Department.GetDepartmentId(Department.DepartmentName);
            conID = ContractManager.GetContractManagerId(ContractManager.ContractManagerName);
            cusID = Customer.GetCustomerId(Customer.CustomerName);

            string connectionString = "datasource=127.0.0.1;port=3306;username=root;password=;database=inspectiondatabase;sslmode=none;";
            string query = "INSERT INTO sites(Customer_Id,Department_Id,ContractManager_Id,Site_Name) values('" + cusID + "','" + depID + "','" + conID + "','" + SiteName + "')";
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
            catch (Exception)
            {
                Console.WriteLine("Failed to create a new site");
            }
            GetSiteID(SiteName);
            return SiteID;
        }
    }
}
