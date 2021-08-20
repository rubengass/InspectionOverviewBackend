using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;

namespace ToDoList.Model
{
    public class Customer
    {
        public string CustomerID { get; set; }
        public string CustomerName { get; set; }
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public string CustomerContactName { get; set; }
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public string CustomerContactEmail { get; set; }
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public string CustomerAddressCountry { get; set; }
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public string CustomerAddressCity { get; set; }
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public string CustomerAddressPostal { get; set; }
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public string CustomerAddressStreet { get; set; }
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public string AccountManagerID { get; set; }
        public Boolean GetCustomerDetailsFromID()
        {
            string Query = "SELECT Customer_Id, Customer_Name, Customer_Contact_Name, Customer_Contact_Email, Customer_Address_Country, Customer_Address_City, Customer_Address_Postal, Customer_Address_Street FROM customers WHERE Customer_Id = '" + CustomerID + "';";
            SelectReference reference = new SelectReference();
            reference.CustomerReference();
            InspectionDatabaseManager DBM = InspectionDatabaseManager.getInstance();
            Response<SelectReference> response = new Response<SelectReference>();
            response = DBM.fetch(new DatabaseCommandSelect(), Query, reference);
            if (response.Success)
            {
                CustomerID = response.Data.Value[0];
                CustomerName = response.Data.Value[1];
                CustomerContactName = response.Data.Value[2];
                CustomerContactEmail = response.Data.Value[3];
                CustomerAddressCountry = response.Data.Value[4];
                CustomerAddressCity = response.Data.Value[5];
                CustomerAddressPostal = response.Data.Value[6];
                CustomerAddressStreet = response.Data.Value[7];
            }
            return response.Success;
        }

        public Boolean GetCustomerDetailsFromName()
        {
            string Query = "SELECT Customer_Id, Customer_Name, Customer_Contact_Name, Customer_Contact_Email, Customer_Address_Country, Customer_Address_City, Customer_Address_Postal, Customer_Address_Street FROM customers WHERE Customer_Name = '" + CustomerName + "';";
            SelectReference reference = new SelectReference();
            reference.CustomerReference();
            InspectionDatabaseManager DBM = InspectionDatabaseManager.getInstance();
            Response<SelectReference> response = new Response<SelectReference>();
            response = DBM.fetch(new DatabaseCommandSelect(), Query, reference);
            if (response.Success)
            {
                CustomerID = response.Data.Value[0];
                CustomerName = response.Data.Value[1];
                CustomerContactName = response.Data.Value[2];
                CustomerContactEmail = response.Data.Value[3];
                CustomerAddressCountry = response.Data.Value[4];
                CustomerAddressCity = response.Data.Value[5];
                CustomerAddressPostal = response.Data.Value[6];
                CustomerAddressStreet = response.Data.Value[7];
            }
            return response.Success;
        }

        public Boolean GetCustomerOverviewFromID()
        {
            string Query = "SELECT Customer_Id, Customer_Name FROM customers WHERE Customer_Id = '" + CustomerID + "';";
            SelectReference reference = new SelectReference();
            reference.GenericNameIdReference();
            InspectionDatabaseManager DBM = InspectionDatabaseManager.getInstance();
            Response<SelectReference> response = new Response<SelectReference>();
            response = DBM.fetch(new DatabaseCommandSelect(), Query, reference);
            if (response.Success)
            {
                CustomerID = response.Data.Value[0];
                CustomerName = response.Data.Value[1];
            }
            return response.Success;
        }

        public Boolean GetCustomerOverviewFromName()
        {
            string Query = "SELECT Customer_Id, Customer_Name FROM customers WHERE Customer_Name = '" + CustomerName + "';";
            SelectReference reference = new SelectReference();
            reference.GenericNameIdReference();
            InspectionDatabaseManager DBM = InspectionDatabaseManager.getInstance();
            Response<SelectReference> response = new Response<SelectReference>();
            response = DBM.fetch(new DatabaseCommandSelect(), Query, reference);
            if (response.Success)
            {
                CustomerID = response.Data.Value[0];
                CustomerName = response.Data.Value[1];
            }
            return response.Success;
        }

        public Boolean GetCustomerOverviewFromiNumber(int iNumber)
        {
            string Query = "SELECT Customer_Id, Customer_Name FROM customers LIMIT " + iNumber + ",1;";
            SelectReference reference = new SelectReference();
            reference.GenericNameIdReference();
            InspectionDatabaseManager DBM = InspectionDatabaseManager.getInstance();
            Response<SelectReference> response = new Response<SelectReference>();
            response = DBM.fetch(new DatabaseCommandSelect(), Query, reference);
            if (response.Success)
            {
                CustomerID = response.Data.Value[0];
                CustomerName = response.Data.Value[1];
            }
            return response.Success;
        }
    }
}
