using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using ProductsAppRP.Models;
using ProductsAppRP.Models.ModelsVM;
using ProductsAppRP.Repositories;
using ProductsAppRP.Utils;

namespace ProductsAppRP.Pages
{
    public class EditProductModel : PageModel
    {
        IProductsRepository _rep;
        public EditProductModel(IProductsRepository rep)
        {
            _rep = rep;
        }
        [BindProperty]
        public AddEditProductVM ProdVM { get; set; } // product from UI
        public string Status = "";
        public IActionResult OnGet(int prodid)
        {
            if (SessionFacade.LOGGEDINUSERINFO == null || SessionFacade.LOGGEDINUSERINFO.UserName.ToUpper() != "ADMIN")
                return RedirectToPage("Login");
            else
            {
                ProdVM = new AddEditProductVM();
                Product prod = _rep.GetProductById(prodid);
                ProdVM.Prod = prod;
                ProdVM.ButtonText = "Update Product Via Partial";
                List<Category> CList = _rep.GetCategories();
                // convert List<category> to List<SelectListItem> so that it can
                // be bound to a drop down
                ProdVM.CatList = new SelectList(CList, "CategoryId", "CategoryName");

                return Page();
            }
        }
        public void OnPost()
        {
            ModelState.Remove("CatList"); // without these, it creates error
            ModelState.Remove("ButtonText");
            ModelState.Remove("Prod.Category"); // when database is involved
            
            if (!ModelState.IsValid)
            {
                return;
            }
            List<Category> CList = _rep.GetCategories();
            ProdVM.CatList = new SelectList(CList, "CategoryId", "CategoryName");
            ProdVM.ButtonText = "Update Product Via Partial";
            bool ret = false;
            try
            {
                ret = _rep.UpdateProduct(ProdVM.Prod);
            }
            catch (Exception ex)
            {
                Status = ex.Message;
            }
            if (ret)
                Status = "Product updated successfully..";
            else
                Status = "Problem in updating Product..";
        }
    }
}
