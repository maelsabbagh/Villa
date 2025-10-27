using System.ComponentModel.DataAnnotations;

namespace Villa_WebApp.Models.DTO
{
    public class VillaDTO
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(30)]
        public string Name { get; set; }

        public string Details { get; set; }
        [Required]
        public double Rate { get; set; }

        public int Sqmt { get; set; } //square meter

        public int Occupancy { get; set; }
        public string ImageURL { get; set; }
        public string Amenity { get; set; }
    }
}
