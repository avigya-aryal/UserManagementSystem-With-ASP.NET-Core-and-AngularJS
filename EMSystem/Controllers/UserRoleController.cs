using DomainLayer.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ServiceLayer.UserRoleService;
using ServiceLayer.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EMSystem.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class UserRoleController : ControllerBase
    {
        private readonly IUserRoleService _userRoleService;
        public UserRoleController(IUserRoleService userRoleService)
        {
            _userRoleService = userRoleService;
        }

        [HttpPost(nameof(InsertUserRole))]
        public async Task<IActionResult> InsertUserRole(UserRoleViewModel userRoleModel)
        {
            await _userRoleService.InsertUserRoleAsync(userRoleModel);
            return Ok("Data inserted");
        }


        [HttpGet(nameof(GetAllUserRole))]
        public async Task<IActionResult> GetAllUserRole()
        {
            var result = await _userRoleService.GellAllUserRoleAsync();
            if (result != null)
            {
                return Ok(result);
            }
            return BadRequest("No records found");
        }


        [HttpGet(nameof(GetUserRole))]
        public async Task<IActionResult> GetUserRole(int roleId)
        {
            var result = await _userRoleService.GetUserRoleAsync(roleId);
            if (result != null)
            {
                return Ok(result);
            }
            return BadRequest("No records ");
        }


        [HttpPut(nameof(UpdateUserRole))]
        public async Task<IActionResult> UpdateUserRole(UserRoleViewModel userRoleModel)
        {
            await _userRoleService.UpdateUserRoleAsync(userRoleModel);
            return Ok("Updation done");
        }


        [HttpDelete(nameof(DeleteUserRole))]
        public async Task<IActionResult> DeleteUserRole(int RoleID)
        {
            await _userRoleService.DeleteUserRoleAsync(RoleID);
            return Ok("Deletion done");
        }

        [HttpGet(nameof(GetAllUserRoleForUser))]
        public async Task<IActionResult> GetAllUserRoleForUser()
        {
            var employee = await _userRoleService.GetAllUserRoleForUserAsync();
            return Ok(employee);
        }
    }
}
