using MediatR;

namespace HR.LeaveManagement.Application.Features.LeaveRequest.Queries.GetLeaveRequestDetails
{
    public class LeaveRequestDetailsQuery : IRequest<LeaveRequestDetailsDto>
    {
        public int Id { get; set; }
    }


}
