using System.Data.Common;
using System.Data;
using ProductsAppRP.Utils;
using MySql.Data.MySqlClient;
namespace ProductsAppRP.DataLayer
{
    public class MySqlDataAccess : IDataAccess
    {
        string CONNSTR = ConnectionStringHelper.CONNSTR_MYSQL;
        public MySqlDataAccess() { }
        public MySqlDataAccess(string connstr) // to be able to change connectionstring
             {
             this.CONNSTR = connstr;
             }
    public DataTable GetManyRowsCols(string sql, List<DbParameter> PList,
    DbConnection conn = null, DbTransaction sqtr = null, bool bTransaction =
   false)
    {
        DataTable dt = new DataTable();
        if (bTransaction == false)
            conn = new MySqlConnection(CONNSTR);
        try
        {
            conn.Open();
            MySqlDataAdapter da = new MySqlDataAdapter();
            MySqlCommand cmd = new MySqlCommand(sql, conn as
           MySqlConnection);
            if (PList != null)
            {
                foreach (DbParameter p in PList)
                    cmd.Parameters.Add(p);
            }
            if (bTransaction == true)
                cmd.Transaction = sqtr as MySqlTransaction;
            da.SelectCommand = cmd;
            da.Fill(dt);
        }
        catch (Exception)
        {
            throw;
        }
        finally
        {
            if (bTransaction == false)
                conn.Close();
        }
        return dt;
    }

        public object GetSingleAnswer(string sql, List<DbParameter> PList,
    DbConnection conn = null, DbTransaction sqtr = null, bool bTransaction = false)
        {
            object obj = null;
            if (bTransaction == false)
                conn = new MySqlConnection(CONNSTR);
            try
            {
                if (bTransaction == false)
                    conn.Open();
                MySqlCommand cmd = new MySqlCommand(sql, conn as
               MySqlConnection);
                if (bTransaction == true)
                    cmd.Transaction = sqtr as MySqlTransaction;
                if (PList != null)
                {
                    foreach (DbParameter p in PList)
                        cmd.Parameters.Add(p);
                }
                obj = cmd.ExecuteScalar();
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                if (bTransaction == false)
                    conn.Close();
            }
            return obj;
        }
       
       public int InsertUpdateDelete(string sql, List<DbParameter> PList,
       DbConnection conn = null, DbTransaction sqtr = null, bool bTransaction = false)
        {
            int rows = 0;
            if (bTransaction == false)
                conn = new MySqlConnection(CONNSTR);
            try
            {
                if (bTransaction == false)
                    conn.Open();
                MySqlCommand cmd = new MySqlCommand(sql, conn as
               MySqlConnection);
                if (bTransaction == true)
                    cmd.Transaction = sqtr as MySqlTransaction;
                if (PList != null)
                {
                    foreach (DbParameter p in PList)
                        cmd.Parameters.Add(p);
                }
                rows = cmd.ExecuteNonQuery();
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                if (bTransaction == false)
                    conn.Close();
            }
            return rows;
        }
    }
}