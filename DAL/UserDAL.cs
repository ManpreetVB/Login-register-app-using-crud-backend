using Microsoft.Extensions.Diagnostics.HealthChecks;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Linq.Expressions;
using UserManagement.Models;

namespace UserManagement.DAL
{
    public class UserDAL
    {
        private readonly string _connection;

        public UserDAL(IConfiguration configuration)
        {
            _connection = configuration.GetConnectionString("Connection");
        }

        public List<UserDetails> GetAllUsers()
        {
            List<UserDetails> list = new List<UserDetails>();

            using (SqlConnection con = new SqlConnection(_connection))
            {
                SqlCommand cmd = new SqlCommand("dbo.sp_GetAllUsers", con);
                cmd.CommandType = CommandType.StoredProcedure;

                con.Open();
                SqlDataReader dr = cmd.ExecuteReader();

                while (dr.Read())
                {
                    list.Add(new UserDetails
                    {
                        UserId = Convert.ToInt32(dr["UserId"]),
                        UserName = Convert.ToString(dr["UserName"]),
                        Email = Convert.ToString(dr["Email"]),
                        Password = Convert.ToString(dr["Password"]),
                        Gender = Convert.ToString(dr["Gender"]),
                        Address = Convert.ToString(dr["Address"]),
                        CreatedOn = Convert.ToDateTime(dr["CreatedOn"]),
                        IsActive = Convert.ToInt32(dr["IsActive"])
                    });
                }
            }

            return list;
        }

        public string AddUser(UserDetails userDetails)
        {
            string response = "";

            try
            {
                using (SqlConnection con = new SqlConnection(_connection))
                {
                    SqlCommand cmd = new SqlCommand("dbo.sp_NewUser", con);
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@UserName", userDetails.UserName);
                    cmd.Parameters.AddWithValue("@Email", userDetails.Email);
                    cmd.Parameters.AddWithValue("@Password", userDetails.Password);
                    cmd.Parameters.AddWithValue("@Gender", userDetails.Gender);
                    cmd.Parameters.AddWithValue("@Address", userDetails.Address);

                    SqlParameter outputParameter = new SqlParameter("@OutputMsg", SqlDbType.NVarChar, 100);
                    outputParameter.Direction = ParameterDirection.Output;
                    cmd.Parameters.Add(outputParameter);

                    con.Open();
                    cmd.ExecuteNonQuery();

                    response = outputParameter.Value.ToString();
                }
            }
            catch (Exception ex)
            {
                response = ex.Message;
            }

            return response;
        }

        public string UpdateUser(UserDetails userDetails)
        {
            string response = "";

            try
            {
                using (SqlConnection con = new SqlConnection(_connection))
                {
                    SqlCommand cmd = new SqlCommand("sp_UpdateExistUser", con);
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@UserId", userDetails.UserId);
                    cmd.Parameters.AddWithValue("@UserName", userDetails.UserName);
                    cmd.Parameters.AddWithValue("@Email", userDetails.Email);
                    cmd.Parameters.AddWithValue("@Password", userDetails.Password);
                    cmd.Parameters.AddWithValue("@Gender", userDetails.Gender);
                    cmd.Parameters.AddWithValue("@Address", userDetails.Address);
                    cmd.Parameters.AddWithValue("@IsActive", userDetails.IsActive);
                    SqlParameter outputParameter = new SqlParameter("@OutputMsg", SqlDbType.NVarChar, 100);
                    outputParameter.Direction = ParameterDirection.Output;
                    cmd.Parameters.Add(outputParameter);

                    con.Open();
                    cmd.ExecuteNonQuery();

                    response = outputParameter.Value.ToString();
                }
            }
            catch (Exception ex)
            {
                response = ex.Message;
            }

            return response;
        }

