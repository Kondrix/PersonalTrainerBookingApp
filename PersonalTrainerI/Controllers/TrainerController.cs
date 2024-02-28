using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PersonalTrainerI.Data;
using PersonalTrainerI.Models;
using System.Data;
using System.Reflection.Metadata.Ecma335;

namespace PersonalTrainerI.Controllers
{
    [Authorize(Roles = "Trainer")]
    public class TrainerController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<IdentityUser> _userManager;
        public TrainerController(UserManager<IdentityUser> userManager, ApplicationDbContext context)
        {
            _userManager = userManager;
            _context = context;

        }
        public IActionResult Index()
        {


            return View();

        }
        public async Task<JsonResult> GetTrainingsAsync()
        {
            var trainer = await _userManager.GetUserAsync(User);
            var trainerId = trainer.Id;

            var trainings = _context.Training
                .Where(t => t.PersonalTrainers.User.Id == trainerId)
                .Include(t => t.Gym)
                .Include(t => t.PersonalTrainers)
                .Include(t => t.User)
                .Select(t => new
                {
                    title = "E-mail klienta: " + t.User.Email + "," + " Siłownia: " + t.Gym.GymName,
                    start = t.StartDateTime.ToString("s"),
                    end = t.StartDateTime.AddMinutes(t.Duration.TotalMinutes).ToString("s"),
                    gymName = t.Gym.GymName
                })
                .ToList();

            return Json(trainings);
        }
        public async Task<IActionResult> Availability()
        {
            var trainer = await _userManager.GetUserAsync(User);
            var trainerId = trainer.Id;
            var gymsWithoutTrainer = _context.Gym
              .Where(g => !_context.GymTrainer.Any(gt => gt.GymID == g.ID && gt.PersonalTrainer.User.Id == trainer.Id))
              .ToList();

            var availability = new GymAndAvailabilityViewModel
            {
                Availabilitie = _context.Availability
                .Where(t => t.PersonalTrainer.User.Id == trainerId)
                .OrderByDescending(t => t.Date)
                .ToList(),
                GymTrainer = _context.GymTrainer
                .Include(t => t.Gym)
                .Include(t => t.PersonalTrainer)
                .Where(t => t.PersonalTrainer.User == trainer)
                .ToList(),
                Gyms = gymsWithoutTrainer


            };

            return View(availability);


        }



        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(AvailabilityViewModel model)
        {
            var userId = _userManager.GetUserId(User);
            var trainer = _context.PersonalTrainers
                .FirstOrDefault(pt => pt.User.Id == userId);

            ModelState.Remove("PersonalTrainer");

            if (ModelState.IsValid)
            {

                model.PersonalTrainer = trainer;
                _context.Add(model);
                await _context.SaveChangesAsync();

                return RedirectToAction("Availability");
            }
            return View(model);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            var availabilityDelete = await _context.Availability

            .FirstOrDefaultAsync(t => t.ID == id);

            _context.Availability.Remove(availabilityDelete);
            await _context.SaveChangesAsync();

            return RedirectToAction("Availability", "Trainer");
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddNewGym(GymViewModel gym)
        {


            var newGym = new GymViewModel
            {

                ID = gym.ID,
                GymName = gym.GymName,
                Street = gym.Street,
                StreetNumber = gym.StreetNumber,
                City = gym.City

            };

            await _context.Gym.AddAsync(newGym);
            await _context.SaveChangesAsync();

            return Json(new { ID = newGym.ID, GymName = newGym.GymName });




        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddGymTrainer(List<string> gymtr)
        {
            var userfortrainerID = _userManager.GetUserId(User);

            // Use FirstOrDefault() instead of ToString() to get the PersonalTrainer entity
            var trainer = _context.PersonalTrainers
                .FirstOrDefault(t => t.User.Id == userfortrainerID);

            if (trainer == null)
            {
                // Handle the case where the trainer is not found
                return NotFound();
            }

            foreach (var gymId in gymtr)
            {
                var gymTrainer = new GymTrainerViewModel
                {
                    PersonalTrainerID = trainer.ID,
                    GymID = int.Parse(gymId)
                };

                _context.GymTrainer.Add(gymTrainer);
            }

            await _context.SaveChangesAsync();

            return RedirectToAction("Availability", "Trainer");
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteGym(int gtid)
        {
            var gymtrainerDelete = await _context.GymTrainer

            .FirstOrDefaultAsync(t => t.ID == gtid);

            _context.GymTrainer.Remove(gymtrainerDelete);
            await _context.SaveChangesAsync();

            return RedirectToAction("Availability", "Trainer");
        }
    }
}
