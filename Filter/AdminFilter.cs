using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Misc.Song.SelectEngine.Filter
{
    public class AdminFilter : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            //base.OnActionExecuting(filterContext);
            try
            {
                var phoneNum = filterContext.HttpContext.Session.GetString("uid");
                var pwd = filterContext.HttpContext.Session.GetString("upwd");
                if (phoneNum == null || pwd == null)
                    filterContext.Result = new RedirectResult("/Admin/Index");
            }
            catch
            {
                filterContext.Result = new RedirectResult("/Shared/Error");
            }
        }

        //当方法执行完毕
        public override void OnActionExecuted(ActionExecutedContext filterContext)
        {
            base.OnActionExecuted(filterContext);
        }
    }
}