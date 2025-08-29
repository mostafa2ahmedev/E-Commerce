using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.Domain.Entities.Basket
{
    public class BasketItem
    {
        public int Id { get; set; } // The same as Product ID

        public required string ProductName { get; set; }

        public string? PictureUrl { get; set; }
        public decimal Price { get; set; }

        public int Quantity { get; set; }
        public string? Brand { get; set; }
        public string? Category { get; set; }
    }
}
