﻿using HR.LeaveManagement.Application.Contracts.Persistence;
using HR.LeaveManagement.Domain;
using HR.LeaveManagement.Persistence.DatabaseContext;
using Microsoft.EntityFrameworkCore;

namespace HR.LeaveManagement.Persistence.Repositories
{
    public class LeaveAllocationRepository : GenericRepository<LeaveAllocation>, ILeaveAllocationRepository
    {
        public LeaveAllocationRepository(HrDatabaseContext context) : base(context)
        {
        }

        public Task AddAllocations(List<LeaveAllocation> allocations)
        {
            return _context.AddRangeAsync(allocations);
        }

        public async Task<bool> AllocationExists(string userId, int leaveTypeId, int period)
        {
            return await _context.LeaveAllocations.AnyAsync(q => q.EmployeeId == userId
            && q.LeaveTypeId == leaveTypeId
            && q.Period == period);
        }

        public async Task<LeaveAllocation> GetLeaveAllocationWithDetails(int id)
        {
            var leaveAllocation = await _context.LeaveAllocations
                .Include(q => q.LeaveType)
                .FirstOrDefaultAsync(q => q.Id == id);
            
            return leaveAllocation;
        }

        public async Task<List<LeaveAllocation>> GetLeaveAllocationWithDetails()
        {
            var leaveAllocation = await _context.LeaveAllocations
                .Include(q => q.LeaveType)
                .ToListAsync();
            
            return leaveAllocation;
        }

        public Task<List<LeaveAllocation>> GetLeaveAllocationWithDetails(string userId)
        {
            var leaveAllocation = _context.LeaveAllocations
                .Where(q => q.EmployeeId == userId)
                .Include(q => q.LeaveType)
                .ToListAsync();

            return leaveAllocation;
        }

        public async Task<LeaveAllocation> GetUserAllocations(string userId, int leaveTypeId)
        {
            var userAllocation = await _context.LeaveAllocations
                .FirstOrDefaultAsync(q => q.EmployeeId == userId
                && q.LeaveTypeId == leaveTypeId);

            return userAllocation;
        }
    }
}
