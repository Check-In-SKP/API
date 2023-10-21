using Domain.Entities;

namespace Domain.Interfaces.Repositories
{
    public interface IDeviceRepository : IGenericRepository<Device, Guid>
    {
    }
}
