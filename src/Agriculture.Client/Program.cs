using System;
using System.Threading.Tasks;
using Agriculture.Grpc;
using Grpc.Net.Client;

namespace Agriculture.Client
{
    class Program
    {
        static async Task Main(string[] args)
        {
            using var channel = GrpcChannel.ForAddress("https://localhost:5001");
            var client = new SensorService.SensorServiceClient(channel);

            // Example: Get all sensors
            var sensors = await client.GetAllSensorsAsync(new Empty());
            foreach (var sensor in sensors.Sensors)
            {
                Console.WriteLine($"Sensor {sensor.Id}: {sensor.Name} - Value: {sensor.Value}");
            }

            // Example: Create a new sensor
            var newSensor = new SensorResponse
            {
                Name = "Temperature Sensor 1",
                Type = "Temperature",
                Value = 25.5,
                LastReading = DateTime.UtcNow.ToString("o"),
                IsActive = true
            };

            var created = await client.CreateSensorAsync(newSensor);
            Console.WriteLine($"Created sensor with ID: {created.Id}");
        }
    }
}