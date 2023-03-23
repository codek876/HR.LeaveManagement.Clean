using AutoMapper;
using HR.LeaveManagement.Application.Contracts.Email;
using HR.LeaveManagement.Application.Contracts.Exceptions;
using HR.LeaveManagement.Application.Contracts.Persistence;
using HR.LeaveManagement.Application.Models;
using MediatR;

namespace HR.LeaveManagement.Application.Features.LeaveRequest.Commands.ChangeLeaveRequest
{
    public class ChangeLeaveRequestApprovalCommandHandler : IRequestHandler<ChangeLeaveRequestApprovalCommand,
        Unit>
    {
        private readonly IMapper _mapper;
        private readonly IEmailSender _emailSender;
        private readonly ILeaveTypeRepository _leaveTypeRepository;
        private readonly ILeaveRequestRepository _leaveRequestRepository;

        public ChangeLeaveRequestApprovalCommandHandler(IMapper mapper,
            IEmailSender emailSender,
            ILeaveTypeRepository leaveTypeRepository,
            ILeaveRequestRepository leaveRequestRepository)
        {
            _mapper = mapper;
            _emailSender = emailSender;
            _leaveTypeRepository = leaveTypeRepository;
            this._leaveRequestRepository = leaveRequestRepository;
        }
        public async Task<Unit> Handle(ChangeLeaveRequestApprovalCommand request,
            CancellationToken cancellationToken)
        {
            var leaveRequest = await _leaveRequestRepository.GetByIdAsync(request.Id);

            if (leaveRequest == null)
                throw new NotFoundException(nameof(leaveRequest), request.Id);

            leaveRequest.Arrpoved = request.Arrpoved;
            await _leaveRequestRepository.UpdateAsync(leaveRequest);

            //update employee allocation if approved

            var email = new EmailMessage
            {
                To = string.Empty,
                Body = $"The approvl status leave request for {leaveRequest.StartDate:D}" +
                $"to {leaveRequest.EndDate:D} has been updated successfully",
                Subject = "Leave Request Approval Status Updated"

            };

            await _emailSender.SendEmail(email);

            return Unit.Value;

        }
    }
}
