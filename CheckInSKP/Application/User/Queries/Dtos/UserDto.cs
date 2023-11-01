using AutoMapper;
using AutoMapper.Configuration;
using CheckInSKP.Application.Services.Staff.Queries.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CheckInSKP.Application.Services.User.Queries.Dtos
{
    public class UserDto
    {
        public int Id { get; init; }
        public required string Name { get; init; }
        public required string Username { get; init; }
        public required string PasswordHash { get; init; }
        public int RoleId { get; init; }
        public IEnumerable<TokenDto>? Tokens { get; init; }

        private class Mapping : Profile
        {
            public Mapping()
            {
                CreateMap<Domain.Entities.UserAggregate.User, UserDto>();
            }
        }
    }
}
