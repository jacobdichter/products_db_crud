using Microsoft.AspNetCore.Http;
using ProductsAppRP.Models;
using ProductsAppRP.Utils;

namespace ProductsAppRP.Utils
{
    public class SessionFacade
    //----------------------------------------------------------------------------
    // The purpose of the Facade pattern is to provide safe access to a resource,
    // e.g. the Session object, so that the name of the key cannot be mistyped.
    //
    // Also the datatype stored in a key cannot create typecasting errors.
    //----------------------------------------------------------------------------
    {
        const string LoggedInUserkey = "loggedinuserkey"; // Key Field
        public static LoggedInUserInfo LOGGEDINUSERINFO
        {
            get
            {
                return HttpContextHelper.HttpCtx.Session.Get<LoggedInUserInfo>(LoggedInUserkey);
            }
            set
            {
                HttpContextHelper.HttpCtx.Session.Set(LoggedInUserkey, value);
            }
        }
    }
}
