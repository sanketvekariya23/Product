using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Product.Model
{
    public class Products
    {
        [Key] public int ProductId { get; set; }
        public string ProductName { get; set; }
        public int ProductCode { get; set; }
        public string Color { get; set; }
        public int CategoryId { get; set; }
        [ForeignKey(nameof(CategoryId))]
        public Category Category { get; set; }
        public int Quantity { get; set; }
        [NotMapped]public List<IFormFile> ProductImage { get; set; }
        public List<ImageURL> Images { get; set; }
    }
}
