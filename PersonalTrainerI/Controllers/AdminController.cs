using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PersonalTrainerI.Data;
using PersonalTrainerI.Models;

namespace PersonalTrainerI.Controllers
{
    [Authorize(Roles ="Admin") ]
    public class AdminController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<IdentityUser> _userManager;
        public AdminController(UserManager<IdentityUser> userManager, ApplicationDbContext context)
        {
            _userManager = userManager;
            _context = context;
            
        }
        public IActionResult Index()
        {
            var tables = new AdminViewModel
            {
                PersonalTrainers = _context.PersonalTrainers.Include(t=>t.Training)
                .Include(t=>t.User)
                .Include(t => t.GymTrainers)
                 .ThenInclude(gt => gt.Gym).ToList(),


            };
            return View(tables);
        }
        [HttpPost]
        public async Task<IActionResult> Verify(int id)
        {
            var trainer = await _context.PersonalTrainers
            .Include(t => t.User)
            .FirstOrDefaultAsync(t => t.ID == id);
            if (trainer == null)
            {
                return NotFound();
            }
            var user = trainer.User;
            var roles = await _userManager.GetRolesAsync(user);
            await _userManager.RemoveFromRolesAsync(user, roles);
            await _userManager.AddToRoleAsync(user, "Trainer");
            trainer.IsVerified = true;
           
            _context.Update(trainer);
            await _context.SaveChangesAsync();

            return RedirectToAction("Index", "Admin");
        }

        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            var trainer = await _context.PersonalTrainers
            .Include(t => t.User)
            .FirstOrDefaultAsync(t => t.ID == id);
            if (trainer == null)
            {
                return NotFound();
            }
            var user = trainer.User;
            var roles = await _userManager.GetRolesAsync(user);
            await _userManager.RemoveFromRolesAsync(user, roles);
            await _userManager.AddToRoleAsync(user, "Member");
            _context.PersonalTrainers.Remove(trainer);
            await _context.SaveChangesAsync();

            return RedirectToAction("Index", "Admin");
        }
        [HttpGet]
        public async Task<IActionResult> Details(int id)
        {
            var trainer = await _context.PersonalTrainers
                .Include(t => t.Training)
                .Include(t => t.GymTrainers)
                .ThenInclude(gt => gt.Gym)
                .FirstOrDefaultAsync(t => t.ID == id);

            if (trainer == null)
            {
                return NotFound();
            }

            return View(trainer);
        }
    }
}
