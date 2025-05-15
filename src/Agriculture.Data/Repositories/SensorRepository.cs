using System.Collections.Generic;
using System.Threading.Tasks;
using Agriculture.Data.Context;
using Agriculture.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace Agriculture.Data.Repositories
{
    public class SensorRepository : ISensorRepository
    {
        private readonly AgricultureContext _context;

        public SensorRepository(AgricultureContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Sensor>> GetAllSensorsAsync()
        {
            return await _context.Sensors.ToListAsync();
        }

        public async Task<Sensor> GetSensorByIdAsync(int id)
        {
            return await _context.Sensors.FindAsync(id);
        }

        public async Task<Sensor> AddSensorAsync(Sensor sensor)
        {
            _context.Sensors.Add(sensor);
            await _context.SaveChangesAsync();
            return sensor;
        }

        public async Task UpdateSensorAsync(Sensor sensor)
        {
            _context.Entry(sensor).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }

        public async Task DeleteSensorAsync(int id)
        {
            var sensor = await _context.Sensors.FindAsync(id);
            if (sensor != null)
            {
                _context.Sensors.Remove(sensor);
                await _context.SaveChangesAsync();
            }
        }
    }
}