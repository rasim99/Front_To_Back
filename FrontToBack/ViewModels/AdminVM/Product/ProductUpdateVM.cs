using System.ComponentModel.DataAnnotations;

namespace FrontToBack.ViewModels.AdminVM.Product
{
    public class ProductUpdateVM
    {
        [Required, StringLength(100)]
        public string Name { get; set; }
        public int CategoryId { get; set; }
        [Range(0, 30)]
        public int Count { get; set; }
        public double Price { get; set; }
        [Required]
        public IFormFile[] Photos { get; set; }
    }
}
