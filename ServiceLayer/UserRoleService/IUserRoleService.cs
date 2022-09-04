using DomainLayer.Models;
using ServiceLayer.ViewModel;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ServiceLayer.UserRoleService
{
    public interface IUserRoleService
    {
        Task<bool> InsertUserRoleAsync(UserRoleViewModel userRoleModel);

        Task<IEnumerable<UserRole>> GellAllUserRoleAsync();

        Task<UserRoleViewModel> GetUserRoleAsync(int roleId);

        Task<UserRoleViewModel> UpdateUserRoleAsync(UserRoleViewModel userRoleModel);

        Task<UserRoleViewModel> DeleteUserRoleAsync(int id);

        Task<IList<SelectItemIntViewModel>> GetAllUserRoleForUserAsync();
    }
}
