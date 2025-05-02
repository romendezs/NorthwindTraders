using Microsoft.EntityFrameworkCore;
using NorthwindTraders.Application.Mapping;
using NorthwindTraders.Domain.Entities;
using NorthwindTraders.Domain.Interfaces;
using NorthwindTraders.Infrastructure.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace NorthwindTraders.Infrastructure.Repositories
{
    public class Repository(AppDbContext context) : IRepository
    {

        //POST
        public async Task<int> AddOrder(Order order)
        {   
            await context.Orders.AddAsync(order).ConfigureAwait(false);
            await context.SaveChangesAsync();
            return order.OrderID;
        }

        public async Task<int> AddOrderDetail(OrderDetail orderDetail)
        {
            await context.OrderDetails.AddAsync(orderDetail).ConfigureAwait(false);
            await context.SaveChangesAsync();
            return orderDetail.ProductId;
        }

        //GET
       public async Task<IEnumerable<Order>> GetOrderByDate(DateTime date)
        {
            return await context
                .Set<Order>()
                .AsNoTracking()
                .Where(o => o.OrderDate == date)
                .ToListAsync()
                .ConfigureAwait(false);

        }
       
        public async Task<IEnumerable<Order>> GetOrderByCustomer(string customerID)
        {
            return await context
                .Set<Order>()
                .AsNoTracking()
                .Where(o => o.CustomerID == customerID)
                .ToListAsync()
                .ConfigureAwait(false);
        }

        public async Task<IEnumerable<Order>> GetOrderByEmployee(int employeeID)
        {
            try
            {
                return await context
                    .Set<Order>()
                    .AsNoTracking()
                    .Where(o => o.EmployeeID == employeeID)
                    .ToListAsync()
                    .ConfigureAwait(false);

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex + $"Failed to fetch orders for employeeID: {employeeID}");
                throw;
            }
        }

        public async Task<Order> GetOrderById(int orderId)
        {
            return await context
                .Set<Order>()
                .AsNoTracking()
                .FirstOrDefaultAsync(o => o.OrderID == orderId)
                .ConfigureAwait(false);
        }


        public async Task<IEnumerable<OrderDetail>> GetOrderDetailByOrder(int orderID)
        {
            return await context
                .Set<OrderDetail>()
                .AsNoTracking()
                .Where(od => od.OrderId == orderID)
                .ToListAsync()
                .ConfigureAwait(false);
        }

        public async Task<Employee> GetEmployeeById(int employeeID)
        {
            return await context
                .Set<Employee>()
                .AsNoTracking()
                .FirstOrDefaultAsync(e => e.EmployeeID == employeeID)
                .ConfigureAwait(false);
        }

        public async Task<Customer> GetCustomerById(string customerID)
        {
            return await context
                .Set<Customer>()
                .AsNoTracking()
                .FirstOrDefaultAsync(c => c.CustomerID == customerID)
                .ConfigureAwait(false);
        }

        public async Task<Product> GetProductById(int productID)
        {
            return await context
                .Set<Product>()
                .AsNoTracking()
                .FirstOrDefaultAsync(p => p.ProductID == productID)
                .ConfigureAwait(false);
        }


        //UPDATE
        public async Task<int> UpdateOrder(Order order)
        {
            context.Orders.Update(order);
            await context.SaveChangesAsync();
            return order.OrderID;
        }

        public async Task<int> UpdateOrderDetail(OrderDetail orderDetail)
        {
            context.OrderDetails.Update(orderDetail);
            await context.SaveChangesAsync();
            return orderDetail.ProductId;
        }

        //DELETE
        public async Task<int> DeleteOrder(int orderID)
        {
            var existingOrder = await context.Orders
            .FirstOrDefaultAsync(o => o.OrderID == orderID)
            .ConfigureAwait(false);

            if (existingOrder != null)
            {
                context.Orders.Remove(existingOrder);
                await context.SaveChangesAsync().ConfigureAwait(false);
            }
            return orderID;
        }

        public async Task<int> DeleteOrderDetail(int orderID, int productID)
        {
            var existingOrderDetail = await context.OrderDetails
            .FirstOrDefaultAsync(od => od.ProductId == productID && od.OrderId == orderID)
            .ConfigureAwait(false);

            if (existingOrderDetail != null)
            {
                context.OrderDetails.Remove(existingOrderDetail);
                await context.SaveChangesAsync().ConfigureAwait(false);
            }
            return productID;
        }
    }
}
