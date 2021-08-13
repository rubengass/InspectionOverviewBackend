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
        //[HttpGet]
        //public Site Get(string SiteID)
        //{
        //    Site site = new Site();
        //    site.TEMPGetCompleteSiteData(SiteID);
        //    return site;
        //}

        // GET individual site
        [HttpGet("{SiteID}")]
        public Response<Site> Get(int SiteID)
        {
            Site site = new Site();
            Response<Site> response = new Response<Site>();
            if (site.GetSiteDetails(SiteID))
            {
                response.Success = true;
                response.Data = site;
            } else
            {
                response.FailedToContactServer();
            }
            return response;
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
        public Response<string> Put(int SiteId, [FromBody] Site site)
        {
            InspectionDatabaseManager DBM = InspectionDatabaseManager.getInstance();
            Response<string> response = new Response<string>();
            string Query = site.GenerateUpdateQuery(SiteId);
            response = DBM.execute(new DatabaseCommandUpdate(),Query);
            return response;
        }

        // DELETE delete site
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
