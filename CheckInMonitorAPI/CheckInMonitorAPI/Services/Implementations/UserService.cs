using CheckInMonitorAPI.Data.Repositories.Interfaces;
using CheckInMonitorAPI.Data.Repositories.UnitOfWork.Interfaces;
using CheckInMonitorAPI.Models.DTOs.User;
using CheckInMonitorAPI.Models.Entities;
using CheckInMonitorAPI.Services.Interfaces;

namespace CheckInMonitorAPI.Services.Implementations
{
    public class UserService : GenericService<User, int>, IUserService
    {
        private readonly IGenericRepository<User, int> _repository;

        public UserService(IUnitOfWork unitOfWork, ILogger<GenericService<User, int>> logger) : base(unitOfWork, logger)
        {
            _repository = unitOfWork.GetRepository<User, int>();
        }

        public bool Login(LoginDTO loginDTO)
        {
            var user = _repository.GetAllAsync().Result.Where(u => u.Username == loginDTO.Username).FirstOrDefault();
            if (user == null)
                return false;

            if (user.Username == loginDTO.Username && user.Password == loginDTO.Password)
                return true;
            else
                return false;
        }
    }
}
