using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ToDoList.Model;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ToDoList.Controllers
{
    [Route("api/FormData")]
    [ApiController]
    public class FormController : ControllerBase
    {
        // GET all departments and customers
        [HttpGet]
        public Response<Form> Get()
        {
            Authentication Auth = new Authentication();
            Response<string> AuthenticationResponse = new Response<string>();
            AuthenticationResponse = Auth.AuthenticateUser(Request.Headers["Authorization"]);
            Response<Form> response = new Response<Form>();
            response.Success = AuthenticationResponse.Success;
            response.ResponseMessage = AuthenticationResponse.ResponseMessage;
            response.ErrorCodes = AuthenticationResponse.ErrorCodes;
            if (AuthenticationResponse.Success)
            {
                Form Result = new Form();
                Result.ListOfDepartmentsByOverview();
                Result.ListOfCustomersByOverview();
                Result.ListOfContractManagersByOverview();
                response.Data = Result;
                return response;
            }
            return response;
        }

        // GET all Contract managers for a certain department
        //[HttpGet("{DepartmentName}")]
        //public IEnumerable<Form> Get(string DepartmentName)
        //{
        //    Form Result = new Form();
        //    Result.GetContractManagerNames();
        //    return new Form[] { Result };
        //}
    }
}
