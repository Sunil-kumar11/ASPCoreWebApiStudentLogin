using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ASPCoreWebApiLogin.Models
{
    [Table("Departments")]
    public class Department
    {
        [Key]
        public int DptId { get; set; }

        public String  DepartmentName{ get; set; }
    }
}
