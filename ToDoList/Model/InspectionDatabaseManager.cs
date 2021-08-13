using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ToDoList.Model
{
    public class InspectionDatabaseManager
    {
        private static InspectionDatabaseManager instance = new InspectionDatabaseManager();
        private string ConnectionString;

        private InspectionDatabaseManager() 
        {
            ConnectionString = "datasource=127.0.0.1;port=3306;username=root;password=;database=inspectiondatabase;sslmode=none;";
        }

        public static InspectionDatabaseManager getInstance()
        {
            return instance;
        }

        public Response<string> execute(DatabaseCommand Command, string Query)
        {
            Response<string> response = new Response<string>();
            response = Command.ExecuteCommand(ConnectionString, Query);
            return response;
        }

        public Response<SelectReference> fetch(DatabaseCommandFetch Command, string Query, SelectReference reference)
        {
            Response<SelectReference> response = new Response<SelectReference>();
            response = Command.ExecuteCommand(ConnectionString, Query, reference);
            return response;
        }
    }
}
