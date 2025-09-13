using E_Commerce.Application.Services.DTO.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.Application.Services.DTO.Order
{
    public class OrderToReturnDto
    {
        public int Id { get; set; }
        public required string BuyerEmail { get; set; }
        public DateTime OrderDate { get; set; } 
        public required string Status { get; set; }

        public required AddressDto ShippingAddress { get; set; }
        public int? DeliveryMethodId { get; set; }
        public string? DeliveryMethod  { get; set; }

        public ICollection<OrderItemDto> Items { get; set; } = new HashSet<OrderItemDto>();

        public decimal SubTotal { get; set; }
        public decimal Total { get; set; }

        public string PaymentIntentId { get; set; } =null!;
    }
}
