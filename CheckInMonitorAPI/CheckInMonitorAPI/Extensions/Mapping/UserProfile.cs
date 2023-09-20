using AutoMapper;
using CheckInMonitorAPI.Models.Entities;
using CheckInMonitorAPI.Models.DTOs.User;

namespace CheckInMonitorAPI.Extensions.Mapping
{
    public class UserProfile : Profile
    {
        public UserProfile()
        {
            CreateMap<CreateUserDTO, User>();
            CreateMap<User, ResponseUserDTO>();
            CreateMap<UserDTO, User>();
        }
    }
}
