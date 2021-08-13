using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ToDoList.Model;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ToDoList.Controllers
{
    [Route("api/login")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        // GET DEBUG
        [HttpGet("{AuthenticationKey}")]
        public Boolean Get(string AuthenticationKey)
        {
            Authentication Auth = new Authentication();
            return Auth.AuthenticateUser(AuthenticationKey);
        }

        // POST Login with new login details
        [HttpPost]
        public Response<string> Post([FromBody] Authentication Auth)
        {
            string AuthKey = Auth.Login();
            if (AuthKey != null)
            {
                Response<string> response = new Response<string>();
                response.Success = true;
                response.Data = AuthKey;
                return response;
            }
            else
            {
                Response<string> response = new Response<string>();
                response.FailedToAuthenticate();
                return response;
            }
        }

        // DELETE existing AuthenticationKey
        [HttpDelete("{AuthenticationKey}")]
        public Response<string> Delete(string AuthenticationKey)
        {
            Authentication Auth = new Authentication();
            if (Auth.DeleteAuthenticationKey(AuthenticationKey))
            {
                Response<string> response = new Response<string>();
                response.GenericSuccessCode();
                return response;
            }
            else
            {
                Response<string> response = new Response<string>();
                response.FailedToContactServer();
                return response;
            }
        }
    }
}
