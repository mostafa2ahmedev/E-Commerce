



using E_Commerce.Application.Services.Contracts.Authentication;
using E_Commerce.Application.Services.Contracts.Basket;
using E_Commerce.Application.Services.Contracts.Order;
using E_Commerce.Application.Services.Contracts.Products;

namespace E_Commerce.Application.Services.Contracts
{
    public interface IServiceManager
    {
        IProductService ProductService { get; }
        IBasketService BasketService { get; }
        IAuthService AuthService { get; }
        IOrderService OrderService { get; }
    }
}
