using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CheckInSKP.Application.Services.TimeType.Queries
{
    public class TimeTypeDto
    {
        public int Id { get; init; }
        public string Name { get; init; }

        private class Mapping : Profile
        {
            public Mapping()
            {
                CreateMap<Domain.Entities.TimeType, TimeTypeDto>();
            }
        }
    }
}
