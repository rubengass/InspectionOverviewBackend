using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ToDoList.Model
{
    public class CustomReportFilter
    {
        public string FieldName { get; set; }
        public string InternalFieldName { get; set; }
        public string FieldTable { get; set; }
        public string FieldID { get; set; }
        public string FieldDataType { get; set; }
        public List<string> FilterOptions { get; set; }

        public CustomReportFilter(string ID)
        {
            FieldID = ID;
            FilterOptions = new List<string>();
            GetFieldInformation();
            GetFilterOptions();
        }

        private void GetFieldInformation()
        {
            string Query = "SELECT ExternalName, DataType, TableName, InternalName FROM datastructure WHERE FieldReference_Id = " + FieldID + ";";
            SelectReference reference = new SelectReference();
            reference.CustomReportFilter();
            InspectionDatabaseManager IDBM = InspectionDatabaseManager.getInstance();
            Response<SelectReference> response = new Response<SelectReference>();
            response = IDBM.fetch(new DatabaseCommandSelect(), Query, reference);
            if (response.Success)
            {
                FieldName = response.Data.Value[0];
                FieldDataType = response.Data.Value[1];
                FieldTable = response.Data.Value[2];
                InternalFieldName = response.Data.Value[3];
            }
        }

        private void GetFilterOptions()
        {
            if(FieldDataType == "Text")
            {
                // Get Distinct Options
                string Query = "SELECT DISTINCT("+ InternalFieldName +") FROM "+FieldTable +";";
                InspectionDatabaseManager IDBM = InspectionDatabaseManager.getInstance();
                Response<TableReference> response = new Response<TableReference>();
                response = IDBM.fetchtable(new DatabaseCommandSelectTable(), Query, 1);
                if (response.Success)
                {
                    for (int i =0; i < response.Data.Rows.Count() ; i++)
                    {
                        FilterOptions.Add(response.Data.Rows[i].Row[0]);
                    }
                }
            } else if (FieldDataType =="Number" | FieldDataType == "DateTime"){
                // Get Min for range
                string Query = "SELECT MIN("+InternalFieldName+") FROM "+FieldTable+";";
                SelectReference reference = new SelectReference();
                reference.SingleReturnReference();
                InspectionDatabaseManager IDBM = InspectionDatabaseManager.getInstance();
                Response<SelectReference> response = new Response<SelectReference>();
                response = IDBM.fetch(new DatabaseCommandSelect(), Query, reference);
                if (response.Success)
                {
                    FilterOptions.Add(response.Data.Value[0]);
                }
                // Get Max for range
                Query = "SELECT MAX(" + InternalFieldName + ") FROM " + FieldTable + ";";
                response = IDBM.fetch(new DatabaseCommandSelect(), Query, reference);
                if (response.Success)
                {
                    FilterOptions.Add(response.Data.Value[1]);
                }
            } else if(FieldDataType == "Boolean")
            {
                // = True, False
                FilterOptions.Add("TRUE");
                FilterOptions.Add("FALSE");
            }
        }
    }
}
