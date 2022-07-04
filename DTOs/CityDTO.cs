using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebAPI.DTOs
{
    public class CityDTO
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Please enter name")]
        [StringLength(50,MinimumLength =2,ErrorMessage ="Minimum length is required 2 and maximum is 50")]
        public string Name { get; set; }
        [Required(ErrorMessage ="Please enter country")]
        public string Country { get; set; }
    }
}
