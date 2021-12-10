using ASPCoreWebApiLogin.ViewModels;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace ASPCoreWebApiLogin.Models
{
    public class StudentDataAccessLayer
 {
        SqlConnection con;
        public StudentDataAccessLayer()
        {
            var configuation = GetConfiguration();
            con = new SqlConnection(configuation.GetSection("ConnectionStrings").GetSection("sqlConnection").Value);
        }
        public IConfigurationRoot GetConfiguration()
        {
            var builder = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);
            return builder.Build();
        }

       // string ConnectionString = "Data Source=DESKTOP-S6PCJR3;Initial Catalog=College;User ID=Sa;Password=Bizgaze@123";

        //string Query = "INSERT INTO [dbo].[Students](StudentName, Roll_No, Gender, DptId) VALUES(@StudentName, @Roll_No, @Gender, @DptId)";

        public IEnumerable<StudentDisplayViewModel> GetAllStudents()
        {

            List<StudentDisplayViewModel> lststudents = new List<StudentDisplayViewModel>();

            //To View all Students Details
            //using (SqlConnection con = new SqlConnection(ConnectionString))
            //{
                
                string query = "select * from Students s inner join Departments d on s.DptId = d.DptId";
                SqlCommand cmd = new SqlCommand(query, con);
                con.Open();
                SqlDataReader rdr = cmd.ExecuteReader();

                while (rdr.Read())
                {
                    StudentDisplayViewModel student = new StudentDisplayViewModel();
                    student.SId = Convert.ToInt32(rdr["SId"]);
                    student.StudentName = rdr["StudentName"].ToString();
                    student.Roll_No = Convert.ToInt32(rdr["Roll_No"]);
                    student.Gender = rdr["Gender"].ToString();
                    student.DptId = Convert.ToInt32(rdr["DptId"]);
                    student.Department1 = rdr["DepartmentName"].ToString();

                    lststudents.Add(student);
                }
                con.Close();
            //}
            return lststudents;

        }
        public Student GetStudentData(int? id)
        {
            Student student = new Student();

            //using (SqlConnection con = new SqlConnection(ConnectionString))
            //{
                string sqlQuery = "select * from Students WHERE SID = " + id;
                SqlCommand cmd = new SqlCommand(sqlQuery, con);

                con.Open();
                SqlDataReader rdr = cmd.ExecuteReader();

                while (rdr.Read())
                {
                    student.SId = Convert.ToInt32(rdr["SId"]);
                    student.StudentName = rdr["StudentName"].ToString();
                    student.Gender = rdr["Gender"].ToString();
                    student.Roll_No = Convert.ToInt32(rdr["Roll_No"]);
                    student.DptId = Convert.ToInt32(rdr["DptId"]);
                }
            //}
            return student;
        }


        //To Add new Student record      
        public void AddStudent(Student student)
        {
          string  Query = "IF EXISTS ( SELECT * FROM Students WHERE SId =" + student.SId + ") UPDATE dbo.Students SET StudentName = @StudentName,Roll_No = @Roll_No,Gender = @Gender,DptId= DptId  WHERE SId =" + student.SId + "; ELSE INSERT INTO [dbo].[Students](StudentName, Roll_No, Gender, DptId) VALUES(@StudentName, @Roll_No, @Gender, @DptId)";

            //using (SqlConnection con = new SqlConnection(ConnectionString))
            //{
                SqlCommand cmd = new SqlCommand( Query, con);
                //cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@StudentName", student.StudentName);
                cmd.Parameters.AddWithValue("@Roll_No", student.Roll_No);
                cmd.Parameters.AddWithValue("@Gender", student.Gender);
                cmd.Parameters.AddWithValue("@DptId", student.DptId);


                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
            //}
        }
        //To Update the records of a particluar Student
        public void UpdateStudent(Student student)
        {
            //using (SqlConnection con = new SqlConnection(ConnectionString))
            //{
                string Query = "IF EXISTS ( SELECT * FROM Students WHERE SId =" + student.SId + ") UPDATE dbo.Students SET StudentName = @StudentName,Roll_No = @Roll_No,Gender = @Gender,DptId= DptId  WHERE SId =" + student.SId + "; ELSE INSERT INTO [dbo].[Students](StudentName, Roll_No, Gender, DptId) VALUES(@StudentName, @Roll_No, @Gender, @DptId)";
                SqlCommand cmd = new SqlCommand(Query, con);


                cmd.Parameters.AddWithValue("@StudentName", student.StudentName);
                cmd.Parameters.AddWithValue("@Roll_No", student.Roll_No);
                cmd.Parameters.AddWithValue("@Gender", student.Gender);
                cmd.Parameters.AddWithValue("@DptId", student.DptId);

                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
            //}
        }

        //To Delete the record on a particular Student    
        public void DeleteStudent(int? id)
        {

            //using (SqlConnection con = new SqlConnection(ConnectionString))
            //{
                SqlCommand cmd = new SqlCommand(" DELETE FROM Students WHERE SId= "+id, con);
              

                cmd.Parameters.AddWithValue("@SId", id);

                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
            //}
        }

    }
}
