using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;

namespace ToDoList.Model
{
    public class Authentication
    {
        private string _Username;
        private string _Password;
        private string AuthenticationKey;
        private int UserId;

        public string Username
        {
            get => _Username;
            set { _Username = value; }
        }
        public string Password
        {
            get => _Password;
            set { _Password = value; }
        }

        private bool PerformLogin()
        {
            string connectionString = "datasource=127.0.0.1;port=3306;username=root;password=;database=inspectiondatabase;sslmode=none;";
            string query = "SELECT * FROM login WHERE Username = '" + _Username + "' AND Password = '"+_Password+"';";
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
                        string UID = reader.GetString(0);
                        UserId = Int32.Parse(UID);
                        if(UserId >0)
                        {
                            databaseConnection.Close();
                            return true;
                        }
                        databaseConnection.Close();
                        return false;

                    }
                    databaseConnection.Close();
                    return false;
                }
                else
                {
                    Console.WriteLine("No rows found");
                    databaseConnection.Close();
                    return false;
                }
            }
            catch (Exception)
            {
                Console.WriteLine("Failed verify login details");
                return false;
            }
        }

        public string Login()
        {
            if (PerformLogin())
            {
                if (AuthenticationKeyExistsForUser())
                {
                    RemoveDuplicateKeys();
                }
                GenerateAuthenticationKey();
                SaveAuthenticationKey();
            }
            return AuthenticationKey;
        }

        private bool AuthenticationKeyExistsForUser()
        {
            string connectionString = "datasource=127.0.0.1;port=3306;username=root;password=;database=inspectiondatabase;sslmode=none;";
            string query = "SELECT COUNT(*) FROM tempkeys WHERE UserId = '" + UserId + "';";
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
                        int UserExists = Int32.Parse(Count);
                        if (UserExists >0)
                        {
                            databaseConnection.Close();
                            return true;
                        }
                        databaseConnection.Close();
                        return false;

                    }
                    databaseConnection.Close();
                    return false;
                }
                else
                {
                    Console.WriteLine("No rows found");
                    databaseConnection.Close();
                    return false;
                }
            }
            catch (Exception)
            {
                Console.WriteLine("Failed to check for duplicate user keys");
                return false;
            }
        }

        private void GenerateAuthenticationKey()
        {
            int size = 10;
            char[] chars = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ1234567890".ToCharArray();
            byte[] data = new byte[4 * size];
            using (RNGCryptoServiceProvider crypto = new RNGCryptoServiceProvider())
            {
                crypto.GetBytes(data);
            }
            StringBuilder result = new StringBuilder(size);
            for (int i = 0; i < size; i++)
            {
                var rnd = BitConverter.ToUInt32(data, i * 4);
                var idx = rnd % chars.Length;

                result.Append(chars[idx]);
            }
            AuthenticationKey = result.ToString();
        }

        private void SaveAuthenticationKey()
        {
            string connectionString = "datasource=127.0.0.1;port=3306;username=root;password=;database=inspectiondatabase;sslmode=none;";
            string query = "INSERT INTO tempkeys(AuthenticationKey, Expiry, UserId) values('" + AuthenticationKey + "','" + DateTime.UtcNow.AddMinutes(30).ToString("yyyy-MM-ddTHH:mm:ssZ") + "','" + UserId+"');";
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
                Console.WriteLine("Failed save the generated authentication key");
            }
        }

        private void RemoveExpiredKeys()
        {
            string connectionString = "datasource=127.0.0.1;port=3306;username=root;password=;database=inspectiondatabase;sslmode=none;";
            string query = "DELETE FROM tempkeys WHERE Expiry < '" + DateTime.UtcNow.ToString("yyyy-MM-ddTHH:mm:ss") + "';";
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
                Console.WriteLine("Failed delete expired keys");
            }
        }

        private void RemoveDuplicateKeys()
        {
            string connectionString = "datasource=127.0.0.1;port=3306;username=root;password=;database=inspectiondatabase;sslmode=none;";
            string query = "DELETE FROM tempkeys WHERE UserId = " + UserId + ";";
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
                Console.WriteLine("Failed delete duplicate keys");
            }
        }

        public Boolean AuthenticateUser(string AuthKey)
        {
            RemoveExpiredKeys();
            return AuthenticationKeyIsValid(AuthKey);
        }

        private bool AuthenticationKeyIsValid(string AuthKey)
        {
            string connectionString = "datasource=127.0.0.1;port=3306;username=root;password=;database=inspectiondatabase;sslmode=none;";
            string query = "SELECT COUNT(*) FROM tempkeys WHERE AuthenticationKey = '" + AuthKey + "';";
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
                        int UserExists = Int32.Parse(Count);
                        if (UserExists > 0)
                        {
                            databaseConnection.Close();
                            return true;
                        }
                        databaseConnection.Close();
                        return false;

                    }
                    databaseConnection.Close();
                    return false;
                }
                else
                {
                    Console.WriteLine("No rows found");
                    databaseConnection.Close();
                    return false;
                }
            }
            catch (Exception)
            {
                Console.WriteLine("Failed to check for duplicate user keys");
                return false;
            }
        }

        public void DeleteAuthenticationKey()
        {
            RemoveExpiredKeys();
        }
    }
}
