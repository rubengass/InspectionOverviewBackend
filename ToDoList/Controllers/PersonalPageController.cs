using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ToDoList.Controllers
{
    [Route("api/User")]
    [ApiController]
    public class PersonalPageController : ControllerBase
    {
        // GET api/User/5
        [HttpGet("{UserType:string}, {UserId:int}")]
        public string Get(string UserType, int UserId)
        {
            return "value";
        }
    }
}
