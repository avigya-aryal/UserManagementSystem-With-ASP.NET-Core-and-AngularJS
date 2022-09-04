using DomainLayer.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RepositoryLayer;
using RepositoryLayer.Repository;
using ServiceLayer;
using ServiceLayer.EmployeeService;
using ServiceLayer.ViewModel;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace OnionArchitecture.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        private readonly IEmployeeService _employeeService;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public EmployeeController(IEmployeeService employeeService, IWebHostEnvironment webHostEnvironment)
        {
            _employeeService = employeeService;
            this._webHostEnvironment = webHostEnvironment;
        }


        [HttpPost(nameof(InsertEmployee))]
        public async Task<IActionResult> InsertEmployee(EmployeeViewModel employee)
        {
            await _employeeService.InsertEmployeeAsync(employee);
            return Ok("Data inserted");
        }


        [HttpGet(nameof(GetAllEmployee))]
        public async Task<IActionResult> GetAllEmployee()
        {
            var result = await _employeeService.GetAllEmployeesAsync();
            if (result != null)
            {
                return Ok(result);
            }
            return BadRequest("No records found");
        }


        [HttpGet(nameof(GetEmployee))]
        public async Task<IActionResult> GetEmployee(int employeeId)
        {
            var result = await _employeeService.GetEmployeeAsync(employeeId);
            if (result != null)
            {
                return Ok(result);
            }
            return BadRequest("No records found");
        }


        [HttpPut(nameof(UpdateEmployee))]
        public async Task<IActionResult> UpdateEmployee(EmployeeViewModel employeeViewModel)
        {
            await _employeeService.UpdateEmployeeAsync(employeeViewModel);

            return Ok("Updation done");
        }


        [HttpDelete(nameof(DeleteEmployee))]
        public async Task<IActionResult> DeleteEmployee(int EmployeeID)
        {
            await _employeeService.DeleteEmployeeAsync(EmployeeID);
            return Ok("Data Deleted");
        }

        [HttpGet(nameof(GetAllEmployeeName))]
        public async Task<IActionResult> GetAllEmployeeName()
        {
            var employee = await _employeeService.GetAllEmployeeNameAsync();
            return Ok(employee);
        }

        [HttpGet(nameof(GetAllEmployeeForUser))]
        public async Task<IActionResult> GetAllEmployeeForUser()
        {
            var employee = await _employeeService.GetAllEmployeeForUserAsync();
            return Ok(employee);
        }

        [HttpGet(nameof(GetAEmployeeForUpdateView))]
        public async Task<IActionResult> GetAEmployeeForUpdateView()
        {
            var employee = await _employeeService.GetAEmployeeForUpdateViewAsync();
            return Ok(employee);
        }

        [HttpPost(nameof(UploadFiles))]
        public async Task<string> UploadFiles(IFormFile file)
        {
            var contentType = file.ContentType;
            var ext = contentType.Substring(contentType.LastIndexOf('/') + 1);

            var fileName = file.FileName + "." + ext;
            var filePath = Path.Combine(_webHostEnvironment.ContentRootPath, @"wwwroot\Images", fileName);

            if (ext == "jpeg" || ext == "png")
            {
                using (FileStream fs = System.IO.File.Create(filePath))
                {
                    file.CopyTo(fs);
                    fs.Flush();
                }
            }

            var path = "/Images/" + fileName;
            return path;
        }
    }
}

