using LogTagSensor.Domain.Entities;
using System.ComponentModel.DataAnnotations;

namespace LogTagSensor.Application.DTOs
{
    public class SensorReadingDto
    {
        [Required]
        public string DeviceSerialNumber { get; set; }
        [Required]
        public SensorType SensorType { get; set; }

        [MaxLength(100)]
        public double Value { get; set; }

    }
}
