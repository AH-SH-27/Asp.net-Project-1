using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using TWebApplication1.Data;
using TWebApplication1.Models;

namespace TWebApplication1.Pages.Dashboard.Products
{
    public class EditModel : PageModel
    {
        private readonly IWebHostEnvironment _environment;
        private readonly ApplicationDbContext _context;

        [BindProperty]
        public ProductDto ProductDto { get; set; } = new ProductDto();

        public Product Product { get; set; } = new Product();

        public string errorMessage = "";

        public string successMessage = "";

        public EditModel(IWebHostEnvironment environment, ApplicationDbContext context)
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

            ProductDto.Name = product.Name;
            ProductDto.Brand = product.Brand;
            ProductDto.Category = product.Category;
            ProductDto.Price = product.Price;
            ProductDto.Description = product.Description;

            Product = product;
        }

        public void OnPost(int? id)
        {
            if(id == null)
            {
                Response.Redirect("/Dashboard/Products/Products");
                return;
			}
            if(!ModelState.IsValid)
            {
                errorMessage = "Please provide all the required fields";
                return;
            }
            var product = _context.Products.Find(id);
            if(product == null)
            {
				Response.Redirect("/Dashboard/Products/Products");
				return;
			}

            // update image file if exist
            string newFileName = product.ImageFileName;
            if(ProductDto.ImageFile != null)
            {
				newFileName = DateTime.Now.ToString("yyyyMMDDHHmmssfff");
				newFileName += Path.GetExtension(ProductDto.ImageFile.FileName);
				
                string imageFullPath = Path.Combine(_environment.WebRootPath, "products", newFileName);
				using (var stream = System.IO.File.Create(imageFullPath))
				{
					ProductDto.ImageFile.CopyTo(stream);
				}

                // delete old image
                string oldImageFullPath = _environment.WebRootPath + "/products/" + product.ImageFileName;
                System.IO.File.Delete(oldImageFullPath);
			}
			// update product in database
			product.Name = ProductDto.Name;
			product.Brand = ProductDto.Brand;
			product.Category = ProductDto.Category;
			product.Price = ProductDto.Price;
			product.Description = ProductDto.Description ?? "";
            product.ImageFileName = newFileName;

            _context.SaveChanges();

			Product = product;

            successMessage = "Product updated successfully";

			Response.Redirect("/Dashboard/Products/Products");

		}
    }
}
