﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.Application.Services.DTO.Products
{
    public class ProductToReturnDto
    {
        public int Id { get; set; }
        public required string Name { get; set; }
        public required string Description { get; set; }

        public string? PictureUrl { get; set; }
        public decimal Price { get; set; }


        public int? BrandId { get; set; }
        public string? Brand { get; set; }
        public int? CategoryId { get; set; }
        public string? Category { get; set; }
    }
}
