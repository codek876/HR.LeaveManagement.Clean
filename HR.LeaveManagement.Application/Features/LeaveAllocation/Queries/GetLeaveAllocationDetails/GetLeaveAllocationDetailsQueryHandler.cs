using AutoMapper;
using HR.LeaveManagement.Application.Contracts.Persistence;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HR.LeaveManagement.Application.Features.LeaveAllocation.Queries.GetLeaveAllocationDetails
{
    public class GetLeaveAllocationDetailsQueryHandler :
        IRequestHandler<GetLeaveAllocationDetailsQuery,LeaveAllocationDetailsDto>
    {
        private readonly IMapper _mapper;
        private readonly ILeaveAllocationRepository _leaveAllocationRepository;

        public GetLeaveAllocationDetailsQueryHandler(IMapper mapper,
            ILeaveAllocationRepository leaveAllocationRepository)
        {
            this._mapper = mapper;
            this._leaveAllocationRepository = leaveAllocationRepository;
        }
        public async Task<LeaveAllocationDetailsDto> Handle(GetLeaveAllocationDetailsQuery
            request, CancellationToken cancellationToken)
        {
            var leaveAllocations = await _leaveAllocationRepository
                .GetLeaveAllocationWithDetails(request.Id);

            var dtoObject = _mapper.Map<LeaveAllocationDetailsDto>(leaveAllocations);

            return dtoObject;
        }
    }
}
