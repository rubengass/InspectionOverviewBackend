using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ToDoList.Model
{
    public class Response<o>
    {
        public bool Success { get; set; }
        public List<int> ErrorCodes { get; set; }
        public string ResponseMessage { get; set; }
        public o Data { get; set; }

        public Response()
        {
            ErrorCodes = new List<int>();
        }

        public void FailedToAuthenticate()
        {
            Success = false;
            ErrorCodes.Add(401);
            ResponseMessage = "Authentication Failed";
        }

        public void ExpiredAuthentication()
        {
            Success = false;
            ErrorCodes.Add(440);
            ResponseMessage = "Session Expired";
        }

        public void FailedToContactServer()
        {
            Success = false;
            ErrorCodes.Add(502);
            ResponseMessage = "Connection failed";
        }

        public void GenericSuccessCode()
        {
            Success = true;
            ErrorCodes.Add(200);
            ResponseMessage = "Request succeeded";
        }
    }
}
