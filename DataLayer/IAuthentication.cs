namespace ProductsAppRP.DataLayer
{
    public interface IAuthentication
    {
        bool VerifyLogin(string username, string password);
    }
}
