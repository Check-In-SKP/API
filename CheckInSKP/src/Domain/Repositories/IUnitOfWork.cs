namespace CheckInSKP.Domain.Repositories
{
    public interface IUnitOfWork : IDisposable
    {
        IDeviceRepository DeviceRepository { get; }
        IRoleRepository RoleRepository { get; }
        IStaffRepository StaffRepository { get; }
        ITimeTypeRepository TimeTypeRepository { get; }
        IUserRepository UserRepository { get; }
        Task<int> CompleteAsync(CancellationToken cancellationToken);
    }
}
