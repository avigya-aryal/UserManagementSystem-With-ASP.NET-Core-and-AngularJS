using RepositoryLayer;
using System;
using RepositoryLayer.ViewModel;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using DomainLayer.Models;
using System.Threading.Tasks;
using RepositoryLayer.Repository;
using ServiceLayer.ViewModel;

namespace ServiceLayer.DepartmentService
{
    public class DepartmentService : IDepartmentService
    {
        private readonly IRepository<Department> _departmentRepository;

        public DepartmentService(IRepository<Department> repository)
        {
            _departmentRepository = repository;
        }

        public async Task<bool> InsertDepartmentAsync(DepartmentViewModel departmentmodel)
        {
            bool result = false;
            var department = new Department();

            var isDepartmentIDExist = (await _departmentRepository.FindByConditionAsync(x => x.DepartmentID == departmentmodel.DepartmentID)).Any();
            if (isDepartmentIDExist)
            {
                result = true;
            }
            else
            {
                department.DepartmentName = departmentmodel.DepartmentName;
                department.CreatedTS = DateTime.Now;
                await _departmentRepository.InsertAsync(department);
                await _departmentRepository.SaveChangesAsync();
                result = true;
            }


            return result;
        }

            public async Task<IEnumerable<Department>> GellAllDepartmentAsync()
            {
                return await _departmentRepository.FindAllAsync();
            }

            public async Task<DepartmentViewModel> GetDepartmentAsync(int departmentID)
           {
            var result = await _departmentRepository.FindByConditionAsync(x => x.DepartmentID == departmentID);
            var department = result.Select(x => new DepartmentViewModel
            {
                DepartmentID = x.DepartmentID,
                DepartmentName = x.DepartmentName
                //CreatedTS = x.CreatedTS,
                //CreatedBy = x.CreatedBy,

            }).FirstOrDefault();

            //null dat
            //null case handle
            return department;
            
        }

        public async Task<DepartmentViewModel> UpdateDepartmentAsync(DepartmentViewModel departmentmodel)
        {

            var doesDepartmentExist = (await _departmentRepository.FindByConditionAsync(x => x.DepartmentID == departmentmodel.DepartmentID)).FirstOrDefault();
            if (doesDepartmentExist != null)
            {
                doesDepartmentExist.DepartmentID = departmentmodel.DepartmentID;
                doesDepartmentExist.DepartmentName = departmentmodel.DepartmentName;
                
                await _departmentRepository.UpdateAsync(doesDepartmentExist);
                await _departmentRepository.SaveChangesAsync();
            }
            return departmentmodel;
        }
        public async Task<DepartmentViewModel> DeleteDepartmentAsync(int departmentId)
        {
            var isDepartmentExist = (await _departmentRepository.FindByConditionAsync(x => x.DepartmentID == departmentId)).FirstOrDefault();
            if (isDepartmentExist != null)
            {
                await _departmentRepository.DeleteAsync(isDepartmentExist);
                await _departmentRepository.SaveChangesAsync();
            }
            return null;
        }

      
    }
}
