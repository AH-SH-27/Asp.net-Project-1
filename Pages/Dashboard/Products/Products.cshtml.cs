using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using TWebApplication1.Data;
using TWebApplication1.Models;

namespace TWebApplication1.Pages.Dashboard.Products
{
    public class ProductsModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public List<Product> Products = [];

        public ProductsModel(ApplicationDbContext context)
        {
            _context = context;
        }
        public void OnGet()
        {
            Products = [.. _context.Products.OrderByDescending(p => p.Id)];
        }
    }
}
