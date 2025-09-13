
using E_Commerce.Shared.DTO.Basket;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.Application.Services.Common.Contracts.Infrastructure
{
    public interface IPaymentService
    {
        Task<CustomerBasketDto?> CreateOrUpdatePaymentIntent(string basketId);
    }
}
