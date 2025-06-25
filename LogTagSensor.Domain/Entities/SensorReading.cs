namespace LogTagSensor.Domain.Entities
{
    public class SensorReading
    {
        public int Id { get; set; }
        public int DeviceId { get; set; }
        public Device Device { get; set; }
        public SensorType SensorType { get; set; }
        public double Value { get; set; }
        public DateTime ReadingTime { get; set; }
    }
}
