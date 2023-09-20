using CheckInMonitorAPI.Data.Repositories.Interfaces;
using CheckInMonitorAPI.Data.Repositories.UnitOfWork.Interfaces;
using CheckInMonitorAPI.Models.Entities;
using CheckInMonitorAPI.Services.Interfaces;

namespace CheckInMonitorAPI.Services.Implementations
{
    public class TimeTypeService : GenericService<TimeType, int>, ITimeTypeService
    {
        private readonly IGenericRepository<TimeType, int> _repository;

        public TimeTypeService(IUnitOfWork unitOfWork, ILogger<GenericService<TimeType, int>> logger) : base(unitOfWork, logger)
        {
            _repository = unitOfWork.GetRepository<TimeType, int>();
        }

        // Other implementations here
    }
}
