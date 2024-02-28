using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PersonalTrainerI.Models
{
    public class PersonalTrainerViewModel
    {
        public int ID { get; set; }
        [Required(ErrorMessage = "Pole Imię jest wymagane.")]
        [StringLength(50, ErrorMessage = "Imię nie może przekroczyć 50 znaków.")]
        [RegularExpression(@"^[a-zA-Z]+$", ErrorMessage = "Imię może zawierać tylko litery.")]
        public string FirstName { get; set; }
        [Required(ErrorMessage = "Pole Nazwisko jest wymagane.")]
        [StringLength(50, ErrorMessage = "Nazwisko nie może przekroczyć 50 znaków.")]
        [RegularExpression(@"^[a-zA-Z]+$", ErrorMessage = "Nazwisko może zawierać tylko litery.")]
        public string LastName { get; set; }
        public decimal Rating { get; set; }
        [Required(ErrorMessage = "Pole Typ treningu jest wymagane.")]
        public string TrainingType { get; set; }

        public bool IsVerified { get; set; }

        public virtual Microsoft.AspNetCore.Identity.IdentityUser User { get; set; }

        // Relacja do treningów
        public virtual ICollection<TrainingViewModel> Training { get; set; }

        // Relacja do siłowni
        public virtual ICollection<GymTrainerViewModel> GymTrainers { get; set; }

        // Relacja do dostępności
        public virtual ICollection<AvailabilityViewModel> Availability { get; set; }
    }
}
