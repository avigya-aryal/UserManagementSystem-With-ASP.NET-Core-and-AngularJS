using DomainLayer.Models;
using Microsoft.AspNetCore.Mvc;
using ServiceLayer.EmployeeService;
using ServiceLayer.LeaveLogService;
using ServiceLayer.ViewModel;
using System.Threading.Tasks;

namespace OnionArchitecture.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LeaveLogController : ControllerBase
    {
        private readonly ILeaveLogService _leavelogService;
        public LeaveLogController(ILeaveLogService leaveLogService)
        {
            _leavelogService = leaveLogService;
        }

        [HttpPost(nameof(InsertLeaveLog))]
        public async Task<IActionResult> InsertLeaveLog(LeaveLogViewModel leavelog)
        {
            await _leavelogService.InsertLeaveLogAsync(leavelog);
            return Ok("Data inserted");
        }

        [HttpGet(nameof(GetAllLeaveLog))]
        public async Task<IActionResult> GetAllLeaveLog()
        {
            var result = await _leavelogService.GetAllLeaveLogAsync();
            if (result != null)
            {
                return Ok(result);
            }
            return BadRequest("No records found");
        }


        [HttpGet(nameof(GetLeaveLog))]
        public async Task<IActionResult> GetLeaveLog(int leaveId)
        {
            var result = await _leavelogService.GetLeaveLogAsync(leaveId);
            if (result != null)
            {
                return Ok(result);
            }
            return BadRequest("No records found");
        }


        [HttpPut(nameof(UpdateLeaveLog))]
        public async Task<IActionResult> UpdateLeaveLog(LeaveLogViewModel leaveLogViewModel)
        {
            await _leavelogService.UpdateLeaveLogAsync(leaveLogViewModel);
            return Ok("Updation done");
        }


        [HttpDelete(nameof(DeleteLeaveLog))]
        public async Task<IActionResult> DeleteLeaveLog(int LeaveLogID)
        {
            await _leavelogService.DeleteLeaveLogAsync(LeaveLogID);
            return Ok("Data Deleted");
        }
    }
}