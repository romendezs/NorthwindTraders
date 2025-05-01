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
               || !int.IsPositive(order.EmployeeID)
               || !int.IsPositive(order.OrderID))
            {
                throw new Exception("Order info is not valid, missing or empty fields");
            }

            var orderDb = NorthwindMapping.ToOrder(order);
            await repository.AddOrder(orderDb).ConfigureAwait(false);

            return 1;
        }
        public async Task<int> AddOrderDetail(LineDto orderDetail)
        {
            var existingLine = await repository.GetOrderDetailByOrder(orderDetail.OrderId).ConfigureAwait(false);
            if (existingLine is not null)
            {
                throw new Exception("Line already exists");
            }

            if (string.IsNullOrWhiteSpace(orderDetail.ProductName)
                || !int.IsPositive(orderDetail.ProductId)
                || orderDetail.Quantity <= 0
                || orderDetail.Price <= 0
                )
            {
                throw new Exception("Line info is not valid, missing or empty fields");
            }

            var orderDetailDb = NorthwindMapping.ToOrderDetail(orderDetail);
            await repository.AddOrderDetail(orderDetailDb).ConfigureAwait(false);
            return 1;
        }

        public async Task<IEnumerable<OrderDto>> GetOrderByDate(DateTime date)
        {
            var ordersDb = await repository.GetOrderByDate(date).ConfigureAwait(false);
            if (ordersDb is null)
            {
                throw new Exception("The order does not exist");
            }
            Console.WriteLine(JsonSerializer.Serialize(ordersDb));
            //var ordersDto = ordersDb.Select(o => mapper.Map<OrderDto>(o)).ToList();
            var ordersDto = ordersDb.Select(o=>NorthwindMapping.ToOrderDto(o)).ToList();
            return ordersDto;

        }
        public async Task<IEnumerable<OrderDto>> GetOrderByCustomer(string customerID) {
            var ordersDb = await repository.GetOrderByCustomer(customerID).ConfigureAwait(false);
            if (ordersDb is null)
            {
                throw new Exception("The order does not exist");
            }

            var ordersDto = ordersDb.Select(o => NorthwindMapping.ToOrderDto(o)).ToList();
            return ordersDto;
        }
        public async Task<IEnumerable<OrderDto>> GetOrderByEmployee(int employeeID) {
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

        public async Task<IEnumerable<LineDto>> GetOrderDetailByOrder(int orderID) {
            var ordersDetailsDb = await repository.GetOrderDetailByOrder(orderID).ConfigureAwait(false);
            if (ordersDetailsDb is null)
            {
                throw new Exception("The order does not exist");
            }

            var ordersDetailsDto = ordersDetailsDb.Select(o => NorthwindMapping.ToLineDto(o)).ToList();
            return ordersDetailsDto;
        }
    }
}
