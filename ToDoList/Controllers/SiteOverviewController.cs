using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ToDoList.Model;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ToDoList.Controllers
{
    [Route("api/GetAllSites")]
    [ApiController]
    public class SiteOverviewController : ControllerBase
    {
        // GET: api/<SiteController>
        //[HttpGet]
        //public IEnumerable<SiteOverview> Get()
        //{
        //    SiteOverview Overview = new SiteOverview();
        //    Overview.FetchSiteNames();
        //    return new SiteOverview[] { Overview };
        //}

        [HttpGet]
        public IEnumerable<SiteOverview> Get()
        {
            SiteOverview Overview = new SiteOverview();
            Overview.FetchAllSites();
            return new SiteOverview[] { Overview };
        }

        // GET api/<SiteController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<SiteController>
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/<SiteController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<SiteController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
