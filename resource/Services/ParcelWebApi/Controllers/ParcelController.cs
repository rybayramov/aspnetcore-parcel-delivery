using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;

namespace ParcelWebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ParcelController : ControllerBase
    {
        private static readonly string[] Summaries = new[]
        {
        "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
    };

        private readonly ILogger<ParcelController> _logger;

        public ParcelController(ILogger<ParcelController> logger)
        {
            _logger = logger;
        }
      //  [Authorize(Roles = "Administrator")]
        [HttpGet]
     //   [Route("{UserName}")]
        public async Task<ActionResult> Get()
        {
            Request.Headers.TryGetValue("UserName", out var UserName);
            var identity = UserName;

            return Ok(true);
        }
    }
}