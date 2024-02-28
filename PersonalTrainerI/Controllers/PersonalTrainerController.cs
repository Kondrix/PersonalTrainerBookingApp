using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using PersonalTrainerI.Data;
using PersonalTrainerI.Models;
using System.Linq;

namespace PersonalTrainerI.Controllers
{
    [Authorize]
    public class PersonalTrainerController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<IdentityUser> _userManager;

        public  ILogger<PersonalTrainerController> _logger;
        public PersonalTrainerController(UserManager<IdentityUser> userManager, ApplicationDbContext context)
        {
            _userManager = userManager;
            _context = context;
        }
        
        [HttpGet]
        public IActionResult Result(string location, string trainingType, DateTime? date)
        {
            var auser = _userManager.GetUserId(User);
            var cities = _context.Gym.Select(g => g.City).Distinct().ToList();
            ViewBag.Cities = cities;
            var trainingTypes = _context.PersonalTrainers.Select(g => g.TrainingType).Distinct().ToList();
            ViewBag.TrainingTypes = trainingTypes;

            if (!date.HasValue)
            {
                ViewBag.Date = "";
            }
            else if (date.HasValue)
            {
                ViewBag.Date = date.Value.Date.ToString("yyyy-MM-dd");
            }

            var trainers = _context.PersonalTrainers 
             
                                    .Include(t => t.GymTrainers)
                                    .ThenInclude(gt => gt.Gym)
                                    .AsQueryable();

            if (!string.IsNullOrEmpty(location))
            {
                trainers = trainers.Where(t => t.GymTrainers.Any(gt => gt.Gym.City == location));
                ViewBag.Location = location;
            }

            if (!string.IsNullOrEmpty(trainingType))
            {
                trainers = trainers.Where(t => t.TrainingType == trainingType);
                ViewBag.TrainingType = trainingType;
            }

            if (date.HasValue)
            {
                  trainers = _context.PersonalTrainers
                      .Include(t => t.GymTrainers)
                      .ThenInclude(gt => gt.Gym)
                      .Include(t => t.Availability)
                      .Where(t => t.Availability.Any(a => a.Date.Date == date.Value.Date));


                if (!string.IsNullOrEmpty(location))
                {
                    trainers = trainers.Where(t => t.GymTrainers.Any(gt => gt.Gym.City == location));
                    ViewBag.Location = location;
                }

                if (!string.IsNullOrEmpty(trainingType))
                {
                    trainers = trainers.Where(t => t.TrainingType == trainingType);
                    ViewBag.TrainingType = trainingType;
                }
                foreach (var trainer in trainers.ToList()) 
                {
                    if (trainer.Availability != null)
                    {
                        var availability = trainer.Availability
                        .Where(a => a.Date.Date == date.Value.Date)
                        .Select(a => new { a.Date, a.StartTime, a.EndTime })
                        .FirstOrDefault();

                        if (availability != null)
                        {
                            var trainings = _context.Training
                                 .Include(t => t.PersonalTrainers)
                                 
                                .AsEnumerable() 
                                .Where(t => t.StartDateTime.Date == availability.Date.Date &&
                                            t.StartDateTime.TimeOfDay <= availability.EndTime &&
                                            t.StartDateTime.TimeOfDay.Add(t.Duration) >= availability.StartTime)
                                .ToList();

                            if (trainings.Count > 0)
                            {
                                
                                var totalAvailableHours = (availability.EndTime - availability.StartTime).TotalHours;
                                var totalTrainingHours = trainings.Sum(t => t.Duration.TotalHours);

                                if (totalTrainingHours >= totalAvailableHours)
                                {
                                   
                                    trainers = trainers.Where(t => t.ID != trainer.ID);
                                }
                            }
                        }
                    }
                }
            }
              var Strainers = trainers  
                                        .OrderByDescending(t => t.GymTrainers.Count > 0) 
                                        .ThenByDescending(t => t.Rating) 
                                        .Where(t => t.IsVerified == true)
                                        .Where(t => t.User.Id != auser)
                                        .ToList();
            return View(Strainers);
        }

        [HttpGet]
        public IActionResult BookingForm(int trainerid)
        {
            var trainer = _context.PersonalTrainers.Find(trainerid);
            var gyms = _context.GymTrainer
                        .Include(t => t.Gym)
                        .Where(gt => gt.PersonalTrainerID == trainerid)
                        .Select(gt => gt.Gym)
                        .AsQueryable();

            
            var availability = _context.Availability
                            .Where(a => a.PersonalTrainer.ID == trainerid)
                            .Select(a => new
                            {
                                ID = a.ID,
                                Date = a.Date.ToString("dd-MM-yyyy")
                            })
                            .AsQueryable();

            var model = new TrainingViewModel
            {
                UserID = _userManager.GetUserId(User), 
                PersonalTrainers = trainer 
            };

            ViewBag.Gyms = new SelectList(gyms, "ID", "GymName"); 
            var currentDate = DateTime.Today;
            var filteredAvailability = _context.Availability
                                    .Where(a => a.PersonalTrainer.ID == trainerid && a.Date >= currentDate)
                                    .Select(a => new
                                    {
                                        ID = a.ID,
                                        Date = a.Date.ToString("dd-MM-yyyy")
                                    })
                                    .ToList();
            ViewBag.Availability = new SelectList(filteredAvailability, "ID", "Date");

            return View(model);
        }

        [HttpGet]
        public IActionResult GetAvailability(int dateId)
        {
            var availability = _context.Availability
                .Where(a => a.ID == dateId)
                .Select(a => new { a.Date, a.StartTime, a.EndTime })
                .FirstOrDefault();

            if (availability != null)
            {
                var trainings = _context.Training
                    .AsEnumerable() 
                    .Where(t => t.StartDateTime.Date == availability.Date.Date &&
                                t.StartDateTime.TimeOfDay <= availability.EndTime &&
                                t.StartDateTime.TimeOfDay.Add(t.Duration) >= availability.StartTime)
                    .ToList();

                var availableHours = new List<TimeSpan>();

                for (var ts = availability.StartTime; ts < availability.EndTime; ts = ts.Add(TimeSpan.FromHours(1)))
                {
                    if (!trainings.Any(t => t.StartDateTime.TimeOfDay <= ts && ts < t.StartDateTime.TimeOfDay.Add(t.Duration)))
                    {
                        availableHours.Add(ts);
                    }
                }

               
                foreach (var training in trainings.Where(t => t.Duration.TotalHours > 2))
                {
                    var endHour = training.StartDateTime.TimeOfDay.Add(training.Duration);
                    availableHours.RemoveAll(ts => ts >= endHour && ts < endHour.Add(training.Duration));
                }

                if (availableHours.Any())
                {
                    return Json(new { AvailableHours = availableHours });
                }
            }

            return Json(new { Message = "Nie dostępne - jest trening" });
        }



        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> BookingForm( TrainingViewModel model)
        {
           
            var training = new TrainingViewModel
            {
                UserID = _userManager.GetUserId(User), 
                GymID = model.GymID,
                StartDateTime = model.StartDateTime, 
                Duration = model.Duration,
                TrainingType = model.TrainingType, 
                TrainingPackage = "0", 
                PersonalTrainersID = model.PersonalTrainersID, 
            };

            _context.Training.Add(training);
            await _context.SaveChangesAsync();

            ViewBag.Alert = "Dodano!";
            return RedirectToAction("Index", "Dashboard");

            
        }




    }
}
