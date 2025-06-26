namespace LogTagSensor.Application.Services
{
    public class DeviceMonitoringService : IDeviceMonitoringService
    {
        private readonly IUnitOfWork _unitOfWork;


        public DeviceMonitoringService(
            IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;

        }

        public async Task ProcessSensorReadingAsync(string deviceSerialNumber, SensorType sensorType, double readingValue)
        {
            var device = await _unitOfWork.GetRepository<IDeviceRepository>().GetDeviceBySerialNumberAsync(deviceSerialNumber);

            if (device == null)
            {

                throw new ArgumentException("Device not found");
            }

            if (!device.IsActive)
            {

                throw new InvalidOperationException("Device is not active");
            }

            // First, save the reading
            var reading = new SensorReading
            {
                DeviceId = device.Id,
                SensorType = sensorType,
                Value = readingValue,
                ReadingTime = DateTime.UtcNow
            };

            await _unitOfWork.GetRepository<ISensorReadingRepository>().AddReadingAsync(reading);

            // Check if alarm has already been triggered for this sensor
            bool hasAlarm = await _unitOfWork.GetRepository<IAlarmRepository>().DeviceHasActiveAlarmOfTypeAsync(device.Id, sensorType);
            if (hasAlarm)
            {
                return;
            }

            // Check thresholds
            bool shouldTriggerAlarm = false;
            double threshold = 0;

            switch (sensorType)
            {
                case SensorType.Humidity:
                    threshold = device.Thresholds.HumidityThreshold;
                    shouldTriggerAlarm = readingValue > threshold;
                    break;
                case SensorType.Temperature:
                    threshold = device.Thresholds.TemperatureThreshold;
                    shouldTriggerAlarm = readingValue > threshold;
                    break;
                case SensorType.Shock:
                    threshold = device.Thresholds.ShockThreshold;
                    shouldTriggerAlarm = readingValue > threshold;
                    break;
            }

            if (shouldTriggerAlarm)
            {
                var alarm = new Alarm
                {
                    DeviceId = device.Id,
                    SensorType = sensorType,
                    TriggerValue = readingValue,
                    TriggerTime = DateTime.UtcNow,
                    IsActive = true
                };

                await _unitOfWork.GetRepository<IAlarmRepository>().AddAlarmAsync(alarm);
                await _unitOfWork.CompleteAsync();


            }
        }

        public async Task<IEnumerable<Alarm>> GetDeviceAlarmsAsync(string deviceSerialNumber)
        {
            var device = await _unitOfWork.GetRepository<IDeviceRepository>().GetDeviceBySerialNumberAsync(deviceSerialNumber);
            if (device == null)
            {
                throw new ArgumentException("Device not found");
            }

            return await _unitOfWork.GetRepository<IAlarmRepository>().GetDeviceAlarmsAsync(device.Id);
        }

        public async Task<DeviceThreshold> GetDeviceThresholdsAsync(string deviceSerialNumber)
        {
            var device = await _unitOfWork.GetRepository<IDeviceRepository>().GetDeviceBySerialNumberAsync(deviceSerialNumber);
            if (device == null)
            {
                throw new ArgumentException("Device not found");
            }

            return device.Thresholds;
        }
    }
}
