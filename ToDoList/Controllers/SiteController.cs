using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ToDoList.Model;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ToDoList.Controllers
{
    [Route("api/SiteData")]
    [ApiController]
    public class SiteController : ControllerBase
    {
        // GET individual site
        [HttpGet("{SiteID}")]
        public Site Get(int SiteID)
        {
            Site site = new Site();
            site.GetSiteDetails(SiteID);
            return site;
        }

        // POST create new site
        [HttpPost]
        public Site Post([FromBody] Site site)
        {
            site.CreateNewSite();
            return site;
        }

        // PUT update existing site
        [HttpPut("{SiteId}")]
        public void Put(int SiteId, [FromBody] Site site)
        {
            site.UpdateSiteValues(SiteId);
        }

        // DELETE delete site
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
