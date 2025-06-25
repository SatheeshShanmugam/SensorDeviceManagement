

namespace LogTagSensor.Infrastructure.Data.DataContext
{
    // DbContext
    public class DeviceMonitoringContext : DbContext
    {
        public DeviceMonitoringContext(DbContextOptions<DeviceMonitoringContext> options) : base(options) { }

        public DbSet<Device> Devices { get; set; }
        public DbSet<SensorReading> SensorReadings { get; set; }
        public DbSet<Alarm> Alarms { get; set; }
        public DbSet<DeviceThreshold> DeviceThresholds { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {            
        }
    }
}
