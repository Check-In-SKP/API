using CheckInMonitorAPI.Data.Repositories.Interfaces;
using CheckInMonitorAPI.Data.Repositories.UnitOfWork.Interfaces;
using CheckInMonitorAPI.Models.Entities;
using CheckInMonitorAPI.Services.Interfaces;

namespace CheckInMonitorAPI.Services.Implementations
{
    public class RoleService : GenericService<Role, int>, IRoleService
    {
        private readonly IGenericRepository<Role, int> _repository;

        public RoleService(IUnitOfWork unitOfWork, ILogger<GenericService<Role, int>> logger) : base(unitOfWork, logger)
        {
            _repository = unitOfWork.GetRepository<Role, int>();
        }

        // Other implementations here
    }
}
