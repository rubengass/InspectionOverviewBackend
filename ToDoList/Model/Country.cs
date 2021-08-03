//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Threading.Tasks;
//using MySql.Data.MySqlClient;

//namespace InspectionOverviewBackend.Model
//{
//    public class Country
//    {
//        public string CountryName { get; set; }
//        public string CountryID { get; set; }
//        public string RegionName { get; set; }
//        public string RegionId { get; set; }
//        public string ContinentName { get; set; }
//        public string ContinentId { get; set; }
//        public List<string> OfficialLanguages { get; set; }
//        public List<string> OtherLanguages { get; set; }
//        public List<string> OfficialLanguageIds { get; set; }
//        public List<string> OtherLanguageIds { get; set; }



//        public Country(string Name)
//        {
//            CountryName = Name;
//            //Languages = new List<string> { "Language1", "Language2" };
//        }

//        public void FetchCountryId(string CountryName)
//        {
//            string connectionString = "datasource=127.0.0.1;port=3306;username=root;password=;database=nation;sslmode=none;";
//            string query = "SELECT * FROM countries WHERE name = '" + CountryName + "'";
//            MySqlConnection databaseConnection = new MySqlConnection(connectionString);
//            MySqlCommand commandDatabase = new MySqlCommand(query, databaseConnection);
//            commandDatabase.CommandTimeout = 60;
//            MySqlDataReader reader;

//            try
//            {
//                databaseConnection.Open();
//                reader = commandDatabase.ExecuteReader();
//                if (reader.HasRows)
//                {
//                    reader.Read();
//                    CountryID = reader.GetString(0);
//                    RegionId = reader.GetString(6);
//                    FetchRegion(RegionId);
//                    string CountryId = reader.GetString(0);
//                    FetchLanguageIds(CountryId);
//                    FetchOfficialLanguageIds(CountryId);
//                }
//                else
//                {
//                    Console.WriteLine("No rows found");

//                }
//                databaseConnection.Close();
//            }
//            catch (Exception ex)
//            {
//                Console.WriteLine("Failed to retrieve a word from db, fallback word is selected.");
//            }
//        }

//        public string FetchRegion(string RegionId)
//        {
//            string connectionString = "datasource=127.0.0.1;port=3306;username=root;password=;database=nation;sslmode=none;";
//            string query = "SELECT * FROM regions WHERE region_id = '"+ RegionId + "'";
//            MySqlConnection databaseConnection = new MySqlConnection(connectionString);
//            MySqlCommand commandDatabase = new MySqlCommand(query, databaseConnection);
//            commandDatabase.CommandTimeout = 60;
//            MySqlDataReader reader;

//            try
//            {
//                databaseConnection.Open();
//                reader = commandDatabase.ExecuteReader();
//                if (reader.HasRows)
//                {
//                    reader.Read();
//                    RegionName = reader.GetString(1);
//                    ContinentId = reader.GetString(2);
//                    FetchContinent(ContinentId);
//                }
//                else
//                {
//                    Console.WriteLine("No rows found");
//                }
//                databaseConnection.Close();
//            }
//            catch (Exception ex)
//            {
//                Console.WriteLine("Failed to retrieve a word from db, fallback word is selected.");
//            }
//            return RegionName;
//        }

//        public string FetchContinent(string ContinentId)
//        {
//            string connectionString = "datasource=127.0.0.1;port=3306;username=root;password=;database=nation;sslmode=none;";
//            string query = "SELECT * FROM continents WHERE continent_id = '" + ContinentId + "'";
//            MySqlConnection databaseConnection = new MySqlConnection(connectionString);
//            MySqlCommand commandDatabase = new MySqlCommand(query, databaseConnection);
//            commandDatabase.CommandTimeout = 60;
//            MySqlDataReader reader;

//            try
//            {
//                databaseConnection.Open();
//                reader = commandDatabase.ExecuteReader();
//                if (reader.HasRows)
//                {
//                    reader.Read();
//                    ContinentName = reader.GetString(1);
//                }
//                else
//                {
//                    Console.WriteLine("No rows found");
//                }
//                databaseConnection.Close();
//            }
//            catch (Exception ex)
//            {
//                Console.WriteLine("Failed to retrieve a word from db, fallback word is selected.");
//            }
//            return ContinentName;
//        }

//        public void FetchLanguageIds(string CountryId)
//        {
//            string connectionString = "datasource=127.0.0.1;port=3306;username=root;password=;database=nation;sslmode=none;";
//            string query = "SELECT * FROM country_languages WHERE country_id = '" + CountryId + "' and official ='0'";
//            MySqlConnection databaseConnection = new MySqlConnection(connectionString);
//            MySqlCommand commandDatabase = new MySqlCommand(query, databaseConnection);
//            commandDatabase.CommandTimeout = 60;
//            MySqlDataReader reader;

