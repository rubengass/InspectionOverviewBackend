using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ToDoList.Model
{
    public class SelectReference
    {
        public List<string> Key { get; set; }
        public List<int> ColumnReference { get; set; }
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
    }
}
