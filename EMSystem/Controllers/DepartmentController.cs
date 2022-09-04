using DomainLayer.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ServiceLayer;
using ServiceLayer.DepartmentService;
using ServiceLayer.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace sample.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DepartmentController : ControllerBase
    {
        private readonly IDepartmentService _departmentService;

        public DepartmentController(IDepartmentService departmentService)
        {
            _departmentService = departmentService;
        }


        [HttpPost(nameof(InsertDepartment))]
        public async Task<IActionResult> InsertDepartment(DepartmentViewModel departmentmodel)
        {
            await _departmentService.InsertDepartmentAsync(departmentmodel);
            return Ok("Data Inserted");
        }

        [HttpGet(nameof(GetAllDepartment))]
        public async Task<IActionResult> GetAllDepartment()
        {
            var result = await _departmentService.GellAllDepartmentAsync();
            if (result != null)
            {
                return Ok(result);
            }
            return BadRequest("No records found");
        }


        [HttpGet(nameof(GetDepartment))]
        public async Task<IActionResult> GetDepartment(int departmentId)
        {
            var result = await _departmentService.GetDepartmentAsync(departmentId);
            if (result != null)
            {
                return Ok(result);
            }
            return BadRequest("No records found");
        }




        [HttpPut(nameof(UpdateDepartment))]
        public async Task<IActionResult> UpdateDepartment(DepartmentViewModel department)
        {
            await _departmentService.UpdateDepartmentAsync(department);
            return Ok("Updation Done");
        }

        [HttpDelete(nameof(DeleteDepartment))]
        public async Task<IActionResult> DeleteDepartment(int DepartmentID)
        {
            await _departmentService.DeleteDepartmentAsync(DepartmentID);
            return Ok("Data deleted");
        }

    }
}

