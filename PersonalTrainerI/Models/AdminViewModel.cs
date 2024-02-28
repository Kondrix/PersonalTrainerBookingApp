namespace PersonalTrainerI.Models
{
    internal class AdminViewModel
    {
        public List<PersonalTrainerViewModel> PersonalTrainers { get; set; }
        public List<GymViewModel> Gyms { get; set; }
        public List<TrainingViewModel> Training { get; set; }
    }
}