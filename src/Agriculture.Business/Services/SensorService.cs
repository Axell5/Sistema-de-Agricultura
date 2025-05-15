using System.Collections.Generic;
using System.Threading.Tasks;
using Agriculture.Data.Models;
using Agriculture.Data.Repositories;

namespace Agriculture.Business.Services
{
    public class SensorService : ISensorService
    {
        private readonly ISensorRepository _sensorRepository;

        public SensorService(ISensorRepository sensorRepository)
        {
            _sensorRepository = sensorRepository;
        }

        public async Task<IEnumerable<Sensor>> GetAllSensorsAsync()
        {
            return await _sensorRepository.GetAllSensorsAsync();
        }

        public async Task<Sensor> GetSensorByIdAsync(int id)
        {
            return await _sensorRepository.GetSensorByIdAsync(id);
        }

        public async Task<Sensor> AddSensorAsync(Sensor sensor)
        {
            return await _sensorRepository.AddSensorAsync(sensor);
        }

        public async Task UpdateSensorAsync(Sensor sensor)
        {
            await _sensorRepository.UpdateSensorAsync(sensor);
        }

        public async Task DeleteSensorAsync(int id)
        {
            await _sensorRepository.DeleteSensorAsync(id);
        }
    }
}