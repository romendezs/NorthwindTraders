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
        //Post
        Task<int> AddOrder(OrderDto order);
        Task<int> AddOrderDetail(LineDto orderDetail);

        //Get
        
        Task<IEnumerable<OrderDto>> GetOrderByDate(DateTime date);
        Task<IEnumerable<OrderDto>> GetOrderByCustomer(string customerID);
        Task<IEnumerable<OrderDto>> GetOrderByEmployee(int employeeID);
        Task<OrderDto> GetOrderByOrder(int orderID);
        Task<IEnumerable<LineDto>> GetOrderDetailByOrder(int orderID);
        Task<EmployeeDto> GetEmployeeById(int id);
        Task<CustomerDto> GetCustomerById(string id);
        Task<ProductDto> GetProductById(int id);

        //Update
        Task<int> UpdateOrder(OrderDto order);
        Task<int> UpdateOrderDetail(LineDto orderDetail, INorthwindService service);
        
        //Delete
        Task<int> DeleteOrder(int orderID);
        Task<int> DeleteOrderDetail(int orderID, int productID);
    }
}
