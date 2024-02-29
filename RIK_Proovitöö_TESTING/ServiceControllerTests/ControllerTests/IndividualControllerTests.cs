using Microsoft.AspNetCore.Mvc;
using Moq;
using RIK_Proovitöö.Models;
using RIK_Proovitöö.Repository;

namespace RIK_Proovitöö.Tests
{
    [TestClass]
    public class IndividualControllerTests
    {
        private Mock<IIndividualRepository>? _mockRepo;
        private IndividualController? _controller;

        [TestInitialize]
        public void TestInitialize()
        {
            _mockRepo = new Mock<IIndividualRepository>();
            _controller = new IndividualController(_mockRepo.Object);
        }

        [TestMethod]
        public async Task GetIndividuals_ReturnsOkResult_WithListOfIndividuals()
        {
            // Arrange
            _mockRepo?.Setup(repo => repo.GetIndividuals()).ReturnsAsync(GetTestIndividuals());

            // Act
            var result = await _controller!.GetIndividuals();

            // Assert
            var okResult = result as OkObjectResult;
            var individuals = okResult?.Value as List<Individual>;

            Assert.IsNotNull(okResult);
            Assert.AreEqual(200, okResult.StatusCode);
            Assert.AreEqual(2, individuals?.Count);
        }

        [TestMethod]
        public async Task GetIndividual_ReturnsNotFound_WhenIndividualDoesNotExist()
        {
            // Arrange
            _mockRepo?.Setup(repo => repo.GetIndividual(It.IsAny<int>())).ReturnsAsync((Individual?)null);

            // Act
            var result = await _controller!.GetIndividual(1);

            // Assert
            Assert.IsInstanceOfType(result, typeof(NotFoundObjectResult));
        }

        [TestMethod]
        public async Task PutIndividual_ReturnsBadRequest_WhenIdDoesNotMatch()
        {
            // Arrange
            var individual = new Individual { ID = 1, FirstName = "Individual 1" };

            // Act
            var result = await _controller!.PutIndividual(2, individual);

            // Assert
            Assert.IsInstanceOfType(result, typeof(BadRequestObjectResult));
        }

        [TestMethod]
        public async Task PostIndividual_ReturnsCreatedAtAction_WithNewIndividual()
        {
            // Arrange
            var individual = new Individual { FirstName = "New Individual" };

            // Act
            var result = await _controller!.PostIndividual(individual);

            // Assert
            var createdAtActionResult = result as CreatedAtActionResult;
            var returnedIndividual = createdAtActionResult?.Value as Individual;

            Assert.IsNotNull(createdAtActionResult);
            Assert.AreEqual("GetIndividual", createdAtActionResult.ActionName);
            Assert.AreEqual(individual.FirstName, returnedIndividual?.FirstName);
        }

        [TestMethod]
        public async Task DeleteIndividual_ReturnsNotFound_WhenIndividualDoesNotExist()
        {
            // Arrange
            _mockRepo?.Setup(repo => repo.GetIndividual(It.IsAny<int>())).ReturnsAsync((Individual?)null);

            // Act
            var result = await _controller!.DeleteIndividual(1);

            // Assert
            Assert.IsInstanceOfType(result, typeof(NotFoundObjectResult));
        }

        [TestMethod]
        public async Task DeleteIndividual_ReturnsNoContent_WhenIndividualExists()
        {
            // Arrange
            _mockRepo?.Setup(repo => repo.GetIndividual(It.IsAny<int>())).ReturnsAsync(new Individual());

            // Act
            var result = await _controller!.DeleteIndividual(1);

            // Assert
            Assert.IsInstanceOfType(result, typeof(OkResult));
        }

        private List<Individual> GetTestIndividuals()
        {
            return new List<Individual>
            {
                new Individual { ID = 1, FirstName = "Individual 1" },
                new Individual { ID = 2, FirstName = "Individual 2" }
            };
        }
    }
}
