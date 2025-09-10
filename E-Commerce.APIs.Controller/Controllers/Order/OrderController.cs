using E_Commerce.Application.Services.Contracts;
using E_Commerce.Application.Services.DTO.Order;
using E_Commerce.Controllers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;


namespace E_Commerce.APIs.Controller.Controllers.Order
{
    public class OrderController : BaseApiController
    {
        private readonly IServiceManager _serviceManager;

        public OrderController(IServiceManager serviceManager)
        {
            _serviceManager = serviceManager;
        }


        [HttpPost]
        [Authorize]
        public async Task<ActionResult<OrderToReturnDto>> CreateOrder(OrderToCreateDto orderDto) {
            var buyerEmail = User.FindFirstValue(ClaimTypes.Email);
            var result = await _serviceManager.OrderService.CreateOrderAsync(buyerEmail!, orderDto);

            return Ok(result);

        }
        [HttpGet]
        [Authorize]
        public async Task<ActionResult<IEnumerable<OrderToReturnDto>>> GetOrdersForUser()
        {
            var buyerEmail = User.FindFirstValue(ClaimTypes.Email);
            var result = await _serviceManager.OrderService.GetOrdersForUserAsync(buyerEmail!);

            return Ok(result);

        }
        [HttpGet("{id}")]
        [Authorize]
        public async Task<ActionResult<OrderToReturnDto>> GetOrder(int id)
        {
            var buyerEmail = User.FindFirstValue(ClaimTypes.Email);
            var result = await _serviceManager.OrderService.GetOrderByIdAsync(buyerEmail!,id);

            return Ok(result);

        }
        [HttpGet("deliveryMethods")]
        [Authorize]
        public async Task<ActionResult<IEnumerable<DeliveryMethodDto>>> GetDeliveryMethods()
        {
        
            var result = await _serviceManager.OrderService.GetAllDeliveryMethodsAsync();

            return Ok(result);

        }
    }
}
