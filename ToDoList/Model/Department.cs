using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;

namespace ToDoList.Model
{
    public class Department
    {
        public string DepartmentID { get; set; }
        public string DepartmentName { get; set; }
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public List<ContractManager> ContractManagers { get; set; }

        public Boolean GetDepartmentDetailsFromID()
        {
            string Query = "SELECT Department_Id, Department_Name FROM departments WHERE Department_Id = '" + DepartmentID + "';";
            SelectReference reference = new SelectReference();
            reference.DepartmentReference();
            InspectionDatabaseManager DBM = InspectionDatabaseManager.getInstance();
            Response<SelectReference> response = new Response<SelectReference>();
            response = DBM.fetch(new DatabaseCommandSelect(), Query, reference);
            if (response.Success)
            {
                DepartmentID = response.Data.Value[0];
                DepartmentName = response.Data.Value[1];
            }
            return response.Success;
        }

        public Boolean GetDepartmentDetailsFromName()
        {
            string Query = "SELECT Department_Id, Department_Name FROM departments WHERE Department_Name = '" + DepartmentName + "';";
            SelectReference reference = new SelectReference();
            reference.DepartmentReference();
            InspectionDatabaseManager DBM = InspectionDatabaseManager.getInstance();
            Response<SelectReference> response = new Response<SelectReference>();
            response = DBM.fetch(new DatabaseCommandSelect(), Query, reference);
            if (response.Success)
            {
                DepartmentID = response.Data.Value[0];
                DepartmentName = response.Data.Value[1];
            }
            return response.Success;
        }

        public Boolean GetDepartmentOverviewFromiNumber(int iNumber)
        {
            string Query = "SELECT Department_Id, Department_Name FROM departments LIMIT " + iNumber + ",1;";
            SelectReference reference = new SelectReference();
            reference.DepartmentReference();
            InspectionDatabaseManager DBM = InspectionDatabaseManager.getInstance();
            Response<SelectReference> response = new Response<SelectReference>();
            response = DBM.fetch(new DatabaseCommandSelect(), Query, reference);
            if (response.Success)
            {
                DepartmentID = response.Data.Value[0];
                DepartmentName = response.Data.Value[1];
                ContractManagers = new List<ContractManager>();
                int NumberOfContractManagers = CountNumberOfContractManagersInDepartment();
                for (int i =0; i< NumberOfContractManagers; i++)
                {
                    ContractManager Object = new ContractManager();
                    Object.GetContractManagerFromDepartmentIDFromiNumber(DepartmentID, i);
                    ContractManagers.Add(Object);
                }
            }
            return response.Success;
        }

        public int CountNumberOfContractManagersInDepartment()
        {
            string Query = "SELECT COUNT(*) FROM contractmanagers WHERE Department_Id = '" + DepartmentID + "';";
            InspectionDatabaseManager DBM = InspectionDatabaseManager.getInstance();
            Response<string> response = new Response<string>();
            response = DBM.execute(new DatabaseCommandCount(), Query);
            if (response.Success)
            {
                return Convert.ToInt32(response.Data);
            } else
            {
                return 0;
            }
        }
    }
}
