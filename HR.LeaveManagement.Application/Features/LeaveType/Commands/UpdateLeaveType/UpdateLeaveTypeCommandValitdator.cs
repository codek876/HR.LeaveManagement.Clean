using FluentValidation;
using HR.LeaveManagement.Application.Contracts.Persistence;
using HR.LeaveManagement.Application.Features.LeaveType.Commands.CreateLeaveType;

namespace HR.LeaveManagement.Application.Features.LeaveType.Commands.UpdateLeaveType
{
    public class UpdateLeaveTypeCommandValitdator : AbstractValidator<UpdateLeaveTypeCommand>
    {
        private readonly ILeaveTypeRepository _leaveTypeRepository;

        public UpdateLeaveTypeCommandValitdator(ILeaveTypeRepository leaveTypeRepository)
        {
            RuleFor(p => p.Id)
                .NotEmpty()
                .MustAsync(LeaveTypeMustExsit);

            RuleFor(p => p.Name)
            .NotEmpty().WithMessage("{PropertyName} is required")
            .NotNull()
            .MaximumLength(70).WithMessage("{PropertyName} must be fewer than 70 charaters");

            RuleFor(p => p.DefaultDays)
                .LessThan(100).WithMessage("{PropertyName} annot exeed 100")
                .GreaterThan(1).WithMessage("{PropertyName} annot be less than 1");                

            RuleFor(q => q)
                .MustAsync(LeaveTypeNameUnique)
                .WithMessage("Leave Type already exsits");

            this._leaveTypeRepository = leaveTypeRepository;
        }

        private async Task<bool> LeaveTypeMustExsit(int id, CancellationToken token)
        {
            var leaveType = await _leaveTypeRepository.GetByIdAsync(id);
            return leaveType != null;
        }

        private async Task<bool> LeaveTypeNameUnique(UpdateLeaveTypeCommand command,
            CancellationToken token)
        {
            return await _leaveTypeRepository.IsLeaveTypeUnique(command.Name);
        }
    }
}