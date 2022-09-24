using JwtAuthenticationManager.Infrastructure;
using JwtAuthenticationManager.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace JwtAuthenticationManager
{
    public class JwtTokenHandler
    {
        public const string JWT_SECURITY_KEY = "yPkCqn4kSWLtaJwXvN2jGzpQRyTZ3gdXkt7FeBJP";
        private const int JWT_TOKEN_VALIDITY_MINS = 20;

        private readonly IConfiguration configuration;

        public JwtTokenHandler(IConfiguration configuration)
        {
            this.configuration = configuration;
        }


        //private readonly List<UserAccount> _userAccountList;

        //public JwtTokenHandler()
        //{
        //    _userAccountList = new List<UserAccount>
        //    {
        //        new UserAccount{ UserName = "admin", Password = "admin123", Role = "Administrator" },
        //        new UserAccount{ UserName = "user01", Password = "user01", Role = "User" },
        //    };
        //}

        public AuthenticationResponse? GenerateJwtToken(AuthenticationRequest authenticationRequest)
        {
            UserRepository _repository=new UserRepository(configuration);
            if (string.IsNullOrWhiteSpace(authenticationRequest.UserName) || string.IsNullOrWhiteSpace(authenticationRequest.Password))
                return null;

            /* Validation */
            var userRole = _repository.GetUserRole(authenticationRequest.UserName, authenticationRequest.Password).Result;
            if (userRole == null) return null;

            var tokenExpiryTimeStamp = DateTime.Now.AddMinutes(JWT_TOKEN_VALIDITY_MINS);
            var tokenKey = Encoding.ASCII.GetBytes(JWT_SECURITY_KEY);
            var claimsIdentity = new ClaimsIdentity(new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Name, authenticationRequest.UserName),
                new Claim("Role", userRole)
            });

            var signingCredentials = new SigningCredentials(
                new SymmetricSecurityKey(tokenKey),
                SecurityAlgorithms.HmacSha256Signature);

            var securityTokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = claimsIdentity,
                Expires = tokenExpiryTimeStamp,
                SigningCredentials = signingCredentials
            };

            var jwtSecurityTokenHandler = new JwtSecurityTokenHandler();
            var securityToken = jwtSecurityTokenHandler.CreateToken(securityTokenDescriptor);
            var token = jwtSecurityTokenHandler.WriteToken(securityToken);

            return new AuthenticationResponse
            {
                UserName = authenticationRequest.UserName,
                ExpiresIn = (int)tokenExpiryTimeStamp.Subtract(DateTime.Now).TotalSeconds,
                JwtToken = token
            };
        }

        public string CreateCourierUser(CreateCourierRequest user)
        {
           return (new UserRepository(configuration)).CreateCourierUser(user, "courier").Result;
        }
        public string CreateUser(CreateUserRequest user)
        {
          return  (new UserRepository(configuration)).CreateUser(user, "user").Result;
           
        }
    }
}
