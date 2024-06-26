using OrderManagement.Domain;
using System;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using MassTransit;
using Events;

namespace OrderManagement.DomainServices
{
    public class OrderService(IOrderRepository queryRepository, IEventStore eventStore, IBusControl serviceBus) : IOrderService
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
            // Publish the event to the service bus
            await serviceBus.Publish(@event);
        }

        public async Task UpdateOrderAsync(Guid id, Order order)
        {
            order.PriceTotal = order.Products.Sum(p => p.UnitPrice * p.Quantity);
            order.EstimatedDeliveryDate = CalculateEstimatedDeliveryDate(order.OrderDate);

            var @event = new OrderUpdated
            {
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
            
            // Append the event to the event store
            await eventStore.AppendAsync(@event);
            // Publish the event to the service bus
            await serviceBus.Publish(@event);
        }

        public async Task<List<Order>> GetOrderHistory(Guid orderId)
        {
            var events = await eventStore.ReadAsync(orderId);
            if (events.Count == 0)
            {
                return null;
            }
            
            var orderHistory = new List<Order>();
            foreach (var orderEvent in events)
            {
                orderHistory.Add(ApplyEvent(orderEvent));
            }
            
            return orderHistory;
        }
        
        private static DateTime CalculateEstimatedDeliveryDate(DateTime orderDate)
        {
            return orderDate.AddDays(3);
        }
        
        private Order ApplyEvent(OrderEvent orderEvent)
        {
            var order = new Order
            {
                Id = orderEvent.Id,
                OrderId = orderEvent.OrderId,
                CustomerName = orderEvent.CustomerName,
                CustomerEmail = orderEvent.CustomerEmail,
                OrderDate = orderEvent.OrderDate,
                Products = orderEvent.Products,
                PriceTotal = orderEvent.PriceTotal,
                OrderStatus = orderEvent.OrderStatus,
                PaymentStatus = orderEvent.PaymentStatus,
                ShippingCompany = orderEvent.ShippingCompany,
                ShippingAddress = orderEvent.ShippingAddress,
                EstimatedDeliveryDate = orderEvent.EstimatedDeliveryDate,
                CreatedAt = orderEvent.CreatedAt,
                UpdatedAt = orderEvent.UpdatedAt
            };
            
            return order;
        }
    }
}
