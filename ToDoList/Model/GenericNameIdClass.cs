using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ToDoList.Model
{
    public class GenericNameIdClass
    {
        public string ID { get; set; }
        public string Name { get; set; }

        public void GetDataFromiNumber(string Query)
        {
            SelectReference reference = new SelectReference();
            reference.GenericNameIdReference();
            InspectionDatabaseManager DBM = InspectionDatabaseManager.getInstance();
            Response<SelectReference> response = new Response<SelectReference>();
            response = DBM.fetch(new DatabaseCommandSelect(), Query, reference);
            if (response.Success)
            {
                ID = response.Data.Value[0];
                Name = response.Data.Value[1];
            }
        }
    }
}
