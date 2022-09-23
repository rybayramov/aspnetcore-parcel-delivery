using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Collections.Generic;
using System.Security.Claims;

namespace AuthAPI.Business
{
   public static class TokenHandler
   {
      public static IConfiguration _configuration;
      public static dynamic CreateAccessToken(string UserRole)
      {
         SymmetricSecurityKey symmetricSecurityKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(_configuration["JWT:Security"]));
         TokenValidationParameters tokenValidationParameters = new TokenValidationParameters
         {
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = symmetricSecurityKey,
            ValidateIssuer = true,
            ValidIssuer = _configuration["JWT:Issuer"],
            ValidateAudience = true,
            ValidAudience = _configuration["JWT:Audience"],
            ValidateLifetime = true,
            ClockSkew = TimeSpan.Zero,
            RequireExpirationTime = true
         };
         DateTime now = DateTime.UtcNow;
         JwtSecurityToken jwt = new JwtSecurityToken(
                  issuer: _configuration["JWT:Issuer"],
                  audience: _configuration["JWT:Audience"],
                  claims: new List<Claim> {
                         new Claim(ClaimTypes.Role, UserRole)
                  },
                  notBefore: now,
                  expires: now.Add(TimeSpan.FromMinutes(2)),
                  signingCredentials: new SigningCredentials(symmetricSecurityKey, SecurityAlgorithms.HmacSha256)
              );
         return new
         {
            AccessToken = new JwtSecurityTokenHandler().WriteToken(jwt),
            Expires = TimeSpan.FromMinutes(20).TotalSeconds
         };
      }
   }
}