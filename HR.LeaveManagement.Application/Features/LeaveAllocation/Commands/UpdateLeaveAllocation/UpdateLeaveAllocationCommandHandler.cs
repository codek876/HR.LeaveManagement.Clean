﻿using AutoMapper;
using HR.LeaveManagement.Application.Contracts.Exceptions;
using HR.LeaveManagement.Application.Contracts.Persistence;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HR.LeaveManagement.Application.Features.LeaveAllocation.Commands.UpdateLeaveAllocation
{
    public class UpdateLeaveAllocationCommandHandler : IRequestHandler<UpdateLeaveAllocationCommand,
        Unit>
    {
        private readonly IMapper _mapper;
        private readonly ILeaveTypeRepository _leaveTypeRepository;
        private readonly ILeaveAllocationRepository _leaveAllocationRepository;

        public UpdateLeaveAllocationCommandHandler(IMapper mapper,
            ILeaveTypeRepository leaveTypeRepository,
            ILeaveAllocationRepository leaveAllocationRepository)
        {
            this._mapper = mapper;
            this._leaveTypeRepository = leaveTypeRepository;
            this._leaveAllocationRepository = leaveAllocationRepository;
        }
        public async Task<Unit> Handle(UpdateLeaveAllocationCommand request, CancellationToken cancellationToken)
        {
            var validator = new UpdateLeaveAllocationCommandValidator(_leaveTypeRepository,
                _leaveAllocationRepository);
            var validationResult = await validator.ValidateAsync(request);

            if (validationResult.Errors.Any())
            {
                throw new BadRequestException("Inalid Leave Allocation", validationResult);
            }

            var allocation = await _leaveAllocationRepository.GetByIdAsync(request.Id);

            if (allocation == null)
            {
                throw new NotFoundException(nameof(LeaveAllocation), request.Id);
            }

            _mapper.Map(request, allocation);

            await _leaveAllocationRepository.UpdateAsync(allocation);

            return Unit.Value;
        }
    }
}
