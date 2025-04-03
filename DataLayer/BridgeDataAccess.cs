using System.Data;
using System.Data.Common;

namespace ProductsAppRP.DataLayer
{
    //-----------------------------------------------
    // Implementation of Bridge pattern to allow
    // switching between SQL Server and MySQL data
    // access
    //------------------------------------------------

    public class BridgeDataAccess : IDataAccess
    {
        IDataAccess _idac = null;
        public BridgeDataAccess(IDataAccess idac) { _idac = idac; }
        public DataTable GetManyRowsCols(string sql, List<DbParameter> PList, 
            DbConnection conn = null, DbTransaction sqtr = null, bool bTransaction = false)
            {
            return _idac.GetManyRowsCols(sql, PList, conn, sqtr, bTransaction);
            }

        public object GetSingleAnswer(string sql, List<DbParameter> PList,
            DbConnection conn = null, DbTransaction sqtr = null, bool bTransaction = false)
        {
            return _idac.GetSingleAnswer(sql, PList, conn, sqtr, bTransaction);
        }
        public int InsertUpdateDelete(string sql, List<DbParameter> PList,
            DbConnection conn = null, DbTransaction sqtr = null, bool bTransaction = false)
        {
            return _idac.InsertUpdateDelete(sql, PList, conn, sqtr, bTransaction);
        }
    }
}
