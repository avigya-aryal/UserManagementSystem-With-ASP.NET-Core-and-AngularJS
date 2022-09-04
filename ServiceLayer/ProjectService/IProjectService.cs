using DomainLayer.Models;
using ServiceLayer.ViewModel;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ServiceLayer.ProjectService
{
    public interface IProjectService
    {
        Task<IEnumerable<Project>> GetAllProjectAsync();
        Task<ProjectViewModel> GetProjectAsync(int projectid);
        Task<bool> InsertProjectAsync(ProjectViewModel projectModel);
        Task<ProjectViewModel> UpdateProjectAsync(ProjectViewModel projectModel);
        Task<ProjectViewModel> DeleteProjectAsync(int projectid);
    }
}
