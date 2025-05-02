using NorthwindTraders.Application.DTOs;
using NorthwindTraders.Application.Interfaces;
using NorthwindTraders.Domain.Entities;
using NorthwindTraders.Domain.Interfaces;
using NorthwindTraders.Application.Mapping;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace NorthwindTraders.Application.Services
{
    public class NorthwindService(IRepository repository) : INorthwindService
    {
        //Post
        public async Task<int> AddOrder(OrderDto order)
        {

            var existingOrder = await repository.GetOrderById(order.OrderID).ConfigureAwait(false);
            if (existingOrder is not null)
            {
                throw new Exception("Order already exists");
            }

            if (string.IsNullOrWhiteSpace(order.CustomerID)
                || order.OrderDate == DateTime.MinValue
               || string.IsNullOrWhiteSpace(order.ShipAddress)
               )
            {
                throw new Exception("Order info is not valid, missing or empty fields");
            }

            // Normalize OrderDate to remove time zone offset and ensure compatibility with SQL Server
            order.OrderDate = order.OrderDate.ToUniversalTime().Date;

            // Ensure OrderDate is within the valid range for SQL Server's datetime type
            if (order.OrderDate < new DateTime(1753, 1, 1) || order.OrderDate > new DateTime(9999, 12, 31))
            {
                throw new Exception("OrderDate is out of range for SQL Server's datetime type.");
            }

            var orderDb = NorthwindMapping.ToOrder(order);
            await repository.AddOrder(orderDb).ConfigureAwait(false);

            return orderDb.OrderID;
        }
        public async Task<int> AddOrderDetail(LineDto orderDetail)
        {
            var existingLine = await repository.GetOrderDetailByOrder(orderDetail.OrderId).ConfigureAwait(false);

            var existingOrder = await repository.GetOrderDetailByOrder(orderDetail.OrderId).ConfigureAwait(false);

            if (existingOrder is null)
            {
                throw new Exception("The Order does not exist");
            }

            if (!int.IsPositive(orderDetail.ProductId)
                || orderDetail.Quantity <= 0
                )
            {
                throw new Exception("Line info is not valid, missing or empty fields");
            }

            if (existingOrder.Any(line => line.ProductId == orderDetail.ProductId))
            {
                throw new Exception("A line for this product already exists, delete it and create a new one, or edit this one");
            }

            var orderDetailDb = NorthwindMapping.ToOrderDetail(orderDetail);
            await repository.AddOrderDetail(orderDetailDb).ConfigureAwait(false);
            return orderDetailDb.OrderId;
        }

        //Get
        public async Task<IEnumerable<OrderDto>> GetOrderByDate(DateTime date)
        {
            var ordersDb = await repository.GetOrderByDate(date).ConfigureAwait(false);
            if (ordersDb is null)
            {
                throw new Exception("The order does not exist");
            }
            Console.WriteLine(JsonSerializer.Serialize(ordersDb));
            var ordersDto = ordersDb.Select(o => NorthwindMapping.ToOrderDto(o)).ToList();
            return ordersDto;

        }

        public async Task<IEnumerable<OrderDto>> GetOrderByCustomer(string customerID)
        {
            var ordersDb = await repository.GetOrderByCustomer(customerID).ConfigureAwait(false);
            if (ordersDb is null)
            {
                throw new Exception("The order does not exist");
            }

            var ordersDto = ordersDb.Select(o => NorthwindMapping.ToOrderDto(o)).ToList();
            return ordersDto;
        }
        public async Task<IEnumerable<OrderDto>> GetOrderByEmployee(int employeeID)
        {
            var ordersDb = await repository.GetOrderByEmployee(employeeID).ConfigureAwait(false);
            if (ordersDb is null)
            {
                throw new Exception("The order does not exist");
            }

            var ordersDto = ordersDb.Select(o => NorthwindMapping.ToOrderDto(o)).ToList();
            return ordersDto;

        }

        public async Task<OrderDto> GetOrderByOrder(int order)
        {
            var orderDb = await repository.GetOrderById(order).ConfigureAwait(false);
            if (orderDb is null)
            {
                throw new Exception("The order does not exist");
            }
            var orderDto = NorthwindMapping.ToOrderDto(orderDb);
            return orderDto;
        }

        public async Task<IEnumerable<LineDto>> GetOrderDetailByOrder(int orderID)
        {
            var ordersDetailsDb = await repository.GetOrderDetailByOrder(orderID).ConfigureAwait(false);
            if (ordersDetailsDb is null)
            {
                throw new Exception("The order does not exist");
            }

            var ordersDetailsDto = ordersDetailsDb.Select(o => NorthwindMapping.ToLineDto(o, this)).ToList();
            return ordersDetailsDto;
        }

        public async Task<EmployeeDto> GetEmployeeById(int id)
        {
            var employeeDb = await repository.GetEmployeeById(id).ConfigureAwait(false);
            if (employeeDb is null)
            {
                throw new Exception("The employee does not exist");
            }

            var employeeDto = NorthwindMapping.ToEmployeeDto(employeeDb);
            return employeeDto;
        }

        public async Task<CustomerDto> GetCustomerById(string id)
        {
            var customerDb = await repository.GetCustomerById(id).ConfigureAwait(false);
            if (customerDb is null)
            {
                throw new Exception("The customer does not exist");
            }

            var customerDto = NorthwindMapping.ToCustomerDto(customerDb);
            return customerDto;
        }

        public async Task<ProductDto> GetProductById(int id)
        {
            var productDb = await repository.GetProductById(id).ConfigureAwait(false);
            if (productDb is null)
            {
                throw new Exception("The product does not exist");
            }
            var productDto = NorthwindMapping.ToProductDto(productDb);
            return productDto;
        }

        //Update
        public async Task<int> UpdateOrder(OrderDto order)
        {
            var existingOrder = await repository.GetOrderById(order.OrderID).ConfigureAwait(false);

            if (existingOrder is null)
            {
                throw new Exception("Order does not exist");
            }

            var existingCustomer = await repository.GetCustomerById(order.CustomerID).ConfigureAwait(false);

            if (existingCustomer == null)
            {
                throw new Exception("Customer does not exist");
            }

            // Update only the fields provided in the request
            if (!string.IsNullOrWhiteSpace(order.CustomerID))
            {
                existingOrder.CustomerID = order.CustomerID;
            }

            if (order.OrderDate != DateTime.MinValue)
            {
                existingOrder.OrderDate = order.OrderDate.ToUniversalTime();
            }

            if (!string.IsNullOrWhiteSpace(order.ShipAddress))
            {
                existingOrder.ShipAddress = order.ShipAddress;
            }

            if (order.EmployeeID.HasValue && order.EmployeeID > 0)
            {
                existingOrder.EmployeeID = order.EmployeeID;
            }

            /*if (order.EmployeeID.HasValue && order.EmployeeID > 0)
            {
                existingOrder.EmployeeID = order.EmployeeID;

                if (existingOrder is null)
            {
                throw new Exception("Order does not exist");
            }
            */
            if (string.IsNullOrWhiteSpace(order.CustomerID)
               || order.OrderDate == DateTime.MinValue
               || string.IsNullOrWhiteSpace(order.ShipAddress)
               || (order.EmployeeID <= 0)
               || !int.IsPositive(order.OrderID))
            {
                throw new Exception("Order info is not valid, missing or empty fields");
            }

            if (existingOrder.Equals(order))
            {
                throw new Exception("There is nothing to update for this Order");
            }
            var orderDb = NorthwindMapping.ToOrder(order);
            await repository.UpdateOrder(orderDb).ConfigureAwait(false);
            return 1;
        }

        public async Task<int> UpdateOrderDetail(LineDto orderDetails, INorthwindService service)
        {
            if (orderDetails == null)
            {
                throw new ArgumentNullException(nameof(orderDetails), "Order details cannot be null.");
            }

            if (orderDetails.Quantity <= 0)
            {
                throw new ArgumentException("Quantity must be greater than zero.", nameof(orderDetails.Quantity));
            }

            // Fetch the existing order details for the specified order
            var existingLines = await repository.GetOrderDetailByOrder(orderDetails.OrderId).ConfigureAwait(false);
            if (existingLines == null || !existingLines.Any())
            {
                throw new InvalidOperationException($"No order details found for OrderId: {orderDetails.OrderId}.");
            }

            Console.WriteLine(existingLines);

            // Locate the line to update
            var lineToUpdate = existingLines.FirstOrDefault(line => line.ProductId == orderDetails.ProductId);
            if (lineToUpdate == null)
            {
                throw new Exception("Line not found");
            }

            Console.WriteLine(lineToUpdate);

            // Update the existing line if it already exists
            var product = await service.GetProductById(lineToUpdate.ProductId).ConfigureAwait(false);
            lineToUpdate.UnitPrice = product.Price; // Example of resetting the discount
            lineToUpdate.Quantity = (short)orderDetails.Quantity;


            await repository.DeleteOrderDetail(orderDetails.OrderId, orderDetails.ProductId).ConfigureAwait(false);
            await repository.AddOrderDetail(lineToUpdate).ConfigureAwait(false);

            return 1;
        }

        //DELETE
        public async Task<int> DeleteOrder(int orderID)
        {
            var existingOrder = await repository.GetOrderById(orderID).ConfigureAwait(false);
            if (existingOrder == null)
            {
                throw new Exception("Line does not exist");
            }

            await repository.DeleteOrder(orderID).ConfigureAwait(false);
            return 1;
        }

        public async Task<int> DeleteOrderDetail(int orderID, int productID)
        {
            var existingLine = await repository.GetOrderDetailByOrder(orderID).ConfigureAwait(false);
            if (existingLine is null)
            {
                throw new Exception("Line does not exist");
            }

            await repository.DeleteOrderDetail(orderID, productID).ConfigureAwait(false);
            return 1;
        }
    }
}
