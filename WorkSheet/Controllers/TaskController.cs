using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using WorkSheet.Models.Entity;
using WorkSheet.Models.Context;
using WorkSheet.Services.Interface;
using WorkSheet.Services;
using System.IO;
using System.Web.UI;
using WorkSheet.Models.Enum;
using WorkSheet.Helper;


namespace WorkSheet.Controllers
{
    public class TaskController : Controller
    {
        private WorkContext db = new WorkContext();
        ITaskService taskService;
        User currentUser;

        public TaskController()
        {
            taskService = new TaskService();
            currentUser = (User)System.Web.HttpContext.Current.Session["User"];
        }

        // GET: /Task/
        //[OutputCache(VaryByParam = "none", Duration = 3600)]
        public ActionResult Index()
        {
            if (currentUser != null)
            {
                ViewBag.currentUser = currentUser.Name + " " + currentUser.Surname;
                return View(taskService.PrepareJobForMulti());
            }
            else
                return RedirectToAction("Login", "Login");
        }

        [HttpPost]
        public ActionResult Index(FormCollection c)
        {
            if (ModelState.IsValid)
            {
                var jobIdArray = c.GetValues("item.JobId");
                var jobNameArray = c.GetValues("item.Name");
                var minuteArray = c.GetValues("item.Minute");
                var quantityArray = c.GetValues("item.Quantity");
                string[] check200Array = c.GetValues("item.Check200");
                var newItemsAutomaticArray = c.GetValues("item.NewItemsAutomatic");
                var priceCheck150Array = c.GetValues("item.PriceCheck150");
                var mPCheckChangeArray = c.GetValues("item.MPCheckChange");

                int j = 0;
                foreach (var jobId in jobIdArray)
                {
                    taskService.CreateTask(Convert.ToInt16(jobId),
                        quantityArray == null ? 0 : Convert.ToInt16(quantityArray[j]),
                        check200Array == null ? 0 : Convert.ToInt16(check200Array[j]),
                        newItemsAutomaticArray == null ? 0 : Convert.ToInt16(newItemsAutomaticArray[j]),
                        priceCheck150Array == null ? 0 : Convert.ToInt16(priceCheck150Array[j]),
                        mPCheckChangeArray == null ? 0 : Convert.ToInt16(mPCheckChangeArray[j]),
                        Convert.ToInt16(minuteArray[j]),
                        jobNameArray[j].ToString());
                    j++;
                }
            }
            return View(taskService.PrepareJobForMulti());
        }

        public ActionResult IndexHistory()
        {
            return View(taskService.GetTaskList());
        }

        public ActionResult IndexHistory2()
        {
            ViewBag.TaskList = taskService.GetTaskList();
            return View();
        }

        //I am not using httpGet or httpPost because it will be used for both
        public ActionResult IndexAdmin(string Name, string Surname, string StartDate, string EndDate, string JobId, string RoleId)
        {
            var jobs = taskService.GetJobList().Select(l => new { l.JobId, l.Name });
            ViewData["JobList"] = new SelectList(jobs, "JobId", "Name");
            //var userTypes = new SelectList(Enum.GetValues(typeof(UserType))
            //                          .OfType<Enum>()
            //                          .Select(x =>
            //                          new SelectListItem
            //                          {
            //                              Value = (Convert.ToInt32(x)).ToString(),
            //                              Text = Enum.GetName(typeof(UserType), x)
            //                          }
            //                             ), "Value", "Text"); //Enum.GetValues(typeof(UserType)).Cast<UserType>().ToList();
            //ViewData["UserTypeList"] = new SelectList(userTypes, "Value", "Text");
            var roles = taskService.GetRoleList().Select(l => new { l.RoleId, l.Name }); ;
            ViewData["RoleList"] = new SelectList(roles, "RoleId", "Name");

            if (currentUser.UserTypeId.Equals((int)UserType.Admin))
            {
                return View(taskService.GetTaskList(Name, JobId, RoleId, StartDate, EndDate));
            }
            return View();
        }

        public void ExportData(string Name, string Surname, string StartDate, string EndDate, string JobId, string UserTypeId)
        {
            System.Web.UI.WebControls.GridView gv = new System.Web.UI.WebControls.GridView();
            gv.DataSource = taskService.GetTaskList(Name, JobId, UserTypeId, StartDate, EndDate)
                            .Select(t => new
                            {
                                t.User.Name,
                                t.Description,
                                t.Point,
                                t.Quantity,
                                t.Check200,
                                t.NewItemsAutomatic,
                                t.PriceCheck150,
                                t.MPCheckChange,
                                t.Minute,
                                t.EventDate
                            });
            gv.DataBind();
            Response.ClearContent();
            Response.Buffer = true;
            Response.AddHeader("content-disposition", "attachment; filename=TaskList.xls");
            Response.ContentType = "application/ms-excel";
            Response.Charset = "";
            StringWriter sw = new StringWriter();
            HtmlTextWriter htw = new HtmlTextWriter(sw);
            gv.RenderControl(htw);
            Response.Output.Write(sw.ToString());
            Response.Flush();
            Response.End();
        }

        public JsonResult getTaskList()
        {
            var task = db.Task.Where(t => t.Status == 1).Include(t => t.Job).Include(t => t.User);
            return Json(task, JsonRequestBehavior.AllowGet); ;
        }

        private void FillJobDropDown()
        {
            List<Job> slJob = db.Job.Where(t => t.Status == 1).ToList();
            slJob.Add(new Job { Name = "Other", Status = 1, Point = 0 });
            ViewBag.JobId = new SelectList(slJob, "JobId", "Name");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
