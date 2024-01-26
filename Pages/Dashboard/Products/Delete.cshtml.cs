using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using TWebApplication1.Data;

namespace TWebApplication1.Pages.Dashboard.Products
{
    public class DeleteModel : PageModel
    {
		private readonly IWebHostEnvironment _environment;
		private readonly ApplicationDbContext _context;

		public DeleteModel(IWebHostEnvironment environment, ApplicationDbContext context)
		{
			_context = context;
			_environment = environment;
		}


		public void OnGet(int? id)
        {
			if(id == null)
			{
				Response.Redirect("/Dashboard/Products/Products");
				return;
			}

			var product = _context.Products.Find(id);
			if(product == null)
			{
				Response.Redirect("/Dashboard/Products/Products");
				return;
			}

			string imageFullPath = _environment.WebRootPath + "/products/" + product.ImageFileName;
			System.IO.File.Delete(imageFullPath);

			_context.Products.Remove(product);
			_context.SaveChanges();

			Response.Redirect("/Dashboard/Products/Products");
		}
    }
}
