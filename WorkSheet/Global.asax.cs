using WorkSheet.Models.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using WorkSheet.Models.Entity;
using WorkSheet.Models.Enum;

namespace WorkSheet
{
    // Note: For instructions on enabling IIS6 or IIS7 classic mode, 
    // visit http://go.microsoft.com/?LinkId=9394801

    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            using (WorkContext db = new WorkContext())
            {
                db.Database.CreateIfNotExists();

                //CreateData(db);
                Product pr = new Product();
                pr.ProductId = "sdasd";
                pr.Quantity = 5;
                pr.Description = "asdasdasdsddd";
                db.Product.Add(pr);

                db.SaveChanges();
            }

            AreaRegistration.RegisterAllAreas();

            WebApiConfig.Register(GlobalConfiguration.Configuration);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            //AuthConfig.RegisterAuth();
            //performans için
            ViewEngines.Engines.Clear();
            ViewEngines.Engines.Add(new RazorViewEngine());
            //performans için
            ModelMetadataProviders.Current = new CachedDataAnnotationsModelMetadataProvider();
        }

        private void CreateData(WorkContext db)
        {
            JobGroup jobGrp1 = new JobGroup();
            jobGrp1.Name = "Job Group1";
            db.JobGroup.Add(jobGrp1);


            Job job1 = new Job();
            job1.Name = "job1";
            job1.UserTypeId = 1;
            //job1.JobTypeId = JobType.Normal;
            job1.JobGroupId = jobGrp1.JobGroupId;
            db.Job.Add(job1);

            Job job2 = new Job();
            job2.Name = "job2";
            job2.UserTypeId = 1;
            //job2.JobTypeId = JobType.Normal;
            job2.JobGroupId = jobGrp1.JobGroupId;
            db.Job.Add(job2);

            User user1 = new User();
            user1.UserTypeId = 1;
            user1.Name = "ali";
            user1.Surname = "yildiz";
            user1.Email = "huseyin";
            user1.Password = "1";
            db.User.Add(user1);

            JobGroupUser jobGrpUser1 = new JobGroupUser();
            jobGrpUser1.JobGroupId = jobGrp1.JobGroupId;
            jobGrpUser1.UserId = user1.UserId;
            db.JobGroupUser.Add(jobGrpUser1);

            Task task1 = new Task();
            task1.Hour = 5;
            task1.EventDate = DateTime.Now.Date;
            task1.Job = job1;
            task1.Point = 14;
            task1.Quantity = 3;
            db.Task.Add(task1);


            Menu m1 = new Menu();
            m1.Name = "menu1";
            m1.Description = "menu1 Desc";
            db.Menu.Add(m1);

            Menu m2 = new Menu();
            m2.Name = "menu2";
            m2.Description = "menu2 Desc";
            db.Menu.Add(m2);

            Rule rule1 = new Rule();
            rule1.Name = "rule1";
            db.Rule.Add(rule1);

            Rule rule2 = new Rule();
            rule2.Name = "rule2";
            db.Rule.Add(rule2);

            Role role1 = new Role();
            role1.Name = "role1-Admin";
            db.Role.Add(role1);

            RoleRule roleRule1 = new RoleRule();
            //roleRule1.RoleId = role1.RoleId;
            //roleRule1.RuleId = rule1.RuleId;
            roleRule1.Role = role1;
            roleRule1.Rule = rule1;
            db.RoleRule.Add(roleRule1);

            //RoleRule roleRule2 = new RoleRule();
            //roleRule1.Role = role1;
            //roleRule1.Rule = rule2;
            //db.RoleRule.Add(roleRule2);
        }
    }
}