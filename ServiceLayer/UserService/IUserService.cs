using DomainLayer.Models;
using ServiceLayer.ViewModel;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ServiceLayer.UserService
{
    public interface IUserService
    {
        Task<bool> InsertUserAsync(UserViewModel userModel);

        Task<IList<UserListViewModel>> GetAllUserAsync();

        Task<UserViewModel> GetUserAsync(int userId);

        Task<UserViewModel> UpdateUserAsync(UserViewModel userModel);

        Task<UserViewModel> DeleteUserAsync(int id);

        Task<IList<SelectItemIntViewModel>> GetAllManagerAsync();
    }
}