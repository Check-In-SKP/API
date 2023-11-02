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
        private readonly IStaffRepository _staffRepository;
        private readonly IUnitOfWork _unitOfWork;
        public CreateStaffCommandHandler(StaffFactory staffFactory, IStaffRepository staffRepository, IUnitOfWork unitOfWork)
        {
            _staffFactory = staffFactory ?? throw new ArgumentNullException(nameof(staffFactory));
            _staffRepository = staffRepository ?? throw new ArgumentNullException(nameof(staffRepository));
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
        }
        public async Task<int> Handle(CreateStaffCommand request, CancellationToken cancellationToken)
        {
            var entity = _staffFactory.CreateNewStaff(request.UserId, request.PhoneNumber, request.CardNumber, request.PhoneNotification);
            await _staffRepository.AddAsync(entity);
            await _unitOfWork.CompleteAsync(cancellationToken);

            return entity.Id;
        }
    }
}
