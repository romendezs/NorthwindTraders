using Microsoft.Extensions.DependencyInjection;
using NorthwindTraders.Application.DTOs;
using NorthwindTraders.Application.Interfaces;
using NorthwindTraders.Application.Services;
using NorthwindTraders.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NorthwindTraders.Application.Mapping
{
    public static class NorthwindMapping
    {
        public static OrderDto ToOrderDto(Order order)
        {
            if (order == null) return null!;

            return new OrderDto
            {
                OrderID = order.OrderID,
                CustomerID = order.CustomerID ?? string.Empty,
                EmployeeID = order.EmployeeID ?? 0,
                OrderDate = Convert.ToDateTime(order.OrderDate),
                ShipAddress = order.ShipAddress ?? string.Empty,
                ShipCity = order.ShipCity ?? string.Empty,
                ShipRegion = order.ShipRegion ?? string.Empty,
                ShipPostalCode = order.ShipPostalCode ?? string.Empty,
                ShipCountry = order.ShipCountry ?? string.Empty
            };
        }

        public static Order ToOrder(OrderDto orderDto)
        {
            if (orderDto == null) return null!;
            return new Order
            {
                OrderID = orderDto.OrderID,
                CustomerID = orderDto.CustomerID ?? string.Empty,
                EmployeeID = orderDto.EmployeeID,
                OrderDate = orderDto.OrderDate.ToUniversalTime().Date,
                ShipAddress = orderDto.ShipAddress ?? string.Empty,
                ShipCity = orderDto.ShipCity ?? string.Empty,
                ShipRegion = orderDto.ShipRegion ?? string.Empty,
                ShipPostalCode = orderDto.ShipPostalCode ?? string.Empty,
                ShipCountry = orderDto.ShipCountry ?? string.Empty

            };
        }

        public static LineDto ToLineDto(OrderDetail detail, INorthwindService service)
        {
            if (detail == null) return null!;

            // Apply discount to get the final unit price
            var productTask = service.GetProductById(detail.ProductId);
            var product = productTask.Result; // Ensure the task is awaited or resolved
            var discountedPrice = product.Price * (1 - (decimal)detail.Discount);

            return new LineDto
            {
                OrderId = detail.OrderId,
                ProductId = detail.ProductId,
                Quantity = detail.Quantity,
                Amount = decimal.Round(discountedPrice * detail.Quantity, 2)
            };
        }

        public static OrderDetail ToOrderDetail(LineDto dto)
        {
            if (dto == null) return null!;

            return new OrderDetail
            {
                OrderId = dto.OrderId,
                ProductId = dto.ProductId,
                Quantity = (short)dto.Quantity,
                UnitPrice = dto.Amount/dto.Quantity,
                Discount = 0 // Default discount
            };
        }

        public static EmployeeDto ToEmployeeDto(Employee employee)
        {
            if (employee == null) return null!;
            return new EmployeeDto
            {
                EmployeeID = employee.EmployeeID,
                FirstName = employee.FirstName ?? string.Empty,
                LastName = employee.LastName ?? string.Empty
            };
        }

        public static CustomerDto ToCustomerDto(Customer customer)
        {
            if(customer == null) return null!;
            return new CustomerDto
            {
                CustomerID = customer.CustomerID,
                CompanyName = customer.CompanyName ?? string.Empty,
            };
        }

        public static ProductDto ToProductDto(Product product)
        {
            if (product == null) return null!;
            return new ProductDto
            {
                ProductID = product.ProductID,
                ProductName = product.ProductName ?? string.Empty,
                Price = product.UnitPrice
            };
        }
    }
}
