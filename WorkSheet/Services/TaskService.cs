using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Objects;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WorkSheet.Models.Context;
using WorkSheet.Models.Entity;
using WorkSheet.Services.Interface;
using WorkSheet.Models.Enum;
using WorkSheet.Helper;
using System.Globalization;

namespace WorkSheet.Services
{
    public class TaskService : ITaskService
    {
        private WorkContext db = new WorkContext();

        public void CreateTask(int? jobId, int? quantity, int? check200, int? newItemsAutomatic, int? priceCheck150, int? mPCheckChange, int minute, string other)
        {
            User user = (User)System.Web.HttpContext.Current.Session["User"];
            Job job = this.GetJob(jobId, user.UserTypeId);
            DateTime eventDate = DateTime.Now.Date;
            int taskId = db.Task.AsNoTracking().Where(o => o.JobId == jobId && o.UserId == user.UserId && EntityFunctions.TruncateTime(o.EventDate) == eventDate && o.Status == 1).Select(o => o.TaskId).FirstOrDefault();
            if (taskId.Equals(0))
            {
                if ((quantity + check200 + newItemsAutomatic + priceCheck150 + mPCheckChange) > 0) //minute > 0 ||
                {
                    Task task = new Task();
                    task.JobId = jobId;
                    task.Quantity = quantity;
                    task.Check200 = check200;
                    task.NewItemsAutomatic = newItemsAutomatic;
                    task.PriceCheck150 = priceCheck150;
                    task.MPCheckChange = mPCheckChange;
                    task.Minute = minute;
                    task.UserId = user.UserId;
                    task.Description = other;
                    try
                    {
                        task.Point = job.Point;
                    }
                    catch { }
                    task.EventDate = eventDate;
                    task.CreateUser = user.UserId;
                    db.Task.Add(task);
                }
            }
            else
            {
                Task myTask = db.Task.Find(taskId);
                if (myTask.EventDate.Equals(eventDate))
                {
                    myTask.Description = other;
                    myTask.Quantity = quantity;
                    myTask.Check200 = check200;
                    myTask.NewItemsAutomatic = newItemsAutomatic;
                    myTask.PriceCheck150 = priceCheck150;
                    myTask.MPCheckChange = quantity;
                    myTask.Minute = minute;
                }
                else
                    return;

            }
            db.SaveChanges();
        }

        public Job GetJob(int? jobId, int userTypeId)
        {
            return db.Job.AsNoTracking().Where(t => t.JobId == jobId && t.UserTypeId == userTypeId && t.Status == 1).FirstOrDefault();
        }

        public List<Job> GetJobList()
        {
            return db.Job.AsNoTracking().Where(t => t.Status == 1 && t.Name != null && t.Name != String.Empty).OrderBy(t => t.Name).ToList();
        }

        public List<Role> GetRoleList()
        {
            return db.Role.AsNoTracking().Where(t => t.Status == 1).OrderBy(t => t.Name).ToList();
        }

        public List<Job> PrepareJobForMulti()
        {
            User user = ((User)System.Web.HttpContext.Current.Session["User"]);
            List<Job> jobList = this.GetUserAuthorizedJobList(user);//db.Job.Where(t => t.UserTypeId == user.UserTypeId && t.Status == 1).ToList();
            DateTime eventDate = DateTime.Now.Date;
            List<Task> taskList = db.Task.AsNoTracking().Where(o => o.UserId == user.UserId && EntityFunctions.TruncateTime(o.EventDate) == eventDate && o.Status == 1).ToList();

            Task task;
            if (taskList.Count > 0)
            {
                foreach (var job in jobList)
                {
                    task = taskList.Find(x => x.JobId == job.JobId);
                    if (task != null)
                    {
                        job.Quantity = task.Quantity != null ? task.Quantity : 0;
                        job.Check200 = task.Check200 != null ? task.Check200 : 0;
                        job.NewItemsAutomatic = task.NewItemsAutomatic != null ? task.NewItemsAutomatic : 0;
                        job.PriceCheck150 = task.PriceCheck150 != null ? task.PriceCheck150 : 0;
                        job.MPCheckChange = task.MPCheckChange != null ? task.MPCheckChange : 0;
                        job.Minute = task.Minute != null ? task.Minute : 0;
                        if (job.JobTypeId == JobType.Other)
                        {
                            job.Name = task.Description;
                        }
                    }
                }
            }

            return jobList.OrderBy(l => l.Name).ToList();
        }

