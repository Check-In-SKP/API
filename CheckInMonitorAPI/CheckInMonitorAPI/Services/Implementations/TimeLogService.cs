using CheckInMonitorAPI.Data.Repositories.Interfaces;
using CheckInMonitorAPI.Data.Repositories.UnitOfWork.Interfaces;
using CheckInMonitorAPI.Models.Entities;
using CheckInMonitorAPI.Services.Interfaces;

namespace CheckInMonitorAPI.Services.Implementations
{
    public class TimeLogService : GenericService<TimeLog, int>, ITimeLogService
    {
        private readonly IGenericRepository<TimeLog, int> _repository;

        public TimeLogService(IUnitOfWork unitOfWork, ILogger<GenericService<TimeLog, int>> logger) : base(unitOfWork, logger)
        {
            _repository = unitOfWork.GetRepository<TimeLog, int>();
        }

        // Other implementations here
    }
}
