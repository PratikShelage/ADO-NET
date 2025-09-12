using ADO.NETCRUD.Helper;
using Core.Entities;
using Npgsql;
using NpgsqlTypes;
using System.Data;
using System.Text.Json;
using TestAppProject.DTO;
using WebApi.DTO;
using WebApi.IRepositoy;

namespace WebApi.Repository
{
    public class UserRepo:IUserRepo
    {
        private readonly string _cs;
        private readonly IConfiguration _config;
        
        public UserRepo(IConfiguration config)
        {
            _config = config;
            _cs = _config.GetConnectionString("DefaultConnection")!;


        }

        public async Task<IEnumerable<User>> GetAllAsync(DesignDTO? model)
        {
            try
            {
                List<User> Users = new List<User>();
                using (var connection = new NpgsqlConnection(_cs))
                {
                    
                    NpgsqlCommand cm = new NpgsqlCommand("SELECT * FROM public.\"get_user_values\"( @page ,@pageSize ,@totalPages ,@sortByName ,@sortByType ,@searchByName ,@StartDate ,@EndDate )", connection);
                    cm.Parameters.AddWithValue("@page", Convert.ToInt32(model.page));
                    cm.Parameters.AddWithValue("@pageSize", Convert.ToInt32(model.pageSize));
                    cm.Parameters.AddWithValue("@totalPages", Convert.ToInt32(model.totalPages));
                    if (model.sortByName == null)
                    {

                        cm.Parameters.AddWithValue("@sortByName", DBNull.Value);
                    }
                    else
                    {
                        cm.Parameters.AddWithValue("@sortByName", model.sortByName.ToString());
                    }

                    if (model.sortByType == null)
                    {

                        cm.Parameters.AddWithValue("@sortByType", DBNull.Value);
                    }
                    else
                    {
                        cm.Parameters.AddWithValue("@sortByType", model.sortByType.ToString());
                    }
                    if (model.searchByName == null)
                    {

                        cm.Parameters.AddWithValue("@searchByName", DBNull.Value);
                    }
                    else
                    {
                        cm.Parameters.AddWithValue("@searchByName", model.searchByName.ToString());
                    }
                    if (model.StartDate == null)
                    {

                        cm.Parameters.AddWithValue("@StartDate", DBNull.Value);
                    }
                    else
                    {
                        cm.Parameters.AddWithValue("@StartDate", model.StartDate.ToString());
                    }
                    if (model.StartDate == null)
                    {

                        cm.Parameters.AddWithValue("@EndDate", DBNull.Value);
                    }
                    else
                    {
                        cm.Parameters.AddWithValue("@EndDate", model.StartDate.ToString());
                    }

                    cm.CommandType = System.Data.CommandType.Text;
                    connection.Open();
                    await cm.ExecuteNonQueryAsync();

                    using (NpgsqlDataReader sdr = cm.ExecuteReader())
                    {
                        while (sdr.Read())
                        {
                            User userval = new User();
                            userval.Id =Convert.ToInt32(sdr["id1"]);
                            userval.profilepic = sdr["profilepic1"].ToString();
                            userval.email = sdr["email1"].ToString();
                            userval.password = sdr["password1"].ToString();
                            userval.dob = sdr["dob1"].ToString();
                            userval.age = sdr["age1"].ToString();
                            userval.gender = sdr["gender1"].ToString();
                            userval.about = sdr["about1"].ToString();
                            userval.hobbies = sdr["hobbies1"] as string[];
                            userval.firstname = sdr["firstname1"].ToString();
                            userval.middlename = sdr["middlename1"].ToString();
                            userval.lastname = sdr["lastname1"].ToString();
                            userval.phoneNo = sdr["phoneno1"].ToString();
                            userval.address = sdr["address1"].ToString();
                            userval.landmark = sdr["landmark1"].ToString();
                            userval.pincode = sdr["pincode1"].ToString();
                            userval.country = sdr["country1"].ToString();
                            userval.state = sdr["state1"].ToString();
                            userval.city = sdr["city1"].ToString();
                            userval.role = sdr["role1"].ToString();
                            userval.resetPasswordToken = sdr["resetpasswordtoken1"].ToString() ;
                            userval.resetpasswordExpiry = sdr["resetpasswordexpiry1"].ToString();

                            Users.Add(userval);

                        }
                        return Users;
                    }
                }
            }
            catch (Exception ex)
            {
                throw;
            }


        }

