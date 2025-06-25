namespace LogTagSensor.Domain.Entities
{
    public class DeviceThreshold
    {
        public int Id { get; set; }
        public int DeviceId { get; set; }
        public Device Device { get; set; }
        public double HumidityThreshold { get; set; }
        public double TemperatureThreshold { get; set; }
        public double ShockThreshold { get; set; }
    }
}
