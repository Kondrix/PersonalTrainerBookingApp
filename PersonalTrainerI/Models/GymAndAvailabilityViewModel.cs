namespace PersonalTrainerI.Models
{
    public class GymAndAvailabilityViewModel
    {
        public List<AvailabilityViewModel> Availabilitie { get; set; }
        public List<GymTrainerViewModel> GymTrainer { get; set; }

        public List<GymViewModel> Gyms { get; set; }
        public GymViewModel Gym { get; set;}
    }
}
