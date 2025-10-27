using System.ComponentModel.DataAnnotations;

namespace Villa_WebApp.Models.DTO
{
    public class VillaNumberUpdateDTO
    {
        [Required]
        public int? VillaNo { get; set; }
        public string SpecialDetails { get; set; }

        [Required]
        public int? VillaId { get; set; }
    }
}
