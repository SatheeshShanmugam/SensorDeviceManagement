using LogTagSensor.Application.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogTagSensor.Application.Abstraction.Data
{
    public interface IDeviceService
    {
        /// <summary>
        /// Adds device details to the system based on the provided serial number.
        /// </summary>
        /// <param name="serialNumber"></param>
        /// <returns></returns>
        Task<int> ProcessDeviceDetails(DeviceDto device);
    }
}
