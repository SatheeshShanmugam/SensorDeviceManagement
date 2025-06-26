using LogTagSensor.Infrastructure.Data.DataContext;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogTagSensor.Infrastructure.Extensions
{
    public class DeviceMonitoringContextFactory : IDesignTimeDbContextFactory<DeviceMonitoringContext>
    {
        public DeviceMonitoringContext CreateDbContext(string[] args)
        {
            // Step safely up from bin/Debug/netX.X/ to solution root
            var solutionRoot = Directory.GetParent(Directory.GetCurrentDirectory())
                                         .FullName;

            var basePath = Path.Combine(solutionRoot, "LogTagSensorSln");
            Console.WriteLine($"[Factory] BasePath: {basePath}");

            // Step 2: Confirm if appsettings.json exists
            var settingsPath = Path.Combine(basePath, "appsettings.json");
            Console.WriteLine($"[Factory] Config file exists: {File.Exists(settingsPath)}");

            if (!File.Exists(settingsPath))
                throw new FileNotFoundException("appsettings.json not found at: " + settingsPath);

            var configuration = new ConfigurationBuilder()
                .SetBasePath(basePath)
                .AddJsonFile("appsettings.json", optional: false)
                .Build();

            var optionsBuilder = new DbContextOptionsBuilder<DeviceMonitoringContext>();
            var connectionString = configuration.GetConnectionString("DefaultConnection");

            optionsBuilder.UseSqlServer(connectionString);

            return new DeviceMonitoringContext(optionsBuilder.Options);
        }
    }    

}