        public UserDetails GetUserById(int UserID)
        {
            UserDetails user = new UserDetails();
            using (SqlConnection con = new SqlConnection(_connection))
            {
                SqlCommand cmd = new SqlCommand("sp_GetByIdUser", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@UserId", UserID);
           
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                con.Open();
                da.Fill(dt);
                if (dt.Rows.Count>0)
                {
                    user.UserId = Convert.ToInt32(dt.Rows[0]["UserId"]);
                    user.UserName = Convert.ToString(dt.Rows[0]["UserName"]);
                    user.Email = Convert.ToString(dt.Rows[0]["Email"]);
                    user.Password = Convert.ToString(dt.Rows[0]["Password"]);
                    user.Gender = Convert.ToString(dt.Rows[0]["Gender"]);
                    user.Address = Convert.ToString(dt.Rows[0]["Address"]);
                    user.CreatedOn = Convert.ToDateTime(dt.Rows[0]["CreatedOn"]);
                    user.IsActive = Convert.ToInt32(dt.Rows[0]["IsActive"]);

                }
                return user;

            }

            
        }

        public string DeleteUser( int UserId )
        {
            string response = "";

            try
            {
                using (SqlConnection con = new SqlConnection(_connection))
                {
                    SqlCommand cmd = new SqlCommand("sp_DeleteUser", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@UserId", UserId);

                  
                    SqlParameter outputParameter = new SqlParameter("@OutputMsg", SqlDbType.NVarChar, 100);
                    outputParameter.Direction = ParameterDirection.Output;
                    cmd.Parameters.Add(outputParameter);

                    con.Open();
                    cmd.ExecuteNonQuery();

                    response = outputParameter.Value.ToString();
                }
            }
            catch (Exception ex)
            {
                response = ex.Message;
            }

            return response;
        }


        public UserDetails LoginUser(UserDetails userDetails)

        {
            string response = "";
            UserDetails user = new UserDetails();
            using (SqlConnection con = new SqlConnection(_connection))
            {
                SqlCommand cmd = new SqlCommand("sp_UserLogin", con);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@Email", userDetails.Email);
                cmd.Parameters.AddWithValue("@Password", userDetails.Password);

                SqlParameter outputParameter = new SqlParameter("@OutputMsg", SqlDbType.NVarChar, 100);
                outputParameter.Direction = ParameterDirection.Output;
                cmd.Parameters.Add(outputParameter);

                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                con.Open();
                da.Fill(dt);
                if (dt.Rows.Count > 0)
                {
                    user.UserId = Convert.ToInt32(dt.Rows[0]["UserId"]);
                    user.UserName = Convert.ToString(dt.Rows[0]["UserName"]);
                    user.Email = Convert.ToString(dt.Rows[0]["Email"]);
                    user.Password = Convert.ToString(dt.Rows[0]["Password"]);
                    user.Gender = Convert.ToString(dt.Rows[0]["Gender"]);
                    user.Address = Convert.ToString(dt.Rows[0]["Address"]);
                    user.CreatedOn = Convert.ToDateTime(dt.Rows[0]["CreatedOn"]);
                    user.IsActive = Convert.ToInt32(dt.Rows[0]["IsActive"]);

                }
                response = outputParameter.Value.ToString();
                return user;

            }
        }

            public string RegisterUser(UserDetails userDetails)
        {
            string response = "";

            try
            {
                using (SqlConnection con = new SqlConnection(_connection))
                {
                    SqlCommand cmd = new SqlCommand("sp_UserRegistration", con);
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@UserName", userDetails.UserName);
                    cmd.Parameters.AddWithValue("@Email", userDetails.Email);
                    cmd.Parameters.AddWithValue("@Password", userDetails.Password);
                    cmd.Parameters.AddWithValue("@Gender", userDetails.Gender);
                    cmd.Parameters.AddWithValue("@Address", userDetails.Address);

                    SqlParameter outputParameter = new SqlParameter("@OutputMsg", SqlDbType.NVarChar, 100);
                    outputParameter.Direction = ParameterDirection.Output;
                    cmd.Parameters.Add(outputParameter);

                    con.Open();
                    cmd.ExecuteNonQuery();

                    response = outputParameter.Value.ToString();
                }
            }
            catch (Exception ex)
            {
                response = ex.Message;
            }

            return response;
        }





    }
}
