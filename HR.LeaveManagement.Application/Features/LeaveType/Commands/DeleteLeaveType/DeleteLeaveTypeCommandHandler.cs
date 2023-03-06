using AutoMapper;
using HR.LeaveManagement.Application.Contracts.Exceptions;
using HR.LeaveManagement.Application.Contracts.Persistence;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HR.LeaveManagement.Application.Features.LeaveType.Commands.DeleteLeaveType
{
    public class DeleteLeaveTypeCommandHandler : IRequestHandler<DeleteLeaveTypeCommand, Unit>
    {
        private readonly ILeaveTypeRepository _leaveTypeRepository;

        public DeleteLeaveTypeCommandHandler(ILeaveTypeRepository leaveTypeRepository)
        {
            this._leaveTypeRepository = leaveTypeRepository;
        }
        public async Task<Unit> Handle(DeleteLeaveTypeCommand request, CancellationToken cancellationToken)
        {
            //receieve leaveType by id to be deleted
            var deleteLeaveType = await _leaveTypeRepository.GetByIdAsync(request.Id);

            //validate exsistence
            if (deleteLeaveType == null)
                throw new NotFoundException(nameof(LeaveType), request.Id);

            //delete query
            await _leaveTypeRepository.DeleteAsync(deleteLeaveType);

            return Unit.Value;
        }
    }
}