        public async Task<IEnumerable<User>> GetAllUser()
        {
            try
            {
                List<User> Users = new List<User>();
                using (var connection = new NpgsqlConnection(_cs))
                {

                    NpgsqlCommand cm = new NpgsqlCommand("SELECT * FROM public.\"getall_user_values\"( )", connection);

                    cm.CommandType = System.Data.CommandType.Text;
                    connection.Open();
                    await cm.ExecuteNonQueryAsync();

                    using (NpgsqlDataReader sdr = cm.ExecuteReader())
                    {
                        while (sdr.Read())
                        {
                            User userval = new User();
                            userval.Id = Convert.ToInt32(sdr["id1"]);
                            userval.profilepic = sdr["profilepic1"].ToString();
                            userval.email = sdr["email1"].ToString();
                            userval.password = sdr["password1"].ToString();
                            userval.dob = sdr["dob1"].ToString();
                            userval.age = sdr["age1"].ToString();
                            userval.gender = sdr["gender1"].ToString();
                            userval.about = sdr["about1"].ToString();
                            userval.hobbies = sdr["hobbies1"] as string[];
                            userval.firstname = sdr["firstname1"].ToString();
                            userval.middlename = sdr["middlename1"].ToString();
                            userval.lastname = sdr["lastname1"].ToString();
                            userval.phoneNo = sdr["phoneno1"].ToString();
                            userval.address = sdr["address1"].ToString();
                            userval.landmark = sdr["landmark1"].ToString();
                            userval.pincode = sdr["pincode1"].ToString();
                            userval.country = sdr["country1"].ToString();
                            userval.state = sdr["state1"].ToString();
                            userval.city = sdr["city1"].ToString();
                            userval.role = sdr["role1"].ToString();
                            userval.resetPasswordToken = sdr["resetpasswordtoken1"].ToString();
                            userval.resetpasswordExpiry = sdr["resetpasswordexpiry1"].ToString();

                            Users.Add(userval);

                        }
                        return Users;
                    }
                }
            }
            catch (Exception ex)
            {
                throw;
            }


        }

