using LogTagSensor.Application.Abstraction.Data;
using LogTagSensor.Application.DTOs;
using LogTagSensor.Domain.Entities;
using Microsoft.AspNetCore.Mvc;

namespace LogTagSensor.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
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
    [ProducesResponseType(typeof(int), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status502BadGateway)]
    public async Task<ActionResult<int>> AddDevices([FromBody] DeviceDto reading)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        //Just tried to create new ObjectResult. 
        if(reading == null)
        {
            return new ObjectResult(new ProblemDetails
            {
                Title = "Invalid Device Data",
                Detail = "The device data provided is null.",
                Status = StatusCodes.Status400BadRequest
            })
            { StatusCode = StatusCodes.Status400BadRequest };
        }       
        

        int id = await _deviceService.ProcessDeviceDetails(reading);

        return id;

    }

    

}
