using System.Net;
using System.Web.Mvc;

namespace WebRequestRateLimit.ActionFilter
{
    public class RateLimitActionFilterAttribute : BaseRateLimitActionFilterAttribute
    {
        protected override ActionResult GetFailActionResult(int seconds)
        {
            ContentResult result = new ContentResult
            {
                Content = string.Format("Please wait for {0} seconds", seconds)
            };

            return result;
        }

        protected override HttpStatusCode GetFailStatusCode()
        {
            return HttpStatusCode.BadRequest;
        }
    }
}