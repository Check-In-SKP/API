using CheckInSKP.Application.Common.Interfaces;
using CheckInSKP.Domain.Common;
using CheckInSKP.Domain.Repositories;
using CheckInSKP.Infrastructure.Data;

namespace CheckInSKP.Infrastructure.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext _context;
        private readonly IDomainEventDispatcher _domainEventDispatcher;
        public UnitOfWork(ApplicationDbContext context,
        IDeviceRepository deviceRepository,
        IRoleRepository roleRepository,
        IStaffRepository staffRepository,
        ITimeTypeRepository timeTypeRepository,
        IUserRepository userRepository,
        IDomainEventDispatcher domainEventDispatcher)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
            DeviceRepository = deviceRepository ?? throw new ArgumentNullException(nameof(deviceRepository));
            RoleRepository = roleRepository ?? throw new ArgumentNullException(nameof(roleRepository));
            StaffRepository = staffRepository ?? throw new ArgumentNullException(nameof(staffRepository));
            TimeTypeRepository = timeTypeRepository ?? throw new ArgumentNullException(nameof(timeTypeRepository));
            UserRepository = userRepository ?? throw new ArgumentNullException(nameof(userRepository));
            _domainEventDispatcher = domainEventDispatcher ?? throw new ArgumentNullException(nameof(domainEventDispatcher));
        }

        public IDeviceRepository DeviceRepository { get; }
        public IRoleRepository RoleRepository { get; }
        public IStaffRepository StaffRepository { get; }
        public ITimeTypeRepository TimeTypeRepository { get; }
        public IUserRepository UserRepository { get; }

        public async Task<int> CompleteAsync(CancellationToken cancellationToken)
        {
            // Dispatches domain events if any
            var domainEntities = this._context.ChangeTracker
                .Entries<DomainEntity>()
                .Where(x => x.Entity.DomainEvents != null && x.Entity.DomainEvents.Any())
                .ToList();

            var domainEvents = domainEntities
                .SelectMany(x => x.Entity.DomainEvents)
                .ToList();

            domainEntities.ForEach(entity => entity.Entity.ClearDomainEvents());

            await _domainEventDispatcher.DispatchEventsAsync(domainEvents, cancellationToken);

            return await _context.SaveChangesAsync(cancellationToken);
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