//            try
//            {
//                databaseConnection.Open();
//                reader = commandDatabase.ExecuteReader();
//                if (reader.HasRows)
//                {
//                    OtherLanguageIds = new List<string>();
//                    while (reader.Read())
//                    {
//                        OtherLanguageIds.Add(reader.GetString(1));
//                        FetchOtherLanguageNames(OtherLanguageIds);
//                    }
//                }
//                else
//                {
//                    Console.WriteLine("No rows found");
//                }
//                databaseConnection.Close();
//            }
//            catch (Exception ex)
//            {
//                Console.WriteLine("Failed to retrieve a word from db, fallback word is selected.");
//            }
//        }

//        public void FetchOfficialLanguageIds(string CountryId)
//        {
//            string connectionString = "datasource=127.0.0.1;port=3306;username=root;password=;database=nation;sslmode=none;";
//            string query = "SELECT * FROM country_languages WHERE country_id = '" + CountryId + "' and official ='1'";
//            MySqlConnection databaseConnection = new MySqlConnection(connectionString);
//            MySqlCommand commandDatabase = new MySqlCommand(query, databaseConnection);
//            commandDatabase.CommandTimeout = 60;
//            MySqlDataReader reader;

//            try
//            {
//                databaseConnection.Open();
//                reader = commandDatabase.ExecuteReader();
//                if (reader.HasRows)
//                {
//                    OfficialLanguageIds = new List<string>();
//                    while (reader.Read())
//                    {
//                        OfficialLanguageIds.Add(reader.GetString(1));
//                        FetchOfficialLanguages(OfficialLanguageIds);
//                    }
//                }
//                else
//                {
//                    Console.WriteLine("No rows found");
//                }
//                databaseConnection.Close();
//            }
//            catch (Exception ex)
//            {
//                Console.WriteLine("Failed to retrieve a word from db, fallback word is selected.");
//            }
//        }

//        public List<string> FetchOtherLanguageNames(List<string> OtherLanguageIds)
//        {
//            string connectionString = "datasource=127.0.0.1;port=3306;username=root;password=;database=nation;sslmode=none;";
//            string query = "SELECT * FROM languages";
//            MySqlConnection databaseConnection = new MySqlConnection(connectionString);
//            MySqlCommand commandDatabase = new MySqlCommand(query, databaseConnection);
//            commandDatabase.CommandTimeout = 60;
//            MySqlDataReader reader;

//            try
//            {
//                databaseConnection.Open();
//                reader = commandDatabase.ExecuteReader();
//                if (reader.HasRows)
//                {
//                    OtherLanguages = new List<string>();

//                    while (reader.Read())
//                    {
//                        if (OtherLanguageIds.Contains(reader.GetString(0)))
//                        {
//                            OtherLanguages.Add(reader.GetString(1));
//                        }
//                    }
//                }
//                else
//                {
//                    Console.WriteLine("No rows found");
//                }
//                databaseConnection.Close();
//            }
//            catch (Exception ex)
//            {
//                Console.WriteLine("Failed to retrieve a word from db, fallback word is selected.");
//            }
//            return OtherLanguageIds;
//        }

//        public List<string> FetchOfficialLanguages(List<string> OfficialLanguageIds)
//        {
//            string connectionString = "datasource=127.0.0.1;port=3306;username=root;password=;database=nation;sslmode=none;";
//            string query = "SELECT * FROM languages";
//            MySqlConnection databaseConnection = new MySqlConnection(connectionString);
//            MySqlCommand commandDatabase = new MySqlCommand(query, databaseConnection);
//            commandDatabase.CommandTimeout = 60;
//            MySqlDataReader reader;

//            try
//            {
//                databaseConnection.Open();
//                reader = commandDatabase.ExecuteReader();
//                if (reader.HasRows)
//                {
//                    OfficialLanguages = new List<string>();

//                    while (reader.Read())
//                    {
//                        if (OfficialLanguageIds.Contains(reader.GetString(0)))
//                        {
//                            OfficialLanguages.Add(reader.GetString(1));
//                        }
//                    }
//                }
//                else
//                {
//                    Console.WriteLine("No rows found");
//                }
//                databaseConnection.Close();
//            }
//            catch (Exception ex)
//            {
//                Console.WriteLine("Failed to retrieve a word from db, fallback word is selected.");
//            }
//            return OfficialLanguageIds;
//        }
//    }
//}
