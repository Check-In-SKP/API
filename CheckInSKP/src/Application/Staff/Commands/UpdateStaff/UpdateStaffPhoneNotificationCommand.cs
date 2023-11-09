using CheckInSKP.Domain.Repositories;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CheckInSKP.Application.Staff.Commands.UpdateStaff
{
    public record UpdateStaffPhoneNotificationCommand : IRequest
    {
        public int StaffId { get; init; }
        public required bool PhoneNotification { get; init; }
    }

    public class UpdateStaffPhoneNotificationCommandHandler : IRequestHandler<UpdateStaffPhoneNotificationCommand>
    {
        private readonly IStaffRepository _staffRepository;
        private readonly IUnitOfWork _unitOfWork;
        public UpdateStaffPhoneNotificationCommandHandler(IStaffRepository staffRepository, IUnitOfWork unitOfWork)
        {
            _staffRepository = staffRepository ?? throw new ArgumentNullException(nameof(staffRepository));
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
        }
        public async Task Handle(UpdateStaffPhoneNotificationCommand request, CancellationToken cancellationToken)
        {
            Domain.Entities.StaffAggregate.Staff staff = await _staffRepository.GetByIdAsync(request.StaffId) ?? throw new Exception($"Staff with id {request.StaffId} not found");
            staff.UpdatePhoneNotification(request.PhoneNotification);
            await _unitOfWork.CompleteAsync(cancellationToken);
            return;
        }
    }
}
