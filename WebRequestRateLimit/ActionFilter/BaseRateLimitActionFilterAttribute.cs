using System.Web.Mvc;
using System.Web;
using System;
using System.Web.Caching;
using System.Net;

namespace WebRequestRateLimit.ActionFilter
{
    [AttributeUsage(AttributeTargets.Method, AllowMultiple = false)]
    public abstract class BaseRateLimitActionFilterAttribute : ActionFilterAttribute
    {
        protected readonly int _seconds;
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

            string cacheValue = "1";
            CacheDependency dependency = null;
            CacheItemRemovedCallback onRemoveCallback = null;

            HttpRuntime.Cache.Add(cacheKey,
                cacheValue,
                dependency,
                DateTime.Now.AddSeconds(_seconds),
                Cache.NoSlidingExpiration,
                CacheItemPriority.Low,
                onRemoveCallback);
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

            return $"{controllerName}-{actionName}-{identityValue}";
        }
    }
}