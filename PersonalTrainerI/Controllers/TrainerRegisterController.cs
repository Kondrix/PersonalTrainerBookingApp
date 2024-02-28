using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using PersonalTrainerI.Data;
using PersonalTrainerI.Models;
using System.Linq;
using Microsoft.Extensions.Logging;

namespace PersonalTrainerI.Controllers
{
    [Authorize]
    public class TrainerRegisterController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly ILogger<TrainerRegisterController> _logger;

        public TrainerRegisterController(UserManager<IdentityUser> userManager, ApplicationDbContext context, ILogger<TrainerRegisterController> logger)
        {
            _userManager = userManager;
            _context = context;
            _logger = logger;
        }

        [HttpGet]
        public async Task<IActionResult> IndexAsync()
        {
            var gyms = _context.Gym.ToList();
            var gymList = new List<SelectListItem>();

            foreach (var gym in gyms)
            {
                gymList.Add(new SelectListItem { Value = gym.ID.ToString(), Text = gym.GymName });
            }

            var user = await _userManager.FindByIdAsync(_userManager.GetUserId(User));
            var isPersonalTrainer = _context.PersonalTrainers.Any(pt=>pt.User == user);

            var model = new PersonalTrainerAndGymViewModel
            {
                PersonalTrainer = new PersonalTrainerViewModel(),
                GymList = gymList, 
                isPersonalTrainer = isPersonalTrainer
            };

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddNewGym(GymViewModel gym)
        {
          
            
                var newGym = new GymViewModel
                {
                    
                    ID= gym.ID,
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
        public async Task<IActionResult> AddT(PersonalTrainerAndGymViewModel model, List<string> gyms)
        {
            var user = await _userManager.FindByIdAsync(_userManager.GetUserId(User));
            var newTrainer = new PersonalTrainerViewModel
            {
                FirstName = model.PersonalTrainer.FirstName,
                LastName = model.PersonalTrainer.LastName,
                Rating = 0,
                TrainingType = model.PersonalTrainer.TrainingType,
                IsVerified = false,
                User = user
            };

            _context.PersonalTrainers.Add(newTrainer);
            await _context.SaveChangesAsync();

            foreach (var gymId in gyms)
            {
                var gymTrainer = new GymTrainerViewModel
                {
                    PersonalTrainerID = newTrainer.ID,
                    GymID = int.Parse(gymId)
                };
                _context.GymTrainer.Add(gymTrainer);
            }
            await _context.SaveChangesAsync();

            TempData["Alert"] = "Dodano!";
            return RedirectToAction("Index");
        }
      
    }
}
