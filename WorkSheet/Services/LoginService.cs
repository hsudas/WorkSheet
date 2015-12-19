using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WorkSheet.Models.Context;
using WorkSheet.Models.Entity;
using WorkSheet.Models.Enum;
using WorkSheet.Services.Interface;

namespace WorkSheet.Services
{
    public class LoginService : ILoginService
    {
        private WorkContext db = new WorkContext();

        public User LoginUser(LoginModel loginmodel)
        {
            return db.User.AsNoTracking().Where(t => t.Email == loginmodel.Email && t.Password == loginmodel.Password && t.Status == 1).FirstOrDefault();
        }

        public List<Menu> GetMenu()
        {
            User user = (User)System.Web.HttpContext.Current.Session["User"];
            List<Menu> menuList = db.Menu.AsNoTracking().Where(t => (t.RoleId == user.RoleId || t.RoleId == null) && t.Status == 1).OrderBy(t => t.OrderNumber).ToList();
            return menuList;
        }
    }
}