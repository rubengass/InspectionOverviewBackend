using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ToDoList.Model
{
    public class TableReference
    {
        public List<SelectReference> ListOfValues { get; set; }

        public TableReference()
        {
            ListOfValues = new List<SelectReference>();
        }

    }
}
