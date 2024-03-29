﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace ToDoList.Model
{
    public class Site
    {
        public string SiteID { get; set; }
        public string SiteName { get; set; }
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
        public DateTime SiteActiveSince { get; set; }
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
        public DateTime SiteContractStart { get; set; }
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
        public DateTime SiteContractEnd { get; set; }
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public string SiteContactName { get; set; }
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public string SiteContactEmail { get; set; }
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public string SiteAddressCountry { get; set; }
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public string SiteAddressCity { get; set; }
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public string SiteAddressPostal { get; set; }
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public string SiteAddressStreet { get; set; }
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public Customer Customer { get; set; }
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public ContractManager ContractManager { get; set; }


        public Boolean GetSiteOverviewDataiNumber(int iNumber)
        {
            string Query = "SELECT Site_Id, Site_Name, Customer_Id, ContractManager_Id FROM sites LIMIT " + iNumber + ",1;";
            SelectReference reference = new SelectReference();
            reference.SiteOverviewReference();
            InspectionDatabaseManager DBM = InspectionDatabaseManager.getInstance();
            Response<SelectReference> response = new Response<SelectReference>();
            response = DBM.fetch(new DatabaseCommandSelect(), Query, reference);

            if (response.Success)
            {
                SiteID = response.Data.Value[0];
                SiteName = response.Data.Value[1];
                Customer = new Customer();
                Customer.CustomerID = response.Data.Value[2];
                Customer.GetCustomerOverviewFromID();
                ContractManager = new ContractManager();
                ContractManager.ContractManagerID = response.Data.Value[3];
                ContractManager.GetContractManagerOverviewFromID();
                ContractManager.GetDepartmentDetails();
            }
            return response.Success;
        }

        public Boolean GetSiteDetailsFromID()
        {
            string Query = "SELECT Site_Id, Site_Name, Site_Active_Since, Site_Contract_Start, Site_Contract_End, Site_Contact_Name, Site_Contact_Email, Site_Address_Country, Site_Address_City, Site_Address_Postal, Site_Address_Street, Customer_Id, ContractManager_Id FROM sites WHERE Site_Id = '" + SiteID + "';";
            SelectReference reference = new SelectReference();
            reference.SiteReference();
            InspectionDatabaseManager DBM = InspectionDatabaseManager.getInstance();
            Response<SelectReference> response = new Response<SelectReference>();
            response = DBM.fetch(new DatabaseCommandSelect(), Query, reference);
            if (response.Success)
            {
                SiteID = response.Data.Value[0];
                SiteName = response.Data.Value[1];
                SiteActiveSince = Convert.ToDateTime(response.Data.Value[2]);
                SiteContractStart = Convert.ToDateTime(response.Data.Value[3]);
                SiteContractEnd = Convert.ToDateTime(response.Data.Value[4]);
                SiteContactName = response.Data.Value[5];
                SiteContactEmail = response.Data.Value[6];
                SiteAddressCountry = response.Data.Value[7];
                SiteAddressCity = response.Data.Value[8];
                SiteAddressPostal = response.Data.Value[9];
                SiteAddressStreet = response.Data.Value[10];
                Customer = new Customer();
                Customer.CustomerID = response.Data.Value[11];
                Customer.GetCustomerDetailsFromID();
                ContractManager = new ContractManager();
                ContractManager.ContractManagerID = response.Data.Value[12];
                ContractManager.GetContractManagerDetailsFromID();
            }
            return response.Success;
        }

        //public Boolean CreateNewSite()
        //{
        //    string Query = "INSERT INTO sites(Customer_Id,Department_Id,ContractManager_Id,Site_Name) values('" + Customer.GetCustomerOverviewFromID() + "','" + ContractManager.Department.DepartmentID + "','" + ContractManager.GetContractManagerId(ContractManager.ContractManagerName) + "','" + SiteName + "')";
        //    InspectionDatabaseManager DBM = InspectionDatabaseManager.getInstance();
        //    Response<string> response = new Response<string>();
        //    response = DBM.execute(new DatabaseCommandInsert(), Query);
        //    if (response.Data == "1")
        //    {
        //        return true;
        //    }
        //    else
        //    {
        //        return false;
        //    }
        //}

        public string GenerateUpdateQuery(int SiteId)
        {
            string query = "UPDATE sites SET ";
            Boolean PreviousInput = false;
            if (SiteName != null) {
                if (PreviousInput)
                {
                    query = query + ", Site_Name = '" + SiteName + "'";
                }
                else
                {
                    query = query + " Site_Name = '" + SiteName + "'";
                    PreviousInput = true;
                }
            }
            if (SiteContractStart != DateTime.MinValue)
            {
                if (PreviousInput)
                {
                    query = query + ", Site_Contract_Start = '" + SiteContractStart.ToShortDateString() + "'";
                }
                else
                {
                    query = query + " Site_Contract_Start = '" + SiteContractStart.ToShortDateString() + "'";
                    PreviousInput = true;
                }
            }
            if (SiteContractEnd != DateTime.MinValue)
            {
                if (PreviousInput)
                {
                    query = query + ", Site_Contract_End = '" + SiteContractEnd.ToShortDateString() + "'";
                }
                else
                {
                    query = query + " Site_Contract_End = '" + SiteContractEnd.ToShortDateString() + "'";
                    PreviousInput = true;
                }
            }
            if (SiteContactName != null)
            {
                if (PreviousInput)
                {
                    query = query + ", Site_Contact_Name = '" + SiteContactName + "'";
                }
                else
                {
                    query = query + " Site_Contact_Name = '" + SiteContactName + "'";
                    PreviousInput = true;
                }
            }
            if (SiteContactEmail != null)
            {
                if (PreviousInput)
                {
                    query = query + ", Site_Contact_Email = '" + SiteContactEmail + "'";
                }
                else
                {
                    query = query + " Site_Contact_Email = '" + SiteContactEmail + "'";
                    PreviousInput = true;
                }
            }
            if (SiteAddressCountry != null)
            {
                if (PreviousInput)
                {
                    query = query + ", Site_Address_Country = '" + SiteAddressCountry + "'";
                }
                else
                {
                    query = query + " Site_Address_Country = '" + SiteAddressCountry + "'";
                    PreviousInput = true;
                }
            }
            if (SiteAddressCity != null)
            {
                if (PreviousInput)
                {
                    query = query + ", Site_Address_City = '" + SiteAddressCity + "'";
                }
                else
                {
                    query = query + " Site_Address_City = '" + SiteAddressCity + "'";
                    PreviousInput = true;
                }
            }
            if (SiteAddressPostal != null)
            {
                if (PreviousInput)
                {
                    query = query + ", Site_Address_Postal = '" + SiteAddressPostal + "'";
                }
                else
                {
                    query = query + " Site_Address_Postal = '" + SiteAddressPostal + "'";
                    PreviousInput = true;
                }
            }
            if (SiteAddressStreet != null)
            {
                if (PreviousInput)
                {
                    query = query + ", Site_Address_Street = '" + SiteAddressStreet + "'";
                }
                else
                {
                    query = query + " Site_Address_Street = '" + SiteAddressStreet + "'";
                    PreviousInput = true;
                }
            }
            if(ContractManager != null){
                if (ContractManager.ContractManagerName != null)
                {
                    ContractManager.GetContractManagerOverviewFromName();
                    if (PreviousInput)
                    {
                        query = query + ", ContractManager_Id = '" + ContractManager.ContractManagerID + "'";
                    }
                    else
                    {
                        query = query + " ContractManager_Id = '" + ContractManager.ContractManagerID + "'";
                        PreviousInput = true;
                    }
                }
            }
            query = query + " WHERE Site_Id = '" + SiteId + "';";
            return query;
        }
    }
}
