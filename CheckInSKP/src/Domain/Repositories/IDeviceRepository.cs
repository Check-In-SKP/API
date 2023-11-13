using CheckInSKP.Domain.Entities;
using System.Runtime.CompilerServices;

namespace CheckInSKP.Domain.Repositories
{
    public interface IDeviceRepository : IGenericRepository<Device, Guid>
    {
        //new Task<Device?> AddAsync(Device device);
    }
}
