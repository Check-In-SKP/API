using CheckInSKP.Domain.Entities.StaffAggregate;

namespace CheckInSKP.Domain.Repositories
{
    public interface IStaffRepository : IGenericRepository<Staff, int>
    {
        Task UpdateTimeLogAsync(int userId, TimeLog updatedTimeLog);
        Task RemoveTimeLogAsync(int userId, int tokenId);
        Task AddTimeLogAsync(int userId, TimeLog timeLog);
        Task<Staff?> GetStaffWithPagedTimeLogs(int userId, int page, int pageSize);
        Task<Staff?> GetByCardNumberAsync(string cardNumber);
        Task<IEnumerable<Staff?>> GetAvailableStaffsWithTodayTimeLogs();
    }
}
