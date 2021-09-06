using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ToDoList.Model
{
    public class CustomReport
    {
        public string CustomReportId { get; set; }
        public string ReportType { get; set; }
        public string ReportName { get; set; }
        public string TableName { get; set; }
        public string TableQuery { get; set; }
        public string TableSize { get; set; }
        public string LastUpdated { get; set; }
        public string LastAccessed { get; set; }
        public string NextUpdate { get; set; }
        public TableReference tablereference { get; set; }


        public Boolean GetCustomReportData(string UserId, int i)
        {
            string Query = "SELECT ReportName, TableName, Query, TableSize, LastUpdated, LastAccessed, NextUpdate, ReportType, CustomReportId FROM customreports WHERE UserId = '" + UserId + "' LIMIT "+i + ",1;";
            SelectReference reference = new SelectReference();
            reference.CustomReportReference();
            UserDatabaseManager UDBM = UserDatabaseManager.getInstance();
            Response<SelectReference> response = new Response<SelectReference>();
            response = UDBM.fetch(new DatabaseCommandSelect(), Query, reference);
            if (response.Success)
            {
                ReportName = response.Data.Value[0];
                TableName = response.Data.Value[1];
                TableQuery = response.Data.Value[2];
                TableSize = response.Data.Value[3];
                LastUpdated = response.Data.Value[4];
                LastAccessed = response.Data.Value[5];
                NextUpdate = response.Data.Value[6];
                ReportType = response.Data.Value[7];
                CustomReportId = response.Data.Value[8];
            }
            return response.Success;
        }
        public void DeleteCustomReport(string TableName)
        {
            string DeleteEntryQuery = "DELETE FROM customreports WHERE TableName = '" + TableName + "';";
            UserDatabaseManager UDBM = UserDatabaseManager.getInstance();
            UDBM.execute(new DatabaseCommandDelete(), DeleteEntryQuery);
            string DeleteTableQuery = "DROP TABLE "+TableName+";";
            ReportDatabaseManager RDBM = ReportDatabaseManager.getInstance();
            RDBM.execute(new DatabaseCommandDelete(), DeleteTableQuery);
        }

        public Boolean GetCustomReport(string TableName)
        {
            ReportDatabaseManager RDBM = ReportDatabaseManager.getInstance();
            Response<TableReference> response = new Response<TableReference>();
            tablereference = new TableReference();
            string CountQuery = "SELECT COUNT(*) FROM INFORMATION_SCHEMA.COLUMNS WHERE table_schema = 'customreportsdatabase' AND table_name = '"+TableName + "';";
            int Colnum = Convert.ToInt32(RDBM.execute(new DatabaseCommandCount(), CountQuery).Data);
            string HeaderQuery = "SELECT COLUMN_NAME FROM INFORMATION_SCHEMA.COLUMNS WHERE table_schema = 'customreportsdatabase' AND table_name = '" + TableName + "';";
            response = RDBM.fetchtable(new DatabaseCommandSelectTable(), HeaderQuery, 1);
            RowReference HeaderList = new RowReference();
            for (int i = 0; i < Colnum; i++)
            {
                HeaderList.Row.Add(response.Data.Rows[i].Row[0]);
            }
            tablereference.Rows.Add(HeaderList);
            string Query = "SELECT * FROM " + TableName +";";
            response = RDBM.fetchtable(new DatabaseCommandSelectTable(), Query, Colnum);
            tablereference.Rows.AddRange(response.Data.Rows);
            return response.Success;
        }

        public Boolean CheckForUpdate(string TableName)
        {
            UserDatabaseManager UDBM = UserDatabaseManager.getInstance();
            string CountQuery = "SELECT COUNT(*) FROM customreports WHERE TableName = '" + TableName + "' AND NextUpdate < '"+ DateTime.UtcNow.ToString("yyyy-MM-ddTHH:mm:ss") + "';";
            int Colnum = Convert.ToInt32(UDBM.execute(new DatabaseCommandCount(), CountQuery).Data);
            if(Colnum > 0)
            {
                return true;
            } else
            {
                return false;
            }
        }

        public void UpdateAccessDetails(string TableName)
        {
            UserDatabaseManager UDBM = UserDatabaseManager.getInstance();
            Response<string> response = new Response<string>();
            string Query = "UPDATE customreports SET LastAccessed = '" + DateTime.UtcNow.ToString("yyyy-MM-ddTHH:mm:ss") + "' WHERE TableName = '"+TableName+"';";
            UDBM.execute(new DatabaseCommandUpdate(), Query);
        }

        public string GetReportQuery(string TableName)
        {
            string Query = "SELECT Query FROM customreports WHERE TableName = '" + TableName + "';";
            SelectReference reference = new SelectReference();
            reference.SingleReturnReference();
            UserDatabaseManager UDBM = UserDatabaseManager.getInstance();
            Response<SelectReference> response = new Response<SelectReference>();
            response = UDBM.fetch(new DatabaseCommandSelect(), Query, reference);
            if (response.Data.Value.Count() != 0)
            {
                return response.Data.Value[0];
            }
            else
            {
                return null;
            }
        }

        public void DeleteCustomReportTable(string TableName)
        {
            string Query = "DROP TABLE customreportsdatabase." + TableName + ";";
            ReportDatabaseManager RDBM = ReportDatabaseManager.getInstance();
            RDBM.execute(new DatabaseCommandDelete(), Query);
        }

        public void UpdateUpdateCycle(string TableName)
        {
            ReportDatabaseManager RDBM = ReportDatabaseManager.getInstance();
            string SizeQuery = "SELECT ROUND((DATA_LENGTH + INDEX_LENGTH)/1024,2) FROM information_schema.TABLES WHERE table_schema = 'customreportsdatabase' AND TABLE_NAME = '" + TableName + "';";
            string TableSize = RDBM.execute(new DatabaseCommandCount(), SizeQuery).Data;
            UserDatabaseManager UDBM = UserDatabaseManager.getInstance();
            string Query = "UPDATE customreports SET LastUpdated = '" + DateTime.UtcNow.ToString("yyyy-MM-ddTHH:mm:ss") + "', NextUpdate ='" + DateTime.UtcNow.AddDays(1).ToString("yyyy-MM-ddTHH:mm:ss") + "', TableSize = "+TableSize+" WHERE TableName = '" + TableName + "';";
            UDBM.execute(new DatabaseCommandUpdate(), Query);
        }
    }
}
