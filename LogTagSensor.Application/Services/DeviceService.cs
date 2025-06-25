using LogTagSensor.Application.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogTagSensor.Application.Services
{
    public class DeviceService : IDeviceService
    {
        private readonly IUnitOfWork _unitOfWork;
        public DeviceService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<int> ProcessDeviceDetails(DeviceDto device)
        {
            try
            {   
                Device dev = new Device
                {
                    SerialNumber = device.SerialNumber,
                    IsActive = true,
                    ActivationDate = device.ActivationDate

                };
                var devicerepo = _unitOfWork.GetRepository<IDeviceRepository>();
                await devicerepo.Add(dev);
                await _unitOfWork.CompleteAsync();
                return dev.Id;
            }
            catch (Exception ex)
            {
                // Log the exception (not implemented here)
                throw new ApplicationException("An error occurred while processing device details.", ex);
            }
        }
    }
}
