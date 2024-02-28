using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PersonalTrainerI.Data;

namespace PersonalTrainerI.Controllers
{
    public class DashboardController : Controller
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly ApplicationDbContext _context;

        public DashboardController(UserManager<IdentityUser> userManager, ApplicationDbContext context)
        {
            _userManager = userManager;
            _context = context;
        }
        public async Task<IActionResult> Index()
        {
            var user = await _userManager.GetUserAsync(User);
            var userId = user?.Id;


           
            var allTrainings = await _context.Training
                                           .Include(t => t.PersonalTrainers)
                                           .Include(t => t.Gym)
                                           .Where(t => t.UserID == userId)
                                           .ToListAsync();

            var today = DateTime.Now;
            var totalTrainings = allTrainings.Count(t => t.StartDateTime <= today);
            ViewBag.TotalTrainings = totalTrainings;
            var totalTrainingMinutes = allTrainings.Where(t => t.StartDateTime <= today).Sum(t => t.Duration.TotalMinutes);
        
            int hours = (int)(totalTrainingMinutes / 60);
            int minutes = (int)(totalTrainingMinutes % 60);
            string formattedTime = string.Format("{0} godzin {1:D2} minut", hours, minutes);
            
             ViewBag.FormattedTime = formattedTime;


            return View(allTrainings);
        }
        public async Task<IActionResult> TrainingHoursChart()
        {
            var user = await _userManager.GetUserAsync(User);
            var userId = user?.Id;

            
            var trainings = await _context.Training
                                        .Where(t => t.UserID == userId)
                                        .ToListAsync();

            
            var today = DateTime.Today;

            
            int dayOfWeek = ((int)today.DayOfWeek + 6) % 7;
            var startOfWeek = today.AddDays(-dayOfWeek);

          
            var endOfWeek = startOfWeek.AddDays(7);

       
            var trainingsThisWeek = trainings.Where(t => t.StartDateTime >= startOfWeek && t.StartDateTime < endOfWeek);

            
            var trainingHoursPerDay = new Dictionary<DayOfWeek, double>
              {
                { DayOfWeek.Monday, 0 },
                { DayOfWeek.Tuesday, 0 },
                { DayOfWeek.Wednesday, 0 },
                { DayOfWeek.Thursday, 0 },
                { DayOfWeek.Friday, 0 },
                { DayOfWeek.Saturday, 0 },
                { DayOfWeek.Sunday, 0 }
             };

            
            foreach (var training in trainingsThisWeek)
            {
                trainingHoursPerDay[training.StartDateTime.DayOfWeek] += training.Duration.TotalHours;
            }

            
            var chartData = trainingHoursPerDay
             .Select(x => new { DayOfWeek = (int)x.Key, TotalDuration = x.Value })
             .OrderBy(x => x.DayOfWeek)
             .ToList();

            return Json(chartData);
        }

    }
}
