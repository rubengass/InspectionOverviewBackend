using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using ToDoList.Model;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ToDoList.Controllers
{
    [Route("api/GetAllSites")]
    [ApiController]
    public class SiteOverviewController : ControllerBase
    {
        // GET all sites
        [HttpGet]
        public IEnumerable<SiteOverview> Get()
        {
            SiteOverview Overview = new SiteOverview();
            Overview.FetchAllSites();
            return new SiteOverview[] { Overview };
        }

        // GET specified number and scope of sites
        [HttpGet("{NumberOfRowsMin:int},{NumberOfRowsMax:int}")]
        public IEnumerable<SiteOverview> Get(int NumberOfRowsMin, int NumberOfRowsMax, string AuthKey)
        {
            string AuthKey2 = null;
            HttpRequestMessage re = new HttpRequestMessage();
            var headers = re.Headers;
            if (headers.Contains("Authorization"))
            {
                AuthKey2 = headers.GetValues("Authorization").First();
            }
            Authentication Auth = new Authentication();
            if (Auth.AuthenticateUser(AuthKey2))
            {
                SiteOverview Overview = new SiteOverview();
                Overview.FetchPaginatedSites(NumberOfRowsMin, NumberOfRowsMax);
                return new SiteOverview[] { Overview };
            } else
            {
                return null;
            }
        }

        // GET sites based on search result
        [HttpGet("{Filters:int}")] //,{CustomerNameSearch:string},{ContractManagerNameSearch:string},{DepartmentNameSearch:string}
        public IEnumerable<SiteOverview> Get(int Filters, string SiteIDSearch, string SiteNameSearch, string CustomerNameSearch, string ContractManagerNameSearch, string DepartmentNameSearch) 
        {
            SiteOverview Overview = new SiteOverview();
            Overview.FetchSearchResults(SiteIDSearch, SiteNameSearch, CustomerNameSearch, ContractManagerNameSearch, DepartmentNameSearch, Filters); 
            return new SiteOverview[] { Overview };
        }

        // POST api/<SiteController>
        //[HttpPost]
        //public void Post([FromBody] string value)
        //{
        //}

        //// PUT api/<SiteController>/5
        //[HttpPut("{id}")]
        //public void Put(int id, [FromBody] string value)
        //{
        //}

        //// DELETE api/<SiteController>/5
        //[HttpDelete("{id}")]
        //public void Delete(int id)
        //{
        //}
    }
}
