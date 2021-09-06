using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace ToDoList.Model
{
    public class SelectReference
    {
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
        public List<string> Key { get; set; }
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
        public List<int> ColumnReference { get; set; }
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
        public List<string> Value { get; set; }

        public SelectReference()
        {
            Key = new List<string>();
            ColumnReference = new List<int>();
            Value = new List<string>();
        }

        public void LoginReference()
        {
            // Return List<string> Value references:
            // [0] = UserId
            // [1] = Username
            // [2] = Password

            Key.Add("UserId");
            ColumnReference.Add(0);
            Key.Add("Username");
            ColumnReference.Add(1);
            Key.Add("Password");
            ColumnReference.Add(2);
        }

        public void SiteReference()
        {
            // Return List<string> Value references:
            // [0] = Site_Id
            // [1] = Site_Name
            // [2] = Site_Active_Since
            // [3] = Site_Contract_Start
            // [4] = Site_Contract_End
            // [5] = Site_Contact_Name
            // [6] = Site_Contact_Email
            // [7] = Site_Address_Country
            // [8] = Site_Address_City
            // [9] = Site_Address_Postal
            // [10] = Site_Address_Street
            // [11] = Customer_Id
            // [12] = ContractManager_Id

            Key.Add("Site_Id");
            ColumnReference.Add(0);
            Key.Add("Site_Name");
            ColumnReference.Add(1);
            Key.Add("Site_Active_Since");
            ColumnReference.Add(2);
            Key.Add("Site_Contract_Start");
            ColumnReference.Add(3);
            Key.Add("Site_Contract_End");
            ColumnReference.Add(4);
            Key.Add("Site_Contact_Name");
            ColumnReference.Add(5);
            Key.Add("Site_Contact_Email");
            ColumnReference.Add(6);
            Key.Add("Site_Address_Country");
            ColumnReference.Add(7);
            Key.Add("Site_Address_City");
            ColumnReference.Add(8);
            Key.Add("Site_Address_Postal");
            ColumnReference.Add(9);
            Key.Add("Site_Address_Street");
            ColumnReference.Add(10);
            Key.Add("Customer_Id");
            ColumnReference.Add(11);
            Key.Add("ContractManager_Id");
            ColumnReference.Add(12);
        }

        public void CustomReportReference()
        {
            // Return List<string> Value references:
            // [0] = ReportName
            // [1] = TableName
            // [2] = TableQuery
            // [3] = TableSize
            // [4] = LastUpdated
            // [5] = LastAccessed
            // [6] = NextUpdate
            // [7] = ReportType
            // [8] = CustomReportID

            Key.Add("ReportName");
            ColumnReference.Add(0);
            Key.Add("TableName");
            ColumnReference.Add(1);
            Key.Add("TableQuery");
            ColumnReference.Add(2);
            Key.Add("TableSize");
            ColumnReference.Add(3);
            Key.Add("LastUpdated");
            ColumnReference.Add(4);
            Key.Add("LastAccessed");
            ColumnReference.Add(5);
            Key.Add("NextUpdate");
            ColumnReference.Add(6);
            Key.Add("ReportType");
            ColumnReference.Add(7);
            Key.Add("CustomReportID");
            ColumnReference.Add(8);
        }

        public void SiteOverviewReference()
        {
            // Return List<string> Value references:
            // [0] = Site_Id
            // [1] = Site_Name
            // [2] = Customer_Id
            // [3] = ContractManager_Id

            Key.Add("Site_Id");
            ColumnReference.Add(0);
            Key.Add("Site_Name");
            ColumnReference.Add(1);
            Key.Add("Customer_Id");
            ColumnReference.Add(2);
            Key.Add("ContractManager_Id");
            ColumnReference.Add(3);
        }

        public void CustomerReference()
        {
            // Return List<string> Value references:
            // [0] = Customer_Id
            // [1] = Customer_Name
            // [2] = Customer_Contact_Name
            // [3] = Customer_Contact_Email
            // [4] = Customer_Address_Country
            // [5] = Customer_Address_City
            // [6] = Customer_Address_Postal
            // [7] = Customer_Address_Street

            Key.Add("Customer_Id");
            ColumnReference.Add(0);
            Key.Add("Customer_Name");
            ColumnReference.Add(1);
            Key.Add("Customer_Contact_Name");
            ColumnReference.Add(2);
            Key.Add("Customer_Contact_Email");
            ColumnReference.Add(3);
            Key.Add("Customer_Address_Country");
            ColumnReference.Add(4);
            Key.Add("Customer_Address_City");
            ColumnReference.Add(5);
            Key.Add("Customer_Address_Postal");
            ColumnReference.Add(6);
            Key.Add("Customer_Address_Street");
            ColumnReference.Add(7);
        }

        public void GenericNameIdReference()
        {
            // Return List<string> Value references:
            // [0] = Id
            // [1] = Name
 
            Key.Add("Id");
            ColumnReference.Add(0);
            Key.Add("Name");
            ColumnReference.Add(1);
        }

        public void SingleReturnReference()
        {
            // Return List<string> Value references:
            // [0] = Generic_Return_Value

            Key.Add("Generic_Return_Value");
            ColumnReference.Add(0);
        }

        public void ContractManagerReference()
        {
            // Return List<string> Value references:
            // [0] = ContractManager_Id
            // [1] = ContractManager_Name
            // [2] = ContractManager_Email
            // [3] = Department_Id

            Key.Add("ContractManager_Id");
            ColumnReference.Add(0);
            Key.Add("ContractManager_Name");
            ColumnReference.Add(1);
            Key.Add("ContractManager_Email");
            ColumnReference.Add(2);
            Key.Add("Department_Id");
            ColumnReference.Add(3);
        }

        public void DepartmentReference()
        {
            // Return List<string> Value references:
            // [0] = Department_Id
            // [1] = Department_Name

            Key.Add("Department_Id");
            ColumnReference.Add(0);
            Key.Add("Department_Name");
            ColumnReference.Add(1);
        }

        public void CustomReportFields()
        {
            // Return List<string> Value references:
            // [0] = SQLPath
            // [1] = TableName
            // [2] = DataModelRole

            Key.Add("SQLPath");
            ColumnReference.Add(0);
            Key.Add("TableName");
            ColumnReference.Add(1);
            Key.Add("DataModelRole");
            ColumnReference.Add(2);
        }
        public void CustomReportFilter()
        {
            // Return List<string> Value references:
            // [0] = FieldName
            // [1] = DataType
            // [2] = FieldTable
            // [3] = InternalName

            Key.Add("FieldName");
            ColumnReference.Add(0);
            Key.Add("DataType");
            ColumnReference.Add(1);
            Key.Add("FieldTable");
            ColumnReference.Add(2);
            Key.Add("InternalName");
            ColumnReference.Add(3);
        }
    }
}