        public List<Job> GetUserAuthorizedJobList(User user)
        {
            int[] jobGroupIds = db.JobGroupUser.AsNoTracking().Where(t => t.UserId == user.UserId && t.Status == 1).Select(t => t.JobGroupId).ToArray();
            return db.Job.AsNoTracking().Where(t => jobGroupIds.Contains(t.JobGroupId) && t.Status == 1).ToList();
        }

        public List<Task> GetTaskList()
        {
            User user = ((User)System.Web.HttpContext.Current.Session["User"]);
            return db.Task.AsNoTracking().Where(t => t.UserId == user.UserId && t.Status == 1).OrderByDescending(t => t.EventDate).ThenBy(t => t.Description).ToList();
        }

        public List<Task> GetTaskList(string Name, string JobId, string RoleId, string StartDate, string EndDate)
        {
            //db.Task.Include(t => t.User);
            User user = ((User)System.Web.HttpContext.Current.Session["User"]);
            DateTime startDate = new DateTime();
            DateTime endDate = new DateTime();
            IFormatProvider culture = System.Threading.Thread.CurrentThread.CurrentCulture;
            if (!string.IsNullOrEmpty(StartDate))
            {
                //Convert.ToDateTime(DateTime.Parse(StartDate).ToString("MM/dd/yyyy"));//("dd/MM/yyyy"));//("yyyy/MM/dd"));
                startDate = DateTime.Parse(StartDate, culture, System.Globalization.DateTimeStyles.AssumeLocal); 
            }
            if (!string.IsNullOrEmpty(EndDate))
            {
                endDate = DateTime.Parse(EndDate, culture, System.Globalization.DateTimeStyles.AssumeLocal);
            }
            List<Task> TaskList = db.Task.AsNoTracking().Include("User").Where(t => t.Status == 1).ToList();
            if (!string.IsNullOrEmpty(Name))
            {
                TaskList = TaskList.Where(t => t.User.Name.ToUpper().StartsWith(Name.ToUpper())).ToList();
            }
            if (!string.IsNullOrEmpty(JobId))
            {
                int jobId = Convert.ToInt16(JobId);
                TaskList = TaskList.Where(t => t.JobId == jobId).ToList();
            }
            if (!string.IsNullOrEmpty(RoleId))
            {
                int roleId = Convert.ToInt16(RoleId);
                TaskList = TaskList.Where(t => t.User.RoleId == roleId).ToList();
            }
            if (startDate != DateTime.MinValue)
            {
                TaskList = TaskList.Where(t => t.EventDate >= startDate).ToList();
                //TaskList = TaskList.Where(t => t.EventDate == startDate).ToList();
            }
            if (endDate != DateTime.MinValue)
            {
                TaskList = TaskList.Where(t => t.EventDate <= endDate).ToList();
            }

            return TaskList.OrderByDescending(t => t.EventDate).ThenBy(t => t.Description).ToList();
        }

        public bool isAuthorizedRoleForRule(int roleId, int ruleId)
        {
            bool result = false;
            result = db.RoleRule.AsNoTracking().Any(t => t.RoleId == roleId && t.RuleId == ruleId && t.Status == 1);
            return result;
        }

        public bool isAuthorizedUserForRule(int userId, int ruleId)
        {
            bool result = false;
            int? roleId = db.User.AsNoTracking().Where(t => t.UserId == userId && t.Status == 1).Select(t => t.RoleId).FirstOrDefault();
            result = db.RoleRule.AsNoTracking().Any(t => t.RoleId == roleId && t.RuleId == ruleId && t.Status == 1);
            return result;
        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }
    }
}