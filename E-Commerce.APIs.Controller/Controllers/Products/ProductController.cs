using E_Commerce.Application.Services.Common;
using E_Commerce.Application.Services.Contracts;
using E_Commerce.Application.Services.DTO.Products;
using E_Commerce.Controllers;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;



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
        [Authorize(AuthenticationSchemes = "Identity.Application")]
        public async Task<ActionResult<Pagination<ProductToReturnDto>>> GetProducts([FromQuery]ProductSpecParams productSpecParams) {

            var products = await _serviceManager.ProductService.GetProductsAsync(productSpecParams);
            return Ok(products);
       
        }
        [HttpGet("{id:int}")]

        public async Task<ActionResult<ProductToReturnDto>> GetProduct(int id)
        {

            var product = await _serviceManager.ProductService.GetProductAsync(id);

            //if(product is null)
            //    return NotFound(new ApiResponse(401,$"The Product with id:{id} is not found."));

            return Ok(product);
        }
        [HttpGet("brands")]

        public async Task<ActionResult<BrandToReturnDto>> GetBrands()
        {

            var Brands = await _serviceManager.ProductService.GetBrandsAsync();



            return Ok(Brands);
        }
        [HttpGet("Categories")]

        public async Task<ActionResult<CategoryToReturnDto>> GetCategories()
        {

            var Categories = await _serviceManager.ProductService.GetCategoriesAsync();



            return Ok(Categories);
        }
    }
}
