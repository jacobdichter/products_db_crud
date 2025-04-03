using Org.BouncyCastle.Crypto;
using ProductsAppRP.DataLayer;
using ProductsAppRP.Models;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Drawing;

namespace ProductsAppRP.Repositories
{
    public class RepositoryDBSQL : IProductsRepository
    {
        IDataAccess _idac = new BridgeDataAccess(new SQLDataAccess());

        //------------------------
        //---- AddProduct --------
        //------------------------
        public bool AddProduct(Product pr)
        {
            bool ret = false;
            try
            {
                string sql = "INSERT INTO Products(ProductName, Description, Price, StockLevel, CategoryId, PColor, OnSale, Discontinued) VALUES("
                    + "@ProductName,@Description,@Price,@StockLevel,@CategoryId,@PColor,@OnSale,@Discontinued)";
            List<DbParameter> plist = new List<DbParameter>();
                DbParameter p1 = new SqlParameter
                {
                    ParameterName = "@ProductName",
                    SqlDbType = SqlDbType.VarChar,
                    Value = pr.ProductName
                };
                plist.Add(p1);
                DbParameter p2 = new SqlParameter
                {
                    ParameterName = "@Description",
                    SqlDbType = SqlDbType.VarChar,
                    Value = pr.Description
                };
                plist.Add(p2);
                DbParameter p3 = new SqlParameter
                {
                    ParameterName = "@Price",
                    SqlDbType = SqlDbType.Money,
                    Value = pr.Price
                };
                plist.Add(p3);
                DbParameter p4 = new SqlParameter
                {
                    ParameterName = "@StockLevel",
                    SqlDbType = SqlDbType.Int,
                    Value = pr.StockLevel
                };
                plist.Add(p4);
                DbParameter p5 = new SqlParameter
                {
                    ParameterName = "@CategoryId",
                    SqlDbType = SqlDbType.Int,
                    Value = pr.CategoryId
                };
                plist.Add(p5);
                DbParameter p6 = new SqlParameter
                {
                    ParameterName = "@PColor",
                    SqlDbType = SqlDbType.Int,
                    Value = pr.Pcolor
                };
                plist.Add(p6);
                DbParameter p7 = new SqlParameter
                {
                    ParameterName = "@OnSale",
                    SqlDbType = SqlDbType.Bit,
                    Value = pr.OnSale
                };
                plist.Add(p7);
                DbParameter p8 = new SqlParameter
                {
                    ParameterName = "@Discontinued",
                    SqlDbType = SqlDbType.Bit,
                    Value = pr.Discontinued
                };
                plist.Add(p8);
                int rows = _idac.InsertUpdateDelete(sql, plist);
                if (rows > 0)
                    ret = true;
                else
                    ret = false;
            }
            catch (Exception ex)
            {
                throw;
            }
            return ret;
        }

        //------------------------
        //---- ApplyDiscount -----
        //------------------------
        public bool ApplyDiscount(int prodid, double amt)
        {
            bool ret = false;
            try
            {
                string sql = "UPDATE Products SET Price = Price - Price * @amt/100 WHERE "
                    + "ProductId=@ProductId";
                List<DbParameter> plist = new List<DbParameter>();
                DbParameter p1 = new SqlParameter
                {
                    ParameterName = "@ProductId",
                    SqlDbType = SqlDbType.Int,
                    Value = prodid
                };
                plist.Add(p1);
                DbParameter p2 = new SqlParameter
                {
                    ParameterName = "@amt",
                    SqlDbType = SqlDbType.Float,
                    Value = amt
                };
                plist.Add(p2);
                int rows = _idac.InsertUpdateDelete(sql, plist);
                if (rows > 0)
                    ret = true;
                else
                    ret = false;
            }
            catch (Exception)
            {
                throw;
            }
            return ret;
        }

        //------------------------
        //---- DeleteProduct -----
        //------------------------
        public bool DeleteProduct(int prodid)
        {
            bool ret = false;
            try
            {
                string sql = "DELETE FROM Products WHERE ProductId=@ProductId";
                List<DbParameter> plist = new List<DbParameter>();
                DbParameter p1 = new SqlParameter
                {
                    ParameterName = "@ProductId",
                    SqlDbType = SqlDbType.Int,
                    Value = prodid
                };
                plist.Add(p1);
                int rows = _idac.InsertUpdateDelete(sql, plist);
                if (rows > 0)
                    ret = true;
                else
                    ret = false;
            }
            catch (Exception)
            {
                throw;
            }
            return ret;
        }

