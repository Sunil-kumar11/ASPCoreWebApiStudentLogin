using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;

namespace ASPCoreWebApiLogin.Models
{
    public class Departmentviewmodel
    {
        [DisplayName("Department")]
        public string DptId { get; set; }

        public List<SelectListItem> ListOfDepartment { get; set; }
    }
}
