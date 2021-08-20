using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ToDoList.Model
{
    public class CustomReport
    {
        public List<GenericNameIdClass> ReportRowElements { get; set; }
        public List<string> KPIOptions { get; set; }
        public string KPI { get; set; }
        public TableReference tablereference { get; set; }
        public List<string> TableList { get; set; }
        public List<string> OrderedTableList { get; set; }
        public List<string> FieldsList { get; set; }

        public CustomReport()
        {
            TableList = new List<string>();
            OrderedTableList = new List<string>();
            FieldsList = new List<string>();
        }

        public void GetReportRowElements()
        {
            string CountQuery = "SELECT COUNT(*) FROM datastructure WHERE DataModelRole != 'Foreign';";
            InspectionDatabaseManager DBM = InspectionDatabaseManager.getInstance();
            Response<string> response = new Response<string>();
            response = DBM.execute(new DatabaseCommandCount(), CountQuery);
            ReportRowElements = new List<GenericNameIdClass>();
            SelectReference reference = new SelectReference();
            for (int i = 0; i < Convert.ToInt32(response.Data); i++)
            {
                string Query = "SELECT FieldReference_Id, ExternalName FROM datastructure WHERE DataModelRole != 'Foreign' ORDER BY ExternalName ASC LIMIT " +i+",1;";
                GenericNameIdClass Object = new GenericNameIdClass();
                Object.GetDataFromiNumber(Query);
                ReportRowElements.Add(Object);
            }
        }

        public void GetKPIOptions()
        {
            KPIOptions = new List<string>();
            string CountQuery = "SELECT COUNT(DISTINCT TableName) FROM datastructure;";
            InspectionDatabaseManager DBM = InspectionDatabaseManager.getInstance();
            Response<string> response = new Response<string>();
            response = DBM.execute(new DatabaseCommandCount(), CountQuery);
            SelectReference reference = new SelectReference();
            for (int i = 0; i < Convert.ToInt32(response.Data); i++)
            {
                string Query = "SELECT DISTINCT(TableName) FROM datastructure LIMIT " + i + ",1;";
                SelectReference selectref = new SelectReference();
                selectref.SingleReturnReference();
                Response<SelectReference> responseselect = new Response<SelectReference>();
                responseselect = DBM.fetch(new DatabaseCommandSelect(), Query, selectref);
                if (response.Success)
                {
                    KPIOptions.Add(responseselect.Data.Value[0]);
                }
            }
        }
        public void RunCustomReport()
        {
            string Query = BuildQuery();
            SelectReference reference = new SelectReference();
            reference.GenericNameIdReference();
            InspectionDatabaseManager DBM = InspectionDatabaseManager.getInstance();
            Response<TableReference> response = new Response<TableReference>();
            response = DBM.fetchtable(new DatabaseCommandSelectTable(), Query, ReportRowElements.Count()+1);
            tablereference = response.Data;
        }

        private string BuildQuery()
        {
            string Query = null;
            List<string> FieldsList = new List<string>();
            List<string> TableList = new List<string>();
            string TableJoins = null;
            InspectionDatabaseManager DBM = InspectionDatabaseManager.getInstance();
            // Fields
            for (int i = 0; i < ReportRowElements.Count; i++)
            {
                SelectReference reference = new SelectReference();
                reference.CustomReportFields();
                Response<SelectReference> response = new Response<SelectReference>();
                string FieldsQuery = "SELECT SQLPath,TableName,DataModelRole FROM datastructure WHERE FieldReference_Id = " + ReportRowElements[i].ID + ";";
                response = DBM.fetch(new DatabaseCommandSelect(), FieldsQuery, reference);
                FieldsList.Add(response.Data.Value[0]);
                if (!TableList.Contains(response.Data.Value[1]))
                {
                    TableList.Add(response.Data.Value[1]);
                }
            }
            if (!TableList.Contains(KPI))
            {
                TableList.Add(KPI);
            }
            if (TableList.Count() > 1)
            {
                if (TableList.Contains("inspections"))
                {
                    TableJoins += " INNER JOIN accountmanagers ON departments.Department_Id = accountmanagers.Department_Id";
                    TableJoins += " INNER JOIN customers ON customers.AccountManager_Id = accountmanagers.AccountManager_Id";
                    TableJoins += " INNER JOIN sites ON sites.Customer_Id = customers.Customer_Id";
                    TableJoins += " INNER JOIN inspections ON inspections.Site_Id = sites.Site_Id";
                } else if (TableList.Contains("sites"))
                {
                    TableJoins += " INNER JOIN accountmanagers ON departments.Department_Id = accountmanagers.Department_Id";
                    TableJoins += " INNER JOIN customers ON customers.AccountManager_Id = accountmanagers.AccountManager_Id";
                    TableJoins += " INNER JOIN sites ON sites.Customer_Id = customers.Customer_Id";
                } else if (TableList.Contains("customers"))
                {
                    TableJoins += " INNER JOIN accountmanagers ON departments.Department_Id = accountmanagers.Department_Id";
                    TableJoins += " INNER JOIN customers ON customers.AccountManager_Id = accountmanagers.AccountManager_Id";
                } else
                {
                    TableJoins += " INNER JOIN accountmanagers ON departments.Department_Id = accountmanagers.Department_Id";
                }
                if (TableList.Contains("contractmanagers"))
                {
                    TableJoins += " INNER JOIN contractmanagers ON departments.Department_Id = contractmanagers.Department_Id";
                }
                if (TableList.Contains("serviceengineers"))
                {
                    TableJoins += " INNER JOIN serviceengineers ON departments.Department_Id = serviceengineers.Department_Id";
                }
                TableList[0] = "departments";
            }
            string CountType = null;
            switch (KPI)
            {
                case "sites":
                    CountType = "sites.Site_Id";
                    break;
                case "customers":
                    CountType = "customers.Customer_Id";
                    break;
                case "accountmanagers":
                    CountType = "accountmanagers.AccountManager_Id";
                    break;
                case "contractmanagers":
                    CountType = "contractmanagers.ContractManager_Id";
                    break;
                case "serviceengineers":
                    CountType = "serviceengineers.ServiceEngineer_Id";
                    break;
                case "departments":
                    CountType = "departments.Department_Id";
                    break;
                case "inspections":
                    CountType = "inspections.Inspection_Id";
                    break;
            }
            string Fields = string.Join(", ", FieldsList.ToArray());
            Query = "SELECT " + Fields + ", COUNT(" + CountType + ") AS Count FROM " + TableList[0] + TableJoins + " GROUP BY " + Fields +";";
            return Query;
        }
    }
}
