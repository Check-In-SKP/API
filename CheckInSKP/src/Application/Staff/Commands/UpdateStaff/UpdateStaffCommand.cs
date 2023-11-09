using CheckInSKP.Domain.Repositories;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CheckInSKP.Application.Staff.Commands.UpdateStaff
{
    public record UpdateStaffCommand : IRequest
    {
        public int StaffId { get; init; }
        public required string PhoneNumber { get; init; }
        public required string CardNumber { get; init; }
        public bool PhoneNotification { get; init; }
        public bool Preoccupied { get; init; }
        public TimeOnly MeetingTime { get; init; }
    }

    public class UpdateStaffCommandHandler : IRequestHandler<UpdateStaffCommand>
    {
        private readonly IStaffRepository _staffRepository;
        private readonly IUnitOfWork _unitOfWork;
        public UpdateStaffCommandHandler(IStaffRepository staffRepository, IUnitOfWork unitOfWork)
        {
            _staffRepository = staffRepository ?? throw new ArgumentNullException(nameof(staffRepository));
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
        }
        public async Task Handle(UpdateStaffCommand request, CancellationToken cancellationToken)
        {
            Domain.Entities.StaffAggregate.Staff staff = await _staffRepository.GetByIdAsync(request.StaffId) ?? throw new Exception($"Staff with id {request.StaffId} not found");
            staff.Update(request.PhoneNumber, request.CardNumber, request.PhoneNotification, request.Preoccupied, request.MeetingTime);

            await _staffRepository.UpdateAsync(staff);
            await _unitOfWork.CompleteAsync(cancellationToken);
            return;
        }
    }
}
