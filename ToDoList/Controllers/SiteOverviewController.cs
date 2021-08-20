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
        public Response<SiteOverview> Get()
        {
            Authentication Auth = new Authentication();
            Response<string> AuthenticationResponse = new Response<string>();
            AuthenticationResponse = Auth.AuthenticateUser(Request.Headers["Authorization"]);
            Response<SiteOverview> response = new Response<SiteOverview>();
            response.Success = AuthenticationResponse.Success;
            response.ResponseMessage = AuthenticationResponse.ResponseMessage;
            response.ErrorCodes = AuthenticationResponse.ErrorCodes;
            return response;
        }

        // GET specified number and scope of sites
        [HttpGet("{NumberOfRowsMin:int},{NumberOfRowsMax:int}")]
        public Response<SiteOverview> Get(int NumberOfRowsMin, int NumberOfRowsMax)
        {
            Authentication Auth = new Authentication();
            Response<string> AuthenticationResponse = new Response<string>();
            AuthenticationResponse = Auth.AuthenticateUser(Request.Headers["Authorization"]);
            Response<SiteOverview> response = new Response<SiteOverview>();
            response.Success = AuthenticationResponse.Success;
            response.ResponseMessage = AuthenticationResponse.ResponseMessage;
            response.ErrorCodes = AuthenticationResponse.ErrorCodes;
            if (AuthenticationResponse.Success)
            {
                SiteOverview Overview = new SiteOverview();
                Overview.FetchPaginatedSites(NumberOfRowsMin, NumberOfRowsMax);
                response.Data = Overview;
                return response;
            }
            return response;
        }

        // GET sites based on search result
        [HttpGet("{Filters:int}")]
        public Response<SiteOverview> Get(int Filters, string SiteIDSearch, string SiteNameSearch, string CustomerNameSearch, string ContractManagerNameSearch, string DepartmentNameSearch) 
        {
            Authentication Auth = new Authentication();
            if (Auth.AuthenticateUser(Request.Headers["Authorization"]).Success)
            {
                SiteOverview Overview = new SiteOverview();
                Overview.FetchSearchResults(SiteIDSearch, SiteNameSearch, CustomerNameSearch, ContractManagerNameSearch, DepartmentNameSearch, Filters);
                Response<SiteOverview> response = new Response<SiteOverview>();
                response.Success = true;
                response.Data = Overview;
                return response;
            }
            else
            {
                Response<SiteOverview> response = new Response<SiteOverview>();
                response.FailedToAuthenticate();
                return response;
            }
        }

    }
}
