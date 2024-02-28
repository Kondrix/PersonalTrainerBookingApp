using System.ComponentModel.DataAnnotations;

namespace PersonalTrainerI.Models
{
    public class GymViewModel
    {
        public int ID { get; set; }

        [Required]
        [StringLength(255)]
        [RegularExpression(@"^[a-zA-Z]+$", ErrorMessage = "Miasto może zawierać tylko litery.")]
        public string City { get; set; }

        [Required]
        [StringLength(255)]
        public string GymName { get; set; }

        [Required]
        [StringLength(20)]
        [RegularExpression(@"^[a-zA-Z]+$", ErrorMessage = "Ulica może zawierać tylko litery.")]
        public string Street { get; set; }

        [Required]
        [StringLength(10)]
        [RegularExpression(@"^\d{1,3}[a-zA-Z]{0,2}$", ErrorMessage = "Numer ulicy może zawierać tylko cyfry i maksymalnie 2 litery.")]
        public string StreetNumber { get; set; }

        // Relacja do trenerów
        public virtual ICollection<GymTrainerViewModel> GymTrainers { get; set; }
    }
}
