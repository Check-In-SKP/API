using CheckInSKP.Domain.Repositories;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CheckInSKP.Application.Services.User.Commands.UpdateUser
{
    public record UpdateUserPasswordHashCommand : IRequest
    {
        public required int UserId { get; init; }
        public required string PasswordHash { get; init; }
    }

    public class UpdateUserPasswordHashCommandHandler : IRequestHandler<UpdateUserPasswordHashCommand>
    {
        private readonly IUserRepository _userRepository;
        private readonly IUnitOfWork _unitOfWork;
        public UpdateUserPasswordHashCommandHandler(IUserRepository userRepository, IUnitOfWork unitOfWork)
        {
            _userRepository = userRepository ?? throw new ArgumentNullException(nameof(userRepository));
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
        }

        public async Task Handle(UpdateUserPasswordHashCommand request, CancellationToken cancellationToken)
        {
            Domain.Entities.UserAggregate.User user = await _userRepository.GetByIdAsync(request.UserId) ?? throw new Exception($"User with id {request.UserId} not found");
            user.UpdatePasswordHash(request.PasswordHash);
            await _unitOfWork.CompleteAsync(cancellationToken);
            return;
        }
    }
}
