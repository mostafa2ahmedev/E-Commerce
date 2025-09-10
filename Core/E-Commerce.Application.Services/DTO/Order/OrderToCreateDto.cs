using E_Commerce.Application.Services.DTO.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.Application.Services.DTO.Order
{
    public class OrderToCreateDto
    {
        public required string BasketId { get; set; }
        public int DeliveryMethodId { get; set; }
        public required AddressDto AddressDto { get; set; }
    }
}
