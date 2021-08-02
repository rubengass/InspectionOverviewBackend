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
        public int NumberOfSites;

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
            catch (Exception ex)
            {
                Console.WriteLine("Failed to get the number of sites.");
            }
            return NumberOfSites;
        }
    }
}
