using AutoMapper;
using HR.LeaveManagement.Application.Contracts.Logging;
using HR.LeaveManagement.Application.Contracts.Persistence;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HR.LeaveManagement.Application.Features.LeaveType.Queries.GetAllLeaveTypes
{
    public class GetLeaveTypesQueryHandler : IRequestHandler<GetLeaveTypesQuery,
        List<LeaveTypeDto>>
    {
        private readonly IMapper _mapper;
        private readonly ILeaveTypeRepository _leaveTypeRepository;
        private readonly IAppLogger<GetLeaveTypesQueryHandler> _logger;

        public GetLeaveTypesQueryHandler(IMapper mapper,
            ILeaveTypeRepository leaveTypeRepository,
            IAppLogger<GetLeaveTypesQueryHandler> logger)
        {
            this._mapper = mapper;
            this._leaveTypeRepository = leaveTypeRepository;
            this._logger = logger;
        }
        public async Task<List<LeaveTypeDto>> Handle(GetLeaveTypesQuery request,
            CancellationToken cancellationToken)
        {
            //db query
            var leaveTypes = await _leaveTypeRepository.GetAsync();

            //onvert data objets to dto objets
            var dtoObject = _mapper.Map<List<LeaveTypeDto>>(leaveTypes);

            _logger.LogInformation("This Leave Type was retrieved successfully.");
            //dtoObject List
            return dtoObject;
        }
    }
}
