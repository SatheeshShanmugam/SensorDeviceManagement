using LogTagSensor.Application.Services;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogTagSensor.Application.Extensions
{
    public static class ApplicationLayerExtensions
    {
        public static IServiceCollection  AddApplicationLayerDIs(this IServiceCollection services)
        {
            services.AddScoped<IDeviceMonitoringService, DeviceMonitoringService>();
            services.AddScoped<IDeviceService, DeviceService>();
            return services;
        }

    }
}
