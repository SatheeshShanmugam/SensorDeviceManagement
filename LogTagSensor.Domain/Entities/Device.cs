namespace LogTagSensor.Domain.Entities
{
    public class Device
    {
        public int Id { get; set; }
        public string SerialNumber { get; set; }
        public DateTime ActivationDate { get; set; }
        public bool IsActive { get; set; }
        public ICollection<SensorReading> SensorReadings { get; set; } = new List<SensorReading>();
        public ICollection<Alarm> Alarms { get; set; } = new List<Alarm>();
        public DeviceThreshold Thresholds { get; set; } = new DeviceThreshold();
    }
}
