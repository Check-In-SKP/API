﻿using AutoMapper;
using CheckInMonitorAPI.Models.DTOs.Role;
using CheckInMonitorAPI.Models.Entities;

namespace CheckInMonitorAPI.Extensions
{
    public class RoleProfile : Profile
    {
        public RoleProfile()
        {
            CreateMap<CreateRoleDTO, Role>();
        }
    }
}
