using Microsoft.Extensions.DependencyInjection;
using NorthwindTraders.Application.Interfaces;
using NorthwindTraders.Application.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NorthwindTraders.Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            services.AddScoped<INorthwindService, NorthwindService>();
            return services;
        }

    }
}
