using E_Commerce.Domain.Entities.Orders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.Domain.Contracts.Specifications.Orders
{
    public class OrderByPaymentIntentSpecifications : BaseSpecifications<Order, int>
    {
        public OrderByPaymentIntentSpecifications(string paymentIntentId) :
            base(order=>order.PaymentIntentId == paymentIntentId)
        {
        }
    }
}
