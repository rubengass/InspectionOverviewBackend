using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;

namespace ToDoList.Model
{
    public class Form
    {
        private int NumberOfRecords;
        public List<Department> Departments { get; set; }
        public List<ContractManager> ContractManagers { get; set; }
        public List<Customer> Customers { get; set; }

        public int GetNumberOfRecords(string table)
        {
            string connectionString = "datasource=127.0.0.1;port=3306;username=root;password=;database=inspectiondatabase;sslmode=none;";
            string query = "SELECT COUNT(*) FROM " + table;
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
                        NumberOfRecords = Int32.Parse(Count);
                    }
                }
                databaseConnection.Close();
            }
            catch (Exception)
            {
                Console.WriteLine("Failed to get the number of sites.");
            }
            return NumberOfRecords;
        }

        public int GetNumberOfFilteredRecords(string table, string ColumnName, string RowValue)
        {
            string connectionString = "datasource=127.0.0.1;port=3306;username=root;password=;database=inspectiondatabase;sslmode=none;";
            string query = "SELECT COUNT(*) FROM " + table +" WHERE "+ColumnName+" =  '"+RowValue+"'";
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
                        NumberOfRecords = Int32.Parse(Count);
                    }
                }
                databaseConnection.Close();
            }
            catch (Exception)
            {
                Console.WriteLine("Failed to get the number of filtered sites.");
            }
            return NumberOfRecords;
        }

        public void GetAllDepartmentNames()
        {
            NumberOfRecords = GetNumberOfRecords("departments");
            Departments = new List<Department>();
            for (int i = 0; i < NumberOfRecords; i++)
            {
                Department Object = new Department();
                Object.GetAllDepartments(i);
                Departments.Add(Object);
            }
        }

        public void GetContractManagerNames()
        {
            NumberOfRecords = GetNumberOfRecords("contractmanagers");
            ContractManagers = new List<ContractManager>();
            for (int i = 0; i < NumberOfRecords; i++)
            {
                ContractManager Object = new ContractManager();
                Object.GetContractManagers(i);
                ContractManagers.Add(Object);
            }
        }

        public void GetAllCustomerNames()
        {
            NumberOfRecords = GetNumberOfRecords("customers");
            Customers = new List<Customer>();
            for (int i = 0; i < NumberOfRecords; i++)
            {
                Customer Object = new Customer();
                Object.GetAllCustomers(i);
                Customers.Add(Object);
            }
        }
    }
}
