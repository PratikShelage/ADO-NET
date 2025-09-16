using Core.Entities;
using Npgsql;
using System.Data;
using TestAppProject.DTO;
using WebApi.DTO;
using WebApi.Entities;
using WebApi.IRepositoy;

namespace WebApi.Repository
{
    public class AttendanceRepo:IAttendance
    {
        private readonly string _cs;
        private readonly IConfiguration _config;
        public AttendanceRepo(IConfiguration config)
        {
            _config = config;
            _cs = _config.GetConnectionString("DefaultConnection")!;

        }
        public async Task<IEnumerable<Attendance>> GetAllAsync(int page, int pageSize , string attendancesearch)
        {
            try
            {
                var attendances = new List<Attendance>();

                using var connection = new NpgsqlConnection(_cs);
                using var cmd = new NpgsqlCommand("SELECT * FROM public.\"getall_attendance_values\"(@page, @pageSize,@attendancesearch)", connection);

                cmd.Parameters.AddWithValue("@page", page);
                cmd.Parameters.AddWithValue("@pageSize", pageSize);
                cmd.Parameters.AddWithValue("@attendancesearch", attendancesearch);
                cmd.CommandType = System.Data.CommandType.Text;

                await connection.OpenAsync();

                using var reader = await cmd.ExecuteReaderAsync();
                while (await reader.ReadAsync())
                {
                    var attendance = new Attendance
                    {
                        aid = Convert.ToInt32(reader["aid1"]),
                        firstname = reader["firstname1"].ToString()!,
                        lastname = reader["lastname1"].ToString()!,
                        rollno = Convert.ToInt32(reader["rollno1"]),
                        studentclass = reader["studentclass1"].ToString()!,
                        presentdate = reader["presentdate1"].ToString()!,
                        ispresent = Convert.ToBoolean(reader["ispresent1"]),
                        isabsent = Convert.ToBoolean(reader["isabsent1"])
                    };

                    attendances.Add(attendance);
                }

                return attendances;
            }
            catch (Exception ex)
            {
                throw new Exception($"Error fetching attendance data: {ex.Message}", ex);
            }
        }



        public async Task AddAsync(Attendance model)
        {
            try
            {
                using (NpgsqlConnection con = new NpgsqlConnection(_cs))
                {

                    NpgsqlCommand cmd = new NpgsqlCommand("SELECT * FROM public.\"create_attendance\"(@aid,@firstname,@lastname,@rollno,@studentclass,@presentdate,@ispresent,@isabsent)", con);
                    cmd.Parameters.AddWithValue("@aid", Convert.ToInt32(model.aid));
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

            public async Task<IEnumerable<Attendance>> GetAllAttendanceAsync()
        {
            try
            {
                List<Attendance> attendances = new List<Attendance>();

                using (var connection = new NpgsqlConnection(_cs))
                {

                    NpgsqlCommand cm = new NpgsqlCommand("SELECT * FROM public.\"getall_attendance_values_without_pagination\"()", connection);
                    cm.CommandType = System.Data.CommandType.Text;
                    connection.Open();

                    using (NpgsqlDataReader sdr = cm.ExecuteReader())
                    {
                        while (sdr.Read())
                        {
                            Attendance attendance = new Attendance
                            {
                                aid = Convert.ToInt32(sdr["aid1"]),
                                firstname = sdr["firstname1"].ToString()!,
                                lastname = sdr["lastname1"].ToString()!,
                                rollno = Convert.ToInt32(sdr["rollno1"]),
                                studentclass = sdr["studentclass1"].ToString()!,
                                presentdate = sdr["presentdate1"].ToString()!,
                                ispresent = Convert.ToBoolean(sdr["ispresent1"]),
                                isabsent = Convert.ToBoolean(sdr["isabsent1"])
                            };

                            attendances.Add(attendance);
                        }
                    }
                }

                return attendances;
            }
            catch (Exception ex)
            {

                throw new Exception("Error fetching attedance data", ex);
            }
        }
    }
    
}
