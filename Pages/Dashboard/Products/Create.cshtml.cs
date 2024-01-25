using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using TWebApplication1.Models;

namespace TWebApplication1.Pages.Dashboard.Products
{
    public class CreateModel : PageModel
    {
        [BindProperty]
        public ProductDto ProductDto { get; set; } = new ProductDto();

        public void OnGet()
        {
        }
    }
}
