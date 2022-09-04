using DomainLayer.Models;
using RepositoryLayer.DapperRepository;
using RepositoryLayer.Repository;
using ServiceLayer.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ServiceLayer.EmployeeService
{
    public class EmployeeService : IEmployeeService
    {
        private IRepository<Employee> _employeeRepository;
        private IApplicationReadDbConnection _applicationReadDbConnection;

        public EmployeeService(IRepository<Employee> repository, IApplicationReadDbConnection applicationReadDbConnection)
        {
            _employeeRepository = repository;
            _applicationReadDbConnection = applicationReadDbConnection;
        }

        public async Task<bool> InsertEmployeeAsync(EmployeeViewModel employeeModel)
        {
            bool result = false;
            var employee = new Employee();

            var isEmployeeExist = (await _employeeRepository.FindByConditionAsync(x => x.ContactID == employeeModel.ContactID)).Any();
            if (isEmployeeExist)
            {
                result = true;
            }
            else
            {
                employee.Designation = employeeModel.Designation;
                employee.Status = employeeModel.Status;
                employee.ContactID = employeeModel.ContactID;
                employee.CreatedTS = DateTime.Now;
                employee.JoinedDate = employeeModel.JoinedDate;

                await _employeeRepository.InsertAsync(employee);
                await _employeeRepository.SaveChangesAsync();
                result = true;
            }
            return result;
        }

        //without dapper
        /* public async Task<IEnumerable<Employee>> GetAllEmployeesAsync()
        {
            return await _employeeRepository.FindAllAsync();
        } */


        //using dapper
        public async Task<IList<EmployeeListViewModel>> GetAllEmployeesAsync()
        {
            var query = @"select EmployeeID, CONCAT(Contacts.FirstName, ' ', Contacts.LastName) as EmployeeName, Designation,
                           Status, JoinedDate from Employee
                          inner join Contacts on Contacts.ContactID=Employee.ContactID";
            var employee = await _applicationReadDbConnection.QueryAsync<EmployeeListViewModel>(query);
            return employee.ToList();
        }


        public async Task<EmployeeViewModel> GetEmployeeAsync(int employeeId)
        {

            var result = await _employeeRepository.FindByConditionAsync(x => x.EmployeeID == employeeId);
            var employeeByID = result.Select(x => new EmployeeViewModel {
                EmployeeID = x.EmployeeID,
                Designation = x.Designation,
                Status = x.Status,
                ContactID = x.ContactID,
                JoinedDate = x.JoinedDate,
                LeftDate = x.LeftDate,
                CitizenshipImage = x.CitizenshipImage,
                ImageOwnerUniqueName = x.ImageOwnerUniqueName
            }).FirstOrDefault();
            return employeeByID;
            //select * from Employee where contactID == employeeID;
            //return employeeModel
        }

        public async Task<EmployeeViewModel> UpdateEmployeeAsync(EmployeeViewModel employeeViewModel)
        {
          
            var doesEmployeeExist = (await _employeeRepository.FindByConditionAsync(x => x.EmployeeID == employeeViewModel.EmployeeID)).FirstOrDefault();

            if (doesEmployeeExist != null)
            {
                doesEmployeeExist.Designation = employeeViewModel.Designation;
                doesEmployeeExist.Status = employeeViewModel.Status;
                doesEmployeeExist.ContactID = employeeViewModel.ContactID;
                doesEmployeeExist.JoinedDate = employeeViewModel.JoinedDate;
                doesEmployeeExist.LeftDate = employeeViewModel.LeftDate;
                doesEmployeeExist.CitizenshipImage = employeeViewModel.CitizenshipImage;
                doesEmployeeExist.ImageOwnerUniqueName = employeeViewModel.ImageOwnerUniqueName;
                await _employeeRepository.UpdateAsync(doesEmployeeExist);
                await _employeeRepository.SaveChangesAsync();
            }
            
            return employeeViewModel;
        }


        public async Task<EmployeeViewModel> DeleteEmployeeAsync(int id)
        {
            var employeeResullt = (await _employeeRepository.FindByConditionAsync(x => x.EmployeeID == id)).FirstOrDefault();

            if (employeeResullt != null)
            {
                await _employeeRepository.DeleteAsync(employeeResullt);
                await _employeeRepository.SaveChangesAsync();
            }
            return null;
        }

        public async Task<IList<SelectItemIntViewModel>> GetAllEmployeeNameAsync()
        {
            var query = @"select Employee.EmployeeID AS ID, CONCAT(Contacts.FirstName, ' ' , Contacts.LastName) AS Name 
                            FROM Contacts
                            left join Employee on Contacts.ContactID = Employee.ContactID where Employee.ContactID is not null";
            var user = await _applicationReadDbConnection.QueryAsync<SelectItemIntViewModel>(query);
            return user.ToList();
        }

        public async Task<IList<SelectItemIntViewModel>> GetAllEmployeeForUserAsync()
        {
            var query = @"select Employee.EmployeeID AS ID, CONCAT(Contacts.FirstName, ' ' ,Contacts.MiddleName, ' ', Contacts.LastName) AS Name 
                            FROM Employee
                            LEFT JOIN [User] ON [User].EmployeeID = Employee.EmployeeID
                            LEFT JOIN Contacts ON Employee.ContactID = Contacts.ContactID
                            where [User].UserID is null";
            var user = await _applicationReadDbConnection.QueryAsync<SelectItemIntViewModel>(query);
            return user.ToList();
        }

        public async Task<IList<SelectItemIntViewModel>> GetAEmployeeForUpdateViewAsync()
        {
            var query = @"select Employee.EmployeeID AS ID, CONCAT(Contacts.FirstName, ' ' ,Contacts.MiddleName, ' ', Contacts.LastName) AS Name 
                            FROM Employee
                            LEFT JOIN [User] ON [User].EmployeeID = Employee.EmployeeID
                            LEFT JOIN Contacts ON Employee.ContactID = Contacts.ContactID
                            where [User].UserID is not null";
            var user = await _applicationReadDbConnection.QueryAsync<SelectItemIntViewModel>(query);
            return user.ToList();
        }
    }
}