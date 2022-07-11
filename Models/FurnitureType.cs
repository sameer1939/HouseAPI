using System.ComponentModel.DataAnnotations;

namespace WebAPI.Models
{
    public class FurnitureType:BaseEntity
    {
        [Required]
        public string Name { get; set; }
    }
}