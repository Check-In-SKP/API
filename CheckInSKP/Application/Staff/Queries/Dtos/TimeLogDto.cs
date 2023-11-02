using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CheckInSKP.Application.Staff.Queries.Dtos
{
    public class TimeLogDto
    {
        public int Id { get; init; }
        public DateTime TimeStamp { get; init; }
        public int TimeTypeId { get; init; }

        private class Mapping : Profile
        {
            public Mapping()
            {
                CreateMap<Domain.Entities.StaffAggregate.TimeLog, TimeLogDto>();
            }
        }
    }
}
