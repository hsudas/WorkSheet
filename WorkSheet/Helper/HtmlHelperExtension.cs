using System;
using System.Web;
using System.Web.Mvc;
using WorkSheet.Models.Entity;
using WorkSheet.Services;
using WorkSheet.Services.Interface;


namespace WorkSheet.Helper
{
    public static class HtmlHelperExtension
    {
        static ITaskService taskService;

        //public static MvcHtmlString CurrencyFormat(this HtmlHelper helper, string value)
        //{
        //    var result = string.Format("{0:C2}", value);
        //    return new MvcHtmlString(result);
        //}

        public static MvcHtmlString CurrencyFormat(this HtmlHelper helper, string value)
        {
            var result = string.Format("{0:C2}", value);
            return new MvcHtmlString(result);
        }

        public static bool IsAuthorizedForRule(this HtmlHelper helper, int ruleId)
        {
            //return true;
            bool result = false;
            //ITaskService taskService = new TaskService();
            taskService = new TaskService();

            try
            {
                User user = ((User)System.Web.HttpContext.Current.Session["User"]);
                result = taskService.isAuthorizedUserForRule(user.UserId, ruleId);
            }
            catch
            {
                return false;
            }
            finally
            {
                taskService=null;
            }
            return result;
        }

        public static IHtmlString LabelWithMark(this HtmlHelper helper, string content)
        {
            string htmlString = String.Format("<label><mark>{0}</mark></label>", content);//
            return new HtmlString(htmlString);
        }

    }
}