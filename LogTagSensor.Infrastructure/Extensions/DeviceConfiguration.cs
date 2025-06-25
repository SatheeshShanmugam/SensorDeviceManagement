using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogTagSensor.Infrastructure.Extensions
{
    public class DeviceConfiguration : IEntityTypeConfiguration<Device>
    {
        public void Configure(EntityTypeBuilder<Device> entity)
        {           
            entity.HasOne(d => d.Thresholds)
                .WithOne(t => t.Device)
                .HasForeignKey<DeviceThreshold>(t => t.DeviceId);

            entity.HasMany(d => d.SensorReadings)
                .WithOne(sr => sr.Device)
                .HasForeignKey(sr => sr.DeviceId);

            entity.HasMany(d => d.Alarms)
                .WithOne(a => a.Device)
                .HasForeignKey(a => a.DeviceId);

            entity.ToTable("Devices");
        }
    }
    
}
