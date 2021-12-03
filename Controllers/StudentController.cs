using ASPCoreWebApiLogin.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ASPCoreWebApiLogin.Controllers
{
    public class StudentController : Controller
    {
        private StudentDBContext _databaseContext;
        public StudentController(StudentDBContext databaseContex)
        {
            _databaseContext = databaseContex;
        }
        StudentDataAccessLayer objStudent = new StudentDataAccessLayer();
        List<Student> lstStudent = new List<Student>();
        public IActionResult Index()
        {
        lstStudent = objStudent.GetAllStudents().ToList();

            return View(lstStudent);
        } 

        public IActionResult Create()
        {

            var DepartmentList=(from Department in _databaseContext.Departments
                select new SelectListItem() 
                { 
                   Text=Department.DepartmentName, 
                   Value=Department.DptId.ToString()
                  }).ToList();
            DepartmentList.Insert(0, new SelectListItem()
            {
                Text = "-----Select----",
                Value = string.Empty
            });
            SelectList list = new SelectList(DepartmentList, "DptId", "DepartmentName");



            ViewBag.ListofDepartments = list;
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Student student)
        {

           
            objStudent.AddStudent(student);
            //if(0>1)
            //{
                ViewBag.message = "student details added successfully";
                ModelState.Clear();
                return View("Create");
            //}
            //else
            //{
            //    ViewBag.message = "student details added failed";

            //    return View("Create");
            //}
        }
    }
}
