using AutoMapper;
using HR.LeaveManagement.Application.Contracts.Persistence;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HR.LeaveManagement.Application.Features.LeaveRequest.Queries.GetLeaveRequestList
{
    public class LeaveRequestListQueryHandler : IRequestHandler<LeaveRequestListQuery,
        List<LeaveRequestListDto>>
    {
        private readonly IMapper _mapper;
        private readonly ILeaveRequestRepository _leaveRequestRepository;

        public LeaveRequestListQueryHandler(IMapper mapper,
            ILeaveRequestRepository leaveRequestRepository)
        {
            this._mapper = mapper;
            this._leaveRequestRepository = leaveRequestRepository;
        }
        public async Task<List<LeaveRequestListDto>> Handle(LeaveRequestListQuery request,
            CancellationToken cancellationToken)
        {
            var leaveRequest = await _leaveRequestRepository.GetLeaveRequestWithDetails();
            var requests = _mapper.Map<List<LeaveRequestListDto>>(leaveRequest);
            return requests;
        }
    }
}
