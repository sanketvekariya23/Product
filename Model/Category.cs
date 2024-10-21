using System.ComponentModel.DataAnnotations;

namespace Product.Model
{
    public class Category
    {
        [Key] public int CategoryId { get; set; }
        public string CategoryName { get; set; }
    }
}
