using FluentValidation;
using HR.LeaveManagement.Application.Contracts.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HR.LeaveManagement.Application.Features.LeaveType.Commands.CreateLeaveType;

public class CreateLeaveTypeCommandValidator : AbstractValidator<CreateLeaveTypeCommand>
{
    private readonly ILeaveTypeRepository _leaveTypeRepository;

    public CreateLeaveTypeCommandValidator(ILeaveTypeRepository leaveTypeRepository)
	{
		RuleFor(p => p.Name)
			.NotEmpty().WithMessage("{PropertyName} is required")
			.NotNull()
			.MaximumLength(70).WithMessage("{PropertyName} must be fewer than 70 charaters");
		
		RuleFor(p => p.DefaultDays)
			.GreaterThan(1).WithMessage("{PropertyName} annot be less than 1")
			.LessThan(100).WithMessage("{PropertyName} annot exeed 100");

		RuleFor(q => q)
			.MustAsync(LeaveTypeNameUnique)
			.WithMessage("Leave Type already exsits");


        this._leaveTypeRepository = leaveTypeRepository;
    }

    private Task<bool> LeaveTypeNameUnique(CreateLeaveTypeCommand command, CancellationToken token)
    {
		return _leaveTypeRepository.IsLeaveTypeUnique(command.Name);
    }
}
