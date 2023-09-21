using AutoMapper;
using CheckInMonitorAPI.Models.DTOs.TimeLog;
using CheckInMonitorAPI.Models.DTOs.TimeType;
using CheckInMonitorAPI.Models.Entities;

namespace CheckInMonitorAPI.Extensions.Mapping
{
    public class TimeLogProfile : Profile
    {
        public TimeLogProfile()
        {
            CreateMap<CreateTimeLogDTO, TimeLog>();
            CreateMap<TimeLog, ResponseTimeLogDTO>();
            CreateMap<TimeLogDTO, TimeLog>();
        }
    }
}
