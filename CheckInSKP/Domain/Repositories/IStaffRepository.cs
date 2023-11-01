using CheckInSKP.Domain.Entities.StaffAggregate;
using CheckInSKP.Domain.Entities.UserAggregate;

namespace CheckInSKP.Domain.Repositories
{
    public interface IStaffRepository : IGenericRepository<Staff, int>
    {
        Task UpdateTimeLogAsync(int staffId, TimeLog updatedTimeLog);
        Task RemoveTimeLogAsync(int staffId, int tokenId);
        Task AddTimeLogAsync(int staffId, TimeLog timeLog);
        Task<Staff?> GetStaffWithPagedTimeLogs(int staffId, int page, int pageSize);
    }
}
