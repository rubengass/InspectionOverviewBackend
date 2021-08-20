using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;

namespace ToDoList.Model
{
    public class Form
    {
        public List<Department> Departments { get; set; }
        public List<ContractManager> ContractManagers { get; set; }
        public List<Customer> Customers { get; set; }

        public void ListOfDepartmentsByOverview()
        {
            string Query = "SELECT COUNT(*) FROM departments;";
            InspectionDatabaseManager DBM = InspectionDatabaseManager.getInstance();
            Response<string> response = new Response<string>();
            response = DBM.execute(new DatabaseCommandCount(), Query);       
            Departments = new List<Department>();
            for (int i = 0; i < Convert.ToInt32(response.Data); i++)
            {
                Department Object = new Department();
                Object.GetDepartmentOverviewFromiNumber(i);
                Departments.Add(Object);
            }
        }

        public void ListOfContractManagersByOverview()
        {
            string Query = "SELECT COUNT(*) FROM contractmanagers;";
            InspectionDatabaseManager DBM = InspectionDatabaseManager.getInstance();
            Response<string> response = new Response<string>();
            response = DBM.execute(new DatabaseCommandCount(), Query); ContractManagers = new List<ContractManager>();
            for (int i = 0; i < Convert.ToInt32(response.Data); i++)
            {
                ContractManager Object = new ContractManager();
                Object.GetContractManagerOverviewFromiNumber(i);
                ContractManagers.Add(Object);
            }
        }

        public void ListOfCustomersByOverview()
        {
            string Query = "SELECT COUNT(*) FROM customers;";
            InspectionDatabaseManager DBM = InspectionDatabaseManager.getInstance();
            Response<string> response = new Response<string>();
            response = DBM.execute(new DatabaseCommandCount(), Query); Customers = new List<Customer>();
            for (int i = 0; i < Convert.ToInt32(response.Data); i++)
            {
                Customer Object = new Customer();
                Object.GetCustomerOverviewFromiNumber(i);
                Customers.Add(Object);
            }
        }
    }
}
