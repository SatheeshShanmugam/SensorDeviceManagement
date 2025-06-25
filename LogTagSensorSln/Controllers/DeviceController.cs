using LogTagSensor.Application.Abstraction.Data;
using LogTagSensor.Application.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace LogTagSensor.Api.Controllers;

public class DeviceController : ControllerBase
{
    private readonly IDeviceMonitoringService _monitoringService;
    private readonly IDeviceService _deviceService;
    public DeviceController(IDeviceMonitoringService monitoringService, IDeviceService deviceService)
    {
        _monitoringService = monitoringService;
        _deviceService = deviceService;
    }

    [HttpPost("reading")]
    [Produces("application/json")]
    [ProducesResponseType(typeof(void), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(IActionResult), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(IActionResult), StatusCodes.Status502BadGateway)]
    public async Task<IActionResult> PostReading([FromBody] SensorReadingDto reading)
    {
        if (ModelState.IsValid == false)
        {
            return BadRequest(ModelState);
        }
        await _monitoringService.ProcessSensorReadingAsync(
            reading.DeviceSerialNumber,
            reading.SensorType,
            reading.Value);

        return Ok();
    }

    [HttpGet("{serialNumber}/alarms")]
    [Produces("application/json")]
    [ProducesResponseType(typeof(IActionResult), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(IActionResult), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(IActionResult), StatusCodes.Status502BadGateway)]
    public async Task<IActionResult> GetAlarms([FromRoute] string serialNumber)
    {
        var alarms = await _monitoringService.GetDeviceAlarmsAsync(serialNumber);
        return Ok(alarms);

    }

    [HttpPost("insert")]
    [Produces("application/json")]
    [ProducesResponseType(typeof(IActionResult), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(IActionResult), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(IActionResult), StatusCodes.Status502BadGateway)]
    public async Task<IActionResult> AddDevices([FromBody] DeviceDto reading)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }
        int id= await _deviceService.ProcessDeviceDetails(reading);

        return Ok(id);

    }

}
