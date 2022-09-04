using DomainLayer.Models;
using RepositoryLayer.DapperRepository;
using RepositoryLayer.Repository;
using ServiceLayer.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceLayer.EmployeeScheduleService
{
   public class EmployeeScheduleService:IEmployeeScheduleService
    {
        private readonly IRepository<EmployeeSchedule> employeeSchedulerepo;
        private readonly IApplicationReadDbConnection applicationReadDbConnection;

        public EmployeeScheduleService(IRepository<EmployeeSchedule> employeeSchedulerepo,IApplicationReadDbConnection applicationReadDbConnection)
        {
            this.employeeSchedulerepo = employeeSchedulerepo;
            this.applicationReadDbConnection = applicationReadDbConnection;
        }

     

        public async Task< IEnumerable<EmployeeScheduleModel>> GetAllEmployeeScheduleAsync()
        {
            var query = $"SELECT ScheduleID, StartHour, EndHour from EmployeeSchedule";
            var schedule = await applicationReadDbConnection.QueryAsync<EmployeeScheduleModel>(query);
            return schedule;
        }

        public async Task<EmployeeScheduleModel> GetEmployeeScheduleAsync(int employeescheduleid)
        {


            var result = await employeeSchedulerepo.FindByConditionAsync(x => x.ScheduleId == employeescheduleid);
            var employeeSchedule = result.Select(x => new EmployeeScheduleModel
            {
                ScheduleId = x.ScheduleId,
                StartHour = x.StartHour,
                EndHour = x.EndHour,
              
            })

                .FirstOrDefault();
            return employeeSchedule;
           
        }

        public async Task<bool> InsertEmployeeScheduleAsync(EmployeeScheduleModel employeeScheduleModel)
        {
            bool result = false;
            var employeeSchedule = new EmployeeSchedule();
            var isemployeeScheduleExist = (await employeeSchedulerepo.FindByConditionAsync(x => x.StartHour == employeeScheduleModel.StartHour)).Any();
            if (isemployeeScheduleExist)
            {
                result = true;
            }
            else
            {
                employeeSchedule.StartHour = employeeScheduleModel.StartHour;
            employeeSchedule.EndHour = employeeScheduleModel.EndHour;
            employeeSchedule.CreatedTS = DateTime.Now;
          
            await employeeSchedulerepo.InsertAsync(employeeSchedule);
            await employeeSchedulerepo.SaveChangesAsync();
            result = true;
        }
            return result;
        }

        public async Task<EmployeeScheduleModel> UpdateEmployeeScheduleAsync(EmployeeScheduleModel employeeScheduleModel)
        {
            var EmployeeScheduleExists = (await employeeSchedulerepo.FindByConditionAsync(x => x.ScheduleId == employeeScheduleModel.ScheduleId)).FirstOrDefault();
            if (EmployeeScheduleExists != null)
            {
                EmployeeScheduleExists.StartHour = employeeScheduleModel.StartHour;
                EmployeeScheduleExists.EndHour = employeeScheduleModel.EndHour;
                EmployeeScheduleExists.ModifiedTS = DateTime.Now;
                //EmployeeScheduleExists.CreatedTS = DateTime.Now;
                // EmployeeScheduleExists.CreatedBy = employeeScheduleModel.CreatedBy;
                // EmployeeScheduleExists.ModifiedBy = employeeScheduleModel.ModifiedBy;
                await employeeSchedulerepo.UpdateAsync(EmployeeScheduleExists);
                await employeeSchedulerepo.SaveChangesAsync();
            }
            return employeeScheduleModel;
            
        }
        public async Task<EmployeeScheduleModel> DeleteEmployeeScheduleAsync(int employeescheduleid)
        {
            var EmployeeScheduleExists = (await employeeSchedulerepo.FindByConditionAsync(x => x.ScheduleId == employeescheduleid)).FirstOrDefault();
            if (EmployeeScheduleExists != null)
            {
                await employeeSchedulerepo.DeleteAsync(EmployeeScheduleExists);
                await employeeSchedulerepo.SaveChangesAsync();
            }
            return null;
        }
    }
}
