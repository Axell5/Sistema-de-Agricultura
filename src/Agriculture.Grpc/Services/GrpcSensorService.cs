using System;
using System.Linq;
using System.Threading.Tasks;
using Agriculture.Business.Services;
using Agriculture.Data.Models;
using Grpc.Core;
using Microsoft.Extensions.Logging;

namespace Agriculture.Grpc.Services
{
    public class GrpcSensorService : SensorService.SensorServiceBase
    {
        private readonly ISensorService _sensorService;
        private readonly ILogger<GrpcSensorService> _logger;

        public GrpcSensorService(ISensorService sensorService, ILogger<GrpcSensorService> logger)
        {
            _sensorService = sensorService;
            _logger = logger;
        }

        public override async Task<SensorList> GetAllSensors(Empty request, ServerCallContext context)
        {
            var sensors = await _sensorService.GetAllSensorsAsync();
            var response = new SensorList();
            response.Sensors.AddRange(sensors.Select(s => MapToGrpcSensor(s)));
            return response;
        }

        public override async Task<SensorResponse> GetSensor(SensorRequest request, ServerCallContext context)
        {
            var sensor = await _sensorService.GetSensorByIdAsync(request.Id);
            if (sensor == null)
            {
                throw new RpcException(new Status(StatusCode.NotFound, "Sensor not found"));
            }
            return MapToGrpcSensor(sensor);
        }

        public override async Task<SensorResponse> CreateSensor(SensorResponse request, ServerCallContext context)
        {
            var sensor = MapToDataSensor(request);
            var created = await _sensorService.AddSensorAsync(sensor);
            return MapToGrpcSensor(created);
        }

        public override async Task<Empty> UpdateSensor(SensorResponse request, ServerCallContext context)
        {
            var sensor = MapToDataSensor(request);
            await _sensorService.UpdateSensorAsync(sensor);
            return new Empty();
        }

        public override async Task<Empty> DeleteSensor(SensorRequest request, ServerCallContext context)
        {
            await _sensorService.DeleteSensorAsync(request.Id);
            return new Empty();
        }

        private SensorResponse MapToGrpcSensor(Sensor sensor)
        {
            return new SensorResponse
            {
                Id = sensor.Id,
                Name = sensor.Name,
                Type = sensor.Type,
                Value = sensor.Value,
                LastReading = sensor.LastReading.ToString("o"),
                IsActive = sensor.IsActive
            };
        }

        private Sensor MapToDataSensor(SensorResponse response)
        {
            return new Sensor
            {
                Id = response.Id,
                Name = response.Name,
                Type = response.Type,
                Value = response.Value,
                LastReading = DateTime.Parse(response.LastReading),
                IsActive = response.IsActive
            };
        }
    }
}