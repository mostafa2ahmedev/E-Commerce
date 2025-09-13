using AutoMapper;
using E_Commerce.Application.Common.Exceptions;
using E_Commerce.Application.Services.Common.Contracts.Infrastructure;
using E_Commerce.Application.Services.Contracts.Order;
using E_Commerce.Application.Services.DTO.Order;
using E_Commerce.Domain.Contracts.Infrastructure;
using E_Commerce.Domain.Contracts.Persistence;
using E_Commerce.Domain.Contracts.Specifications.Orders;
using E_Commerce.Domain.Entities.Orders;
using E_Commerce.Domain.Entities.Products;

namespace E_Commerce.Application.Services.Order
{
    public class OrderService : IOrderService
    {
        private readonly IBasketService _basketService;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IPaymentService _paymentService;

        public OrderService(IBasketService basketService,IUnitOfWork unitOfWork,IMapper mapper,IPaymentService paymentService  )
        {
            _basketService = basketService;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _paymentService = paymentService;
        }
        public async Task<OrderToReturnDto> CreateOrderAsync(string buyerEmail, OrderToCreateDto order)
        {
            //1. Get Basket From Baskets Repo
            var basket =await _basketService.GetCustomerBasketAsync(order.BasketId);

            //2. Get Selected Items at Basket From Products Repo

            var orderItems = new List<OrderItem>();
            if (basket.Items.Count > 0) {
                var productRepo = _unitOfWork.GetRepository<Product, int>();

                foreach (var item in basket.Items)
                {
                   var product =await productRepo.GetAsync(item.Id);
                    if (product is not null) {
                        var productItemOrdered = new ProductItemOrdered() { 
                            ProductId = product.Id,
                            ProductName = product.Name,
                            PictureUrl= product.PictureUrl ?? ""
                        };
                        var orderItem = new OrderItem()
                        {
                            Product = productItemOrdered,
                            Price = product.Price,
                            Quantity = item.Quantity,
                            
                        };
                        orderItems.Add(orderItem);
                    }
                }
            }
            //3.Calculate SubTotal 
            var subTotal = orderItems.Sum(item => item.Price * item.Quantity);

            //Get DeliveryMethod 
            var method = await _unitOfWork.GetRepository<DeliveryMethod, int>().GetAsync(order.DeliveryMethodId);

            //4. Create Order with Order Items with checking for the payment intent id first
            var orderRepo = _unitOfWork.GetRepository<Domain.Entities.Orders.Order, int>();
            var orderSpec = new OrderByPaymentIntentSpecifications(basket.PaymentIntentId!);
            var existingOrder = await orderRepo.GetAsyncWithSpec(orderSpec);

            if (existingOrder is not null) {
                orderRepo.Delete(existingOrder);
                await _paymentService.CreateOrUpdatePaymentIntent(basket.Id);

            }
            var ordertToCreate = new Domain.Entities.Orders.Order() {
                  BuyerEmail = buyerEmail,
                  ShippingAddress = _mapper.Map<Address>(order.AddressDto),
                  DeliveryMethodId = order.DeliveryMethodId,
                  DeliveryMethod = method,
                  Items = orderItems,
                  SubTotal = subTotal,
                  PaymentIntentId =  basket.PaymentIntentId!
            };
            await _unitOfWork.GetRepository<Domain.Entities.Orders.Order, int>().AddAsync(ordertToCreate);

            //5. Save to database
            var created = await _unitOfWork.CompleteAsync() > 0;
 

            if (!created) throw new BadRequestException("An error has occurred during creating the order");

            return _mapper.Map<OrderToReturnDto>(ordertToCreate);
        }
        public async Task<IEnumerable<OrderToReturnDto>> GetOrdersForUserAsync(string buyerEmail)
        {
            var orderSpec = new OrderSpecification(buyerEmail);
            var orders= await _unitOfWork.GetRepository<Domain.Entities.Orders.Order, int>().GetAllAsyncWithSpec(orderSpec);

            return _mapper.Map<IEnumerable<OrderToReturnDto>>(orders);

        }
        public async Task<OrderToReturnDto> GetOrderByIdAsync(string buyerEmail, int orderId)
        {
            var orderSpec = new OrderSpecification(buyerEmail,orderId);
            var order = await _unitOfWork.GetRepository<Domain.Entities.Orders.Order, int>().GetAsyncWithSpec(orderSpec);

            if (order is null) throw new NotFoundException(nameof(Order),orderId);

            return _mapper.Map<OrderToReturnDto>(order);
        }
        public async Task<IEnumerable<DeliveryMethodDto>> GetAllDeliveryMethodsAsync()
        {
            var methods = await _unitOfWork.GetRepository<DeliveryMethod, int>().GetAllAsync();

            return    _mapper.Map< IEnumerable<DeliveryMethodDto>>(methods);
        }

  

    
    }
}
