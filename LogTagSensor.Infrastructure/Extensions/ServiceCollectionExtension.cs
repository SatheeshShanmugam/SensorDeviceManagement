using LogTagSensor.Domain.IRepositories;
using LogTagSensor.Infrastructure.Data.DataContext;
using LogTagSensor.Infrastructure.Data.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogTagSensor.Infrastructure.Extensions
{
    public static class ServiceCollectionExtension
    {
        public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration config)
        {
            services.AddDbContext<DeviceMonitoringContext>(options =>
            {
                // Configure your database context options here, e.g. using SQL Server
                options.UseSqlServer(config.GetConnectionString("DefaultConnection"));
            });

            // Register your infrastructure services here
            services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<IDeviceRepository,DeviceRepository>();
            services.AddScoped<ISensorReadingRepository, SensorReadingRepository>();
            services.AddScoped<IAlarmRepository, AlarmRepository>();

            return services;
        }
    }
}
