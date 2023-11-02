using CheckInSKP.Domain.Repositories;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CheckInSKP.Application.Staff.Commands.UpdateStaff
{
    public record UpdateStaffPhoneNumberCommand : IRequest
    {
        public int StaffId { get; init; }
        public required string PhoneNumber { get; init; }
    }

    public class UpdateStaffPhoneNumberCommandHandler : IRequestHandler<UpdateStaffPhoneNumberCommand>
    {
        private readonly IStaffRepository _staffRepository;
        private readonly IUnitOfWork _unitOfWork;
        public UpdateStaffPhoneNumberCommandHandler(IStaffRepository staffRepository, IUnitOfWork unitOfWork)
        {
            _staffRepository = staffRepository ?? throw new ArgumentNullException(nameof(staffRepository));
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
        }
        public async Task Handle(UpdateStaffPhoneNumberCommand request, CancellationToken cancellationToken)
        {
            Domain.Entities.StaffAggregate.Staff staff = await _staffRepository.GetByIdAsync(request.StaffId);
            if (staff == null)
            {
                throw new Exception($"Staff with id {request.StaffId} not found");
            }
            staff.UpdatePhoneNumber(request.PhoneNumber);
            await _unitOfWork.CompleteAsync(cancellationToken);
            return;
        }
    }
}
