using CheckInSKP.Domain.Repositories;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CheckInSKP.Application.User.Commands.DeleteUser
{
    public record DeleteUserCommand : IRequest
    {
        public int UserId { get; init; }
    }

    public class DeleteUserCommandHandler : IRequestHandler<DeleteUserCommand>
    {
        private readonly IUserRepository _userRepository;
        private readonly IUnitOfWork _unitOfWork;

        public DeleteUserCommandHandler(IUserRepository userRepository, IUnitOfWork unitOfWork)
        {
            _userRepository = userRepository ?? throw new ArgumentNullException(nameof(userRepository));
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
        }

        public async Task Handle(DeleteUserCommand request, CancellationToken cancellationToken)
        {
            Domain.Entities.User user = await _userRepository.GetByIdAsync(request.UserId);

            if (user == null)
            {
                throw new Exception($"User with id {request.UserId} not found");
            }

            await _userRepository.RemoveAsync(user.Id);
            await _unitOfWork.CompleteAsync(cancellationToken);

            return;
        }
    }
}
