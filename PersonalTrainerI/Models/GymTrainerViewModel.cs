
namespace PersonalTrainerI.Models
{
    public class GymTrainerViewModel
    {

        public int ID { get; set; }

        public int PersonalTrainerID { get; set; }
        public PersonalTrainerViewModel PersonalTrainer { get; set; }

        public int GymID { get; set; }
        public GymViewModel Gym { get; set; }
    }
}