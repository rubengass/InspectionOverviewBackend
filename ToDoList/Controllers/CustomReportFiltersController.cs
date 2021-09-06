using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ToDoList.Model;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ToDoList.Controllers
{
    [Route("api/CustomReportFilter")]
    [ApiController]
    public class CustomReportFiltersController : ControllerBase
    {
        // GET api/<CustomReportFiltersController>/5
        [HttpGet("{FieldReferenceId}")]
        public Response<CustomReportFilter> Get(string FieldReferenceId)
        {
            Response<CustomReportFilter> response = new Response<CustomReportFilter>();
            response.GenericSuccessCode();
            response.Data = new CustomReportFilter(FieldReferenceId);
            return response;
        }
    }
}
