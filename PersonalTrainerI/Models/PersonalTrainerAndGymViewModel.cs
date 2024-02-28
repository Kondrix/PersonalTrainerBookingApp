using Microsoft.AspNetCore.Mvc.Rendering;

namespace PersonalTrainerI.Models
{
    public class PersonalTrainerAndGymViewModel
    {
        internal bool isPersonalTrainer { get; set; }

        public PersonalTrainerViewModel PersonalTrainer { get; set; }
        public GymViewModel Gyms { get; set; }
        public List<SelectListItem> GymList { get; set; } 


    }
}
