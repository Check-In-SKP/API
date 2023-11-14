using CheckInSKP.Domain.Entities.StaffAggregate;

namespace CheckInSKP.Domain.Repositories
{
    public interface IStaffRepository : IGenericRepository<Staff, Guid>
    {
        Task UpdateTimeLogAsync(Guid userId, TimeLog updatedTimeLog);
        Task RemoveTimeLogAsync(Guid userId, int timeLogId);
        Task AddTimeLogAsync(Guid userId, TimeLog timeLog);
        Task<Staff?> GetStaffWithPagedTimeLogs(Guid userId, int page, int pageSize);
        Task<Staff?> GetByCardNumberAsync(string cardNumber);
        Task<IEnumerable<Staff?>> GetAvailableStaffsWithTodayTimeLogs();
    }
}
