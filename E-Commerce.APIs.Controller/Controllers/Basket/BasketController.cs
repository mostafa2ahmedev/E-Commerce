using E_Commerce.Application.Services.Contracts;
using E_Commerce.Application.Services.DTO.Basket;
using E_Commerce.Controllers;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace E_Commerce.APIs.Controller.Controllers.Basket
{
    public class BasketController : BaseApiController
    {
        private readonly IServiceManager _serviceManager;

        public BasketController(IServiceManager serviceManager)
        {
            _serviceManager = serviceManager;
        }

        [HttpGet]

        public async Task<ActionResult<CustomerBasketDto>> GetBasket(string id) { 
            var basket = await _serviceManager.BasketService.GetCustomerBasketAsync(id);


            return Ok(basket);
        }
        [HttpPost]

        public async Task<ActionResult<CustomerBasketDto>> UpdateBasket(CustomerBasketDto customerBasketDto)
        {
            var basket = await _serviceManager.BasketService.UpdateCustomerBasketAsync(customerBasketDto);


            return Ok(basket);
        }
        [HttpDelete]

        public async Task DeleteBasket(string id)
        {
             await _serviceManager.BasketService.DeleteCustomerBasketAsync(id);


            
        }
    }
}
