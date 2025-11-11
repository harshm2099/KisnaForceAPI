using Microsoft.Data.SqlClient;
using NewAvatarWebApis.Core.Application.Responses;
using NewAvatarWebApis.Core.Domain.Common.NewAvatarWebApis.Models;
using NewAvatarWebApis.Infrastructure.Data;
using NewAvatarWebApis.Infrastructure.Services.Interfaces;
using System.Data;
using System.Text.RegularExpressions;

// We can use IActionResult in Framework version 8, which gives you some by default methods,
namespace NewAvatarWebApis.Infrastructure.Services
{
    public class UserService : IUserService
    {
        private readonly string _Connection = DBCommands.CONNECTION_STRING;
        private readonly IConfiguration _configuration;

        //public UserService(IConfiguration configuration)
        //{
        //    _Connection = configuration.GetConnectionString("KisnaDBConnection");
        //}

        public async Task<CommonResponse> ChangePassword(ChangePasswordParams request)
        {
            var response = new CommonResponse { success = false, message = "Something went wrong." };

            try
            {
                if (string.IsNullOrWhiteSpace(request.data_id) || string.IsNullOrWhiteSpace(request.password))
                {
                    response.message = "data_id and password are required.";
                    return response;
                }

                var passwordPattern = new Regex(@"^(?=.*?[A-Z])(?=.*?[a-z])(?=.*?[0-9])(?=.*?[#?!@$%^&*\-]).{6,}$");
                if (!passwordPattern.IsMatch(request.password))
                {
                    response.message = "Your password must be more than 6 characters long, should contain at least 1 Uppercase, 1 Lowercase, 1 Numeric and 1 special character.";
                    return response;
                }

                var hashedPassword = BCrypt.Net.BCrypt.HashPassword(request.password);

                using (SqlConnection dbConnection = new SqlConnection(_Connection))
                {
                    dbConnection.Open();
                    using (SqlCommand cmd = new SqlCommand(DBCommands.CHANGE_PASSWORD, dbConnection))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@DataID", request.data_id);
                        cmd.Parameters.AddWithValue("@Password", hashedPassword);

                        var returnValue = cmd.Parameters.Add("@ReturnVal", SqlDbType.Int);
                        returnValue.Direction = ParameterDirection.Output;

                        cmd.ExecuteNonQuery();
                        int result = (int)returnValue.Value;

                        if (result == 1)
                        {
                            response.success = true;
                            response.message = "Change password successfully.";
                        }
                        else if (result == -1)
                        {
                            response.message = "No data available.";
                        }
                    }
                }
                return response;
            }
            catch (SqlException sqlEx)
            {
                return new CommonResponse
                {
                    status = "400",
                    status_code = "400",
                    success = false,
                    message = $"SQL error: {sqlEx.Message}",
                };
            }
        }

        public async Task<CommonResponse> ResetPassword(ChangePasswordParams request)
        {
            var response = new CommonResponse { success = false, message = "Something went wrong." };

            try
            {
                if (string.IsNullOrWhiteSpace(request.data_id) || string.IsNullOrWhiteSpace(request.password))
                {
                    response.message = "data_id and password are required.";
                    return response;
                }

                var passwordPattern = new Regex(@"^(?=.*?[A-Z])(?=.*?[a-z])(?=.*?[0-9])(?=.*?[#?!@$%^&*\-]).{6,}$");
                if (!passwordPattern.IsMatch(request.password))
                {
                    response.message = "Your password must be more than 6 characters long, should contain at least 1 Uppercase, 1 Lowercase, 1 Numeric and 1 special character.";
                    return response;
                }

                var hashedPassword = BCrypt.Net.BCrypt.HashPassword(request.password);

                using (SqlConnection dbConnection = new SqlConnection(_Connection))
                {
                    dbConnection.Open();
                    using (SqlCommand cmd = new SqlCommand(DBCommands.CHANGE_PASSWORD, dbConnection))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@DataID", request.data_id);
                        cmd.Parameters.AddWithValue("@Password", hashedPassword);

                        var returnValue = cmd.Parameters.Add("@ReturnVal", SqlDbType.Int);
                        returnValue.Direction = ParameterDirection.Output;

                        cmd.ExecuteNonQuery();
                        int result = (int)returnValue.Value;

                        if (result == 1)
                        {
                            response.success = true;
                            response.message = "Change password successfully.";
                        }
                        else if (result == -1)
                        {
                            response.message = "No data available.";
                        }
                    }
                }
                return response;
            }
            catch (SqlException sqlEx)
            {
                return new CommonResponse
                {
                    status = "400",
                    status_code = "400",
                    success = false,
                    message = $"SQL error: {sqlEx.Message}",
                };
            }
        }
    }
}
