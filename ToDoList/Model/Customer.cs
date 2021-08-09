using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;

namespace ToDoList.Model
{
    public class Customer
    {
        public string CustomerID { get; set; }
        public string CustomerName { get; set; }
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public string CustomerContactName { get; set; }
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public string CustomerContactEmail { get; set; }
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public string CustomerAddressCountry { get; set; }
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public string CustomerAddressCity { get; set; }
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public string CustomerAddressPostal { get; set; }
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public string CustomerAddressStreet { get; set; }
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public string AccountManagerID { get; set; }


        public string GetCustomerName(string CustomerID)
        {
            string connectionString = "datasource=127.0.0.1;port=3306;username=root;password=;database=inspectiondatabase;sslmode=none;";
            string query = "SELECT * FROM customers WHERE Customer_Id = '" + CustomerID + "'";
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
                        CustomerName = reader.GetString(1);
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
                Console.WriteLine("Failed to retrieve CustomerName");
            }
            return CustomerName;
        }
        public string GetCustomerDetailsFromID(string CustomerID)
        {
            string connectionString = "datasource=127.0.0.1;port=3306;username=root;password=;database=inspectiondatabase;sslmode=none;";
            string query = "SELECT * FROM customers WHERE Customer_Id = '" + CustomerID + "'";
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
                        CustomerName = reader.GetString(1);
                        CustomerContactName = reader.GetString(2);
                        CustomerContactEmail = reader.GetString(3);
                        CustomerAddressCountry = reader.GetString(4);
                        CustomerAddressCity = reader.GetString(5);
                        CustomerAddressPostal = reader.GetString(6);
                        CustomerAddressStreet = reader.GetString(7);
                        AccountManagerID = reader.GetString(8);
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
                Console.WriteLine("Failed to retrieve CustomerName");
            }
            return CustomerName;
        }

        public string GetCustomerId(string CustomerName)
        {
            string connectionString = "datasource=127.0.0.1;port=3306;username=root;password=;database=inspectiondatabase;sslmode=none;";
            string query = "SELECT * FROM customers WHERE Customer_Name = '" + CustomerName + "'";
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
                        CustomerID = reader.GetString(0);
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
                Console.WriteLine("Failed to retrieve CustomerID");
            }
            return CustomerID;
        }

        public void GetAllCustomers(int iNumber)
        {
            string connectionString = "datasource=127.0.0.1;port=3306;username=root;password=;database=inspectiondatabase;sslmode=none;";
            string query = "SELECT * FROM customers LIMIT " + iNumber + ",1";
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
                        CustomerID = reader.GetString(0);
                        CustomerName = reader.GetString(1);
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
                Console.WriteLine("Failed to retrieve Customers");
            }
        }
    }
}
