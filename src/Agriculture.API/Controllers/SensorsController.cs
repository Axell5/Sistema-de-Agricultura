using System.Collections.Generic;
using System.Threading.Tasks;
using Agriculture.Business.Services;
using Agriculture.Data.Models;
using Microsoft.AspNetCore.Mvc;

namespace Agriculture.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class SensorsController : ControllerBase
    {
        private readonly ISensorService _sensorService;

        public SensorsController(ISensorService sensorService)
        {
            _sensorService = sensorService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Sensor>>> GetSensors()
        {
            var sensors = await _sensorService.GetAllSensorsAsync();
            return Ok(sensors);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Sensor>> GetSensor(int id)
        {
            var sensor = await _sensorService.GetSensorByIdAsync(id);
            if (sensor == null)
            {
                return NotFound();
            }
            return Ok(sensor);
        }

        [HttpPost]
        public async Task<ActionResult<Sensor>> CreateSensor(Sensor sensor)
        {
            var createdSensor = await _sensorService.AddSensorAsync(sensor);
            return CreatedAtAction(nameof(GetSensor), new { id = createdSensor.Id }, createdSensor);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateSensor(int id, Sensor sensor)
        {
            if (id != sensor.Id)
            {
                return BadRequest();
            }
            await _sensorService.UpdateSensorAsync(sensor);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSensor(int id)
        {
            await _sensorService.DeleteSensorAsync(id);
            return NoContent();
        }
    }
}