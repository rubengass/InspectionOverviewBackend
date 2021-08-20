using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ToDoList.Model
{
    public class DatabaseCommandInsert : DatabaseCommand
    {
        public DatabaseCommandInsert()
        {
        }

        public override Response<string> ExecuteCommand(string ConnectionString, string Query)
        {
            Response<string> response = new Response<string>();
            MySqlConnection databaseConnection = new MySqlConnection(ConnectionString);
            MySqlCommand commandDatabase = new MySqlCommand(Query, databaseConnection);
            commandDatabase.CommandTimeout = 60;
            MySqlDataReader reader;

            try
            {
                databaseConnection.Open();
                reader = commandDatabase.ExecuteReader();
                databaseConnection.Close();
                response.GenericSuccessCode();
            }
            catch (Exception)
            {
                response.FailedToContactServer();
            }
            return response;
        }
    }
}
