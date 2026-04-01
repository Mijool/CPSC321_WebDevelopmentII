using System.ComponentModel.DataAnnotations;
namespace CPSC321_A05_MC.Models
{
    public class CarModel
    {
        [Required]
        public int Id { get; set; }
        [Required]
        public string Make { get; set; }
        [Required]
        public string Model { get; set; }
        [Required]
        
        [Range(1940, 2080)]

        public int Year { get; set; }
        [Required]
        
        [Range(0,999_999)]
        public int Mileage { get; set; }
        [Required]
        public string BodyStyle { get; set; }
        [Required]
        public string Color { get; set; }

    }
}
