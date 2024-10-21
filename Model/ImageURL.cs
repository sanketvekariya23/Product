using System.ComponentModel.DataAnnotations.Schema;

namespace Product.Model
{
    public class ImageURL
    {
        public int ImageurlId { get; set; }
        public string Url { get; set; }
        public int ProductId { get; set; }
        [ForeignKey(nameof(ProductId))]
        public Products Products { get; set; }
    }
}
