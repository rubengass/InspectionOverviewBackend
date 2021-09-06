using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ToDoList.Model;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ToDoList.Controllers
{
    [Route("api/CustomReportEditor")]
    [ApiController]
    public class CustomReportCreatorController : ControllerBase
    {
        // Get a list of elements which can be in the report
        [HttpGet]
        public Response<CustomReportCreator> Get()
        {
            Authentication Auth = new Authentication();
            Response<string> AuthenticationResponse = new Response<string>();
            AuthenticationResponse = Auth.AuthenticateUser(Request.Headers["Authorization"]);
            Response<CustomReportCreator> response = new Response<CustomReportCreator>();
            response.Success = AuthenticationResponse.Success;
            response.ResponseMessage = AuthenticationResponse.ResponseMessage;
            response.ErrorCodes = AuthenticationResponse.ErrorCodes;
            if (AuthenticationResponse.Success)
            {
                CustomReportCreator Report = new CustomReportCreator();
                Report.GetReportRowElements();
                Report.GetKPIOptions();
                response.Data = Report;
                return response;
            }
            return response;
        }

        // PUT run the custom report preview
        [HttpPut("{KPI}")]
        public Response<CustomReportCreator> Put(string KPI, [FromBody] CustomReportCreator Report)
        {
            Authentication Auth = new Authentication();
            Response<string> AuthenticationResponse = new Response<string>();
            AuthenticationResponse = Auth.AuthenticateUser(Request.Headers["Authorization"]);
            Response<CustomReportCreator> response = new Response<CustomReportCreator>();
            response.Success = AuthenticationResponse.Success;
            response.ResponseMessage = AuthenticationResponse.ResponseMessage;
            response.ErrorCodes = AuthenticationResponse.ErrorCodes;
            if (AuthenticationResponse.Success)
            {
                string Query = Report.BuildQuery(KPI);
                Report.RunCustomReport(Query);
                response.Data = Report;
                return response;
            }
            return response;
        }

        //Post the custom report
       [HttpPost("{KPI}")]
        public Response<CustomReportCreator> Post(string KPI, string ReportType, string ReportName, [FromBody] CustomReportCreator Report)
        {
            Authentication Auth = new Authentication();
            Response<string> AuthenticationResponse = new Response<string>();
            AuthenticationResponse = Auth.AuthenticateUser(Request.Headers["Authorization"]);
            string UserID = Auth.GetUserId(Request.Headers["Authorization"]);
            Response<CustomReportCreator> response = new Response<CustomReportCreator>();
            response.Success = AuthenticationResponse.Success;
            response.ResponseMessage = AuthenticationResponse.ResponseMessage;
            response.ErrorCodes = AuthenticationResponse.ErrorCodes;
            if (AuthenticationResponse.Success)
            {
                Report.SaveCustomReport(ReportName, ReportType, UserID, KPI);
                response.Data = Report;
                return response;
            }
            return response;
        }
    }
}
