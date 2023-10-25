using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CheckInSKP.Application.Services.Role.Queries
{
    public class RoleDto
    {
        public int Id { get; set; }
        public string Name { get; set; }

        private class Mapping : Profile
        {
            public Mapping()
            {
                CreateMap<Domain.Entities.Role, RoleDto>();
            }
        }
    }
}
