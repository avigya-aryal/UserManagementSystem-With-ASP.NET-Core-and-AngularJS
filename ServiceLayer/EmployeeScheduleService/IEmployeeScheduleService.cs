using DomainLayer.Models;
using ServiceLayer.ViewModel;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ServiceLayer.EmployeeScheduleService
{
   public interface IEmployeeScheduleService
    {
        Task<IEnumerable<EmployeeScheduleModel>> GetAllEmployeeScheduleAsync();
        Task<EmployeeScheduleModel> GetEmployeeScheduleAsync(int employeescheduleid);
        Task<bool> InsertEmployeeScheduleAsync(EmployeeScheduleModel employeeScheduleModel);
        Task<EmployeeScheduleModel> UpdateEmployeeScheduleAsync(EmployeeScheduleModel employeeScheduleModel);
        Task<EmployeeScheduleModel> DeleteEmployeeScheduleAsync(int employeescheduleid);
    }
}
