using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using OrderManagement.Domain;
using OrderManagement.DomainServices;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace OrderManagement.Endpoints
{
    public static class Orders
    {
        public static void RegisterOrderEndpoints(this IEndpointRouteBuilder routes)
        {
            var orders = routes.MapGroup("/api/orders");

            // GET: /api/orders
            orders.MapGet("/", async (IOrderService orderService) =>
            {
                var ordersList = await orderService.GetAllOrders();
                return Results.Ok(ordersList.ToList());
            })
            .WithName("GetAllOrders")
            .WithTags("Orders");

            // GET: /api/orders/{id:guid}
            orders.MapGet("/{id:guid}", async (IOrderService orderService, Guid id) =>
            {
                var order = await orderService.GetOrderById(id);
                return order != null ? Results.Ok(order) : Results.NotFound();
            })
            .WithName("GetOrderById")
            .WithTags("Orders");

            // POST: /api/orders
            orders.MapPost("/", async (IOrderService orderService, Order order) =>
            {
                await orderService.CreateOrder(order);
                return Results.Created($"/api/orders/{order.Id}", order);
            })
            .WithName("CreateOrder")
            .WithTags("Orders");

            // PUT: /api/orders/{id:guid}
            orders.MapPut("/{id:guid}", async (IOrderService orderService, Guid id, Order order) =>
            {
                try
                {
                    await orderService.UpdateOrder(id, order);
                    return Results.Ok(order);
                }
                catch (HttpRequestException ex) when ((int)ex.StatusCode == 404)
                {
                    return Results.NotFound("Order to update not found");
                }
                catch (Exception)
                {
                    return Results.Problem("An error occurred while updating the order");
                }
            })
            .WithName("UpdateOrder")
            .WithTags("Orders");

            // GET: /api/orders/history/{id:guid}
            orders.MapGet("/history/{id:guid}", async (IOrderService orderService, Guid id) =>
            {
                var orderHistory = await orderService.GetOrderHistory(id);
                return orderHistory.Any() ? Results.Ok(orderHistory.ToList()) : Results.NotFound();
            })
            .WithName("GetOrderHistory")
            .WithTags("Orders");
        }
    }
}
