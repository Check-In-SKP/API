using AutoMapper;
using CheckInMonitorAPI.Models.DTOs.TimeType;
using CheckInMonitorAPI.Models.DTOs.User;
using CheckInMonitorAPI.Models.Entities;

namespace CheckInMonitorAPI.Extensions.Mapping
{
    public class TimeTypeProfile : Profile
    {
        public TimeTypeProfile()
        {
            CreateMap<CreateTimeTypeDTO, TimeType>();
            CreateMap<TimeType, ResponseTimeTypeDTO>();
            CreateMap<TimeTypeDTO, TimeType>();
            CreateMap<UpdateTimeTypeDTO, TimeType>();
        }
    }
}
