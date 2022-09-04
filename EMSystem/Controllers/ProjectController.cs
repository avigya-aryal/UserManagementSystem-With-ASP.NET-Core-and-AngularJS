using DomainLayer.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ServiceLayer.ProjectService;
using ServiceLayer.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EMSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProjectController : ControllerBase
    {
        private readonly IProjectService projectService;

        public ProjectController(IProjectService projectService)
        {
            this.projectService = projectService;
        }

        [HttpGet(nameof(GetAllProject))]
        public async Task<IActionResult> GetAllProject()
        {
            var result = await projectService.GetAllProjectAsync();
            if (result != null)
            {
                return Ok(result);
            }
            return BadRequest("No records found");
        }

        [HttpGet(nameof(GetProject))]
        public async Task<IActionResult> GetProject(int projectid)
        {
            var result =await projectService.GetProjectAsync(projectid);
            if (result != null)
            {
                return Ok(result);
            }
            return BadRequest("No records found");
        }

        [HttpPost(nameof(InsertProject))]
        public async Task<IActionResult> InsertProject(ProjectViewModel projectModel)
        {
            await projectService.InsertProjectAsync(projectModel);
            return Ok("Data Inserted");
        }

        [HttpPut(nameof(UpdateProject))]
        public async Task<IActionResult> UpdateProject(ProjectViewModel projectModel)
        {
            await projectService.UpdateProjectAsync(projectModel);
            return Ok("Data Updated");
        }

        [HttpDelete(nameof(DeleteProject))]
        public async Task<IActionResult> DeleteProject(int projectid)
        {
           await projectService.DeleteProjectAsync(projectid);
            return Ok("Data Deleted");
        }

      
    }
}
