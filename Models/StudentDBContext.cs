using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ASPCoreWebApiLogin.Models
{
    public class StudentDBContext :DbContext
    {
        public StudentDBContext(DbContextOptions options) : base(options)
        { }
        public DbSet<Department> Departments { get; set; } 
        public DbSet<Student> Students { get; set; }

    }
}
