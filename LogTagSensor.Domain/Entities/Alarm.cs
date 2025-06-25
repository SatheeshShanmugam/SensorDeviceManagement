namespace LogTagSensor.Domain.Entities
{
    public class Alarm
    {
        public int Id { get; set; }
        public int DeviceId { get; set; }
        public Device Device { get; set; }
        public SensorType SensorType { get; set; }
        public double TriggerValue { get; set; }
        public DateTime TriggerTime { get; set; }
        public bool IsActive { get; set; } = true;
    }
}
