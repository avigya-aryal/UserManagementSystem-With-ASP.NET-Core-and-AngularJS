using DomainLayer.Models;
using RepositoryLayer.DapperRepository;
using RepositoryLayer.Repository;
using ServiceLayer.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
//using Microsoft.AspNetCore.Mvc;


namespace ServiceLayer.UserService
{
    public class UserService : IUserService
    {
        private IRepository<User> _userRepository;
        private IApplicationReadDbConnection _applicationReadDbConnection;
     

        public UserService(IRepository<User> repository, IApplicationReadDbConnection applicationReadDbConnection)
        {
            _userRepository = repository;
            _applicationReadDbConnection = applicationReadDbConnection;
        }

        public async Task<bool> InsertUserAsync(UserViewModel userModel)
        {
            bool result = false;
            var user = new User();

            var isUserExist = (await _userRepository.FindByConditionAsync(x => x.EmployeeID == userModel.EmployeeID)).Any();
            if (isUserExist) 
            {
                result = true;
            }
            else
            {
                user.EmployeeID = userModel.EmployeeID;
                user.Role = userModel.Role;
                user.Status = userModel.Status;
                user.CreatedTS = DateTime.Now;
                await _userRepository.InsertAsync(user);
                await _userRepository.SaveChangesAsync();
                result = true;
            }
            return result;
        }


        //public async Task<IEnumerable<User>> GetAllUserAsync()
        //{
        //    return await _userRepository.FindAllAsync();
        //}

        public async Task<IList<UserListViewModel>> GetAllUserAsync()
        {
            var query = @"select [User].UserID, CONCAT(Contacts.FirstName, ' ' ,Contacts.MiddleName, ' ', Contacts.LastName)AS FullName, [User].Role, [User].Status
                            FROM Employee
                            INNER JOIN[User] ON[User].EmployeeID = Employee.EmployeeID
                            INNER JOIN Contacts ON Employee.ContactID = Contacts.ContactID";
            var user = await _applicationReadDbConnection.QueryAsync<UserListViewModel>(query);
            return user.ToList();
        }


        public async Task<UserViewModel> GetUserAsync(int userId)
        {
            var result = await _userRepository.FindByConditionAsync(x => x.UserID == userId);
            var userById = result.Select(x => new UserViewModel {
                UserID = x.UserID,
                EmployeeID = x.EmployeeID,
                Role = x.Role,
                Status = x.Status
         
            }).FirstOrDefault();
            
            return userById;
        }
        

        public async Task<UserViewModel> UpdateUserAsync(UserViewModel userModel)
        {

            var isUserExist = (await _userRepository.FindByConditionAsync(x => x.EmployeeID == userModel.EmployeeID)).FirstOrDefault();
            if (isUserExist != null)
            {
                isUserExist.EmployeeID = userModel.EmployeeID;
                isUserExist.Role = userModel.Role;
                isUserExist.Status = userModel.Status;
                isUserExist.ModifiedTS = DateTime.Now;
                await _userRepository.UpdateAsync(isUserExist);
                await _userRepository.SaveChangesAsync();
            }
            return userModel;
        }


        public async Task<UserViewModel> DeleteUserAsync(int id)
        {   
            var user = (await _userRepository.FindByConditionAsync(x => x.UserID == id)).FirstOrDefault();
            if (user != null)
            {
                await _userRepository.DeleteAsync(user);
                await _userRepository.SaveChangesAsync();
            }
            return null;
        }

        public async Task<IList<SelectItemIntViewModel>> GetAllManagerAsync()
        {
            var query = @"select CONCAT(Contacts.FirstName, ' ' ,Contacts.MiddleName, ' ', Contacts.LastName) AS Name 
                            FROM Employee
                            INNER JOIN [User] ON [User].EmployeeID = Employee.EmployeeID
                            INNER JOIN Contacts ON Employee.ContactID = Contacts.ContactID
                            where [User].Role = 'Manager' ";
            var usermanager = await _applicationReadDbConnection.QueryAsync<SelectItemIntViewModel>(query);
            return usermanager.ToList();
        }
    }
}
