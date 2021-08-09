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
        public string ContractManagerEmail { get; set; }
        public Department Department { get; set; }

        public void GetContractManagers(int iNumber)
        {
            string connectionString = "datasource=127.0.0.1;port=3306;username=root;password=;database=inspectiondatabase;sslmode=none;";
            string query = "SELECT * FROM contractmanagers LIMIT " + iNumber + ",1";
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
                        ContractManagerName = reader.GetString(1);
                        ContractManagerEmail = reader.GetString(2);
                        GetDepartmentData();
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
                Console.WriteLine("Failed to retrieve ContractManagers");
            }
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
                        ContractManagerEmail = reader.GetString(2);
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
                Console.WriteLine("Failed to retrieve ContractManagerName.");
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
                        ContractManagerEmail = reader.GetString(2);
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
                Console.WriteLine("Failed to retrieve ContractManagerID");
            }
            return ContractManagerID;
        }

        public string GetDepartmentData()
        {
            string query = "SELECT * FROM contractmanagers WHERE ";
            if(ContractManagerID != null)
            {
                query = query + "ContractManager_Id = '" + ContractManagerID + "';";
            } else if (ContractManagerName !=null)
            {
                query = query + "ContractManager_Name = '" + ContractManagerName + "';";
            }
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
                    while (reader.Read())
                    {
                        Department = new Department();
                        Department.DepartmentID = reader.GetString(3);
                        Department.GetDepartmentName(Department.DepartmentID);
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
                Console.WriteLine("Failed to retrieve ContractManagerID");
            }
            return ContractManagerID;
        }

        public string GetContractManagerDataFromDepartmentID(string DepartmentID, int i)
        {
            string connectionString = "datasource=127.0.0.1;port=3306;username=root;password=;database=inspectiondatabase;sslmode=none;";
            string query = "SELECT * FROM contractmanagers WHERE Department_Id = '" + DepartmentID + "' LIMIT "+i+",1;";
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
                        ContractManagerName = reader.GetString(1);
                        ContractManagerEmail = reader.GetString(2);
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
                Console.WriteLine("Failed to retrieve ContractManagerName.");
            }
            return ContractManagerName;
        }
    }
}
