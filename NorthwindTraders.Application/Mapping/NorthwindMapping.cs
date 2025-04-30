using AutoMapper;
using NorthwindTraders.Application.DTOs;
using NorthwindTraders.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NorthwindTraders.Application.Mapping
{
    public class NorthwindMapping
    {
        public static void ConfigureMappings(IMapperConfigurationExpression cfg)
        {
            // Map Customer to CustomerDto
            cfg.CreateMap<Order, OrderDto>()
                .ForMember(dest => dest.CustomerID, opt => opt.MapFrom(src => src.Customer.CustomerID))
                .ForMember(dest => dest.EmployeeID, opt => opt.MapFrom(src => src.Employee.EmployeeID))
                .ForMember(dest => dest.ShipAddress, opt => opt.MapFrom(src => src.ShipAddress));

            cfg.CreateMap<OrderDto, Order>()
                .ForMember(dest => dest.Customer, opt => opt.Ignore())
                .ForMember(dest => dest.Employee, opt => opt.Ignore());

            // Map OrderDetail to LineDto
            cfg.CreateMap<OrderDetail, LineDto>()
                .ForMember(dest => dest.ProductId, opt => opt.MapFrom(src => src.ProductId))
                .ForMember(dest => dest.Quantity, opt => opt.MapFrom(src => src.Quantity))
                .ForMember(dest => dest.Price, opt => opt.MapFrom(src => src.UnitPrice));

            cfg.CreateMap<LineDto, OrderDetail>()
                .ForMember(dest => dest.ProductId, opt => opt.MapFrom(src => src.ProductId))
                .ForMember(dest => dest.Quantity, opt => opt.MapFrom(src => src.Quantity))
                .ForMember(dest => dest.UnitPrice, opt => opt.MapFrom(src => src.Price));
        }


    }
}
