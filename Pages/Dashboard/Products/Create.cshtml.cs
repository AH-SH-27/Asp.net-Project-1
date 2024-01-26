using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using TWebApplication1.Data;
using TWebApplication1.Models;

namespace TWebApplication1.Pages.Dashboard.Products
{
    public class CreateModel : PageModel
    {
        private readonly IWebHostEnvironment _environment;
        private readonly ApplicationDbContext _context;

        public CreateModel(IWebHostEnvironment environment, ApplicationDbContext context)
        {
               _context = context;
               _environment = environment;
        }

        [BindProperty]
        public ProductDto ProductDto { get; set; } = new ProductDto();

        public void OnGet()
        {
        }

        public string errorMessage = "";
        public string successMessage = "";

        public void OnPost()
        {
            if (ProductDto.ImageFile == null)
            {
                ModelState.AddModelError("ProductDto.ImageFile", "The image file is required");
            }
            if (!ModelState.IsValid)
            {
                errorMessage = "Please provide all the required fileds";
                return;
            }

            // save image file
            string newFileName = DateTime.Now.ToString("yyyyMMDDHHmmssfff");
            newFileName += Path.GetExtension(ProductDto.ImageFile!.FileName);
            string imageFullPath = _environment.WebRootPath + "/products/" + newFileName;
            using (var stream = System.IO.File.Create(imageFullPath))
            {
                ProductDto.ImageFile.CopyTo(stream);
            }

            // save the new product in database
            Product product = new Product()
            {
                Name = ProductDto.Name,
                Brand = ProductDto.Brand,
                Category = ProductDto.Category,
                Price = ProductDto.Price,
                Description = ProductDto.Description ?? "",
                ImageFileName = newFileName,
            };
            _context.Products.Add(product);
            _context.SaveChanges();

            // clear form
            ProductDto.Name = "";
            ProductDto.Brand = "";
            ProductDto.Category = "";
            ProductDto.Price = 0;
            ProductDto.Description = "";
            ProductDto.ImageFile = null;

            ModelState.Clear();

            successMessage = "Prduct created successfully";

            //Response.Redirect("/Dashboard/Products/Products");
        }
    }
}
