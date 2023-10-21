using Domain.Interfaces.Repositories;
using Infrastructure.Data;

namespace Infrastructure.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext _context;
        public UnitOfWork(ApplicationDbContext context,
        IDeviceRepository deviceRepository,
        IRoleRepository roleRepository,
        IStaffRepository staffRepository,
        ITimeTypeRepository timeTypeRepository,
        IUserRepository userRepository)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
            DeviceRepository = deviceRepository ?? throw new ArgumentNullException(nameof(deviceRepository));
            RoleRepository = roleRepository ?? throw new ArgumentNullException(nameof(roleRepository));
            StaffRepository = staffRepository ?? throw new ArgumentNullException(nameof(staffRepository));
            TimeTypeRepository = timeTypeRepository ?? throw new ArgumentNullException(nameof(timeTypeRepository));
            UserRepository = userRepository ?? throw new ArgumentNullException(nameof(userRepository));
        }

        public IDeviceRepository DeviceRepository { get; }
        public IRoleRepository RoleRepository { get; }
        public IStaffRepository StaffRepository { get; }
        public ITimeTypeRepository TimeTypeRepository { get; }
        public IUserRepository UserRepository { get; }

        public async Task CompleteAsync()
        {
            await _context.SaveChangesAsync();
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
