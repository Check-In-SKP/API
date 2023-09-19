using CheckInMonitorAPI.Data.Repositories.Interfaces;
using CheckInMonitorAPI.Data.Repositories.UnitOfWork.Interfaces;
using CheckInMonitorAPI.Models.Entities;
using CheckInMonitorAPI.Services.Interfaces;

namespace CheckInMonitorAPI.Services.Implementations
{
    public class RoleService : GenericService<Role, int>, IRoleService
    {
        private readonly IGenericRepository<Role, int> _repository;

        public RoleService(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
            _repository = unitOfWork.GetRepository<Role, int>();
        }

        // Other implementations here
    }
}
