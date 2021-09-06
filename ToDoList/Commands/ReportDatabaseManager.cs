using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ToDoList.Model
{
    public class ReportDatabaseManager
    {
        private static ReportDatabaseManager instance = new ReportDatabaseManager();
        private string ConnectionString;

        private ReportDatabaseManager() 
        {
            ConnectionString = "datasource=127.0.0.1;port=3306;username=root;password=;database=customreportsdatabase;sslmode=none;";
        }

        public static ReportDatabaseManager getInstance()
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

        public Response<TableReference> fetchtable(DatabaseCommandFetchTable Command, string Query, int ColNum)
        {
            Response<TableReference> response = new Response<TableReference>();
            response = Command.ExecuteCommand(ConnectionString, Query, ColNum);
            return response;
        }
    }
}
