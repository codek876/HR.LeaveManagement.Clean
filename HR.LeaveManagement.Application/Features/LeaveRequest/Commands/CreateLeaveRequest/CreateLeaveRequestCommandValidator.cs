using FluentValidation;
using HR.LeaveManagement.Application.Contracts.Persistence;
using HR.LeaveManagement.Application.Features.LeaveRequest.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HR.LeaveManagement.Application.Features.LeaveRequest.Commands.CreateLeaveRequest;

public class CreateLeaveRequestCommandValidator :
    AbstractValidator<CreateLeaveRequestCommand>
{
    private readonly ILeaveTypeRepository _leaveTypeRepository;

    public CreateLeaveRequestCommandValidator(ILeaveTypeRepository leaveTypeRepository)
	{
        this._leaveTypeRepository = leaveTypeRepository;

        Include(new BaseLeaveRequestValidator(_leaveTypeRepository));

        RuleFor(p => p.LeaveTypeId)
                .GreaterThan(0)
                .MustAsync(LeaveTypeMustExist)
                .WithMessage("{PropertyName} does not exist");
    }

    private async Task<bool> LeaveTypeMustExist(int id, CancellationToken arg2)
    {
        var leaveType = await _leaveTypeRepository.GetByIdAsync(id);
        return leaveType != null;
    }
}
