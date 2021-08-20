using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ToDoList.Model
{
    public abstract class DatabaseCommandFetch
    {
        public abstract Response<SelectReference> ExecuteCommand(string ConnectionString, string Query, SelectReference reference);
    }
}
