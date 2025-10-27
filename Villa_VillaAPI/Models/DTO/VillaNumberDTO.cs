using System.ComponentModel.DataAnnotations;

namespace Villa_VillaAPI.Models.DTO
{
    public class VillaNumberDTO
    {
        [Required]
        public int? VillaNo { get; set; }
        public string SpecialDetails { get; set; }

        [Required]
        public int? VillaId { get; set; }
    }

}
