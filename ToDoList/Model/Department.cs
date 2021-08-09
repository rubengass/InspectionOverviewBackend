using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;

namespace ToDoList.Model
{
    public class Department
    {
        public string DepartmentID { get; set; }
        public string DepartmentName { get; set; }
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public List<ContractManager> ContractManagers { get; set; }

        public string GetDepartmentId(string DepartmentName)
        {
            string connectionString = "datasource=127.0.0.1;port=3306;username=root;password=;database=inspectiondatabase;sslmode=none;";
            string query = "SELECT * FROM departments WHERE Department_Name ='" + DepartmentName + "'";
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
                        DepartmentID = reader.GetString(0);
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
                Console.WriteLine("Failed to retrieve DepartmentID");
            }
            return DepartmentID;
        }

        public string GetDepartmentName(string DepartmentID)
        {
            string connectionString = "datasource=127.0.0.1;port=3306;username=root;password=;database=inspectiondatabase;sslmode=none;";
            string query = "SELECT * FROM departments WHERE Department_Id ='" + DepartmentID + "'";
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
                        DepartmentName = reader.GetString(1);
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
                Console.WriteLine("Failed to retrieve DepartmentName");
            }
            return DepartmentName;
        }

        public void GetAllDepartments(int iNumber)
        {
            string connectionString = "datasource=127.0.0.1;port=3306;username=root;password=;database=inspectiondatabase;sslmode=none;";
            string query = "SELECT * FROM departments LIMIT " + iNumber + ",1";
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
                        DepartmentID = reader.GetString(0);
                        DepartmentName = reader.GetString(1);
                        ContractManagers = new List<ContractManager>();
                        int NumberOfRecords = GetNumberOfContractManagersForDepartment(DepartmentID);
                        for (int i = 0; i < NumberOfRecords; i++)
                        {
                            ContractManager Object = new ContractManager();
                            Object.GetContractManagerDataFromDepartmentID(DepartmentID,i);
                            ContractManagers.Add(Object);
                        }
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
                Console.WriteLine("Failed to retrieve Contract Manager Info");
            }
        }

        public int GetNumberOfContractManagersForDepartment(string DepartmentID)
        {
            int NumberOfRecords = 0;
            string connectionString = "datasource=127.0.0.1;port=3306;username=root;password=;database=inspectiondatabase;sslmode=none;";
            string query = "SELECT COUNT(*) FROM contractmanagers WHERE Department_Id = '"+DepartmentID+"';";
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
    }
}
