using NorthwindTraders.Application.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NorthwindTraders.Application.Interfaces
{
    public interface INorthwindService
    {
        Task<int> AddOrder(OrderDto order);
        Task<int> AddOrderDetail(LineDto orderDetail);

        Task<IEnumerable<OrderDto>> GetOrderByDate(DateTime date);
        Task<IEnumerable<OrderDto>> GetOrderByCustomer(string customerID);
        Task<IEnumerable<OrderDto>> GetOrderByEmployee(int employeeID);
        Task<OrderDto> GetOrderByOrder(int orderID);
        Task<IEnumerable<LineDto>> GetOrderDetailByOrder(int orderID);
    }
}
