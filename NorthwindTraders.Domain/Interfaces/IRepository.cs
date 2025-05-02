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
        //Post
        Task<int> AddOrder(Order order);
        Task<int> AddOrderDetail(OrderDetail orderDetail);

        //Get
        Task<IEnumerable<Order>> GetOrderByDate(DateTime date);
        Task<IEnumerable<Order>> GetOrderByCustomer(string customerID);
        Task<IEnumerable<Order>> GetOrderByEmployee(int employeeID);
        Task<Order> GetOrderById(int id);
        Task<IEnumerable<OrderDetail>> GetOrderDetailByOrder(int OrderDetails);

        Task<Employee> GetEmployeeById(int id);
        Task<Customer> GetCustomerById(string id);
        Task<Product> GetProductById(int id); 

        //Update
        Task<int> UpdateOrder(Order order);
        Task<int> UpdateOrderDetail(OrderDetail orderDetail);

        //Delete
        Task<int> DeleteOrder(int orderID);
        Task<int> DeleteOrderDetail(int orderID, int productID);
    }
}
