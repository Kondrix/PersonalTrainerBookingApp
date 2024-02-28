using Xunit;
using NSubstitute;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using PersonalTrainerI.Controllers;
using PersonalTrainerI.Models;
using PersonalTrainerI.Data;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Controller;
using Moq;
using NuGet.DependencyResolver;
using StructureMap.AutoMocking;
using System.Security.Claims;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.Extensions.DependencyInjection;

namespace TestProject1PerT
{
    public class PersonalTrainerControllerTest
    {
        [Fact]
        public void Result_WithNullDate_ReturnsViewResult_WithCorrectModel()
        {
            // Ten test sprawdza, czy metoda `Result` poprawnie obs³uguje sytuacjê, gdy data jest null.

            // Arrange
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseSqlServer("Server=(localdb)\\mssqllocaldb;Database=aspnet-PersonalTrainer-eebebde1-aaed-4216-9973-a1767b0c4a37;Trusted_Connection=True;MultipleActiveResultSets=true")
                .Options;

            var context = new ApplicationDbContext(options);

            var controller = new PersonalTrainerController(null, context);

            string location = "TestLocation";
            string trainingType = "TestTrainingType";
            DateTime? date = null; 

            // Act
            var result = controller.Result(location, trainingType, date);

            // Assert
            var viewResult = Assert.IsType<ViewResult>(result);
            var model = Assert.IsAssignableFrom<List<PersonalTrainerViewModel>>(
                viewResult.ViewData.Model);
        }

        [Fact]
        public void Result_WithEmptyLocation_ReturnsViewResult_WithCorrectModel()
        {
            // Ten test sprawdza, czy metoda `Result` poprawnie obs³uguje sytuacjê, gdy lokalizacja jest pusta.

            // Arrange
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseSqlServer("Server=(localdb)\\mssqllocaldb;Database=aspnet-PersonalTrainer-eebebde1-aaed-4216-9973-a1767b0c4a37;Trusted_Connection=True;MultipleActiveResultSets=true")
                .Options;

            var context = new ApplicationDbContext(options);

            var controller = new PersonalTrainerController(null, context);

            string location = ""; 
            string trainingType = "TestTrainingType";
            DateTime? date = DateTime.Now;

            // Act
            var result = controller.Result(location, trainingType, date);

            // Assert
            var viewResult = Assert.IsType<ViewResult>(result);
            var model = Assert.IsAssignableFrom<List<PersonalTrainerViewModel>>(
                viewResult.ViewData.Model);
        }
        [Fact]
        public void Result_WithEmptyTrainingType_ReturnsViewResult_WithCorrectModel()
        {
            // Ten test sprawdza, czy metoda `Result` poprawnie obs³uguje sytuacjê, gdy lokalizacja jest pusta.

            // Arrange
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseSqlServer("Server=(localdb)\\mssqllocaldb;Database=aspnet-PersonalTrainer-eebebde1-aaed-4216-9973-a1767b0c4a37;Trusted_Connection=True;MultipleActiveResultSets=true")
                .Options;

            var context = new ApplicationDbContext(options);

            var controller = new PersonalTrainerController(null, context);

            string location = "City1";
            string trainingType = "";
            DateTime? date = DateTime.Now;

            // Act
            var result = controller.Result(location, trainingType, date);

            // Assert
            var viewResult = Assert.IsType<ViewResult>(result);
            var model = Assert.IsAssignableFrom<List<PersonalTrainerViewModel>>(
                viewResult.ViewData.Model);
        }
        [Fact]
        public void GetAvailability_ReturnsJsonResult_WithCorrectModel()
        {
            // Ten test sprawdza, czy metoda `GetAvailability` zwraca poprawny model JSON.

            // Arrange
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseSqlServer("Server=(localdb)\\mssqllocaldb;Database=aspnet-PersonalTrainer-eebebde1-aaed-4216-9973-a1767b0c4a37;Trusted_Connection=True;MultipleActiveResultSets=true")
                .Options;

            var context = new ApplicationDbContext(options);

            var controller = new PersonalTrainerController(null, context);

            int dateId = 1; 

          
            // Act
            var result = controller.GetAvailability(dateId);

            // Assert
            var jsonResult = Assert.IsType<JsonResult>(result);
        }



    }
}
