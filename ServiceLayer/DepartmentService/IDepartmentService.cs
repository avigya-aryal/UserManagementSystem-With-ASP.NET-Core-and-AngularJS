using DomainLayer.Models;

using RepositoryLayer.ViewModel;
using ServiceLayer.ViewModel;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ServiceLayer.DepartmentService
{
    public interface IDepartmentService
    {

        Task<IEnumerable<Department>> GellAllDepartmentAsync();
        Task<DepartmentViewModel> GetDepartmentAsync(int departmentId);
        Task<bool> InsertDepartmentAsync(DepartmentViewModel departmentmodel);
        Task<DepartmentViewModel> UpdateDepartmentAsync(DepartmentViewModel departmentmodel);
        Task<DepartmentViewModel> DeleteDepartmentAsync(int DepartmentID);
    }
}
