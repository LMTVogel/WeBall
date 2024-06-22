using OrderManagement.Domain;
using System;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace OrderManagement.DomainServices
{
    public class OrderService(IOrderRepository orderRepository) : IOrderService
    {
        public async Task<Order> GetOrderById(Guid orderId)
        {
            return await orderRepository.GetOrderById(orderId);
        }

        public async Task<IQueryable<Order>> GetAllOrders()
        {
            return await orderRepository.GetAllOrders();
        }

        public async Task CreateOrder(Order order)
        {
            order.PriceTotal = order.Products.Sum(p => p.UnitPrice * p.Quantity);
            order.EstimatedDeliveryDate = CalculateEstimatedDeliveryDate(order.OrderDate);
            
            await orderRepository.CreateOrder(order);
        }

        public async Task UpdateOrder(Guid id, Order order)
        {
            var existingOrder = await GetOrderById(id);
            
            if (existingOrder == null)
            {
                throw new HttpRequestException("Order not found", null, HttpStatusCode.NotFound);
            }
            
            var updatedOrder = new Order()
            {
                Id = id,
                CustomerName = order.CustomerName,
                CustomerEmail = order.CustomerEmail,
                OrderDate = order.OrderDate,
                Products = order.Products,
                PriceTotal = order.Products.Sum(p => p.UnitPrice * p.Quantity),
                Status = order.Status,
                PaymentStatus = order.PaymentStatus,
                ShippingCompany = order.ShippingCompany,
                ShippingAddress = order.ShippingAddress,
                EstimatedDeliveryDate = CalculateEstimatedDeliveryDate(order.OrderDate)
            };
            
            await orderRepository.UpdateOrder(updatedOrder);
        }

        public async Task<IQueryable<Order>> GetOrderHistory(Guid orderId)
        {
            return await orderRepository.GetOrderHistory(orderId);
        }
        
        private DateTime CalculateEstimatedDeliveryDate(DateTime orderDate)
        {
            return orderDate.AddDays(3);
        }
    }
}
