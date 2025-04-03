using System.Data.Common;
using System.Data;

namespace ProductsAppRP.DataLayer
{
 
    //-------------------------------------------------------------
    // IDataAccess is an interface that can issue parameterized SQL
    // and support database transactions. 
    //
    // The optional parameters DbConnection, DbTransaction, and
    // bTransaction allow us to keep the connection to the database
    // open to implement proper transactions.
    //--------------------------------------------------------------
    public interface IDataAccess
    {
        object GetSingleAnswer(string sql, List<DbParameter> PList,
            DbConnection conn = null, DbTransaction sqtr = null, bool bTransaction = false);
        DataTable GetManyRowsCols(string sql, List<DbParameter> PList,
            DbConnection conn = null, DbTransaction sqtr = null, bool bTransaction = false);
        int InsertUpdateDelete(string sql, List<DbParameter> PList,
            DbConnection conn = null, DbTransaction sqtr = null, bool bTransaction = false);
    }
}
