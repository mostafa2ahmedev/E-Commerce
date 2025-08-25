using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.Application.Services.DTO.Products
{
    public class CategoryToReturnDto
    {
        public int Id { get; set; }
        public required string Name { get; set; }
    }
}
