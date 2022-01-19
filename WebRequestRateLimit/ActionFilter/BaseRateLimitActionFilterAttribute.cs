using System.Web.Mvc;
using System.Web;
using System;
using System.Web.Caching;
using System.Net;
using WebRequestRateLimit.Service;

namespace WebRequestRateLimit.ActionFilter
{
    [AttributeUsage(AttributeTargets.Method, AllowMultiple = false)]
    public abstract class BaseRateLimitActionFilterAttribute : ActionFilterAttribute
    {
        protected readonly int _seconds;

        public object ActionExecutingContextModel { get; private set; }

        public BaseRateLimitActionFilterAttribute(int seconds = 10)
        {
            _seconds = seconds;
        }
        protected abstract ActionResult GetFailActionResult(int seconds);
        protected abstract HttpStatusCode GetFailStatusCode();

        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            string cacheKey = GetCacheKey(filterContext);

            if (HttpRuntime.Cache[cacheKey] != null)
            {
                filterContext.Result = GetFailActionResult(_seconds);
                filterContext.HttpContext.Response.StatusCode = (int)GetFailStatusCode();
            }

            CacheHelper.AddCache(cacheKey, "1", null, _seconds, null);



        }

        private string GetCacheKey(ActionExecutingContext filterContext)
        {

            string controllerName = filterContext.ActionDescriptor.ControllerDescriptor.ControllerName;
            string actionName = filterContext.ActionDescriptor.ActionName;
            string identityValue = null;

            if (!HttpContext.Current.User.Identity.IsAuthenticated)
            {
                identityValue = filterContext.HttpContext.Request.UserHostAddress;
            }
            else
            {
                identityValue = HttpContext.Current.User.Identity.ToString();
            }

            return CacheHelper.GetCacheKey(controllerName, actionName, identityValue);
        }
    }
}