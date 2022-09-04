using DomainLayer.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ServiceLayer.EmployeeScheduleService;
using ServiceLayer.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EMSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeScheduleController : ControllerBase
    {
        private readonly IEmployeeScheduleService employeeScheduleService;

        public EmployeeScheduleController(IEmployeeScheduleService employeeScheduleService)
        {
            this.employeeScheduleService = employeeScheduleService;
        }
        [HttpGet(nameof(GetEmployeeSchedule))]
        public async Task<IActionResult> GetEmployeeSchedule(int employeescheduleid)
        {
            var result =await employeeScheduleService.GetEmployeeScheduleAsync(employeescheduleid);
            if (result != null)
            {
                return Ok(result);
            }
            return BadRequest("No records found");
        }
        [HttpGet(nameof(GetAllEmployeeSchedule))]
        public async Task <IActionResult> GetAllEmployeeSchedule()
        {
            var result = await employeeScheduleService.GetAllEmployeeScheduleAsync();
            if (result != null)
            {
                return Ok(result);
            }
            return BadRequest("No records found");
        }

        [HttpPost(nameof(InsertEmployeeSchedule))]
        public async Task<IActionResult> InsertEmployeeSchedule(EmployeeScheduleModel employeeScheduleModel)
        {
           await employeeScheduleService.InsertEmployeeScheduleAsync(employeeScheduleModel);
            return Ok("Data Inserted");
        }

        [HttpPut(nameof(UpdateEmployeeSchedule))]
        public async Task<IActionResult> UpdateEmployeeSchedule(EmployeeScheduleModel employeeScheduleModel)
        {
            await employeeScheduleService.UpdateEmployeeScheduleAsync(employeeScheduleModel);
            return Ok("Data updated");
        }

        [HttpDelete(nameof(DeleteEmployeeSchedule))]
        public async Task<IActionResult> DeleteEmployeeSchedule(int employeescheduleid)
        {
            await employeeScheduleService.DeleteEmployeeScheduleAsync(employeescheduleid);
            return Ok("Data Deleted");
        }
    }
}
