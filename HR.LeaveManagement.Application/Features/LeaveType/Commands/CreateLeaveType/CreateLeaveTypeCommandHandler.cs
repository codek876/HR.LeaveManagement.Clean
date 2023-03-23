using AutoMapper;
using HR.LeaveManagement.Application.Contracts.Exceptions;
using HR.LeaveManagement.Application.Contracts.Persistence;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HR.LeaveManagement.Application.Features.LeaveType.Commands.CreateLeaveType
{
    public class CreateLeaveTypeCommandHandler : IRequestHandler<CreateLeaveTypeCommand, int>
    {
        private readonly IMapper _mapper;
        private readonly ILeaveTypeRepository _leaveTypeRepository;

        public CreateLeaveTypeCommandHandler(IMapper mapper, ILeaveTypeRepository leaveTypeRepository)
        {
            this._mapper = mapper;
            this._leaveTypeRepository = leaveTypeRepository;
        }
        public async Task<int> Handle(CreateLeaveTypeCommand request, CancellationToken cancellationToken)
        {
            //validate data
            var validator = new CreateLeaveTypeCommandValidator(_leaveTypeRepository);
            var validatorResult = await validator.ValidateAsync(request);

            if (validatorResult.Errors.Any())
                throw new BadRequestException("Invalid LeaveType", validatorResult);

            //convert to domain from entity object
            var createLeaveType = _mapper.Map<Domain.LeaveType>(request);

            //add to database
            await _leaveTypeRepository.CreateAsync(createLeaveType);         

            return createLeaveType.Id;
        }
    }
}
