using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;

namespace ToDoList.Model
{
    public class ContractManager
    {
        public string ContractManagerID { get; set; }
        public string ContractManagerName { get; set; }
        public List<string> ContractManagers { get; set; }

        public List<string> GetContractManagerNames(string DepartmentID)
        {
            string connectionString = "datasource=127.0.0.1;port=3306;username=root;password=;database=inspectiondatabase;sslmode=none;";
            string query = "SELECT ContractManager_Name FROM contractmanagers WHERE Department_Id = '" + DepartmentID + "'";
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
                    ContractManagers = new List<string>();
                    while (reader.Read())
                    {
                        ContractManagers.Add(reader.GetString(0));
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
            return ContractManagers;
        }

        public List<string> GetAllContractManagerNames()
        {
            string connectionString = "datasource=127.0.0.1;port=3306;username=root;password=;database=inspectiondatabase;sslmode=none;";
            string query = "SELECT ContractManager_Name FROM contractmanagers";
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
                    ContractManagers = new List<string>();
                    while (reader.Read())
                    {
                        ContractManagers.Add(reader.GetString(0));
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
            return ContractManagers;
        }
        public string GetContractManagerName(string ContractManagerID)
        {
            string connectionString = "datasource=127.0.0.1;port=3306;username=root;password=;database=inspectiondatabase;sslmode=none;";
            string query = "SELECT * FROM contractmanagers WHERE ContractManager_Id = '" + ContractManagerID + "'";
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
                        ContractManagerName = reader.GetString(1);
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
            return ContractManagerName;
        }
        public string GetContractManagerId(string ContractManagerName)
        {
            string connectionString = "datasource=127.0.0.1;port=3306;username=root;password=;database=inspectiondatabase;sslmode=none;";
            string query = "SELECT * FROM contractmanagers WHERE ContractManager_Name = '" + ContractManagerName + "'";
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
                        ContractManagerID = reader.GetString(0);
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
            return ContractManagerID;
        }

    }
}
