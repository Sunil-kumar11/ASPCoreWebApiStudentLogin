using ASPCoreWebApiLogin.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;

namespace ASPCoreWebApiLogin.ViewModels
{
    public class Departmentviewmodel
    {
        [DisplayName("Department")]
        public string DptId { get; set; }

        public List<Department> ListOfDepartment { get; set; }

        public List<SelectListItem> selectLists { get; set; }
    }
}
