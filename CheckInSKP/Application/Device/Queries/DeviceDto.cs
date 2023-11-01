using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CheckInSKP.Application.Services.Device.Queries
{
    public class DeviceDto
    {
        public Guid Id { get; set; }
        public string? Label { get; set; }
        public bool IsAuthorized { get; set; }

        private class Mapping : Profile
        {
            public Mapping()
            {
                CreateMap<Domain.Entities.Device, DeviceDto>();
            }
        }
    }
}
