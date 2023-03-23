using HR.LeaveManagement.Domain;
using HR.LeaveManagement.Persistence.DatabaseContext;
using Microsoft.EntityFrameworkCore;
using Shouldly;

namespace HR.LeaveManagement.Application_IntegrationTest
{
    public class HrDatabaseContextTest
    {
        private HrDatabaseContext _hrDatabaseContext;

        public HrDatabaseContextTest()
        {
            var dbOption = new DbContextOptionsBuilder<HrDatabaseContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString()).Options;

            _hrDatabaseContext = new HrDatabaseContext(dbOption);
        }


        //Three (3) A's Arrange Act Assert
        [Fact]
        public async void Save_SetDateCreatedValue()
        {
            //Arrange
            var leaveTypes = new LeaveType
            {               
                Id = 1,
                DefaultDays = 10,
                Name = "Test Vacation"                
            };

            //Act
            await _hrDatabaseContext.LeaveTypes.AddAsync(leaveTypes);
            await _hrDatabaseContext.SaveChangesAsync();

            //Assert
            leaveTypes.DateCreated.ShouldNotBeNull();

        }

        [Fact]
        public async Task Save_SetDateModifiedValue()
        {
            //Arrange
            var leaveTypes = new LeaveType
            {
                Id = 1,
                DefaultDays = 10,
                Name = "Test Vacation"
            };

            //Act
            await _hrDatabaseContext.LeaveTypes.AddAsync(leaveTypes);
            await _hrDatabaseContext.SaveChangesAsync();

            //Assert
            leaveTypes.DateModified.ShouldNotBeNull();
        }
    }
}