using System.ComponentModel.DataAnnotations;

namespace Villa_VillaAPI.Models.DTO
{
    public class VillaUpdateDTO
    {
        [Required]
        public int Id { get; set; }

        [Required]
        [MaxLength(30)]
        public string Name { get; set; }

        public string Details { get; set; }
        [Required]
        public double? Rate { get; set; }
        [Required]
        public int? Sqmt { get; set; } //square meter

        [Required]
        public int Occupancy { get; set; }
        [Required]
        public string ImageURL { get; set; }
        public string Amenity { get; set; }
    }
}
