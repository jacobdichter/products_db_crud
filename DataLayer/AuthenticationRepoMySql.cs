using ProductsAppRP.Models;
using ProductsAppRP.DataLayer;
using System.Data;

namespace ProductsAppRP.DataLayer
{
    public class AuthenticationRepoMySql : IAuthentication
    {
        IDataAccess _idac = new BridgeDataAccess(new MySqlDataAccess());
        public bool VerifyLogin(string username, string password)
        {
            bool ret = false;
            try
            {
                string sql = "SELECT * FROM Users WHERE Username='" + username + "' and Password='" + password + "'";
                object obj = _idac.GetSingleAnswer(sql, null);
                if (obj != null)
                    ret = true;
                else
                    ret = false;
            }
            catch { throw; }
            return ret;
        }
    }
}
