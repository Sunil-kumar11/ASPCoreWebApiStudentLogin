using ASPCoreWebApiLogin.Models;
using ASPCoreWebApiLogin.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
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
        List<StudentDisplayViewModel> lstStudent = new List<StudentDisplayViewModel>();

        public IActionResult Index()
        {
            var query = (from t1 in _databaseContext.Students
                         join t2 in _databaseContext.Departments on t1.DptId equals t2.DptId
                         select new { t1.StudentName, t1.Roll_No, t1.Gender, t2.DepartmentName }).ToList(); ;

            foreach (var stud in query)
            {
                StudentDisplayViewModel student = new StudentDisplayViewModel();

                student.StudentName = stud.StudentName;
                student.Roll_No = stud.Roll_No;
                student.Gender = stud.Gender;
                student.Department1 = stud.DepartmentName;
                lstStudent.Add(student);
            }
           
            //lstStudent = objStudent.GetAllStudents().ToList();
            return View(lstStudent);
        }

        //public IActionResult Create()
        //{

        //    var DepartmentList = (from Department in _databaseContext.Departments
        //                          select new SelectListItem()
        //                          {
        //                              Text = Department.DepartmentName,
        //                              Value = Department.DptId.ToString()
        //                          }).ToList();
        //DepartmentList.Insert(0, new SelectListItem()
        //{
        //    Text = "-----Select----",
        //    Value = string.Empty
        //});



        //List<String> listValue = new List<String>();
        //List<String> listText = new List<String>();


        //for (int i = 0; i < DepartmentList.Count; i++)
        //{
        //    string Text = DepartmentList[i].Text;
        //    listText.Add(Text);
        //    string Value = DepartmentList[i].Value;
        //    listValue.Add(Value);
        //}


        //ViewBag.ListofDepartments = DepartmentList;

        //SelectList list = new SelectList(DepartmentList, "DptId", "DepartmentName");




        //   ViewBag.ListofDepartments = DepartmentList;
        //ViewBag.selectLists = listValue;
        //    return View();
        //}

        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public IActionResult Create(Student student, IFormCollection form)
        //{

        //    string str = form["ListOfDepartment"].ToString();
        //    student.DptId = Convert.ToInt32(str);
        //    objStudent.AddStudent(student);
        //    //if(0>1)
        //    //{
        //    var DepartmentList = (from Department in _databaseContext.Departments
        //                          select new SelectListItem()
        //                          {
        //                              Text = Department.DepartmentName,
        //                              Value = Department.DptId.ToString()
        //                          }).ToList();
        //    //DepartmentList.Insert(0, new SelectListItem()
        //    //{
        //    //    Text = "-----Select----",
        //    //    Value = string.Empty
        //    //});
        //    ViewBag.ListofDepartments = DepartmentList;
        //    ViewBag.message = "student details added successfully";
        //    ModelState.Clear();
        //    return View("Create");
        //}
        //else
        //{
        //    ViewBag.message = "student details added failed";

        //    return View("Create");
        //}

        //public IActionResult Edit(int Id)

        //{

        //    lstStudent = objStudent.GetAllStudents().ToList();

        //    var std = lstStudent.Where(s => s.SId == Id).FirstOrDefault();

        //    var DepartmentList = (from Department in _databaseContext.Departments
        //                          select new SelectListItem()
        //                          {
        //                              Text = Department.DepartmentName,
        //                              Value = Department.DptId.ToString()
        //                          }).ToList();
        //    DepartmentList.Insert(0, new SelectListItem()
        //    {
        //        Text = "-----Select----",
        //        Value = string.Empty
        //    });

        //    ViewBag.ListofDepartments = DepartmentList;
        //    return View(std);
        //}



        //[HttpPost]
        ////[Route("api/[controller]/{action}/{Id}")]
        //public ActionResult Edit(Student std)
        //{



        //    var student = lstStudent.Where(s => s.SId == std.SId).FirstOrDefault();
        //    lstStudent.Remove(student);
        //    lstStudent.Add(std);

        //    return RedirectToAction("Index");

        //}

        //[HttpGet]
        //public IActionResult Edit(int? id)
        //{

        //    if (id == null)
        //    {
        //        return NotFound();
        //    }
        //    Student student = objStudent.GetStudentData(id);

        //    if (student == null)
        //    {
        //        return NotFound();
        //    }
        //    var DepartmentList = (from Department in _databaseContext.Departments
        //                          select new SelectListItem()
        //                          {
        //                              Text = Department.DepartmentName,
        //                              Value = Department.DptId.ToString()
        //                          }).ToList();


        //    ViewBag.ListofDepartments = DepartmentList;
        //    return View(student);
        //}

        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public IActionResult Edit(Student student, IFormCollection form)
        //{
        //    string str = form["DepartmentName"].ToString();
        //    student.DptId = Convert.ToInt32(str);

        //    student.Roll_No = Convert.ToInt32(form["Roll_No"]);


        //    objStudent.UpdateStudent(student);
        //    return RedirectToAction("Index");
        //}

        [HttpGet]
        public IActionResult Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            Student student = objStudent.GetStudentData(id);
            if (student != null)
            {
                objStudent.DeleteStudent(student.SId);
                return RedirectToAction("Index");
            }


            return View(student);
        }

        [HttpPost]

        public IActionResult Delete(Student student)
        {
            objStudent.DeleteStudent(student.SId);
            return RedirectToAction("Index");
        }

        //AddOrEdit Get Method
        public async Task<IActionResult> AddOrEdit(int Id)
        {
            var DepartmentList = (from Department in _databaseContext.Departments
                                  select new SelectListItem()
                                  {
                                      Text = Department.DepartmentName,
                                      Value = Department.DptId.ToString()
                                  }).ToList();

            ViewBag.ListofDepartments = DepartmentList;

            ViewBag.PageName = Id == null ? "Create Student" : "Edit Student";
            ViewBag.IsEdit = Id == null ? false : true;
            if (Id == null)
            {
                return View();
            }
            else
            {
                int SId = Id;
                var student1 = objStudent.GetStudentData(SId);
                var student = _databaseContext.Students.FindAsync(SId);

                if (student1 == null)
                {
                    return NotFound();
                }
                return View(student1);
            }
        }

        //AddOrEdit Post Method
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddOrEdit(Student studentData, IFormCollection form)
        {
            string str = form["ListOfDepartment"].ToString();
            studentData.DptId = Convert.ToInt32(str);
            bool IsStudentExist = false;
            int SId = studentData.SId;
            var student1 = objStudent.GetStudentData(SId);
            // Student student = await _databaseContext.Students.FindAsync(SId);

            if (student1 != null)
            {
                IsStudentExist = true;
            }
            else
            {
                student1= new Student();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    student1.StudentName = studentData.StudentName;
                    student1.Roll_No = studentData.Roll_No;
                    student1.Gender = studentData.Gender;
                    student1.DptId = studentData.DptId;
                    

                    if (IsStudentExist)
                    {
                        //objStudent.AddStudent(student1);
                        _databaseContext.Update(student1);
                        ViewBag.message = "student details Updated successfully";
                    }
                    else
                    {
                        //objStudent.AddStudent(student1);
                        _databaseContext.Add(student1);
                        ViewBag.message = "student details added successfully";
                    }
                    await _databaseContext.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    throw;
                }
                return RedirectToAction(nameof(Index));
            }
            return View();
        }

    }
}