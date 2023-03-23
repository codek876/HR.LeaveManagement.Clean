using AutoMapper;
using HR.LeaveManagement.Application.Contracts.Logging;
using HR.LeaveManagement.Application.Contracts.Persistence;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HR.LeaveManagement.Application.Features.LeaveAllocation.Queries.GetAllLeaveAllocations
{
    public class GetLeaveAllocationsQueryHandler : IRequestHandler<GetLeaveAllocationsQuery,
        List<LeaveAllocationDto>>
    {
        private readonly IMapper _mapper;
        private readonly ILeaveAllocationRepository _leaveAllocationRepository;
        private readonly IAppLogger<GetLeaveAllocationsQueryHandler> _logger;

        public GetLeaveAllocationsQueryHandler(IMapper mapper,
            ILeaveAllocationRepository leaveAllocationRepository,
            IAppLogger<GetLeaveAllocationsQueryHandler> logger)
        {
            this._mapper = mapper;
            this._leaveAllocationRepository = leaveAllocationRepository;
            this._logger = logger;
        }
        public async Task<List<LeaveAllocationDto>> Handle(GetLeaveAllocationsQuery request,
            CancellationToken cancellationToken)
        {
            //db query
            var leaveAllocation = await _leaveAllocationRepository
                .GetLeaveAllocationWithDetails();

            //onvert data objets to dto objets for leave alllocations
            var dtoObject = _mapper.Map<List<LeaveAllocationDto>>(leaveAllocation);

            _logger.LogInformation("The Leave Allocation was retrieved successfully.");

            return dtoObject;
        }
    }
}
