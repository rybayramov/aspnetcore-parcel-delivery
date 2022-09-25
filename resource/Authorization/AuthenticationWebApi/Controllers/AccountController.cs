using JwtAuthenticationManager;
using JwtAuthenticationManager.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AuthenticationWebApi.Controllers
{
    [Route("api/account")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly JwtTokenHandler _jwtTokenHandler;

        public AccountController(JwtTokenHandler jwtTokenHandler)
        {
            _jwtTokenHandler = jwtTokenHandler;
        }
        [Route("login")]
        [HttpPost]
        public ActionResult<AuthenticationResponse?> Authenticate([FromBody] AuthenticationRequest authenticationRequest)
        {
            var authenticationResponse = _jwtTokenHandler.GenerateJwtToken(authenticationRequest);
            if (authenticationResponse == null) return Unauthorized();
            return authenticationResponse;
        }
        [Route("courier")]
        [HttpPost]
        public ActionResult<string> CreateCourier([FromBody] CreateCourierRequest Request)
        {
            var authenticationResponse = _jwtTokenHandler.CreateCourierUser(Request);
            return authenticationResponse;
        }
        [Route("user")]
        [HttpPost]
        public ActionResult<string> CreateUser([FromBody] CreateUserRequest Request)
        {
            var authenticationResponse = _jwtTokenHandler.CreateUser(Request);
            return authenticationResponse;
        }
    }
}
