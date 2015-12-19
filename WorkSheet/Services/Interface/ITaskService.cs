using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorkSheet.Models.Entity;

namespace WorkSheet.Services.Interface
{
    interface ITaskService : IDisposable
    {

        void CreateTask(int? jobId, int? quantity, int? check200, int? newItemsAutomatic, int? priceCheck150, int? mPCheckChange, int minute, string other);

        List<Job> PrepareJobForMulti();

        List<Job> GetJobList();

        List<Role> GetRoleList();

        List<WorkSheet.Models.Entity.Task> GetTaskList();

        List<WorkSheet.Models.Entity.Task> GetTaskList(string Name, string JobId, string RoleId, string StartDate, string EndDate);

        bool isAuthorizedRoleForRule(int roleId, int ruleId);

        bool isAuthorizedUserForRule(int userId, int ruleId);

    }
}
