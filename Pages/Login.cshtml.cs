using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ProductsAppRP.DataLayer;
using ProductsAppRP.Models;
using ProductsAppRP.Utils;

namespace ProductsAppRP.Pages
{
    public class LoginModel : PageModel
    {
        IAuthentication _iauth = null;
        public LoginModel(IAuthentication iauth) // DEPENDENCY INJECTION
        {
            _iauth = iauth;
        }
        [BindProperty]
        public Users UserLogin { get; set; }
        public string Msg { get; set; } = "";
        public void OnGet()
        {
            Msg = "";
        }
        public void OnPost()
        {
            try
            {
                bool res = _iauth.VerifyLogin(UserLogin.username, UserLogin.password);
                if (res)
                {
                    Msg = "Login successfull..";
                    // store log in info in session using Session facade
                    LoggedInUserInfo uinfo = new LoggedInUserInfo
                    {
                        UserName = UserLogin.username,
                        LoggedInTime = DateTime.Now,
                    };
                    SessionFacade.LOGGEDINUSERINFO = uinfo;
                }
                else
                    Msg = "Invalid Login..";
            }
            catch (Exception ex)
            {
                Msg = ex.Message;
            }
        }
    }
}
