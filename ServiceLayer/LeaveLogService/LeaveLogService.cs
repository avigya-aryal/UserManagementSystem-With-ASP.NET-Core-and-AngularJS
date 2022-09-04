using DomainLayer.Models;
using RepositoryLayer.DapperRepository;
using RepositoryLayer.Repository;
using ServiceLayer.LeaveLogService;
using ServiceLayer.ViewModel;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ServicesLayer.LeaveLogService
{
    public class LeaveLogService : ILeaveLogService
    {
        private IRepository<LeaveLog> _leavelogRepository;
        private IApplicationReadDbConnection _applicationReadDbConnection;

        public LeaveLogService(IRepository<LeaveLog> repository, IApplicationReadDbConnection applicationReadDbConnection)
        {
            _leavelogRepository = repository;
            _applicationReadDbConnection = applicationReadDbConnection;
        }

        public LeaveLogService()
        {
        }

        public async Task<bool> InsertLeaveLogAsync(LeaveLogViewModel leaveLogModel)
        {
            bool result = true;
            var leavelog = new LeaveLog();

            leavelog.StartDate = leaveLogModel.StartDate;
            leavelog.EndDate = leaveLogModel.EndDate;
            leavelog.LeaveType = leaveLogModel.LeaveType;
            leavelog.LeaveReason = leaveLogModel.LeaveReason;
            leavelog.EmployeeID = leaveLogModel.EmployeeID;
            leavelog.LeaveStatus = leaveLogModel.LeaveStatus;
            leavelog.ApprovedBy = leaveLogModel.ApprovedBy;
            leavelog.ApprovedDate = leaveLogModel.ApprovedDate;
            leavelog.CreatedTS = System.DateTime.Now;
            await _leavelogRepository.InsertAsync(leavelog);
            await _leavelogRepository.SaveChangesAsync();

            return result;
        }

        public async Task<IList<LeaveLogListViewModel>> GetAllLeaveLogAsync()
        {
            var query = @"select LeaveLogID, StartDate, EndDate, LeaveType, CONCAT(Contacts.FirstName, '', Contacts.LastName) as EmployeeName, LeaveStatus,
                            DATEDIFF(day, GETDATE(), StartDate) As NoticePeriod from LeaveLog
                            inner join Employee on LeaveLog.EmployeeID = Employee.EmployeeID 
                            inner join Contacts on Contacts.ContactID = Employee.ContactID";
            var employee = await _applicationReadDbConnection.QueryAsync<LeaveLogListViewModel>(query);
            return employee.ToList();
        }

        public async Task<LeaveLogViewModel> GetLeaveLogAsync(int leavelogId)
        {
            var result = await _leavelogRepository.FindByConditionAsync(x => x.LeaveLogID == leavelogId);
            var leavelogByID = result.Select(x => new LeaveLogViewModel
            {
                LeaveLogID = x.LeaveLogID,
                StartDate = x.StartDate,
                EndDate = x.EndDate,
                LeaveType = x.LeaveType,
                LeaveReason = x.LeaveReason,
                EmployeeID = x.EmployeeID,
                LeaveStatus = x.LeaveStatus,
                ApprovedBy = x.ApprovedBy,
                ApprovedDate = x.ApprovedDate
            }).FirstOrDefault();
            return leavelogByID;
        }

        public async Task<LeaveLogViewModel> UpdateLeaveLogAsync(LeaveLogViewModel leaveLogViewModel)
        {
            var doesLeavelogExist = (await _leavelogRepository.FindByConditionAsync(x => x.LeaveLogID == leaveLogViewModel.LeaveLogID)).FirstOrDefault();

            if(doesLeavelogExist != null)
            {
                doesLeavelogExist.StartDate = leaveLogViewModel.StartDate;
                doesLeavelogExist.EndDate = leaveLogViewModel.EndDate;
                doesLeavelogExist.LeaveType = leaveLogViewModel.LeaveType;
                doesLeavelogExist.LeaveReason = leaveLogViewModel.LeaveReason;
                doesLeavelogExist.EmployeeID = leaveLogViewModel.EmployeeID;
                doesLeavelogExist.LeaveStatus = leaveLogViewModel.LeaveStatus;
                doesLeavelogExist.ApprovedBy = leaveLogViewModel.ApprovedBy;
                doesLeavelogExist.ApprovedDate = leaveLogViewModel.ApprovedDate;
                await _leavelogRepository.UpdateAsync(doesLeavelogExist);
                await _leavelogRepository.SaveChangesAsync();
            }
            return leaveLogViewModel;
        }

        public async Task<LeaveLogViewModel> DeleteLeaveLogAsync(int id)
        {
            var leavelogResult = (await _leavelogRepository.FindByConditionAsync(x => x.LeaveLogID == id)).FirstOrDefault();

            if(leavelogResult != null)
            {
                await _leavelogRepository.DeleteAsync(leavelogResult);
                await _leavelogRepository.SaveChangesAsync();
            }
            return null;
        }

    }
}