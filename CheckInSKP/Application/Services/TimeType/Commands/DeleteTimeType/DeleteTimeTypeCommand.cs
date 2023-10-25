using CheckInSKP.Domain.Interfaces.Repositories;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CheckInSKP.Application.Services.TimeType.Commands.DeleteTimeType
{
    public record DeleteTimeTypeCommand : IRequest
    {
        public int Id { get; init; }
    }

    public class DeleteTimeTypeCommandHandler : IRequestHandler<DeleteTimeTypeCommand>
    {
        private readonly ITimeTypeRepository _timeTypeRepository;
        private readonly IUnitOfWork _unitOfWork;

        public DeleteTimeTypeCommandHandler(ITimeTypeRepository timeTypeRepository, IUnitOfWork unitOfWork)
        {
            _timeTypeRepository = timeTypeRepository ?? throw new ArgumentNullException(nameof(timeTypeRepository));
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
        }

        public async Task Handle(DeleteTimeTypeCommand request, CancellationToken cancellationToken)
        {
            Domain.Entities.TimeType timeType = await _timeTypeRepository.GetByIdAsync(request.Id);

            if (timeType == null)
            {
                throw new Exception($"TimeType with id {request.Id} not found");
            }

            await _timeTypeRepository.RemoveAsync(timeType.Id);
            await _unitOfWork.CompleteAsync(cancellationToken);

            return;
        }
    }
}
