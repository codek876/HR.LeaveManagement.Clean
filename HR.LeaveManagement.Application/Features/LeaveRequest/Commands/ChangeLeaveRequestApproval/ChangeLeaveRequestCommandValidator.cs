using FluentValidation;
using HR.LeaveManagement.Application.Contracts.Persistence;

namespace HR.LeaveManagement.Application.Features.LeaveRequest.Commands.ChangeLeaveRequest
{
    public class ChangeLeaveRequestApprovalCommandValidator : AbstractValidator<ChangeLeaveRequestApprovalCommand>
    {

        public ChangeLeaveRequestApprovalCommandValidator()
        {
            RuleFor(p => p.Arrpoved).
                NotNull().WithMessage("Approval cannot be null");
        }
    }
}
