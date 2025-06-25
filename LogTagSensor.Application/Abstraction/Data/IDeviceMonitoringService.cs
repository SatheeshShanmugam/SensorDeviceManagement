namespace LogTagSensor.Application.Abstraction.Data
{
    public interface IDeviceMonitoringService
    {
        Task ProcessSensorReadingAsync(string deviceSerialNumber, SensorType sensorType, double readingValue);
        Task<IEnumerable<Alarm>> GetDeviceAlarmsAsync(string deviceSerialNumber);
        Task<DeviceThreshold> GetDeviceThresholdsAsync(string deviceSerialNumber);
    }
}
