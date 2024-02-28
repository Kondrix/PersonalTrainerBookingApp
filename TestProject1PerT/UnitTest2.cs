using Xunit;
using Microsoft.AspNetCore.Mvc;
using PersonalTrainerI.Controllers;
using PersonalTrainerI.Models;
using PersonalTrainerI.Data;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace TestProject1PerT
{
    public class HomeControllerTests
    {
        [Fact]
        public async Task Index_ReturnsAViewResult_WithAListOfTrainings()
        {
            // Arrange
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseSqlServer("Server=(localdb)\\mssqllocaldb;Database=aspnet-PersonalTrainer-eebebde1-aaed-4216-9973-a1767b0c4a37;Trusted_Connection=True;MultipleActiveResultSets=true")
                .Options;

            var context = new ApplicationDbContext(options);

            var controller = new HomeController(null, context);

            // Act
            var result = await controller.Index();

            // Assert
            var viewResult = Assert.IsType<ViewResult>(result);
            var model = Assert.IsAssignableFrom<List<GymViewModel>>(
                viewResult.ViewData.Model);
        }

    }
}
