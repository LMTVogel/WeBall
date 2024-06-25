using OrderManagement.Domain;
using System;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using OrderManagement.Domain.Events;

namespace OrderManagement.DomainServices
{
    public class OrderService(IOrderRepository queryRepository, IEventStore eventStore) : IOrderService
    {
        public async Task<Order> GetOrderById(Guid orderId)
        {
            return await queryRepository.GetOrderById(orderId);
        }

        public async Task<IQueryable<Order>> GetAllOrders()
        {
            return await queryRepository.GetAllOrders();
        }

        public async Task CreateOrderAsync(Order order)
        {
            order.PriceTotal = order.Products.Sum(p => p.UnitPrice * p.Quantity);
            order.EstimatedDeliveryDate = CalculateEstimatedDeliveryDate(order.OrderDate);
            
            var @event = new OrderCreated
            {
                Id = Guid.NewGuid(),
                OrderId = Guid.NewGuid(),
                CustomerName = order.CustomerName,
                CustomerEmail = order.CustomerEmail,
                OrderDate = order.OrderDate,
                Products = order.Products,
                PriceTotal = order.PriceTotal,
                OrderStatus = order.OrderStatus,
                PaymentStatus = order.PaymentStatus,
                ShippingCompany = order.ShippingCompany,
                ShippingAddress = order.ShippingAddress,
                EstimatedDeliveryDate = order.EstimatedDeliveryDate,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow,
            };
            
            // Append the event to the event store
            await eventStore.AppendAsync(@event);
        }

        public async Task UpdateOrderAsync(Guid id, Order order)
        {
            order.PriceTotal = order.Products.Sum(p => p.UnitPrice * p.Quantity);
            order.EstimatedDeliveryDate = CalculateEstimatedDeliveryDate(order.OrderDate);

            var @event = new OrderUpdated
            {
                Id = Guid.NewGuid(),
                OrderId = id,
                CustomerName = order.CustomerName,
                CustomerEmail = order.CustomerEmail,
                OrderDate = order.OrderDate,
                Products = order.Products,
                PriceTotal = order.PriceTotal,
                OrderStatus = order.OrderStatus,
                PaymentStatus = order.PaymentStatus,
                ShippingCompany = order.ShippingCompany,
                ShippingAddress = order.ShippingAddress,
                EstimatedDeliveryDate = order.EstimatedDeliveryDate,
                CreatedAt = order.CreatedAt,
                UpdatedAt = DateTime.UtcNow,
            };
            
            await eventStore.AppendAsync(@event);
        }

        public async Task<IQueryable<Order>> GetOrderHistory(Guid orderId)
        {
            return await queryRepository.GetOrderHistory(orderId);
        }
        
        private static DateTime CalculateEstimatedDeliveryDate(DateTime orderDate)
        {
            return orderDate.AddDays(3);
        }
    }
}
