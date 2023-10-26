using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CheckInSKP.Application.Services.Staff.Queries.Dtos
{
    public class StaffDto
    {
        public int Id { get; init; }
        public string PhoneNumber { get; init; }
        public string CardNumber { get; init; }
        public bool PhoneNotification { get; init; }
        public bool IsPreoccupied { get; init; }
        public TimeOnly MeetingTime { get; init; }
        public IEnumerable<TimeLogDto> TimeLogs { get; init; }

        private class Mapping : Profile
        {
            public Mapping()
            {
                CreateMap<Domain.Entities.StaffAggregate.Staff, StaffDto>();
            }
        }
    }
}
