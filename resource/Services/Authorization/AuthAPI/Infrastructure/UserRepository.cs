using AuthAPI.Model;
using Dapper;
using Npgsql;
using static Microsoft.AspNetCore.Razor.Language.TagHelperMetadata;

namespace AuthAPI.Infrastructure
{
    public class UserRepository:IUserRepository
    {
        private readonly IConfiguration _configuration;

        public UserRepository(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task<bool> CreateUser(CreateUserModel Request)
        {
            using var connection = new NpgsqlConnection(_configuration.GetValue<string>("DatabaseSettings:ConnectionString"));

            connection.Open();

            using var command = new NpgsqlCommand
            {
                Connection = connection
            };

            command.CommandText = $"INSERT INTO Users(UserName, UserPassword, Rol) VALUES('{Request.UserName}', '{Request.UserPassword}', 'user'); ";
            command.ExecuteNonQuery();
            return true;
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
