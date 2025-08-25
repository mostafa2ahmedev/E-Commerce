using E_Commerce.Application.Services.Contracts;
using E_Commerce.Application.Services.DTO.Products;
using E_Commerce.Controllers;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace E_Commerce.APIs.Controller.Controllers.Products
{
    public class ProductController : BaseApiController
    {
        private readonly IServiceManager _serviceManager;

        public ProductController(IServiceManager serviceManager)
        {
            _serviceManager = serviceManager;
        }



        [HttpGet]

        public async Task<ActionResult<IEnumerable<ProductToReturnDto>>> GetProducts() {

            var products = await _serviceManager.ProductService.GetProductsAsync();
            return Ok(products);
        }
        [HttpGet("{id:int}")]

        public async Task<ActionResult<ProductToReturnDto>> GetProduct(int id)
        {

            var product = await _serviceManager.ProductService.GetProductAsync(id);

            if(product is null)
                return NotFound();

            return Ok(product);
        }
    }
}
