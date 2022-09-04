using DomainLayer.Models;
using RepositoryLayer.DapperRepository;
using RepositoryLayer.Repository;
using ServiceLayer.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceLayer.UserRoleService
{
    public class UserRoleService : IUserRoleService
    {
        private IRepository<UserRole> _userRoleRepository;
        private IApplicationReadDbConnection _applicationReadDbConnection;

        public UserRoleService(IRepository<UserRole> repository, IApplicationReadDbConnection applicationReadDbConnection)
        {
            _userRoleRepository = repository;
            _applicationReadDbConnection = applicationReadDbConnection;
        }

        public async Task<bool> InsertUserRoleAsync(UserRoleViewModel userRoleModel)
        {
            bool result = false;
            var userRole = new UserRole();

            var isUserRoleExist = (await _userRoleRepository.FindByConditionAsync(x => x.RoleType == userRoleModel.RoleType)).Any();
            if (isUserRoleExist)
            {
                result = true;
            }
            else
            {
                userRole.RoleType = userRoleModel.RoleType;
                userRole.CreatedTS = DateTime.Now;
                await _userRoleRepository.InsertAsync(userRole);
                await _userRoleRepository.SaveChangesAsync();
                result = true;
            }
            return result;
        }

        public async Task<IEnumerable<UserRole>> GellAllUserRoleAsync()
        {
            return await _userRoleRepository.FindAllAsync();
        }

        public async Task<UserRoleViewModel> GetUserRoleAsync(int roleId)
        {
            var result = await _userRoleRepository.FindByConditionAsync(x => x.RoleID == roleId);
            var userRoleById = result.Select(x => new UserRoleViewModel
            {
                RoleID = x.RoleID,
                RoleType = x.RoleType
            }).FirstOrDefault();

            return userRoleById;
        }


        public async Task<UserRoleViewModel> UpdateUserRoleAsync(UserRoleViewModel userRoleModel)
        {

            var isUserRoleExist = (await _userRoleRepository.FindByConditionAsync(x => x.RoleID == userRoleModel.RoleID)).FirstOrDefault();
            if (isUserRoleExist != null)
            {
                isUserRoleExist.RoleType = userRoleModel.RoleType;
                //isUserRoleExist.ModifiedTS = DateTime.Now;
                await _userRoleRepository.UpdateAsync(isUserRoleExist);
                await _userRoleRepository.SaveChangesAsync();
            }
            return userRoleModel;
        }


        public async Task<UserRoleViewModel> DeleteUserRoleAsync(int id)
        {
            var user = (await _userRoleRepository.FindByConditionAsync(x => x.RoleID == id)).FirstOrDefault();
            if (user != null)
            {
                await _userRoleRepository.DeleteAsync(user);
                await _userRoleRepository.SaveChangesAsync();
            }
            return null;
        }

        public async Task<IList<SelectItemIntViewModel>> GetAllUserRoleForUserAsync()
        {
            var query = @"select RoleType as Name from UserRole";
            var user = await _applicationReadDbConnection.QueryAsync<SelectItemIntViewModel>(query);
            return user.ToList();
        }
    }
}

