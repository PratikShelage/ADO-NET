using Core.Entities;
using Microsoft.AspNetCore.Http.HttpResults;
using Npgsql;
using Repo.IRepositoy;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection.Metadata;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using TestAppProject.DTO;
using WebApi.DTO;

namespace Repo.Repository
{
    public class StudentRepo : IStudenRepo
    {
        private readonly string _cs;
        private readonly IConfiguration _config;
        public StudentRepo(IConfiguration config)
        {
            _config = config;
            _cs = _config.GetConnectionString("DefaultConnection")!;

        }

        public async Task<IEnumerable<Student>> GetAllAsync(DesignDTO model)
        {
            try
            {
                List<Student> students = new List<Student>();

                using (var connection = new NpgsqlConnection(_cs))
                {
                    
                    NpgsqlCommand cm = new NpgsqlCommand("SELECT * FROM public.\"get_student_values\"(  @page ,@pageSize ,@totalPages ,@sortByName ,@sortByType ,@searchByName ,@StartDate ,@EndDate ,@isPresent ,@isAbsent )", connection);

                   
                    cm.Parameters.AddWithValue("@page", Convert.ToInt32(model.page));
                    cm.Parameters.AddWithValue("@pageSize", Convert.ToInt32(model.pageSize));
                    cm.Parameters.AddWithValue("@totalPages", Convert.ToInt32(model.totalPages));         
                    cm.Parameters.AddWithValue("@sortByName", string.IsNullOrEmpty(model.sortByName) ? (object)DBNull.Value : model.sortByName);
                    cm.Parameters.AddWithValue("@sortByType", string.IsNullOrEmpty(model.sortByType) ? (object)DBNull.Value : model.sortByType);
                    cm.Parameters.AddWithValue("@searchByName", string.IsNullOrEmpty(model.searchByName) ? (object)DBNull.Value : model.searchByName);
                    cm.Parameters.AddWithValue("@StartDate", string.IsNullOrEmpty(model.StartDate) ? (object)DBNull.Value : model.StartDate);
                    cm.Parameters.AddWithValue("@EndDate", string.IsNullOrEmpty(model.EndDate) ? (object)DBNull.Value : model.EndDate);
                    cm.Parameters.AddWithValue("@isPresent", Convert.ToBoolean(model.isPresent));
                    cm.Parameters.AddWithValue("@isAbsent", Convert.ToBoolean(model.isAbsent));

                    cm.CommandType = System.Data.CommandType.Text;

         
                    await connection.OpenAsync();

                    using (var sdr = await cm.ExecuteReaderAsync())
                    {
                        while (await sdr.ReadAsync())
                        {
                            Student student = new Student
                            {
                                sid = Convert.ToInt32(sdr["sid1"]),
                                firstname = sdr["firstname1"].ToString()!,
                                lastname = sdr["lastname1"].ToString()!,
                                rollno = Convert.ToInt32(sdr["rollno1"]),
                                studentclass = sdr["studentclass1"].ToString()!,
                                presentdate = sdr["presentdate1"].ToString()!,
                                ispresent = Convert.ToBoolean(sdr["ispresent1"]),
                                isabsent = Convert.ToBoolean(sdr["isabsent1"])
                            };

                            students.Add(student);
                        }
                    }
                }

                return students;
            }
            catch (Exception ex)
            {

                throw new Exception("Error fetching student data", ex);
            }
        }



        public async Task<IEnumerable<Student>> GetAllStudent()
        {
            try
            {
                List<Student> students = new List<Student>();
                using (var connection = new NpgsqlConnection(_cs))
                {

                    NpgsqlCommand cm = new NpgsqlCommand("SELECT * FROM public.\"getall_student_values\"()", connection);
                    cm.CommandType = System.Data.CommandType.Text;
                    connection.Open();
           

                    using (NpgsqlDataReader sdr = cm.ExecuteReader())
                    {
                        while (sdr.Read())
                        {
                            Student student = new Student();
                            student.sid = Convert.ToInt32(sdr["sid1"]);
                            student.firstname = sdr["firstname1"].ToString()!;
                            student.lastname = sdr["lastname1"].ToString()!;
                            student.rollno = Convert.ToInt32(sdr["rollno1"]);
                            student.studentclass = sdr["studentclass1"].ToString()!;
                            student.presentdate = sdr["presentdate1"].ToString()!;
                            student.ispresent = Convert.ToBoolean(sdr["ispresent1"]);
                            student.isabsent = Convert.ToBoolean(sdr["isabsent1"]);

                            students.Add(student);

                        }
                        return students;
                    }
                }
            }
            catch (Exception ex)
            {
                throw;
            }


        }
        public async Task<Student> GetByIdAsync(int id)
        {
            try
            {
                using (var connection = new NpgsqlConnection(_cs))
                {

                    using (var cmd = new NpgsqlCommand("SELECT * FROM public.\"get_student_byid\"(@id)", connection))
                    {

                        cmd.Parameters.AddWithValue("@id", id);
                        cmd.CommandType = System.Data.CommandType.Text;
                        connection.Open();
                        using (var reader = await cmd.ExecuteReaderAsync())
                        {
                            if (await reader.ReadAsync())
                            {
                                return new Student
                                {
                                    sid = Convert.ToInt32(reader["sid1"]),
                                    firstname = reader["firstname1"].ToString()!,
                                    lastname = reader["lastname1"].ToString()!,
                                    rollno = Convert.ToInt32(reader["rollno1"]),
                                    studentclass = reader["studentclass1"].ToString()!,
                                    presentdate = reader["presentdate1"].ToString()!,
                                    ispresent = Convert.ToBoolean(reader["ispresent1"]),
                                    isabsent = Convert.ToBoolean(reader["isabsent1"])
                                };
                            }
                        }
                        connection.Close();
                    }
                }

                return null!;

            }
            catch (Exception ex)
            {
                throw;
            }

        }



