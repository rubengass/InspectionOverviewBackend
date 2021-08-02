using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;

namespace ToDoList.Model
{
    public class FormNewSite
    {
        public List<string> Departments { get; set; }
        public List<string> ContractManagers { get; set; }
        public List<string> Customers { get; set; }

        public List<string> GetAllDepartmentNames()
        {
            Department List = new Department();
            Departments = List.GetAllDepartmentNames();
            return Departments;
        }

        public List<string> GetContractManagerNames(string departmentName)
        {
            Department Dep = new Department();
            string DepID = Dep.GetDepartmentId(departmentName);
            ContractManager List = new ContractManager();
            ContractManagers = List.GetContractManagerNames(DepID);
            return ContractManagers;
        }

        public List<string> GetAllCustomerNames()
        {
            Customer List = new Customer();
            Customers = List.GetAllCustomerNames();
            return Customers;
        }
    }
}
