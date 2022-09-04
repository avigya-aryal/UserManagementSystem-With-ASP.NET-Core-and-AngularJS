using DomainLayer.Models;
using RepositoryLayer.Repository;
using ServiceLayer.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceLayer.ProjectService
{
    public class ProjectService:IProjectService
    {
        private readonly IRepository<Project> projectrepo;

        public ProjectService(IRepository<Project> projectrepo)
        {
            this.projectrepo = projectrepo;
        }

        public async Task<ProjectViewModel> DeleteProjectAsync(int projectid)
        {
            var ProjectExists = (await projectrepo.FindByConditionAsync(x => x.ProjectId == projectid)).FirstOrDefault();
            if (ProjectExists != null)
            {
                await projectrepo.DeleteAsync(ProjectExists);
                await projectrepo.SaveChangesAsync();
            }
            return null;
        }

       

        public async Task <IEnumerable<Project>> GetAllProjectAsync()
        {
            return await projectrepo.FindAllAsync();
        }

     

        public async Task<ProjectViewModel> GetProjectAsync(int projectid)
        {


            var result = await projectrepo.FindByConditionAsync(x => x.ProjectId == projectid);
            var project = result.Select(x => new ProjectViewModel
            {
                ProjectId = x.ProjectId,
                ProjectName = x.ProjectName,
                Project_Manager=x.Project_Manager,
                Description=x.Description,
                StartDate=x.StartDate,
                EndDate=x.EndDate,
                Status=x.Status

            })

                .FirstOrDefault();
            return project;

        }

        public async Task<bool> InsertProjectAsync(ProjectViewModel projectModel)
        {
            bool result = false;
            var project = new Project();
            var isprojectExist = (await projectrepo.FindByConditionAsync(x => x.ProjectName == projectModel.ProjectName)).Any();
            if (isprojectExist)
            {
                result = true;
            }
            else
            {
            project.ProjectName = projectModel.ProjectName;
                project.Project_Manager= projectModel.Project_Manager;
                project.Description = projectModel.Description;
                project.StartDate = projectModel.StartDate;
                project.EndDate = projectModel.EndDate;
                project.Status = projectModel.Status;
                project.CreatedTS = DateTime.Now;

            await projectrepo.InsertAsync(project);
            await projectrepo.SaveChangesAsync();
            result = true;
        }
            return result;
        }

        public async Task<ProjectViewModel> UpdateProjectAsync(ProjectViewModel projectModel)
        {
            var ProjectExists = (await projectrepo.FindByConditionAsync(x => x.ProjectId == projectModel.ProjectId)).FirstOrDefault();
            if (ProjectExists != null)
            {
                ProjectExists.ProjectName = projectModel.ProjectName;
                ProjectExists.Project_Manager = projectModel.Project_Manager;
                ProjectExists.Description = projectModel.Description;
                ProjectExists.StartDate = projectModel.StartDate;
                ProjectExists.EndDate = projectModel.EndDate;
                ProjectExists.Status = projectModel.Status;
                ProjectExists.ModifiedTS = DateTime.Now;
               
                await projectrepo.UpdateAsync(ProjectExists);
                await projectrepo.SaveChangesAsync();
            }
            return projectModel;

        }

       
    }
}
