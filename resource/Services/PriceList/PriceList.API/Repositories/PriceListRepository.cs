using Dapper;
using PriceList.API.Entities;
using PriceList.API.Repositories.Interfaces;
using Microsoft.Extensions.Configuration;
using Npgsql;
using System;
using System.Threading.Tasks;

namespace PriceList.API.Repositories
{
    public class PriceListRepository : IPriceListRepository
    {
        private readonly IConfiguration _configuration;

        public PriceListRepository(IConfiguration configuration)
        {
            _configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));
        }       

        public async Task<PriceList.API.Entities.PriceList> GetPrice(string productWeight)
        {            
            using var connection = new NpgsqlConnection(_configuration.GetValue<string>("DatabaseSettings:ConnectionString"));

            var _price = await connection.QueryFirstOrDefaultAsync<PriceList.API.Entities.PriceList>
                ("SELECT * FROM price_list WHERE WeightFrom < @productWeight and @productWeight <= WeightTo", new { productWeight = productWeight });

            if (_price == null)
                return new PriceList.API.Entities.PriceList { WeightFrom = 20, Amount = 25, WeightTo = 10000 };
            return _price;
        }

        public async Task<bool> CreatePrice(PriceList.API.Entities.PriceList entity)
        {
            using var connection = new NpgsqlConnection(_configuration.GetValue<string>("DatabaseSettings:ConnectionString"));

            var affected =
                await connection.ExecuteAsync
                    ("INSERT INTO Coupon (WeightFrom, WeightTo, Amount) VALUES (@WeightFrom, @WeightTo, @Amount)",
                            new { ProductName = entity.WeightFrom, Description = entity.WeightTo, Amount = entity.Amount });

            if (affected == 0)
                return false;

            return true;
        }

        public async Task<bool> UpdatePrice(PriceList.API.Entities.PriceList entity)
        {
            using var connection = new NpgsqlConnection(_configuration.GetValue<string>("DatabaseSettings:ConnectionString"));

            var affected = await connection.ExecuteAsync
                    ("UPDATE Coupon SET WeightFrom=@WeightFrom, WeightTo = @WeightTo, Amount = @Amount WHERE Id = @Id",
                            new { WeightFrom = entity.WeightFrom, WeightTo = entity.WeightTo, Amount = entity.Amount, Id = entity.Id });

            if (affected == 0)
                return false;

            return true;
        }

    }
}
