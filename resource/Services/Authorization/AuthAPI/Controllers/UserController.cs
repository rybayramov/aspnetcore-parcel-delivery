using Microsoft.AspNetCore.Mvc;
using AuthAPI.Business;
using NuGet.Protocol.Core.Types;
using AuthAPI.Infrastructure;
using static Microsoft.AspNetCore.Razor.Language.TagHelperMetadata;
using AuthAPI.Extensions;
using AuthAPI.Model;
using Microsoft.AspNetCore.Authorization;

namespace AuthAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UserController : ControllerBase
    {       

        private readonly ILogger<UserController> _logger;
        IConfiguration _configuration;
        IUserRepository _repository;

        public UserController(ILogger<UserController> logger, IConfiguration configuration, IUserRepository repository)
        {
            _logger = logger;
            _configuration = configuration;
            _repository = repository;
        }

        [Route("[action]", Name = "login")]
        [HttpPost]
        public async Task<ActionResult<string>> Login(string userName, string password)
        {
            TokenHandler._configuration = _configuration;
            var userRole = "admin";// await _repository.GetUserRole(userName,password);
            if (userRole == "")
            {
                return NotFound();
            }
            else { return Ok(TokenHandler.CreateAccessToken(userRole));}
        }

    //    [Route("[action]", Name = "create")]
        [HttpPost]
        public async Task<ActionResult<bool>> CreateUser([FromForm] CreateUserModel user)
        {
            user.UserPassword = Cryptography.Encrypt(user.UserPassword);
            var result = await _repository.CreateUser(user);
            return Ok(result);
        }
        [Authorize]
        [Route("[action]", Name = "courier")]
        [HttpPost]
        public async Task<ActionResult<bool>> Courier([FromForm] CreateUserModel user)
        {
            user.UserPassword = Cryptography.Encrypt(user.UserPassword);
            var result = await _repository.CreateUser(user);
            return Ok(result);
        }
    }
}