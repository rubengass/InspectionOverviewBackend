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
    public class CustomReportController : ControllerBase
    {
        // Get a list of elements which can be in the report
        [HttpGet]
        public Response<CustomReport> Get()
        {
            Authentication Auth = new Authentication();
            Response<string> AuthenticationResponse = new Response<string>();
            AuthenticationResponse = Auth.AuthenticateUser(Request.Headers["Authorization"]);
            Response<CustomReport> response = new Response<CustomReport>();
            response.Success = AuthenticationResponse.Success;
            response.ResponseMessage = AuthenticationResponse.ResponseMessage;
            response.ErrorCodes = AuthenticationResponse.ErrorCodes;
            if (AuthenticationResponse.Success)
            {
                CustomReport Report = new CustomReport();
                Report.GetReportRowElements();
                Report.GetKPIOptions();
                response.Data = Report;
                return response;
            }
            return response;
        }

        // PUT api/<CustomReportController>/5
        [HttpPut("{NumberOfRows}")]
        public Response<CustomReport> Put(int NumberOfRows, [FromBody] CustomReport Report)
        {
            Authentication Auth = new Authentication();
            Response<string> AuthenticationResponse = new Response<string>();
            AuthenticationResponse = Auth.AuthenticateUser(Request.Headers["Authorization"]);
            Response<CustomReport> response = new Response<CustomReport>();
            response.Success = AuthenticationResponse.Success;
            response.ResponseMessage = AuthenticationResponse.ResponseMessage;
            response.ErrorCodes = AuthenticationResponse.ErrorCodes;
            if (AuthenticationResponse.Success)
            {
                Report.RunCustomReport();
                response.Data = Report;
                return response;
            }
            return response;
        }
    }
}
