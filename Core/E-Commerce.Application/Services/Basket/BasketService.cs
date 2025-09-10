using AutoMapper;
using E_Commerce.Application.Services.Contracts.Basket;
using E_Commerce.Application.Services.DTO.Basket;
using E_Commerce.Application.Common.Exceptions;

using E_Commerce.Domain.Contracts.Infrastructure;
using E_Commerce.Domain.Entities.Basket;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.Application.Services.Basket
{
    internal class BasketService : IBasketService
    {
        private readonly IBasketRepository _basketRepository;
        private readonly IMapper _mapper;

        public BasketService(IBasketRepository basketRepository , IMapper mapper)
        {
            _basketRepository = basketRepository;
            _mapper = mapper;
        }
        public async Task<CustomerBasketDto> GetCustomerBasketAsync(string basketId)
        {
            var basket = await  _basketRepository.GetAsync(basketId);

            if (basket is null) throw new NotFoundException(nameof(CustomerBasket) , basketId);

            return _mapper.Map<CustomerBasketDto>(basket);

        }
        public async Task<CustomerBasketDto> UpdateCustomerBasketAsync(CustomerBasketDto customerBasketDto)
        {
            var basket = _mapper.Map<CustomerBasket>(customerBasketDto);

            var updatedBasket = await _basketRepository.UpdateAsync(basket);

            if (updatedBasket is null) throw new BadRequestException("Can't update, there is a problem with the basket");

            return customerBasketDto;
        }
        public async Task DeleteCustomerBasketAsync(string basketId)
        {
            var deleted = await _basketRepository.DeleteAsync(basketId);

            if (!deleted) throw new BadRequestException("unable to delete this basket");
        }



    
    }
}
