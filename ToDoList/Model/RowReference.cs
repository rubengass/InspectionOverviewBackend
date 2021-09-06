using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace ToDoList.Model
{
    public class RowReference
    {
        public List<string> Row { get; set; }

        public RowReference()
        {
            Row = new List<string>();
        }
    }
}
