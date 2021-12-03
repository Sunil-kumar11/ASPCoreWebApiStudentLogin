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
        public string Department { get; set; }
    }
}
