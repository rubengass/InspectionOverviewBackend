using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ToDoList.Model
{
    public abstract class DatabaseCommandFetchTable
    {
        public abstract Response<TableReference> ExecuteCommand(string ConnectionString, string Query, int ColNum);
    }
}
