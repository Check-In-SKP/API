using ThwartAPI.Domain.Entities;

namespace ThwartAPI.Domain.Interfaces.Repositories
{
    public interface IDeviceRepository : IGenericRepository<Device, Guid>
    {
    }
}
