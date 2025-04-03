using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using ProductsAppRP.Models;
using ProductsAppRP.Models.ModelsVM;
using ProductsAppRP.Repositories;

namespace ProductsAppRP.Pages
{
    public class AddProductModel : PageModel
    {
        IProductsRepository _rep;
        public AddProductModel(IProductsRepository rep)
        {
            _rep = rep;
        }
        [BindProperty]
        public AddEditProductVM ProdVM { get; set; }

        //Repository _rep = Repository.Instance;

        public string Status = "";
        public void OnGet()
        {
            ProdVM = new AddEditProductVM();
            ProdVM.Prod = new Product();
            ProdVM.Prod.CategoryId = 100;
            ProdVM.ButtonText = "Add New Product Via Partial";
            List<Category> CList = _rep.GetCategories();
            // convert List<category> to List<SelectListItem> so that it can
            // be bound to a drop down
            ProdVM.CatList = new SelectList(CList, "CategoryId", "CategoryName");
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
            bool ret = false;
            try
            {
                ret = _rep.AddProduct(ProdVM.Prod);
            }
            catch (Exception ex)
            {
                Status = ex.Message;
            }
            if (ret)
                Status = "Product created successfully..";
            else
                Status = "Problem in creating Product..";
        }
    }
}
