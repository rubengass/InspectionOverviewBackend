using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ToDoList.Model
{
    public abstract class DatabaseCommand
    {
        public abstract Response<string> ExecuteCommand(string ConnectionString, string Query);
    }
}
