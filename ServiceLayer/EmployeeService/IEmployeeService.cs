using DomainLayer.Models;
using ServiceLayer.ViewModel;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ServiceLayer.EmployeeService
{
    public interface IEmployeeService
    {
        Task <bool> InsertEmployeeAsync(EmployeeViewModel employeeModel);

        Task<IList<EmployeeListViewModel>> GetAllEmployeesAsync();

        Task<EmployeeViewModel> GetEmployeeAsync(int employeeId);

        Task<EmployeeViewModel> UpdateEmployeeAsync(EmployeeViewModel employeeViewModel);

        Task<EmployeeViewModel> DeleteEmployeeAsync(int id);

        Task<IList<SelectItemIntViewModel>> GetAllEmployeeNameAsync();

        Task<IList<SelectItemIntViewModel>> GetAllEmployeeForUserAsync();

        Task<IList<SelectItemIntViewModel>> GetAEmployeeForUpdateViewAsync();
    }
}