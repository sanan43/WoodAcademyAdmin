using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace WoodAcademy.Areas.Admin.ViewModels
{
    public class CreateBrandVM
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Bosh ola bilmez")]
        public string BrandName { get; set; }
        
        [Required, NotMapped]
        public IFormFile Photo { get; set; }
    }
}
