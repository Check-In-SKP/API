using CheckInSKP.Domain.Enums;
using CheckInSKP.Domain.Factories;
using CheckInSKP.Domain.Repositories;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CheckInSKP.Application.Staff.Commands.CreateStaff
{
    public record CreateStaffCommand : IRequest<int>
    {
        public required int UserId { get; init; }
        public required string PhoneNumber { get; init; }
        public required string CardNumber { get; init; }
        public required bool PhoneNotification { get; init; }
    }

    public class CreateStaffCommandHandler : IRequestHandler<CreateStaffCommand, int>
    {
        private readonly StaffFactory _staffFactory;
        private readonly IUserRepository _userRepository;
        private readonly IStaffRepository _staffRepository;
        private readonly IUnitOfWork _unitOfWork;
        public CreateStaffCommandHandler(StaffFactory staffFactory, IUserRepository userRepository, IStaffRepository staffRepository, IUnitOfWork unitOfWork)
        {
            _staffFactory = staffFactory ?? throw new ArgumentNullException(nameof(staffFactory));
            _userRepository = userRepository ?? throw new ArgumentNullException(nameof(userRepository));
            _staffRepository = staffRepository ?? throw new ArgumentNullException(nameof(staffRepository));
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
        }
        public async Task<int> Handle(CreateStaffCommand request, CancellationToken cancellationToken)
        {
            var user = await _userRepository.GetByIdAsync(request.UserId) ?? throw new Exception($"User with id {request.UserId} not found");
            var entity = _staffFactory.CreateNewStaff(request.UserId, request.PhoneNumber, request.CardNumber, request.PhoneNotification);
            
            user.UpdateRole((int)RoleEnum.Staff);

            await _staffRepository.AddAsync(entity);
            await _userRepository.UpdateAsync(user);

            await _unitOfWork.CompleteAsync(cancellationToken);

            return entity.Id;
        }
    }
}
