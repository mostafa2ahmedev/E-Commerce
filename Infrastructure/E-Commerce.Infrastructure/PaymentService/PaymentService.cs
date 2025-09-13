using AutoMapper;
using E_Commerce.Application.Common.Exceptions;
using E_Commerce.Application.Services.Common.Contracts.Infrastructure;
using E_Commerce.Application.Services.Contracts.Products;
using E_Commerce.Domain.Contracts.Infrastructure;
using E_Commerce.Domain.Contracts.Persistence;
using E_Commerce.Domain.Entities.Basket;
using E_Commerce.Domain.Entities.Orders;
using E_Commerce.Domain.Entities.Products;
using E_Commerce.Shared.DTO.Basket;
using Microsoft.Extensions.Configuration;
using Stripe;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Product = E_Commerce.Domain.Entities.Products.Product;

namespace E_Commerce.Infrastructure.PaymentServices
{
    internal class PaymentService : IPaymentService
    {

        private readonly IBasketRepository _basketRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IConfiguration _configuration;

        public PaymentService(IBasketRepository basketRepository,IUnitOfWork unitOfWork,IMapper mapper,IConfiguration configuration)
        {

            _basketRepository = basketRepository;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _configuration = configuration;
        }
        public async Task<CustomerBasketDto?> CreateOrUpdatePaymentIntent(string basketId)
        {

            //1. Get Basket 
            var basket = await _basketRepository.GetAsync(basketId);

            if(basket is null) throw new NotFoundException(nameof(CustomerBasket),basketId);

            //2. validate about the delivery method id 
            if (basket.DeliveryMethodId.HasValue) {
                var deliveryMethod = await _unitOfWork.GetRepository<DeliveryMethod, int>().GetAsync(basket.DeliveryMethodId.Value);
                if(deliveryMethod is null) throw new NotFoundException(nameof(DeliveryMethod), basket.DeliveryMethodId);
                basket.ShippingPrice = deliveryMethod!.Cost;
            }

            //3. validate about the items price
            if(basket.Items.Count > 0) {
                var productRepo =  _unitOfWork.GetRepository<Product, int>();
                foreach (var item in basket.Items)
                {
                    var product = await productRepo.GetAsync(item.Id);
                    if(product is null) throw new NotFoundException(nameof(Product), item.Id);
                    if (item.Price!= product.Price)
                        item.Price = product.Price;
                }
            
            }

            //4. create or update payment intent
            StripeConfiguration.ApiKey = _configuration.GetSection("StripeSettings")["SecretKey"];

            PaymentIntent? paymentIntent = null;
            PaymentIntentService paymentIntentService = new PaymentIntentService();
            if (string.IsNullOrEmpty(basket.PaymentIntentId))  // Create NEW Payment Intent
            {
                var options = new PaymentIntentCreateOptions()
                {
                    Amount = (long)basket.Items.Sum(item => item.Price * 100 * item.Quantity) + (long)(basket.ShippingPrice * 100),
                    Currency = "AED",
                    PaymentMethodTypes = new List<string>() { "card" }
                };
                paymentIntent = await paymentIntentService.CreateAsync(options);
                basket.PaymentIntentId = paymentIntent.Id;
                basket.ClientSecret = paymentIntent.ClientSecret;

              await  _basketRepository.UpdateAsync(basket);
            }
            else // Update an existing Payment Intent
            {
                var options = new PaymentIntentUpdateOptions()
                {
                    Amount = (long)basket.Items.Sum(item => item.Price * 100 * item.Quantity) + (long)(basket.ShippingPrice * 100),
                    
                };
                await paymentIntentService.UpdateAsync(basket.PaymentIntentId,options);
            }

            return _mapper.Map<CustomerBasketDto>(basket);
        }
    }
}
