using System.ComponentModel.DataAnnotations;

namespace FrontToBack.ViewModels.AdminVM.Category
{
    public class CategoryVM
    {
        [Required]
        [MaxLength(10)]
        public string Name { get; set; }
        [Required]
        [MinLength(50, ErrorMessage = "50-den az ola bilmez")]
        public string Desc { get; set; }
    }
}
