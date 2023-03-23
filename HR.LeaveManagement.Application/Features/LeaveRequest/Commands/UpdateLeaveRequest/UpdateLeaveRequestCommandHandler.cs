using AutoMapper;
using HR.LeaveManagement.Application.Contracts.Email;
using HR.LeaveManagement.Application.Contracts.Exceptions;
using HR.LeaveManagement.Application.Contracts.Logging;
using HR.LeaveManagement.Application.Contracts.Persistence;
using HR.LeaveManagement.Application.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HR.LeaveManagement.Application.Features.LeaveRequest.Commands.UpdateLeaveRequest
{
    public class UpdateLeaveRequestCommandHandler : IRequestHandler<UpdateLeaveRequestCommand,
        Unit>
    {
        private readonly IMapper _mapper;
        private readonly IEmailSender _emailSender;
        private readonly ILeaveRequestRepository _leaveRequestRepository;
        private readonly ILeaveTypeRepository _leaveTypeRepository;
        private readonly IAppLogger<UpdateLeaveRequestCommandHandler> _logger;

        public UpdateLeaveRequestCommandHandler(IMapper mapper, IEmailSender emailSender,
            ILeaveRequestRepository leaveRequestRepository,
            ILeaveTypeRepository leaveTypeRepository,
            IAppLogger<UpdateLeaveRequestCommandHandler> logger)
        {
            _mapper = mapper;
            this._emailSender = emailSender;
            _leaveRequestRepository = leaveRequestRepository;
            _leaveTypeRepository = leaveTypeRepository;
            this._logger = logger;
        }

        public async Task<Unit> Handle(UpdateLeaveRequestCommand request,
            CancellationToken cancellationToken)
        {
            var leaveRequest = await _leaveRequestRepository.GetByIdAsync(request.Id);

            if (leaveRequest == null) 
                throw new NotFoundException(nameof(leaveRequest), request.Id);

            var validator = new UpdateLeaveRequestCommandValidator(_leaveRequestRepository,
                _leaveTypeRepository);

            var validationResult = await validator.ValidateAsync(request);

            if (validationResult.Errors.Any())
            {
                throw new BadRequestException("Invalid Leave Request", validationResult);
            }

            _mapper.Map(request, leaveRequest);

            await _leaveRequestRepository.UpdateAsync(leaveRequest);

            try
            {
                var email = new EmailMessage
                {
                    To = string.Empty,
                    Body = $"Your leave request for {request.StartDate:D} to {request.EndDate:D} " +
                            $"has been updated successfully",
                    Subject = "Leave Request Updated"

                };

                await _emailSender.SendEmail(email);
            }
            catch(Exception ex)
            {
                _logger.LogWarning(ex.Message);
            }

            return Unit.Value; 
        }
    }
}
