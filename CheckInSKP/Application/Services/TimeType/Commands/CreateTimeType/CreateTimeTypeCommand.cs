using CheckInSKP.Domain.Factories;
using CheckInSKP.Domain.Interfaces.Repositories;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CheckInSKP.Application.Services.TimeType.Commands.CreateTimeType
{
    public record CreateTimeTypeCommand : IRequest<int>
    {
        public required string Name { get; init; }
    }

    public class CreateTimeTypeCommandHandler : IRequestHandler<CreateTimeTypeCommand, int>
    {
        private readonly TimeTypeFactory _timeTypeFactory;
        private readonly ITimeTypeRepository _timeTypeRepository;
        private readonly IUnitOfWork _unitOfWork;

        public CreateTimeTypeCommandHandler(TimeTypeFactory timeTypeFactory, ITimeTypeRepository timeTypeRepository, IUnitOfWork unitOfWork)
        {
            _timeTypeFactory = timeTypeFactory ?? throw new ArgumentNullException(nameof(timeTypeFactory));
            _timeTypeRepository = timeTypeRepository ?? throw new ArgumentNullException(nameof(timeTypeRepository));
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
        }

        public async Task<int> Handle(CreateTimeTypeCommand request, CancellationToken cancellationToken)
        {
            Domain.Entities.TimeType timeType = _timeTypeFactory.CreateNewTimeType(request.Name);

            await _timeTypeRepository.AddAsync(timeType);
            await _unitOfWork.CompleteAsync(cancellationToken);

            return timeType.Id;
        }
    }
}
