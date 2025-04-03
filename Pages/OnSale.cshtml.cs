using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using ProductsAppRP.Models;
using ProductsAppRP.Repositories;
using ProductsAppRP.Utils;

namespace ProductsAppRP.Pages
{
    //----------------------------------------------
    // Contains different post handlers so users
    // can click on the 5% discount or 10% increase
    // and trigger the corresponding server-side code
    //-----------------------------------------------
    public class OnSaleModel : PageModel
    {
        IProductsRepository _rep;
        public OnSaleModel(IProductsRepository rep)
        { // injection of IProductsRepository
            _rep = rep;
        }
        public SelectList CatList { get; set; }
        public List<Product> PList { get; set; }

        [BindProperty]
        public int CategoryIdSelected { get; set; }
        public void OnGet()
        {
            PList = _rep.GetProductsOnSale();
        }
        public void OnPost()
        {

            PList = _rep.GetProductsOnSale();
        }

        /*
        public IActionResult OnPostApplyDiscount(double amtid, int prodid)
        {
            // if user is not logged in as admin, redirect to Login page
            if (SessionFacade.LOGGEDINUSERINFO == null || SessionFacade.LOGGEDINUSERINFO.UserName.ToUpper() != "ADMIN")
                return RedirectToPage("Login");
            else
            {
                List<Category> CList = _rep.GetCategories();
                CatList = new SelectList(CList, "CategoryId", "CategoryName");
                _rep.ApplyDiscount(prodid, amtid);
                PList = _rep.GetProductsByCategory(CategoryIdSelected);

                return Page();
            }
        }
        public IActionResult OnPostIncreasePrice(double amtid, int prodid)
        {
            if (SessionFacade.LOGGEDINUSERINFO == null || SessionFacade.LOGGEDINUSERINFO.UserName.ToUpper() != "ADMIN")
                return RedirectToPage("Login");
            else
            {
                List<Category> CList = _rep.GetCategories();
                CatList = new SelectList(CList, "CategoryId", "CategoryName");
                _rep.IncreasePrice(prodid, amtid);
                PList = _rep.GetProductsByCategory(CategoryIdSelected);

                return Page();
            }
        }
        public IActionResult OnPostDeleteProd(int prodid)
        {
            if (SessionFacade.LOGGEDINUSERINFO == null || SessionFacade.LOGGEDINUSERINFO.UserName.ToUpper() != "ADMIN")
                return RedirectToPage("Login");
            else
            {
                List<Category> CList = _rep.GetCategories();
                CatList = new SelectList(CList, "CategoryId", "CategoryName");
                _rep.DeleteProduct(prodid);
                PList = _rep.GetProductsByCategory(CategoryIdSelected);

                return Page();
            }
        } */
    }
}
