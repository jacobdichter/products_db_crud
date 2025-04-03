using System.Data.Common;
using System.Data.SqlClient;
using System.Data;
using ProductsAppRP.Utils;

namespace ProductsAppRP.DataLayer
{
    //-------------------------------------------
    // Implements the IDataAccess interface for
    // SQL Server
    //-------------------------------------------
    public class SQLDataAccess : IDataAccess
    {
        string CONNSTR = ConnectionStringHelper.CONNSTR_SQL;
        public SQLDataAccess() { }
        public SQLDataAccess(string connstr) // To change connection string
        {
            this.CONNSTR = connstr;
        }

        public DataTable GetManyRowsCols(string sql, List<DbParameter> PList,
        DbConnection conn = null, DbTransaction sqtr = null, bool bTransaction = false)
        {
            DataTable dt = new DataTable();
            if (bTransaction == false) // If not part of a transaction
                conn = new SqlConnection(CONNSTR);
            try
            {
                conn.Open();
                SqlDataAdapter da = new SqlDataAdapter();
                SqlCommand cmd = new SqlCommand(sql, conn as SqlConnection);

                if (PList != null)
                {
                    foreach (DbParameter p in PList)
                        cmd.Parameters.Add(p);
                }

                if (bTransaction == true) // If part of a transaction
                    cmd.Transaction = sqtr as SqlTransaction;
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
                    conn = new SqlConnection(CONNSTR);
                try
                {
                    if (bTransaction == false)
                        conn.Open();
                    SqlCommand cmd = new SqlCommand(sql, conn as SqlConnection);
                    if (bTransaction == true)
                        cmd.Transaction = sqtr as SqlTransaction;
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
                    conn = new SqlConnection(CONNSTR);
                try
                {
                    if (bTransaction == false)
                        conn.Open();
                    SqlCommand cmd = new SqlCommand(sql, conn as SqlConnection);
                    if (bTransaction == true)
                        cmd.Transaction = sqtr as SqlTransaction;
                    if (PList != null)
                    {
                        foreach (SqlParameter p in PList)
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
