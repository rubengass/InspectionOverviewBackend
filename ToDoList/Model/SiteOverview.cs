using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;

namespace ToDoList.Model
{
    public class SiteOverview
    {
        public List<Site> AllSites { get; set; }
        public int NumberOfSites { get; set; }
        private string DepartmentSearchString;
        public void FetchAllSites()
        {
            GetNumberOfSites();
            AllSites = new List<Site>();
            for (int i = 0; i < NumberOfSites; i++)
            {
                Site Object = new Site();
                Object.GetAllSiteDataWithiNumber(i);
                AllSites.Add(Object);
            }
        }

        public int GetNumberOfSites()
        {
            string connectionString = "datasource=127.0.0.1;port=3306;username=root;password=;database=inspectiondatabase;sslmode=none;";
            string query = "SELECT COUNT(*) FROM sites";
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
                        string Count = reader.GetString(0);
                        NumberOfSites = Int32.Parse(Count);
                    }
                }
                databaseConnection.Close();
            }
            catch (Exception)
            {
                Console.WriteLine("Failed to get the number of sites.");
            }
            return NumberOfSites;
        }

        public void FetchPaginatedSites(int NumberOfRowsMin,int NumberOfRowsMax)
        {
            int Maxi = NumberOfRowsMin + NumberOfRowsMax;
            NumberOfSites = GetNumberOfSites();
            if (NumberOfSites < Maxi){
                Maxi = NumberOfSites;
            }
            AllSites = new List<Site>();
            for (int i = NumberOfRowsMin; i < Maxi; i++)
            {
                Site Object = new Site();
                Object.GetAllSiteDataWithiNumber(i);
                AllSites.Add(Object);
            }
        }

        public void FetchSearchResults(string SiteIDSearch, string SiteNameSearch, string CustomerNameSearch, string ContractManagerNameSearch, string DepartmentNameSearch, int NumberOfFilters)
        {
            string query = "SELECT * FROM sites";
            int PreviousFilter = 0;
            if (NumberOfFilters > 0)
            {
                query = query + " WHERE";
                if(SiteIDSearch != null)
                {
                    query = query + " Site_Id LIKE '%" + SiteIDSearch + "%'";
                    PreviousFilter = 1;
                }
                if (SiteNameSearch != null)
                {
                    if(PreviousFilter == 1)
                    {
                        query = query + " AND Site_Name like '%" + SiteNameSearch + "%'";
                    }
                    else
                    {
                        query = query + " Site_Name LIKE '%" + SiteNameSearch + "%'";
                        PreviousFilter = 1;
                    }
                }
                if (CustomerNameSearch != null)
                {
                    if (PreviousFilter == 1)
                    {
                        query = query + " AND " + GetCustomerIdsMatchingFilter(CustomerNameSearch);
                    }
                    else
                    {
                        query = query + GetCustomerIdsMatchingFilter(CustomerNameSearch);
                        PreviousFilter = 1;
                    }
                }
                if (DepartmentNameSearch != null)
                {
                    DepartmentSearchString = GetDepartmentIdsMatchingFilter(DepartmentNameSearch);
                    if(ContractManagerNameSearch == null)
                    {
                        if (PreviousFilter == 1)
                        {
                            query = query + " AND " + GetContractManagerIdsMatchingFilter(null, DepartmentSearchString);
                        }
                        else
                        {
                            query = query + GetContractManagerIdsMatchingFilter(null, DepartmentSearchString);
                            PreviousFilter = 1;
                        }
                    }
                    else
                    {
                        if (PreviousFilter == 1)
                        {
                            query = query + " AND " + GetContractManagerIdsMatchingFilter(ContractManagerNameSearch, DepartmentSearchString);
                        }
                        else
                        {
                            query = query + GetContractManagerIdsMatchingFilter(ContractManagerNameSearch, DepartmentSearchString);
                            PreviousFilter = 1;
                        }
                    }
                }
                if (ContractManagerNameSearch != null && DepartmentNameSearch == null)
                {

                    if (PreviousFilter == 1)
                    {
                        query = query + " AND " + GetContractManagerIdsMatchingFilter(ContractManagerNameSearch, null);
                    }
                    else
                    {
                        query = query + GetContractManagerIdsMatchingFilter(ContractManagerNameSearch, null);
                        PreviousFilter = 1;
                    }
                }
            }
            query = query + ";";
            string connectionString = "datasource=127.0.0.1;port=3306;username=root;password=;database=inspectiondatabase;sslmode=none;";
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
                    AllSites = new List<Site>();
                    while (reader.Read())
                    {
                        Site Object = new Site();
                        Object.SiteID = reader.GetString(0);
                        Object.SiteName = reader.GetString(1);
                        Object.Customer = new Customer();
                        Object.Customer.CustomerID = reader.GetString(11);
                        Object.Customer.GetCustomerName(Object.Customer.CustomerID);
                        Object.ContractManager = new ContractManager();
                        Object.ContractManager.ContractManagerID = reader.GetString(12);
                        Object.ContractManager.GetContractManagerName(Object.ContractManager.ContractManagerID);
                        Object.ContractManager.GetDepartmentData();
                        AllSites.Add(Object);
                        NumberOfSites++;
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
                Console.WriteLine("Failed to retrieve Search results");
            }
        }

        public string GetCustomerIdsMatchingFilter(string SearchTerm)
        {
            string ReturnString = null;
            string connectionString = "datasource=127.0.0.1;port=3306;username=root;password=;database=inspectiondatabase;sslmode=none;";
            string query = "SELECT * FROM customers WHERE Customer_Name like '%"+SearchTerm+"%';";
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
                    int CountOfReads = 0;
                    while (reader.Read())
                    {
                        if(CountOfReads == 0)
                        {
                            ReturnString = " Customer_Id = " + reader.GetString(0);
                        }
                        else if(CountOfReads == 1)
                        {
                            ReturnString = "(" + ReturnString + " OR Customer_Id = " + reader.GetString(0);
                        } else if (CountOfReads > 1)
                        {
                            ReturnString = ReturnString + " OR Customer_Id = " + reader.GetString(0);
                        }
                        CountOfReads++;
                    }
                    if (CountOfReads > 1)
                    {
                        ReturnString = ReturnString + ")";
                    }
                }
                databaseConnection.Close();
            }
            catch (Exception)
            {
                Console.WriteLine("Failed to get customer ids matching the search term");
            }
            return ReturnString;
        }

        public string GetDepartmentIdsMatchingFilter(string SearchTerm)
        {
            string ReturnString = null;
            string connectionString = "datasource=127.0.0.1;port=3306;username=root;password=;database=inspectiondatabase;sslmode=none;";
            string query = "SELECT * FROM departments WHERE Department_Name like '%" + SearchTerm + "%';";
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
                    int CountOfReads = 0;
                    while (reader.Read())
                    {
                        if (CountOfReads == 0)
                        {
                            ReturnString = " Department_Id = " + reader.GetString(0);
                        }
                        else if (CountOfReads == 1)
                        {
                            ReturnString = "(" + ReturnString + " OR Department_Id = " + reader.GetString(0);
                        }
                        else if (CountOfReads > 1)
                        {
                            ReturnString = ReturnString + " OR Department_Id = " + reader.GetString(0);
                        }
                        CountOfReads++;
                    }
                    if (CountOfReads > 1)
                    {
                        ReturnString = ReturnString + ")";
                    }
                }
                databaseConnection.Close();
            }
            catch (Exception)
            {
                Console.WriteLine("Failed to get department ids matching the search term");
            }
            return ReturnString;
        }

        public string GetContractManagerIdsMatchingFilter(string SearchTerm, string DepartmentFilter)
        {
            string query = null;
            if(DepartmentFilter == null && SearchTerm != null)
            {                
                query = "SELECT * FROM contractmanagers WHERE ContractManager_Name like '%" + SearchTerm + "%';";
            }
            else if (DepartmentFilter != null && SearchTerm == null)
            {
                query = "SELECT * FROM contractmanagers WHERE "+DepartmentFilter +";";
            }
            else if (DepartmentFilter != null && SearchTerm != null)
            {
                query = "SELECT * FROM contractmanagers WHERE ContractManager_Name like '%" + SearchTerm + "%' AND " + DepartmentFilter + ";";
            }
            string ReturnString = null;
            string connectionString = "datasource=127.0.0.1;port=3306;username=root;password=;database=inspectiondatabase;sslmode=none;";
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
                    int CountOfReads = 0;
                    while (reader.Read())
                    {
                        if (CountOfReads == 0)
                        {
                            ReturnString = " ContractManager_Id = " + reader.GetString(0);
                        }
                        else if (CountOfReads == 1)
                        {
                            ReturnString = "(" + ReturnString + " OR ContractManager_Id = " + reader.GetString(0);
                        }
                        else if (CountOfReads > 1)
                        {
                            ReturnString = ReturnString + " OR ContractManager_Id = " + reader.GetString(0);
                        }
                        CountOfReads++;
                    }
                    if (CountOfReads > 1)
                    {
                        ReturnString = ReturnString + ")";
                    }
                }
                databaseConnection.Close();
            }
            catch (Exception)
            {
                Console.WriteLine("Failed to get contractmanager ids matching the search term");
            }
            return ReturnString;
        }
    }
}
