using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ToDoList.Model
{
    public class DatabaseCommandSelect : DatabaseCommandFetch
    {
        public DatabaseCommandSelect()
        {
        }

        public override Response<SelectReference> ExecuteCommand(string ConnectionString, string Query, SelectReference reference)
        {
            Response<SelectReference> response = new Response<SelectReference>();
            response.Data = reference;
            MySqlConnection databaseConnection = new MySqlConnection(ConnectionString);
            MySqlCommand commandDatabase = new MySqlCommand(Query, databaseConnection);
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
                        response.Success = true;
                        for (int i = 0; i< response.Data.ColumnReference.Count; i++)
                        {
                            if (!reader.IsDBNull(response.Data.ColumnReference[i]))
                            {
                                response.Data.Value.Add(reader.GetString(response.Data.ColumnReference[i]));
                            }
                            else
                            {
                                response.Data.Value.Add(null);
                            }
                        }
                    }
                }
                databaseConnection.Close();
            }
            catch (Exception)
            {
                response.FailedToContactServer();
            }
            return response;
        }
    }
}
