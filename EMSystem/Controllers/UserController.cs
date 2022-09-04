using DomainLayer.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ServiceLayer.UserService;
using ServiceLayer.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EMSystem.Controllers
{
    //[Authorize(Policy = Policies.Admin)]
    //[Authorize(Roles = "Admin")]
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        //CONSTRUCTOR
        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        //INSERT ACTION
        [HttpPost(nameof(InsertUser))]
        public async Task<IActionResult> InsertUser(UserViewModel userModel)
        {
            await _userService.InsertUserAsync(userModel);
            return Ok("Data inserted");
        }

        //GET ALL ACTION
      
        [HttpGet(nameof(GetAllUser))]
        //[Authorize(Policy = Policies.Admin)]
        //[Authorize (Roles = "Admin")]
        public async Task<IActionResult> GetAllUser()
        {
            var result = await _userService.GetAllUserAsync();
            if (result != null)
            {
                return Ok(result);
            }
            return BadRequest("No records found");
        }

        //GET USER ACTION
        [HttpGet(nameof(GetUser))]
        public async Task<IActionResult> GetUser(int userId)
        {
            var result = await _userService.GetUserAsync(userId);
            if (result != null)
            {
                return Ok(result);
            }
            return BadRequest("No records ");
        }

        //UPDATE USER ACTION
        [HttpPut(nameof(UpdateUser))]
        public async Task<IActionResult> UpdateUser(UserViewModel userModel)
        {
            await _userService.UpdateUserAsync(userModel);
            return Ok("Updation done");
        }

        //DELETE USER ACTION
        [HttpDelete(nameof(DeleteUser))]
        public async Task<IActionResult> DeleteUser(int UserID)
        {
            await _userService.DeleteUserAsync(UserID);
            return Ok("Deletion done");
        }

        [HttpGet(nameof(GetAllManager))]
        public async Task<IActionResult> GetAllManager()
        {
            var manager = await _userService.GetAllManagerAsync();
            return Ok(manager);
        }

        //[HttpGet(nameof(GetAllUserName))]
        //public async Task<IActionResult> GetAllUserName()
        //{
        //    var user = await _userService.GetAllUserNameAsync();
        //    return Ok(user);
        //}
    }
}
