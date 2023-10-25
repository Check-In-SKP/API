using CheckInSKP.Domain.Interfaces.Repositories;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CheckInSKP.Application.Services.User.Commands.DeleteUserToken
{
    public record DeleteUserTokenCommand : IRequest
    {
        public int UserId { get; init; }
        public int TokenId { get; init; }
    }

    public class DeleteUserTokenCommandHandler : IRequestHandler<DeleteUserTokenCommand>
    {
        private readonly IUserRepository _userRepository;
        private readonly IUnitOfWork _unitOfWork;

        public DeleteUserTokenCommandHandler(IUserRepository userTokenRepository, IUnitOfWork unitOfWork)
        {
            _userRepository = userTokenRepository ?? throw new ArgumentNullException(nameof(userTokenRepository));
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
        }

        public async Task Handle(DeleteUserTokenCommand request, CancellationToken cancellationToken)
        {
            Domain.Entities.UserAggregate.User user = await _userRepository.GetByIdAsync(request.UserId) ?? throw new Exception($"User with id {request.UserId} not found");
            var userToken = user.Tokens.FirstOrDefault(ut => ut.Id == request.TokenId) ?? throw new Exception($"Token with id {request.TokenId} not found");
            
            user.RemoveToken(userToken);
            await _unitOfWork.CompleteAsync(cancellationToken);

            return;
        }
    }
}
