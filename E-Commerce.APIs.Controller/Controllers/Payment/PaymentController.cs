
using E_Commerce.Application.Services.Common.Contracts.Infrastructure;
using E_Commerce.Controllers;
using E_Commerce.Shared.DTO.Basket;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;


namespace E_Commerce.APIs.Controller.Controllers.Payment
{
    public class PaymentController :BaseApiController
    {
        private readonly IPaymentService _paymentService;

        public PaymentController(IPaymentService paymentService)
        {
            _paymentService = paymentService;
        }

        [Authorize]
        [HttpPost("{basketId}")]
        public async Task<ActionResult<CustomerBasketDto>> CreateOrUpdatePaymentIntent(string basketId) { 
        
                var result = await _paymentService.CreateOrUpdatePaymentIntent(basketId);

                return Ok(result);



        }

    }
}
