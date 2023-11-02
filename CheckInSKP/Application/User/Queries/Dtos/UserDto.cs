using AutoMapper;

namespace CheckInSKP.Application.User.Queries.Dtos
{
    public class UserDto
    {
        public int Id { get; init; }
        public required string Name { get; init; }
        public required string Username { get; init; }
        public required string PasswordHash { get; init; }
        public int RoleId { get; init; }

        private class Mapping : Profile
        {
            public Mapping()
            {
                CreateMap<Domain.Entities.User, UserDto>();
            }
        }
    }
}
