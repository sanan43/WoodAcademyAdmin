using System.ComponentModel.DataAnnotations;

namespace WoodAcademy.Models
{
    public class Brend
    {
        public int Id { get; set; }
        [Required]
        public string? Name { get; set; }
        public bool IsDeleted { get; set; } = default;
        [Required]
        public string? ImagePath { get; set; }
    }
}
