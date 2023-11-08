using CheckInSKP.Domain.Factories;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CheckInSKP.Domain.Repositories;

namespace CheckInSKP.Application.Staff.Commands.CreateStaffTimeLog
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
            _staffRepository = staffRepository ?? throw new ArgumentNullException(nameof(staffRepository));
            _timeTypeRepository = timeTypeRepository ?? throw new ArgumentNullException(nameof(timeTypeRepository));
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
        }

        public async Task Handle(CreateStaffTimeLogCommand request, CancellationToken cancellationToken)
        {
            Domain.Entities.StaffAggregate.Staff staff = await _staffRepository.GetByIdAsync(request.StaffId) ?? throw new Exception($"Staff with id {request.StaffId} not found");
            Domain.Entities.TimeType timeType = await _timeTypeRepository.GetByIdAsync(request.TimeTypeId) ?? throw new Exception($"TimeType with id {request.TimeTypeId} not found");

            Domain.Entities.StaffAggregate.TimeLog staffTimeLog = _staffFactory.CreateNewTimeLog(DateTime.Now, timeType.Id);
            
            staff.AddTimeLog(staffTimeLog);
            await _unitOfWork.CompleteAsync(cancellationToken);

            return;
        }
    }
}
