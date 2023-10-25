using CheckInSKP.Domain.Interfaces.Repositories;
using CheckInSKP.Domain.Factories;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CheckInSKP.Application.Services.Staff.Commands.CreateStaffTimeLog
{
    public record CreateStaffTimeLogCommand : IRequest
    {
        public required int StaffId { get; init; }
        public required int TimeTypeId { get; init; }
    }

    public class CreateStaffTimeLogCommandHandler : IRequestHandler<CreateStaffTimeLogCommand>
    {
        private readonly StaffFactory _staffFactory;
        private readonly IStaffRepository _staffRepository;
        private readonly ITimeTypeRepository _timeTypeRepository;
        private readonly IUnitOfWork _unitOfWork;
        public CreateStaffTimeLogCommandHandler(StaffFactory staffFactory, IStaffRepository staffRepository, ITimeTypeRepository timeTypeRepository, IUnitOfWork unitOfWork)
        {
            _staffFactory = staffFactory ?? throw new ArgumentNullException(nameof(staffFactory));
            _staffRepository = unitOfWork.StaffRepository ?? throw new ArgumentNullException(nameof(staffRepository));
            _timeTypeRepository = unitOfWork.TimeTypeRepository ?? throw new ArgumentNullException(nameof(timeTypeRepository));
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
        }

        public async Task Handle(CreateStaffTimeLogCommand request, CancellationToken cancellationToken)
        {
            Domain.Entities.StaffAggregate.Staff staff = await _staffRepository.GetByIdAsync(request.StaffId);
            if (staff == null)
            {
                throw new Exception($"Staff with id {request.StaffId} not found");
            }
            Domain.Entities.TimeType timeType = await _timeTypeRepository.GetByIdAsync(request.TimeTypeId);
            if (timeType == null)
            {
                throw new Exception($"TimeType with id {request.TimeTypeId} not found");
            }

            Domain.Entities.StaffAggregate.TimeLog staffTimeLog = _staffFactory.CreateNewTimeLog(DateTime.Now, timeType.Id);
            staff.AddTimeLog(staffTimeLog);
            await _unitOfWork.CompleteAsync(cancellationToken);

            return;
        }
    }
}
