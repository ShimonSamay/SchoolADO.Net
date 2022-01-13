using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using SchoolADO.Net.Models;

namespace SchoolADO.Net.Controllers.api
{
    public class TeacherController : ApiController
    {
        static string conectionString = "Data Source=SHIMONSAMAY;Initial Catalog=SchoolDB-ADO;Integrated Security=True;Pooling=False";
        public IHttpActionResult Get()
        {
            try
            {
                List<Teacher> teachersList = getAllTeachers(conectionString);
                return Ok(new { teachersList });
            }
            catch (SqlException sqlerr)
            {
                return BadRequest(sqlerr.Message);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

       
        public IHttpActionResult Get(int id)
        {
            try
            {
                Teacher someTeacher = getTeacher(conectionString, id);
                return Ok(new { someTeacher });
            }
            catch (SqlException sqlerr)
            {
                return BadRequest(sqlerr.Message);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

       
        public IHttpActionResult Post([FromBody] Teacher newTeacher)
        {
            try
            {
                addTeacher(conectionString, newTeacher);
                List<Teacher> teacherList = getAllTeachers(conectionString);
                return Ok(new { teacherList });
            }
            catch (SqlException sqlerr)
            {
                return BadRequest(sqlerr.Message);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }

        
        public IHttpActionResult Put(int id, [FromBody] Teacher teacher)
        {
            try
            {
                updateTeacher(conectionString, id, teacher);
                List<Teacher> teacherList = getAllTeachers(conectionString);
                return Ok(new { teacherList });
            }
            catch (SqlException sqlerr)
            {
                return BadRequest(sqlerr.Message);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        
        public IHttpActionResult Delete(int id)
        {
            try
            {
                deleteTeacher(conectionString, id);
                List<Teacher> teacherList = getAllTeachers(conectionString);
                return Ok(new { teacherList });
            }
            catch (SqlException sqlerr)
            {
                return BadRequest(sqlerr.Message);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }






        private List<Teacher> getAllTeachers (string connection)
        {
           
                List<Teacher> TeachersList = new List<Teacher>();
                using (SqlConnection DBconnection = new SqlConnection(connection))
                {
                    DBconnection.Open();
                    string query = "SELECT * FROM Teachers";
                    SqlCommand command = new SqlCommand(query, DBconnection);   
                    SqlDataReader DATA  = command.ExecuteReader();
                    if (DATA.HasRows)
                  {
                    while (DATA.Read())
                    {
                      TeachersList.Add(new Teacher(DATA.GetString(1),DATA.GetString(2),DATA.GetInt32(3),DATA.GetString(4)));
                    }
                    DBconnection.Close();
                    return TeachersList;
                  }
                return TeachersList;

                }
           
        }

        private Teacher getTeacher(string connection , int id) { 
        Teacher someTeacher = new Teacher();
        using (SqlConnection DBconnection = new SqlConnection(connection))
            {
                DBconnection.Open();
                string query = $@"SELECT * FROM Teachers WHERE Id = {id}";
                SqlCommand command = new SqlCommand(query,DBconnection);
                SqlDataReader DATA = command.ExecuteReader();
                if (DATA.HasRows)
                {
                    while (DATA.Read())
                    {
                        someTeacher = new Teacher(DATA.GetString(1), DATA.GetString(2), DATA.GetInt32(3), DATA.GetString(4));
                        
                    }
                    DBconnection.Close();
                    return someTeacher;

                    
                }
                return someTeacher;
            }
        }

        private void addTeacher (string connection , Teacher someTeachr)
        {
            using (SqlConnection DBconnection = new SqlConnection(connection))
            {
                DBconnection.Open();
                string query = $@"INSERT INTO Teachers (FirstName , LastName , Wage , BirthDate)
                                 VALUES ('{someTeachr.FirstName}' , '{someTeachr.LastName}' , {someTeachr.Wage} , '{someTeachr.BirthDate}')";
                SqlCommand command = new SqlCommand(query, DBconnection);
                command.ExecuteNonQuery();
                DBconnection.Close();
            }
        }

        private void updateTeacher (string connection, int id, Teacher someTeachr)
        {
            using (SqlConnection DBconnection = new SqlConnection(connection))
            {
                DBconnection.Open();
                string query = $@"UPDATE Teachers
                                  SET FirstName = '{someTeachr.FirstName}' , LastName = '{someTeachr.LastName}' , Wage = {someTeachr.Wage} , BirthDate = '{someTeachr.BirthDate}'
                                  WHERE Id = {id}";
                SqlCommand command = new SqlCommand(query,DBconnection);
                command.ExecuteNonQuery();
                DBconnection.Close();
            }
        }

        private void deleteTeacher (string connection , int id)
        {
            using(SqlConnection DBconnection = new SqlConnection(connection))
            {
                DBconnection.Open();
                string query = $@"DELETE FROM Teachers WHERE Id = {id}";
                SqlCommand command = new SqlCommand(query, DBconnection);
                command.ExecuteNonQuery();
                DBconnection.Close();

            }
        }



    }
}
