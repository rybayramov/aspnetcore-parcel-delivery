using entity = PriceList.API.Entities;
using PriceList.API.Repositories.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Net;
using System.Threading.Tasks;

namespace PriceList.API.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class PriceListController : ControllerBase
    {
        private readonly IPriceListRepository _repository;

        public PriceListController(IPriceListRepository repository)
        {
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
        }
        
        [HttpGet("{productName}", Name = "GetPrice")]
        [ProducesResponseType(typeof(entity.PriceList), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<entity.PriceList>> GetPrice(string productName)
        {
            var Price = await _repository.GetPrice(productName);
            return Ok(Price);
        }

        [HttpPost]
        [ProducesResponseType(typeof(entity.PriceList), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<entity.PriceList>> CreatePrice([FromBody] entity.PriceList entity)
        {
            await _repository.CreatePrice(entity);
            return CreatedAtRoute("GetPrice", new { productName = entity.Amount }, entity);
        }

        [HttpPut]
        [ProducesResponseType(typeof(entity.PriceList), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<entity.PriceList>> UpdatePrice([FromBody] entity.PriceList entity)
        {
            return Ok(await _repository.UpdatePrice(entity));
        }

    }
}
