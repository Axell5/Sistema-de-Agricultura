using System.Collections.Generic;
using System.Threading.Tasks;
using Agriculture.Data.Models;

namespace Agriculture.Data.Repositories
{
    public interface ISensorRepository
    {
        Task<IEnumerable<Sensor>> GetAllSensorsAsync();
        Task<Sensor> GetSensorByIdAsync(int id);
        Task<Sensor> AddSensorAsync(Sensor sensor);
        Task UpdateSensorAsync(Sensor sensor);
        Task DeleteSensorAsync(int id);
    }
}