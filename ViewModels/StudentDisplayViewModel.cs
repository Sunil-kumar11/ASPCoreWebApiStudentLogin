using ASPCoreWebApiLogin.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ASPCoreWebApiLogin.ViewModels
{
    public class StudentDisplayViewModel
    {
        // public List<tblinsertDepatrtment> DepartmentsList;


        public int SId { get; set; }

        [Required]
        [StringLength(maximumLength: 50, ErrorMessage = "Student Name Is Too Long")]
        [Display(Name = "StudentName")]
        public string StudentName { get; set; }

        [Required]

        public int Roll_No { get; set; }

        [Required]
        public string Gender { get; set; }

        public int DptId { get; set; }

        [Required]
        public string Department1 { get; set; }

        public SelectList Departmet { get; set; }
        public List<Department> ListOfDepartment { get; set; }

    }
}
