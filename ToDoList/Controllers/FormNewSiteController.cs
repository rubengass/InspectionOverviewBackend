using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ToDoList.Model;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ToDoList.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FormNewSiteController : ControllerBase
    {
        // GET: api/<DepartmentsController>
        [HttpGet]
        public IEnumerable<FormNewSite> Get()
        {
            FormNewSite Result = new FormNewSite();
            Result.GetAllDepartmentNames();
            Result.GetAllCustomerNames();
            return new FormNewSite[] { Result };
        }

        // GET api/<DepartmentsController>/5
        [HttpGet("{DepartmentName}")]
        public IEnumerable<FormNewSite> Get(string DepartmentName)
        {
            FormNewSite Result = new FormNewSite();
            Result.GetContractManagerNames(DepartmentName);
            return new FormNewSite[] { Result };
        }

        // POST api/<DepartmentsController>
        [HttpPost]
        public Site Post([FromBody] Site site)
        {
            site.CreateNewSite();
            return site;
        }

        // PUT api/<DepartmentsController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<DepartmentsController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
