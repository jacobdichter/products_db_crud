using Microsoft.AspNetCore.Mvc.Rendering;

namespace ProductsAppRP.Models.ModelsVM
{
    //--------------------------------------------
    // For adding and editing products, we need to
    // send a model to the page view that contains
    // the product info and the dropdown info for
    // the available categories.
    //---------------------------------------------
    public class AddEditProductVM
    {
        public SelectList CatList  { get; set; } // For dropdown categories
        public Product Prod { get; set; }
        public string ButtonText { get; set; } // Whether Add or Edit Product
    }
}
