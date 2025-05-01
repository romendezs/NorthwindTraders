using Microsoft.EntityFrameworkCore;
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

        public async Task<IEnumerable<Order>> GetOrderByDate(DateTime date)
        {
            return await context
                .Set<Order>()
                .Include(o => o.Customer)
                .Include(o => o.Employee)
                .AsNoTracking()
                .Where(o => o.OrderDate.Date == date)
                .ToListAsync()
                .ConfigureAwait(false);

        }

        public async Task<IEnumerable<Order>> GetOrderByCustomer(string customerID)
        {
            return await context
                .Set<Order>()
                .Include(o => o.Customer)
                .Include(o => o.Employee)
                .AsNoTracking()
                .Where(o => o.Customer.CustomerID == customerID)
                .ToListAsync()
                .ConfigureAwait(false);
        }

        public async Task<IEnumerable<Order>> GetOrderByEmployee(int employeeID)
        {
            try
            {
                var orders = await context
                    .Set<Order>()
                    //.Include(o => o.Customer) // Left join with Customer (could be null)
                    //.Include(o => o.Employee) // Left join with Employee (could be null)
                    .AsNoTracking()
                    .Where(o => o.Employee != null && o.Employee.EmployeeID == employeeID) // Ensure Employee is not null
                    .Select(o => new Order
                    {
                        OrderID = o.OrderID,
                        CustomerID = o.CustomerID,
                        EmployeeID = o.EmployeeID,
                        OrderDate = o.OrderDate,
                        RequiredDate = o.RequiredDate,
                        ShippedDate = o.ShippedDate,
                        Freight = o.Freight, // No null-coalescing needed as Freight is not nullable
                        ShipName = o.ShipName ?? string.Empty, // Safeguard nullable strings
                        ShipAddress = o.ShipAddress ?? string.Empty, // Safeguard nullable strings
                        ShipCity = o.ShipCity ?? string.Empty,
                        ShipRegion = o.ShipRegion ?? string.Empty,
                        ShipPostalCode = o.ShipPostalCode ?? string.Empty,
                        ShipCountry = o.ShipCountry ?? string.Empty,

                        // Safeguard related entities for nulls
                        Customer = o.Customer != null
                            ? new Customer
                            {
                                CustomerID = o.Customer.CustomerID,
                                CompanyName = o.Customer.CompanyName ?? string.Empty
                            }
                            : null,
                        Employee = o.Employee != null
                            ? new Employee
                            {
                                EmployeeID = o.Employee.EmployeeID,
                                FirstName = o.Employee.FirstName ?? string.Empty,
                                LastName = o.Employee.LastName ?? string.Empty
                            }
                            : null
                    })
                    .ToListAsync()
                    .ConfigureAwait(false);

               /* foreach (var order in orders)
                {
                    Console.WriteLine(JsonSerializer.Serialize(order));
                }*/

                return orders;

            }
            catch (Exception ex)
            {
                // Log useful details for debugging
                Console.WriteLine(ex + $"Failed to fetch orders for employeeID: {employeeID}");
                throw;
            }
        }

        public async Task<Order> GetOrderById(int orderId)
        {
            return await context
                .Set<Order>()
                .Include(o => o.Customer)
                .Include(o => o.Employee)
                .AsNoTracking()
                .FirstOrDefaultAsync(o => o.OrderID == orderId)
                .ConfigureAwait(false);
        }


        public async Task<IEnumerable<OrderDetail>> GetOrderDetailByOrder(int orderID)
        {
            return await context
                .Set<OrderDetail>()
                .Include(od => od.Order)
                .Include(od => od.Product)
                .AsNoTracking()
                .Where(od => od.Order.OrderID == orderID)
                .ToListAsync()
                .ConfigureAwait(false);
        }
    }
}
