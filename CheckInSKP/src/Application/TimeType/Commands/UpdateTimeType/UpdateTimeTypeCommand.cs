using CheckInSKP.Domain.Repositories;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CheckInSKP.Application.TimeType.Commands.UpdateTimeType
{
    public record UpdateTimeTypeCommand : IRequest
    {
        public int Id { get; init; }
        public required string Name { get; init; }
    }

    public class UpdateTimeTypeCommandHandler : IRequestHandler<UpdateTimeTypeCommand>
    {
        private readonly ITimeTypeRepository _timeTypeRepository;
        private readonly IUnitOfWork _unitOfWork;

        public UpdateTimeTypeCommandHandler(ITimeTypeRepository timeTypeRepository, IUnitOfWork unitOfWork)
        {
            _timeTypeRepository = timeTypeRepository ?? throw new ArgumentNullException(nameof(timeTypeRepository));
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
        }

        public async Task Handle(UpdateTimeTypeCommand request, CancellationToken cancellationToken)
        {
            Domain.Entities.TimeType timeType = await _timeTypeRepository.GetByIdAsync(request.Id) ?? throw new Exception($"TimeType with id {request.Id} not found");
            timeType.UpdateName(request.Name);

            await _timeTypeRepository.UpdateAsync(timeType);
            await _unitOfWork.CompleteAsync(cancellationToken);

            return;
        }
    }
}
