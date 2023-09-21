using AutoMapper;
using CheckInMonitorAPI.Models.DTOs.Role;
using CheckInMonitorAPI.Models.DTOs.TimeType;
using CheckInMonitorAPI.Models.Entities;

namespace CheckInMonitorAPI.Extensions.Mapping
{
    public class RoleProfile : Profile
    {
        public RoleProfile()
        {
            CreateMap<CreateRoleDTO, Role>();
            CreateMap<Role, ResponseRoleDTO>();
            CreateMap<RoleDTO, Role>();
        }
    }
}