        public async Task<User> GetByIdAsync(int id)
        {
            try
            {
                using (var connection = new NpgsqlConnection(_cs))
                {

                    using (var cmd = new NpgsqlCommand("SELECT * FROM public.\"get_user_byid\"(@id)", connection))
                    {

                        cmd.Parameters.AddWithValue("@id", id);
                        cmd.CommandType = System.Data.CommandType.Text;
                        connection.Open();

                        using (var sdr = await cmd.ExecuteReaderAsync())
                        {
                            if (await sdr.ReadAsync())
                            {
                                return new User
                                {
                                    Id = Convert.ToInt32(sdr["id1"]),
                                    profilepic = sdr["profilepic1"].ToString(),
                                    email = sdr["email1"].ToString(),
                                    password = sdr["password1"].ToString(),
                                    dob = sdr["dob1"].ToString(),
                                    age = sdr["age1"].ToString(),
                                    gender = sdr["gender1"].ToString(),
                                    about = sdr["about1"].ToString(),
                                    hobbies = sdr["hobbies1"] as string[],
                                    firstname = sdr["firstname1"].ToString(),
                                    middlename = sdr["middlename1"].ToString(),
                                    lastname = sdr["lastname1"].ToString(),
                                    phoneNo = sdr["phoneno1"].ToString(),
                                    address = sdr["address1"].ToString(),
                                    landmark = sdr["landmark1"].ToString(),
                                    pincode = sdr["pincode1"].ToString(),
                                    country = sdr["country1"].ToString(),
                                    state = sdr["state1"].ToString(),
                                    city = sdr["city1"].ToString(),
                                    role = sdr["role1"].ToString(),
                                    resetPasswordToken = sdr["resetpasswordtoken1"].ToString(),
                                    resetpasswordExpiry = sdr["resetpasswordexpiry1"].ToString()
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



        public async Task AddAsync(UserDto model)
        {
            try
            {

                using (NpgsqlConnection con = new NpgsqlConnection(_cs))
                {

                    NpgsqlCommand cmd = new NpgsqlCommand(@"
                SELECT public.""create_user""(
                    @profilepic,
                    @email,
                    @password,
                    @dob,
                    @age,
                    @gender,
                    @about,
                    @hobbies,
                    @firstname,
                    @middlename,
                    @lastname,
                    @phoneno,
                    @address,
                    @landmark,
                    @pincode,
                    @country,
                    @state,
                    @city,
                    @role,
                    @resetpasswordtoken,
                    @resetpasswordexpiry
                );", con);
                    var passwordhash = PasswordHashing.HashPassword(model.password);
                    cmd.Parameters.Add("@profilepic", NpgsqlTypes.NpgsqlDbType.Jsonb).Value = JsonSerializer.Serialize(model.profilepic);

                    cmd.Parameters.AddWithValue("@email", model.email);
                    cmd.Parameters.AddWithValue("@password", passwordhash);
                    cmd.Parameters.AddWithValue("@dob", model.dob.ToString());  
                    cmd.Parameters.AddWithValue("@age", model.age);
                    cmd.Parameters.AddWithValue("@gender", model.gender);
                    cmd.Parameters.AddWithValue("@about", model.about);

                    cmd.Parameters.Add("@hobbies", NpgsqlTypes.NpgsqlDbType.Array | NpgsqlTypes.NpgsqlDbType.Text).Value = model.hobbies;

                    cmd.Parameters.AddWithValue("@firstname", model.firstname);
                    cmd.Parameters.AddWithValue("@middlename", model.middlename);
                    cmd.Parameters.AddWithValue("@lastname", model.lastname);
                    cmd.Parameters.AddWithValue("@phoneno", model.phoneNo);
                    cmd.Parameters.AddWithValue("@address", model.address);
                    cmd.Parameters.AddWithValue("@landmark", model.landmark);
                    cmd.Parameters.AddWithValue("@pincode", model.pincode);
                    cmd.Parameters.AddWithValue("@country", model.country);
                    cmd.Parameters.AddWithValue("@state", model.state);
                    cmd.Parameters.AddWithValue("@city", model.city);
                    cmd.Parameters.AddWithValue("@role", model.role);
                    if (model.resetPasswordToken == null)
                    {
         
                        cmd.Parameters.AddWithValue("@resetpasswordtoken", DBNull.Value);
                    }
                    else
                    {
                        cmd.Parameters.AddWithValue("@resetpasswordtoken", model.resetPasswordToken.ToString());
                    }

                    if (model.resetpasswordExpiry == null)
                    {
            
                        cmd.Parameters.AddWithValue("@resetpasswordexpiry", DBNull.Value);
                    }
                    else
                    {
                        cmd.Parameters.AddWithValue("@resetpasswordexpiry", model.resetpasswordExpiry.ToString());
                    }



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





        public async Task UpdateAsync(int id, UserDto model)
        {
            try
            {
                using (NpgsqlConnection con = new NpgsqlConnection(_cs))
                {

                 
                    NpgsqlCommand cmd = new NpgsqlCommand(@"SELECT public.""update_user""(@u_id,@u_profilepic,@u_email, @u_password, @u_dob, @u_age, @u_gender,@u_about,@u_hobbies,@u_firstname, @u_middlename, @u_lastname, @u_phoneno, @u_address,@u_landmark,@u_pincode, @u_country, @u_state,@u_city, @u_role, @u_resetpasswordtoken,@u_resetpasswordexpiry)", con);
                    cmd.Parameters.AddWithValue("@u_id", id);
                    cmd.Parameters.Add("@u_profilepic", NpgsqlTypes.NpgsqlDbType.Jsonb).Value = JsonSerializer.Serialize(model.profilepic);
                    cmd.Parameters.AddWithValue("@u_email", model.email);
                    cmd.Parameters.AddWithValue("@u_password", model.password);
                    cmd.Parameters.AddWithValue("@u_dob", model.dob);
                    cmd.Parameters.AddWithValue("@u_age", model.age);
                    cmd.Parameters.AddWithValue("@u_gender", model.gender);
                    cmd.Parameters.AddWithValue("@u_about", model.about);
                    cmd.Parameters.Add("@u_hobbies", NpgsqlTypes.NpgsqlDbType.Array | NpgsqlTypes.NpgsqlDbType.Text).Value = model.hobbies;
                    cmd.Parameters.AddWithValue("@u_firstname", model.firstname);
                    cmd.Parameters.AddWithValue("@u_middlename", model.middlename);
                    cmd.Parameters.AddWithValue("@u_lastname", model.lastname);
                    cmd.Parameters.AddWithValue("@u_phoneno", model.phoneNo);
                    cmd.Parameters.AddWithValue("@u_address", model.address);
                    cmd.Parameters.AddWithValue("@u_landmark", model.landmark);
                    cmd.Parameters.AddWithValue("@u_pincode", model.pincode);
                    cmd.Parameters.AddWithValue("@u_country", model.country);
                    cmd.Parameters.AddWithValue("@u_state", model.state);
                    cmd.Parameters.AddWithValue("@u_city", model.city);
                    cmd.Parameters.AddWithValue("@u_role", model.role);
                    cmd.Parameters.AddWithValue("@u_resetpasswordtoken", model.resetPasswordToken);
                    cmd.Parameters.AddWithValue("@u_resetpasswordexpiry", model.resetpasswordExpiry);

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

        public async Task DeleteAsync(int id)
        {
            try
            {
                using (var connection = new NpgsqlConnection(_cs))
                {

                    using (var cmd = new NpgsqlCommand("SELECT * FROM public.\"delete_user\"(@id)", connection))
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
