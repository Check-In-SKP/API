using AutoMapper;
using CheckInSKP.Application.User.Queries.Dtos;
using CheckInSKP.Domain.Repositories;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CheckInSKP.Application.User.Queries
{
    public record GetUserByIdQuery : IRequest<UserDto>
    {
        public required Guid UserId { get; init; }
    }

    public class GetUserByIdQueryHandler : IRequestHandler<GetUserByIdQuery, UserDto>
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;

        public GetUserByIdQueryHandler(IUserRepository userRepository, IMapper mapper)
        {
            _userRepository = userRepository ?? throw new ArgumentNullException(nameof(userRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        public async Task<UserDto> Handle(GetUserByIdQuery request, CancellationToken cancellationToken)
        {
            Domain.Entities.User user = await _userRepository.GetByIdAsync(request.UserId) ?? throw new Exception($"User with id {request.UserId} not found");
            UserDto userDto = _mapper.Map<UserDto>(user);
            return userDto;
        }
    }
}
