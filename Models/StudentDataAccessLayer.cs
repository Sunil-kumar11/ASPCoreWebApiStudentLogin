using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace ASPCoreWebApiLogin.Models
{
    public class StudentDataAccessLayer
    {
        string ConnectionString = "Data Source=DESKTOP-S6PCJR3;Initial Catalog=College;User ID=Sa;Password=Bizgaze@123";
        
        public IEnumerable<Student> GetAllStudents() { 

        List<Student> lststudents = new List<Student>();

            //To View all Students Details
            using (SqlConnection con = new SqlConnection(ConnectionString))
            {
                string query = "select * from Students s inner join Departments d on s.SId = d.DptId";
                SqlCommand cmd = new SqlCommand(query,con);
                con.Open();
                SqlDataReader rdr = cmd.ExecuteReader();

                while (rdr.Read())
                {
                    Student student = new Student();
                    student.SId = Convert.ToInt32(rdr["SId"]);
                    student.StudentName = rdr["StudentName"].ToString();
                    student.Roll_No = Convert.ToInt32(rdr["Roll_No"]);
                    student.Gender = rdr["Gender"].ToString();
                    student.DptId = Convert.ToInt32(rdr["DptId"]);
                    student.Department = rdr["DepartmentName"].ToString();

                    lststudents.Add(student);
                }
                con.Close();
            }
            return lststudents;
                
         }
        //To Add new Student record      
        public void AddStudent(Student student)
        {
                using (SqlConnection con = new SqlConnection(ConnectionString))
                {
                    SqlCommand cmd = new SqlCommand("SP_AddStudent", con);
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@StudentName", student.StudentName);
                    cmd.Parameters.AddWithValue("@Roll_No", student.Roll_No);
                    cmd.Parameters.AddWithValue("@Gender", student.Gender);
                    cmd.Parameters.AddWithValue("@DepartmentName", student.Department);

                    con.Open();
                    cmd.ExecuteNonQuery();
                    con.Close();
                }
        }

    }
}
