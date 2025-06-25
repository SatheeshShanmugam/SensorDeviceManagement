using System.ComponentModel.DataAnnotations;

namespace LogTagSensor.Application.DTOs
{
    public class DeviceDto
    {
        [Required]
        public string SerialNumber { get; set; }
        [Required]
        public DateTime ActivationDate { get; set; }
    }
}
