using AutoMapper;
using CheckInMonitorAPI.Models.Entities;
using CheckInMonitorAPI.Models.DTOs;

namespace CheckInMonitorAPI.Extensions
{
    public class UserProfile : Profile
    {
        public UserProfile()
        {
            CreateMap<CreateUserDTO, User>();
        }
    }
}
