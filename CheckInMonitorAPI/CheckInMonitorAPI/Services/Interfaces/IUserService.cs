using CheckInMonitorAPI.Models.DTOs.User;
using CheckInMonitorAPI.Models.Entities;
using CheckInMonitorAPI.Services.Implementations;

namespace CheckInMonitorAPI.Services.Interfaces
{
    public interface IUserService : IGenericService<User, int>
    {
        bool Login(LoginDTO loginDTO);
    }
}
