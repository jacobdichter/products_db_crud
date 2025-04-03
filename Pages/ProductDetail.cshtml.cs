using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using ProductsAppRP.Models;
using ProductsAppRP.Models.ModelsVM;
using ProductsAppRP.Repositories;
namespace ProductsAppRP.Pages
{
    public class ProductDetailModel : PageModel
    {
        IProductsRepository _rep;
        public ProductDetailModel(IProductsRepository rep)
        {
            _rep = rep;
        }
        public Product Prod { get; set; }
        public string Status = "";
        public void OnGet(int prodid)
        {
            Prod = _rep.GetProductById(prodid);
        }
    }
}