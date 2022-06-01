using ASPCoreWebApiLogin.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ASPCoreWebApiLogin
{
    public class Student
    {

        [Key]
        public int SId { get; set; }

        [Required(ErrorMessage = "Please enter name"), MaxLength(30)]
        [DataType(DataType.Text)]
        [Display(Name = "StudentName")]
        public string StudentName { get; set; }

        [Required]
        [Display(Name = "Roll_No")]
        [Range(1, 100000, ErrorMessage = "Accommodation invalid (1-100000).")]
        public int Roll_No { get; set; }

        [Required(ErrorMessage = "Please choose Gender")]
        public string Gender { get; set; }

        public int DptId { get; set; }

        [Required(ErrorMessage ="Please select Department")]
        public List<Department> ListOfDepartment { get; set; }
    }
}
