using NorthwindTraders.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NorthwindTraders.Domain.Interfaces
{
    public interface IRepository
    {
        Task<int> AddOrder(Order order);
        Task<int> AddOrderDetail(OrderDetail orderDetail);

        Task<IEnumerable<Order>> GetOrderByDate(DateTime date);
        Task<IEnumerable<Order>> GetOrderByCustomer(string customerID);
        Task<IEnumerable<Order>> GetOrderByEmployee(int employeeID);
        Task<Order> GetOrderById(int id);

        Task<IEnumerable<OrderDetail>> GetOrderDetailByOrder(int OrderDetails);
    }
}