        //------------------------
        //---- GetCategories -----
        //------------------------
        public List<Category> GetCategories()
        {
            List<Category> CList = new List<Category>();
            try
            {
                string sql = "SELECT * FROM Categories";
                DataTable dt = _idac.GetManyRowsCols(sql, null);
                foreach (DataRow dr in dt.Rows)
                {
                    Category cat = new Category
                    {
                        CategoryId = (int)dr["categoryId"],
                        CategoryName = dr["CategoryName"].ToString()
                    };
                    CList.Add(cat);
                }
            }
            catch (Exception)
            {
                throw;
            }
            return CList;
        }

        //------------------------
        //---- GetProductByID ----
        //------------------------
        public Product GetProductById(int prodid)
        {
            Product prod = null;
            try
            {
                string sql = "SELECT * FROM Products WHERE ProductId=@ProductId";
                List<DbParameter> plist = new List<DbParameter>();
                DbParameter p1 = new SqlParameter
                {
                    ParameterName = "@ProductId",
                    SqlDbType = SqlDbType.Int,
                    Value = prodid
                };
                plist.Add(p1);
                DataTable dt = _idac.GetManyRowsCols(sql, plist);
                foreach (DataRow dr in dt.Rows) // may have one row in it
                {
                    var pc = dr["PColor"];
                    string tpn = pc.GetType().Name;
                    prod = new Product
                    {
                        ProductId = (int)dr["ProductId"],
                        ProductName = dr["ProductName"].ToString(),
                        Description = dr["Description"].ToString(),
                        Price = (decimal)dr["Price"],
                        Pcolor = dr["PColor"] == DBNull.Value ? null :
                    (int)dr["PColor"], // to handle null values
                        CategoryId = (int)dr["CategoryId"],
                        StockLevel = (int)dr["StockLevel"],
                        OnSale = (bool)dr["OnSale"],
                        Discontinued = (bool)dr["Discontinued"]
                    };
                }
            }
            catch (Exception)
            {
                throw;
            }
            return prod;
        }

        //------------------------
        //---- GetProductsByCat---
        //------------------------
        public List<Product> GetProductsByCategory(int catid)
        {
            List<Product> ProdList = new List<Product>();
            try
            {
                string sql = "SELECT * FROM Products WHERE CategoryId=@CategoryId";
                List<DbParameter> plist = new List<DbParameter>();
                DbParameter p1 = new SqlParameter
                {
                    ParameterName = "@CategoryId",
                    SqlDbType = SqlDbType.Int,
                    Value = catid
                };
                plist.Add(p1);
                DataTable dt = _idac.GetManyRowsCols(sql, plist);
                foreach (DataRow dr in dt.Rows)
                {
                    Product pr = new Product
                    {
                        ProductId = (int)dr["ProductId"],
                        ProductName = dr["ProductName"].ToString(),
                        Description = dr["Description"].ToString(),
                        Price = (decimal)dr["Price"],
                        StockLevel = (int)dr["StockLevel"],
                        OnSale = (bool)dr["OnSale"],
                        Discontinued = (bool)dr["Discontinued"]
                    };
                    ProdList.Add(pr);
                }
            }
            catch (Exception)
            {
                throw;
            }
            return ProdList;
        }

        //------------------------
        //--- GetProductsOnSale --
        //------------------------
        public List<Product> GetProductsOnSale()
        { 
            List<Product> ProdList = new List<Product>();
            try
            {
                string sql = "SELECT * FROM Products WHERE OnSale=1";
                List<DbParameter> plist = new List<DbParameter>();
                DbParameter p1 = new SqlParameter
                {
                    ParameterName = "OnSale",
                    SqlDbType = SqlDbType.Bit,
                    Value = 1
                };
                plist.Add(p1);
                DataTable dt = _idac.GetManyRowsCols(sql, plist);
                foreach (DataRow dr in dt.Rows)
                {
                    Product pr = new Product
                    {
                        ProductId = (int)dr["ProductId"],
                        ProductName = dr["ProductName"].ToString(),
                        Description = dr["Description"].ToString(),
                        Price = (decimal)dr["Price"],
                        StockLevel = (int)dr["StockLevel"],
                        OnSale = (bool)dr["OnSale"],
                        Discontinued = (bool)dr["Discontinued"]
                    };
                    ProdList.Add(pr);
                }
            }
            catch (Exception)
            {
                throw;
            } 
            return ProdList; 
        }

