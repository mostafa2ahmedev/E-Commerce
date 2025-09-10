using E_Commerce.Domain.Entities.Orders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.Domain.Contracts.Specifications.Orders
{
    public class OrderSpecification : BaseSpecifications<Order, int>
    {


        public OrderSpecification(string buyerEmail) : base(order=>order.BuyerEmail ==buyerEmail)
        {
            AddIncludes();
            AddOrderBy(order=>order.OrderDate);
        }
        public OrderSpecification(string buyerEmail,int orderId) 
            : base((order) =>
                   order.BuyerEmail == buyerEmail
                 &&
                   order.Id == orderId
             )
             {
                
                 AddIncludes();
             
             }

        private protected override void AddIncludes()
        {
            Includes.Add(order => order.DeliveryMethod!);
            Includes.Add(order => order.Items);
        }
     
    }
}
