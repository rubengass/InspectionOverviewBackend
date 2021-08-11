﻿using System;
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
        public string Username;
        public string Password;
        private string AuthenticationKey;

        //public string Username {
        //    get => _Username;
        //    set { _Username = value; } }
        //public string Password
        //{
        //    get => _Password;
        //    set { _Password = value; }
        //}

        private bool PerformLogin()
        {
            string connectionString = "datasource=127.0.0.1;port=3306;username=root;password=;database=inspectiondatabase;sslmode=none;";
            string query = "SELECT COUNT(*) FROM login WHERE Username = '" + Username + "' AND Password = '"+Password+"';";
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
                        int NumberOfResults = Int32.Parse(Count);
                        if (NumberOfResults == 1)
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
                Console.WriteLine("Failed to retrieve ContractManagers");
                return false;
            }
        }

        public string Login()
        {
            if (PerformLogin())
            {
                GenerateAuthenticationKey();
                SaveAuthenticationKey();
            }
            return AuthenticationKey;
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
            string query = "INSERT INTO tempkeys(AuthenticationKey, Expiry) values('" + AuthenticationKey + "','" + DateTime.UtcNow.AddMinutes(30) + "');";
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

        }

        public void AuthenticateKey()
        {
            RemoveExpiredKeys();
        }

        public void DeleteAuthenticationKey()
        {
            RemoveExpiredKeys();
        }
    }
}
