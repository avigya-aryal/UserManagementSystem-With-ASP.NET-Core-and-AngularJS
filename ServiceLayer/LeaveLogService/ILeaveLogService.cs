using DomainLayer.Models;
using ServiceLayer.ViewModel;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ServiceLayer.LeaveLogService
{
    public interface ILeaveLogService
    {
        Task<bool> InsertLeaveLogAsync(LeaveLogViewModel leaveLogModel);

        //Task<IEnumerable<LeaveLog>> GellAllLeaveLogAsync();
        Task<IList<LeaveLogListViewModel>> GetAllLeaveLogAsync();

        Task<LeaveLogViewModel> GetLeaveLogAsync(int leaveId);

        Task<LeaveLogViewModel> UpdateLeaveLogAsync(LeaveLogViewModel leaveLogViewModel);

        Task<LeaveLogViewModel> DeleteLeaveLogAsync(int id);

    }
}