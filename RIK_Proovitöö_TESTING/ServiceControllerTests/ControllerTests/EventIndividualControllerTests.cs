using Microsoft.AspNetCore.Mvc;
using Moq;
using RIK_Proovitöö.Models;
using RIK_Proovitöö.Repository;


namespace RIK_Proovitöö.Tests
{
    [TestClass]
    public class EventIndividualControllerTests
    {
        private Mock<IEventIndividualRepository>? _mockRepo;
        private EventIndividualController? _controller;

        [TestInitialize]
        public void TestInitialize()
        {
            _mockRepo = new Mock<IEventIndividualRepository>();
            _controller = new EventIndividualController(_mockRepo.Object);
        }

        [TestMethod]
        public async Task GetEventIndividuals_ReturnsOkResult_WithListOfEventIndividuals()
        {
            // Arrange
            _mockRepo?.Setup(repo => repo.GetEventIndividuals()).ReturnsAsync(GetTestEventIndividuals());

            // Act
            var result = await _controller!.GetEventIndividuals();

            // Assert
            var okResult = result as OkObjectResult;
            var eventIndividuals = okResult?.Value as List<EventIndividual>;

            Assert.IsNotNull(okResult);
            Assert.AreEqual(200, okResult.StatusCode);
            Assert.AreEqual(2, eventIndividuals?.Count);
        }

        [TestMethod]
        public async Task GetEventIndividual_ReturnsNotFound_WhenEventIndividualDoesNotExist()
        {
            // Arrange
            _mockRepo?.Setup(repo => repo.GetEventIndividual(It.IsAny<int>(), It.IsAny<int>())).ReturnsAsync((EventIndividual?)null);

            // Act
            var result = await _controller!.GetEventIndividual(1, 1);

            // Assert
            Assert.IsInstanceOfType(result, typeof(NotFoundObjectResult));
        }

        [TestMethod]
        public async Task PutEventIndividual_ReturnsBadRequest_WhenIdDoesNotMatch()
        {
            // Arrange
            var eventIndividual = new EventIndividual { EventID = 1, IndividualID = 1 };

            // Act
            var result = await _controller!.PutEventIndividual(2, 2, eventIndividual);

            // Assert
            Assert.IsInstanceOfType(result, typeof(BadRequestObjectResult));
        }

        [TestMethod]
        public async Task PostEventIndividual_ReturnsCreatedAtAction_WithNewEventIndividual()
        {
            // Arrange
            var eventIndividual = new EventIndividual { EventID = 1, IndividualID = 1 };

            // Act
            var result = await _controller!.PostEventIndividual(eventIndividual);

            // Assert
            var createdAtActionResult = result as CreatedAtActionResult;
            var returnedEventIndividual = createdAtActionResult?.Value as EventIndividual;

            Assert.IsNotNull(createdAtActionResult);
            Assert.AreEqual("GetEventIndividual", createdAtActionResult.ActionName);
            Assert.AreEqual(eventIndividual.EventID, returnedEventIndividual?.EventID);
            Assert.AreEqual(eventIndividual.IndividualID, returnedEventIndividual?.IndividualID);
        }

        [TestMethod]
        public async Task DeleteEventIndividual_ReturnsNotFound_WhenEventIndividualDoesNotExist()
        {
            // Arrange
            _mockRepo?.Setup(repo => repo.GetEventIndividual(It.IsAny<int>(), It.IsAny<int>())).ReturnsAsync((EventIndividual?)null);

            // Act
            var result = await _controller!.DeleteEventIndividual(1, 1);

            // Assert
            Assert.IsInstanceOfType(result, typeof(NotFoundObjectResult));
        }

        [TestMethod]
        public async Task DeleteEventIndividual_ReturnsNoContent_WhenEventIndividualExists()
        {
            // Arrange
            _mockRepo?.Setup(repo => repo.GetEventIndividual(It.IsAny<int>(), It.IsAny<int>())).ReturnsAsync(new EventIndividual());

            // Act
            var result = await _controller!.DeleteEventIndividual(1, 1);

            // Assert
            Assert.IsInstanceOfType(result, typeof(OkResult));
        }

        private List<EventIndividual> GetTestEventIndividuals()
        {
            return new List<EventIndividual>
            {
                new EventIndividual { EventID = 1, IndividualID = 1 },
                new EventIndividual { EventID = 2, IndividualID = 2 }
            };
        }
    }
}
