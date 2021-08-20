using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;

namespace ToDoList.Model
{
    public class ContractManager
    {
        public string ContractManagerID { get; set; }
        public string ContractManagerName { get; set; }
        public string ContractManagerEmail { get; set; }
        public Department Department { get; set; }

        public Boolean GetContractManagerOverviewFromiNumber(int iNumber)
        {
            string Query = "SELECT ContractManager_Id, ContractManager_Name FROM contractmanagers LIMIT " + iNumber + ",1;";
            SelectReference reference = new SelectReference();
            reference.GenericNameIdReference();
            InspectionDatabaseManager DBM = InspectionDatabaseManager.getInstance();
            Response<SelectReference> response = new Response<SelectReference>();
            response = DBM.fetch(new DatabaseCommandSelect(), Query, reference);
            if (response.Success)
            {
                ContractManagerID = response.Data.Value[0];
                ContractManagerName = response.Data.Value[1];
            }
            return response.Success;
        }

        public Boolean GetContractManagerOverviewFromID()
        {
            string Query = "SELECT ContractManager_Id, ContractManager_Name FROM contractmanagers WHERE ContractManager_Id = '" + ContractManagerID + "';";
            SelectReference reference = new SelectReference();
            reference.GenericNameIdReference();
            InspectionDatabaseManager DBM = InspectionDatabaseManager.getInstance();
            Response<SelectReference> response = new Response<SelectReference>();
            response = DBM.fetch(new DatabaseCommandSelect(), Query, reference);
            if (response.Success)
            {
                ContractManagerID = response.Data.Value[0];
                ContractManagerName = response.Data.Value[1];
            }
            return response.Success;
        }

        public Boolean GetContractManagerOverviewFromName()
        {
            string Query = "SELECT ContractManager_Id, ContractManager_Name FROM contractmanagers WHERE ContractManager_Name = '" + ContractManagerName + "';";
            SelectReference reference = new SelectReference();
            reference.GenericNameIdReference();
            InspectionDatabaseManager DBM = InspectionDatabaseManager.getInstance();
            Response<SelectReference> response = new Response<SelectReference>();
            response = DBM.fetch(new DatabaseCommandSelect(), Query, reference);
            if (response.Success)
            {
                ContractManagerID = response.Data.Value[0];
                ContractManagerName = response.Data.Value[1];
            }
            return response.Success;
        }

        public Boolean GetContractManagerDetailsFromName()
        {
            string Query = "SELECT ContractManager_Id, ContractManager_Name,ContractManager_Email, Department_Id FROM contractmanagers WHERE ContractManager_Name = '" + ContractManagerName + "';";
            SelectReference reference = new SelectReference();
            reference.ContractManagerReference();
            InspectionDatabaseManager DBM = InspectionDatabaseManager.getInstance();
            Response<SelectReference> response = new Response<SelectReference>();
            response = DBM.fetch(new DatabaseCommandSelect(), Query, reference);
            if (response.Success)
            {
                ContractManagerID = response.Data.Value[0];
                ContractManagerName = response.Data.Value[1];
                ContractManagerEmail = response.Data.Value[2];
                Department = new Department();
                Department.DepartmentID = response.Data.Value[3];
                Department.GetDepartmentDetailsFromID();
            }
            return response.Success;
        }

        public Boolean GetContractManagerDetailsFromID()
        {
            string Query = "SELECT ContractManager_Id, ContractManager_Name,ContractManager_Email, Department_Id FROM contractmanagers WHERE ContractManager_Id = '" + ContractManagerID + "';";
            SelectReference reference = new SelectReference();
            reference.ContractManagerReference();
            InspectionDatabaseManager DBM = InspectionDatabaseManager.getInstance();
            Response<SelectReference> response = new Response<SelectReference>();
            response = DBM.fetch(new DatabaseCommandSelect(), Query, reference);
            if (response.Success)
            {
                ContractManagerID = response.Data.Value[0];
                ContractManagerName = response.Data.Value[1];
                ContractManagerEmail = response.Data.Value[2];
                Department = new Department();
                Department.DepartmentID = response.Data.Value[3];
                Department.GetDepartmentDetailsFromID();
            }
            return response.Success;
        }



        public string GetDepartmentDetails()
        {
            string query = "SELECT * FROM contractmanagers WHERE ";
            if(ContractManagerID != null)
            {
                query = query + "ContractManager_Id = '" + ContractManagerID + "';";
            } else if (ContractManagerName !=null)
            {
                query = query + "ContractManager_Name = '" + ContractManagerName + "';";
            }
            string connectionString = "datasource=127.0.0.1;port=3306;username=root;password=;database=inspectiondatabase;sslmode=none;";
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
                        Department = new Department();
                        Department.DepartmentID = reader.GetString(3);
                        Department.GetDepartmentDetailsFromID();
                    }
                }
                else
                {
                    Console.WriteLine("No rows found");

                }
                databaseConnection.Close();
            }
            catch (Exception)
            {
                Console.WriteLine("Failed to retrieve ContractManagerID");
            }
            return ContractManagerID;
        }

        public Boolean GetContractManagerFromDepartmentIDFromiNumber(string DepartmentID, int iNumber)
        {
            string Query = "SELECT * FROM contractmanagers WHERE Department_Id = '" + DepartmentID + "' LIMIT " + iNumber + ",1;";
            SelectReference reference = new SelectReference();
            reference.ContractManagerReference();
            InspectionDatabaseManager DBM = InspectionDatabaseManager.getInstance();
            Response<SelectReference> response = new Response<SelectReference>();
            response = DBM.fetch(new DatabaseCommandSelect(), Query, reference);
            if (response.Success)
            {
                ContractManagerID = response.Data.Value[0];
                ContractManagerName = response.Data.Value[1];
                ContractManagerEmail = response.Data.Value[2];
            }
            return response.Success;
        }
    }
}