        //------------------------
        //---- IncreasePrice -----
        //------------------------

        public bool IncreasePrice(int prodid, double amt)
        {
            bool ret = false;
            try
            {
                string sql = "UPDATE Products SET Price = Price + Price * @amt/100 WHERE "
                    + "ProductId=@ProductId";
                List<DbParameter> plist = new List<DbParameter>();
                DbParameter p1 = new SqlParameter
                {
                    ParameterName = "@ProductId",
                    SqlDbType = SqlDbType.Int,
                    Value = prodid
                };
                plist.Add(p1);
                DbParameter p2 = new SqlParameter
                {
                    ParameterName = "@amt",
                    SqlDbType = SqlDbType.Float,
                    Value = amt
                };
                plist.Add(p2);
                int rows = _idac.InsertUpdateDelete(sql, plist);
                if (rows > 0)
                    ret = true;
                else
                    ret = false;
            }
            catch (Exception)
            {
                throw;
            }
            return ret;
        }

        //------------------------
        //---- UpdateProduct -----
        //------------------------
        public bool UpdateProduct(Product pr)
        {
            bool ret = false;
            try
            {
                string sql = "UPDATE Products SET ProductName = @ProductName,Description = @Description,Price = @Price,"
                    + "StockLevel=@StockLevel,PColor=@PColor,"
                    + "OnSale=@OnSale,Discontinued=@Discontinued WHERE ProductId = @ProductId";
            List<DbParameter> plist = new List<DbParameter>();
                DbParameter p0 = new SqlParameter
                {
                    ParameterName = "@ProductId",
                    SqlDbType = SqlDbType.Int,
                    Value = pr.ProductId
                };
                plist.Add(p0);
                DbParameter p1 = new SqlParameter
                {
                    ParameterName = "@ProductName",
                    SqlDbType = SqlDbType.VarChar,
                    Value = pr.ProductName
                };
                plist.Add(p1);
                DbParameter p2 = new SqlParameter
                {
                    ParameterName = "@Description",
                    SqlDbType = SqlDbType.VarChar,
                    Value = pr.Description
                };
                plist.Add(p2);
                DbParameter p3 = new SqlParameter
                {
                    ParameterName = "@Price",
                    SqlDbType = SqlDbType.Money,
                    Value = pr.Price
                };
                plist.Add(p3);
                DbParameter p4 = new SqlParameter
                {
                    ParameterName = "@StockLevel",
                    SqlDbType = SqlDbType.Int,
                    Value = pr.StockLevel
                };
                plist.Add(p4);
                DbParameter p5 = new SqlParameter
                {
                    ParameterName = "@CategoryId",
                    SqlDbType = SqlDbType.Int,
                    Value = pr.CategoryId
                };
                plist.Add(p5);
                DbParameter p6 = new SqlParameter
                {
                    ParameterName = "@PColor",
                    SqlDbType = SqlDbType.Int,
                    Value = pr.Pcolor
                };
                plist.Add(p6);
                DbParameter p7 = new SqlParameter
                {
                    ParameterName = "@OnSale",
                    SqlDbType = SqlDbType.Bit,
                    Value = pr.OnSale
                };
                plist.Add(p7);
                DbParameter p8 = new SqlParameter
                {
                    ParameterName = "@Discontinued",
                    SqlDbType = SqlDbType.Bit,
                    Value = pr.Discontinued
                };
                plist.Add(p8);
                int rows = _idac.InsertUpdateDelete(sql, plist);
                if (rows > 0)
                    ret = true;
                else
                    ret = false;
            }
            catch (Exception ex)
            {
                throw;
            }
            return ret;
        }
    }
}

