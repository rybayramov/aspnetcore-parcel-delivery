using AutoMapper;
using Auth.Application.Features.Orders.Commands.CheckoutOrder;
using Auth.Application.Features.Orders.Commands.UpdateOrder;
using Auth.Application.Features.Orders.Queries.GetOrdersList;
using Auth.Domain.Entities;

namespace Auth.Application.Mappings
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Order, OrdersVm>().ReverseMap();
            CreateMap<Order, CheckoutOrderCommand>().ReverseMap();
            CreateMap<Order, UpdateOrderCommand>().ReverseMap();
        }
    }
}
