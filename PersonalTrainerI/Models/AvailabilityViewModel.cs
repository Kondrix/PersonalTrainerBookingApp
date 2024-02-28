using System.ComponentModel.DataAnnotations;

namespace PersonalTrainerI.Models
{
    public class AvailabilityViewModel
    {
        public int ID { get; set; }
      
        [DataType(DataType.Date)]
        public DateTime Date { get; set; }
        public TimeSpan StartTime { get; set; }
        public TimeSpan EndTime { get; set; }


        // Relacja do trenera
        public virtual PersonalTrainerViewModel PersonalTrainer { get; set; }
    }
}
