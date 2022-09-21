using Microsoft.AspNetCore.Mvc;
using Auth.API.Business;
using Auth.API.Repositories.Interfaces;
using Auth.API.Entities;
using System.Net;

namespace Auth.API.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class UsersController : ControllerBase
    {
        IConfiguration _configuration;
        private readonly IDiscountRepository _repository;

        public UsersController(IConfiguration configuration, IDiscountRepository repository)
        {
            _configuration = configuration;
            _repository = repository;
        }

        [Route("[action]", Name = "authenticate")]
        [HttpPost]
        public IActionResult Authenticate(string userName, string password)
        {
            TokenHandler._configuration = _configuration;
            return Ok(userName == "gncy" && password == "12345" ? TokenHandler.CreateAccessToken() : new UnauthorizedResult());
        }
        [HttpGet("{productName}", Name = "GetDiscount")]
        [ProducesResponseType(typeof(Coupon), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<Coupon>> GetDiscount(string productName)
        {
            var discount = await _repository.GetDiscount(productName);
            return Ok(discount);
        }
    }
}
