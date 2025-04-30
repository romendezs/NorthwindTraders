using Microsoft.EntityFrameworkCore;
using NorthwindTraders.Domain.Entities;
using NorthwindTraders.Domain.Interfaces;
using NorthwindTraders.Infrastructure.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
            return await context
                .Set<Order>()
                .Include(o => o.Customer)
                .Include(o => o.Employee)
                .AsNoTracking()
                .Where(o => o.Employee.EmployeeID == employeeID)
                .ToListAsync()
                .ConfigureAwait(false);
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
