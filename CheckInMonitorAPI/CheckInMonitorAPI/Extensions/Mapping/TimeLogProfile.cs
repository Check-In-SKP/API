using AutoMapper;
using CheckInMonitorAPI.Models.DTOs.TimeLog;
using CheckInMonitorAPI.Models.Entities;

namespace CheckInMonitorAPI.Extensions.Mapping
{
    public class TimeLogProfile : Profile
    {
        public TimeLogProfile()
        {
            CreateMap<CreateTimeLogDTO, TimeLog>();
        }
    }
}
