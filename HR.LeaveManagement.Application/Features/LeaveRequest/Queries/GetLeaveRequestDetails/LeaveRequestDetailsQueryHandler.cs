using AutoMapper;
using HR.LeaveManagement.Application.Contracts.Persistence;
using MediatR;

namespace HR.LeaveManagement.Application.Features.LeaveRequest.Queries.GetLeaveRequestDetails
{
    public class LeaveRequestDetailsQueryHandler : IRequestHandler<LeaveRequestDetailsQuery,
        LeaveRequestDetailsDto>
    {
        private readonly IMapper _mapper;
        private readonly ILeaveRequestRepository _leaveRequestRepository;

        public LeaveRequestDetailsQueryHandler(IMapper mapper,
            ILeaveRequestRepository leaveRequestRepository)
        {
            this._mapper = mapper;
            this._leaveRequestRepository = leaveRequestRepository;
        }
        public async Task<LeaveRequestDetailsDto> Handle(LeaveRequestDetailsQuery request,
            CancellationToken cancellationToken)
        {
            var leaveRequest = await _leaveRequestRepository
                .GetLeaveRequestWithDetails(request.Id);
            var requests = _mapper.Map<LeaveRequestDetailsDto>(leaveRequest);

            //add employee details needed

            return requests;
        }
    }


}
