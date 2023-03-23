using AutoMapper;
using HR.LeaveManagement.Application.Contracts.Logging;
using HR.LeaveManagement.Application.Contracts.Persistence;
using HR.LeaveManagement.Application.Features.LeaveType.Queries.GetAllLeaveTypes;
using HR.LeaveManagement.Application.Features.LeaveType.Queries.GetLeaveTypeDetails;
using HR.LeaveManagement.Application.MappingProfiles;
using HR.LeaveManagement.Application.UnitTest.Mocks;
using Moq;
using Shouldly;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HR.LeaveManagement.Application.UnitTest.Features.LeaveTypes.Queries
{
    public class GetLeaeTypesDetailsQueryHandlerTests
    {
        private Mock<ILeaveTypeRepository> _mockRepo;
        private IMapper _mapper;

        public GetLeaeTypesDetailsQueryHandlerTests()
        {
            _mockRepo = MockLeaveTypeRepository.GetMockLeaveTypeRepository();

            var mapperConfig = new MapperConfiguration(c =>
            {
                c.AddProfile<LeaveTypeProfile>();
            });

            _mapper = mapperConfig.CreateMapper();
        }

        //[Theory]
        //public async task getleavetypesdetailstest(int id)
        //{

        //    var handler = new getleavetypedetailsqueryhandler(_mapper, _mockrepo.object);

        //    var result = await handler.handle(new getleavetypedetailsquery(id),
        //        cancellationtoken.none);

        //    result.shouldbeoftype<leavetypedetailsdto>();
            
        //}
    }
}
