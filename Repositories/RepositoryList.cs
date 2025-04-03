using ProductsAppRP.Models;
using ProductsAppRP.Repositories;

namespace ProductsAppRP.Repositories
{
    //------------------------------------------
    // Creates some dummy data and allows us to
    // test our code without any database.
    //-------------------------------------------
    public class RepositoryList : IProductsRepository
    {
        static List<Category> CList = new List<Category>();
        static List<Product> PList = null;

        public RepositoryList()
        {
            Category c1 = new Category { CategoryId = 100, CategoryName = "Electronics" };
            CList.Add(c1);
            Category c2 = new Category { CategoryId = 200, CategoryName = "Sports" };
            CList.Add(c2);
            Category c3 = new Category { CategoryId = 300, CategoryName = "Books" };
            CList.Add(c3);
            PList = new List<Product>
            {   
                 new Product{ ProductId = 1000, ProductName="Laptop",
                 Description="Ultra Light", Discontinued = false, OnSale=true,
                 Price = 897.50m, StockLevel = 300, CategoryId=100 },

                 new Product{ ProductId = 1002, ProductName="Golf Clubs",
                 Description="Nano Composite", Discontinued = false, OnSale=true,
                 Price = 497.50m, StockLevel = 200, CategoryId=200 },

                 new Product{ ProductId = 1004, ProductName="Deep Learning",
                 Description="Advanced", Discontinued = false, OnSale=true,
                 Price = 897.50m, StockLevel = 250, CategoryId=300 }
                 };
        }

        public List<Category> GetCategories()
        {
            return CList;
        }
        public List<Product> GetProductsByCategory(int catid)
        {
            return PList.Where(p => p.CategoryId == catid).ToList();
        }
        public Product GetProductById(int prodid)
        {
            return PList.Where(p => p.ProductId == prodid).FirstOrDefault<Product>();
        }
        public bool AddProduct(Product pr)
        {
            var prod = PList.Where(p => p.ProductId ==
            pr.ProductId).FirstOrDefault<Product>();
            if (prod == null)
            {
                PList.Add(pr);
                return true;
            }
            else
                return false;
        }
        public bool UpdateProduct(Product pr)
        {
            var prod = PList.Where(p => p.ProductId ==
            pr.ProductId).FirstOrDefault<Product>();
            if (prod != null)
            {
                PList.Remove(prod);
                PList.Add(pr);
                return true;
            }
            else
                return false;
        }
        public bool DeleteProduct(int prodid)
        {
            var prod = PList.Where(p => p.ProductId ==
            prodid).FirstOrDefault<Product>();
            if (prod != null)
            {
                PList.Remove(prod);
                return true;
            }
            else
                return false;
        }

        public bool ApplyDiscount(int prodid, double amt)
        {
            var prod = PList.Where(p => p.ProductId ==
            prodid).FirstOrDefault<Product>();
            if (prod != null)
            {
                prod.Price = prod.Price - prod.Price * (decimal)amt / 100.0m;
                return true;
            }
            else
                return false;
        }
        public bool IncreasePrice(int prodid, double amt)
        {
            var prod = PList.Where(p => p.ProductId ==
            prodid).FirstOrDefault<Product>();
            if (prod != null)
            {
                prod.Price = prod.Price + prod.Price * (decimal)amt / 100.0m;
                return true;
            }
            else
                return false;
        }

        public List<Product> GetProductsOnSale()
        {
            throw new NotImplementedException();
        }
    }
}

