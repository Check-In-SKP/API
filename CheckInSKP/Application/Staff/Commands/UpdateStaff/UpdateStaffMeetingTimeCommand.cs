using CheckInSKP.Domain.Repositories;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CheckInSKP.Application.Staff.Commands.UpdateStaff
{
    public record UpdateStaffMeetingTimeCommand : IRequest
    {
        public int StaffId { get; init; }
        public required TimeOnly MeetingTime { get; init; }
    }

    public class UpdateStaffMeetingTimeCommandHandler : IRequestHandler<UpdateStaffMeetingTimeCommand>
    {
        private readonly IStaffRepository _staffRepository;
        private readonly IUnitOfWork _unitOfWork;
        public UpdateStaffMeetingTimeCommandHandler(IStaffRepository staffRepository, IUnitOfWork unitOfWork)
        {
            _staffRepository = staffRepository ?? throw new ArgumentNullException(nameof(staffRepository));
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
        }
        public async Task Handle(UpdateStaffMeetingTimeCommand request, CancellationToken cancellationToken)
        {
            Domain.Entities.StaffAggregate.Staff staff = await _staffRepository.GetByIdAsync(request.StaffId);
            if (staff == null)
            {
                throw new Exception($"Staff with id {request.StaffId} not found");
            }
            staff.UpdateMeetingTime(request.MeetingTime);
            await _unitOfWork.CompleteAsync(cancellationToken);
            return;
        }
    }
}
