using CheckInMonitorAPI.Data.Repositories.Interfaces;
using CheckInMonitorAPI.Data.Repositories.UnitOfWork.Interfaces;
using CheckInMonitorAPI.Models.Entities;
using CheckInMonitorAPI.Services.Interfaces;
using System.IdentityModel.Tokens.Jwt;
using System.Text;

namespace CheckInMonitorAPI.Services.Implementations
{
    public class TokenService : GenericService<Token, int>, ITokenService
    {
        private readonly IGenericRepository<Token, int> _repository;

        public TokenService(IUnitOfWork unitOfWork, ILogger<GenericService<Token, int>> logger) : base(unitOfWork, logger)
        {
            _repository = unitOfWork.GetRepository<Token, int>();
        }
    }
}
