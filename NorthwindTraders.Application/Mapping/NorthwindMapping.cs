using NorthwindTraders.Application.DTOs;
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
                OrderDate = order.OrderDate,
                ShipAddress = order.ShipAddress ?? string.Empty,
                Customer = order.Customer != null ? new CustomerDto
                {
                    // Map properties from Customer to CustomerDto here
                } : null!,
                Employee = order.Employee != null ? new EmployeeDto
                {
                    // Map properties from Employee to EmployeeDto here
                } : null!
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
                OrderDate = orderDto.OrderDate,
                ShipAddress = orderDto.ShipAddress ?? string.Empty,
                Customer = orderDto.Customer != null ? new Customer
                {
                    // Map properties from CustomerDto to Customer here
                } : null!,
                Employee = orderDto.Employee != null ? new Employee
                {
                    // Map properties from EmployeeDto to Employee here
                } : null!
            };
        }

        public static LineDto ToLineDto(OrderDetail detail)
        {
            if (detail == null) return null!;

            // Apply discount to get the final unit price
            var discountedPrice = detail.UnitPrice * (1 - (decimal)detail.Discount);

            return new LineDto
            {
                OrderId = detail.OrderId,
                ProductId = detail.ProductId,
                ProductName = detail.Product?.ProductName ?? "Unknown",
                Quantity = detail.Quantity,
                Price = decimal.Round(discountedPrice, 2),
                Amount = decimal.Round(discountedPrice * detail.Quantity, 2)
            };
        }

        public static OrderDetail ToOrderDetail(LineDto dto)
        {
            if (dto == null) return null!;

            // Assuming Price = UnitPrice after discount
            // We reverse the discount assumption here; if discount is unknown, assume 0
            return new OrderDetail
            {
                OrderId = dto.OrderId,
                ProductId = dto.ProductId,
                Quantity = (short)dto.Quantity,
                UnitPrice = dto.Price, // You may need to adjust this if discount should be backed out
                Discount = 0f, // Cannot recover original discount from LineDto
                Product = new Product
                {
                    ProductID = dto.ProductId,
                    ProductName = dto.ProductName
                }
            };
        }
    }
}
