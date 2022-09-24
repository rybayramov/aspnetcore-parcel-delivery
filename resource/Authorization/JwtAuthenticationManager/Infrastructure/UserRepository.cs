
using Dapper;
using JwtAuthenticationManager.Models;
using Microsoft.Extensions.Configuration;
using Npgsql;


namespace JwtAuthenticationManager.Infrastructure
{
    public class UserRepository
    {
        private readonly IConfiguration _configuration;

        public UserRepository(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task<string> CreateUser(CreateUserRequest Request,string rol)
        {
            using var connection = new NpgsqlConnection(_configuration.GetValue<string>("DatabaseSettings:ConnectionString"));

            connection.Open();
            var role = await connection.QueryFirstOrDefaultAsync<string>
                ("SELECT username FROM Users WHERE username = @UserName", new { UserName = Request.UserName });

            if (role != null)
                return $"{Request.UserName} already defined";

            using var command = new NpgsqlCommand
            {
                Connection = connection
            };

            command.CommandText = $"INSERT INTO Users(UserName, UserPassword, fullname, company, Rol) VALUES('{Request.UserName}', '{Request.Password}','','', '{rol}'); ";
            command.ExecuteNonQuery();
            return "success";
        }
        public async Task<string> CreateCourierUser(CreateCourierRequest Request, string rol)
        {
            using var connection = new NpgsqlConnection(_configuration.GetValue<string>("DatabaseSettings:ConnectionString"));

            connection.Open();
            var role = await connection.QueryFirstOrDefaultAsync<string>
                ("SELECT username FROM Users WHERE username = @UserName", new { UserName = Request.UserName });

            if (role != null)
                return $"{Request.UserName} already defined";

            using var command = new NpgsqlCommand
            {
                Connection = connection
            };

            command.CommandText = $"INSERT INTO Users(UserName, UserPassword, fullname, company, Rol) VALUES('{Request.UserName}', '{Request.Password}', '{Request.FullName}', '{Request.Company}', '{rol}'); ";
            command.ExecuteNonQuery();
            return "success";
        }
        public async Task<string> GetUserRole(string UserName, string Password)
        {
            using var connection = new NpgsqlConnection(_configuration.GetValue<string>("DatabaseSettings:ConnectionString"));

            var role = await connection.QueryFirstOrDefaultAsync<string>
                ("SELECT rol FROM Users WHERE username = @UserName", new { UserName = UserName });

            if (role == null)
                return "";
            return role;
        }
    }
}
