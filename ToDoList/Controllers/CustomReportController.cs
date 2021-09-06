using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ToDoList.Model;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ToDoList.Controllers
{
    [Route("api/CustomReport")]
    [ApiController]
    public class CustomReportController : ControllerBase
    {
        // Get all reports
        [HttpGet]
        public Response<List<CustomReport>> Get()
        {
            Authentication Auth = new Authentication();
            Response<string> AuthenticationResponse = new Response<string>();
            AuthenticationResponse = Auth.AuthenticateUser(Request.Headers["Authorization"]);
            Response<List<CustomReport>> response = new Response<List<CustomReport>>();
            response.Success = AuthenticationResponse.Success;
            response.ResponseMessage = AuthenticationResponse.ResponseMessage;
            response.ErrorCodes = AuthenticationResponse.ErrorCodes;
            if (response.Success)
            {
                string UserId = Auth.GetUserId(Request.Headers["Authorization"]);
                List<CustomReport> customreports = new List<CustomReport>();
                string Query = "SELECT COUNT(*) FROM customreports WHERE UserId = " + UserId + ";";
                UserDatabaseManager UDBM = UserDatabaseManager.getInstance();
                string Count = UDBM.execute(new DatabaseCommandCount(), Query).Data;
                for (int i = 0; i < Convert.ToInt32(Count); i++)
                {
                    CustomReport Object = new CustomReport();
                    Object.GetCustomReportData(UserId, i);
                    customreports.Add(Object);
                }
                response.Data = customreports;
                response.GenericSuccessCode();
            }
            return response;
        }

        // Get CustomReport
        [HttpGet("{TableName}")]
        public Response<TableReference> Get(string TableName)
        {
            Response<TableReference> response = new Response<TableReference>();
            CustomReport report = new CustomReport();
            if (report.CheckForUpdate(TableName))
            {
                CustomReportCreator creator = new CustomReportCreator();
                report.DeleteCustomReportTable(TableName);
                creator.CreateCustomReport(report.GetReportQuery(TableName), TableName);
                report.UpdateUpdateCycle(TableName);
            }
            report.UpdateAccessDetails(TableName);
            if(report.GetCustomReport(TableName))
            {
                response.Data = report.tablereference;
                response.GenericSuccessCode();
            } else
            {
                response.FailedToContactServer();
            }
            return response;
        }

        // Delete the report
        [HttpDelete("{TableName}")]
        public Response<string> Delete(string TableName)
        {
            Authentication Auth = new Authentication();
            Response<string> response = new Response<string>();
            response = Auth.AuthenticateUser(Request.Headers["Authorization"]);
            if (response.Success)
            {
                CustomReport report = new CustomReport();
                report.DeleteCustomReport(TableName);
            }
            return response;
        }
    }
}
