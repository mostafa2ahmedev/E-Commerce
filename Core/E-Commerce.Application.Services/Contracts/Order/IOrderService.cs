using E_Commerce.Application.Services.DTO.Order;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.Application.Services.Contracts.Order
{
    public interface IOrderService
    {
        Task<OrderToReturnDto> CreateOrderAsync(string buyerEmail, OrderToCreateDto order);


        Task<OrderToReturnDto> GetOrderByIdAsync(string buyerEmail, int orderId);

        Task<IEnumerable<OrderToReturnDto>> GetOrdersForUserAsync(string buyerEmail);


        Task<IEnumerable<DeliveryMethodDto>> GetAllDeliveryMethodsAsync();
    }
}