        public async Task AddAsync(StudentDTO model)
        {
            try
            {
                using (NpgsqlConnection con = new NpgsqlConnection(_cs))
                {

                    NpgsqlCommand cmd = new NpgsqlCommand("SELECT * FROM public.\"create_student\"(@firstname,@lastname,@rollno,@studentclass,@presentdate,@ispresent,@isabsent)", con);
                    cmd.Parameters.AddWithValue("@firstname", model.firstname.ToString());
                    cmd.Parameters.AddWithValue("@lastname", model.lastname.ToString());
                    cmd.Parameters.AddWithValue("@rollno", Convert.ToInt32(model.rollno));
                    cmd.Parameters.AddWithValue("@studentclass", model.studentclass.ToString());
                    cmd.Parameters.AddWithValue("@presentdate", model.presentdate.ToString());
                    cmd.Parameters.AddWithValue("@ispresent", Convert.ToBoolean(model.ispresent));
                    cmd.Parameters.AddWithValue("@isabsent", Convert.ToBoolean(model.isabsent));

                    cmd.CommandType = CommandType.Text;
                    await con.OpenAsync();

                    await cmd.ExecuteNonQueryAsync();

                    con.Close();
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }





        public async Task UpdateAsync(int id, StudentDTO model)
        {
            try
            {
                using (NpgsqlConnection con = new NpgsqlConnection(_cs))
                {
                    NpgsqlCommand cmd = new NpgsqlCommand("SELECT * FROM public.\"update_student\"(@sid1,@firstname1,@lastname1,@rollno1,@studentclass1,@presentdate1,@ispresent1,@isabsent1)", con);
                    cmd.Parameters.AddWithValue("@sid1", id);
                    cmd.Parameters.AddWithValue("@firstname1", model.firstname.ToString());
                    cmd.Parameters.AddWithValue("@lastname1", model.lastname.ToString());
                    cmd.Parameters.AddWithValue("@rollno1", Convert.ToInt32(model.rollno));
                    cmd.Parameters.AddWithValue("@studentclass1", model.studentclass.ToString());
                    cmd.Parameters.AddWithValue("@presentdate1", model.presentdate.ToString());
                    cmd.Parameters.AddWithValue("@ispresent1", Convert.ToBoolean(model.ispresent));
                    cmd.Parameters.AddWithValue("@isabsent1", Convert.ToBoolean(model.isabsent));

                    cmd.CommandType = CommandType.Text;
                    con.Open();

                    NpgsqlDataReader reader = cmd.ExecuteReader();

                    con.Close();
                }
            }
            catch (Exception ex)
            {
                throw;
            }


        }

        public async Task DeleteAsync(int id)
        {
            try
            {
                using (var connection = new NpgsqlConnection(_cs))
                {

                    using (var cmd = new NpgsqlCommand("SELECT * FROM public.\"delete_student\"(@id)", connection))
                    {

                        cmd.Parameters.AddWithValue("@id", id);
                        cmd.CommandType = System.Data.CommandType.Text;
                        connection.Open();

                        NpgsqlDataReader reader = cmd.ExecuteReader();

                        connection.Close();
                    }
                }

            }
            catch (Exception ex)
            {
                throw;
            }



        }
    }
}

//after click of view button open one model with all data of student with readonly with bydefault image make it proper se proper color and background color with shodow use bootstrap
//frist i want to create one filter button with icon after click filter button open model with from and to Date filter with save and close  aloso add the one name and roll number input field and in table change name Date to Addmission date and remove the present absent feild add one feild with view with eye icon after click view icon open model with student profile with all data make it proper with alignment and add proper color and showdoe backgorund color and im also sending the css so you and imporve the css and aslo use bootstrap