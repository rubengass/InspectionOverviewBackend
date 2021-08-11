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
        // GET specified number and scope of sites
        [HttpGet("{AuthenticationKey}")]
        public Boolean Get(string AuthenticationKey)
        {
            Authentication Auth = new Authentication();
            return Auth.AuthenticateUser(AuthenticationKey);
        }

        // POST Login with new login details
        [HttpPost]
        public string Post([FromBody] Authentication Auth)
        {
            return Auth.Login();
        }

        // DELETE api/<AuthenticationController>/5
        [HttpDelete("{AuthenticationKey}")]
        public void Delete(string AuthenticationKey)
        {

        }
    }
}
