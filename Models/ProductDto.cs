using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace TWebApplication1.Models
{
	public class ProductDto
	{
		[DisplayName("Name"),Required,MaxLength(100)]
		public string Name { get; set; } = "";

		[DisplayName("Brand"),Required,MaxLength(100)]
		public string Brand { get; set; } = "";

		[DisplayName("Category"),Required, MaxLength(100)]
		public string Category { get; set; } = "";
		
		[DisplayName("Description")]
		public string? Description { get; set; }

		[DisplayName("Price"),Required]
		public decimal Price { get; set; }

		[DisplayName("Image")]
        [DataType(DataType.Upload)]
        public IFormFile? ImageFile{ get; set; }

	}
}
