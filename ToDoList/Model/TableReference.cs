using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ToDoList.Model
{
    public class TableReference
    {
        public List<RowReference> Rows { get; set; }

        public TableReference()
        {
            Rows = new List<RowReference>();
        }
    }
}
