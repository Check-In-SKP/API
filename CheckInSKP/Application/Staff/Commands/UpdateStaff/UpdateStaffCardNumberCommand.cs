using CheckInSKP.Domain.Repositories;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CheckInSKP.Application.Services.Staff.Commands.UpdateStaff
{
    public record UpdateStaffCardNumberCommand : IRequest
    {
        public int StaffId { get; init; }
        public required string CardNumber { get; init;}
    }

    public class UpdateStaffCardNumberCommandHandler : IRequestHandler<UpdateStaffCardNumberCommand>
    {
        private readonly IStaffRepository _staffRepository;
        private readonly IUnitOfWork _unitOfWork;

        public UpdateStaffCardNumberCommandHandler(IStaffRepository staffRepository, IUnitOfWork unitOfWork)
        {
            _staffRepository = staffRepository ?? throw new ArgumentNullException(nameof(staffRepository));
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
        }

        public async Task Handle(UpdateStaffCardNumberCommand request, CancellationToken cancellationToken)
        {
            Domain.Entities.StaffAggregate.Staff staff = await _staffRepository.GetByIdAsync(request.StaffId);
            if (staff == null)
            {
                throw new Exception($"Staff with id {request.StaffId} not found");
            }

            staff.UpdateCardNumber(request.CardNumber);
            await _unitOfWork.CompleteAsync(cancellationToken);
            return;
        }
    }
}
