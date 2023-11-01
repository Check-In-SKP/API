using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CheckInSKP.Application.Services.User.Queries.Dtos
{
    public class TokenDto
    {
        public int Id { get; init; }
        public required string Token { get; init; }
        public bool IsRevoked { get; init; }
        public DateTime Expiration { get; init; }

        private class Mapping : Profile
        {
            public Mapping()
            {
                CreateMap<Domain.Entities.UserAggregate.Token, TokenDto>();
            }
        }
    }
}
