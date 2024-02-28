using Microsoft.AspNetCore.Identity;

namespace PersonalTrainerI.Models
{
    public class TrainingViewModel
    {
        public int ID { get; set; }
        public string UserID { get; set; }
        public int GymID { get; set; } 
        public int PersonalTrainersID { get; set; }
        public DateTime StartDateTime { get; set; }
        public TimeSpan Duration { get; set; }
        public string TrainingType { get; set; }
        public string TrainingPackage { get; set; }


        // Relacja do użytkowników
        public virtual Microsoft.AspNetCore.Identity.IdentityUser User { get; set; }

        // Relacja do trenerów
        public virtual PersonalTrainerViewModel PersonalTrainers { get; set; }

        // Dodana relacja do siłowni
        public virtual GymViewModel Gym { get; set; }
    }
}