// Makes the HttpContext available to a non-Page class
// HttpContext contains important info such as the Session object for the user

namespace ProductsAppRP.Utils
{
    public class HttpContextHelper
    {
        static IHttpContextAccessor _ihttpCtxAccessor = null;

        // Configure() will be triggered in the Program.cs file
        public static void Configure(IHttpContextAccessor accessor)
        {
            _ihttpCtxAccessor = accessor;
        }

        public static HttpContext HttpCtx
        {
            get { return _ihttpCtxAccessor.HttpContext; }
        }
    }
}

