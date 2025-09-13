
using E_Commerce.Application.Services.Common.Contracts.Infrastructure;
using E_Commerce.Application.Services.Contracts;
using E_Commerce.Controllers;
using E_Commerce.Shared.DTO.Basket;
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
        private readonly IBasketService _basketService;

        public BasketController(IBasketService basketService)
        {
            _basketService = basketService;
        }

        [HttpGet]

        public async Task<ActionResult<CustomerBasketDto>> GetBasket(string id) { 
            var basket = await _basketService.GetCustomerBasketAsync(id);


            return Ok(basket);
        }
        [HttpPost]

        public async Task<ActionResult<CustomerBasketDto>> UpdateBasket(CustomerBasketDto customerBasketDto)
        {
            var basket = await _basketService.UpdateCustomerBasketAsync(customerBasketDto);


            return Ok(basket);
        }
        [HttpDelete]

        public async Task DeleteBasket(string id)
        {
             await _basketService.DeleteCustomerBasketAsync(id);


            
        }
    }
}
