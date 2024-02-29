using Microsoft.AspNetCore.Mvc;
using Moq;
using RIK_Proovitöö.Models;
using RIK_Proovitöö.Repository;

namespace RIK_Proovitöö.Tests
{
    [TestClass]
    public class EventsControllerTests
    {
        private Mock<IEventRepository>? _mockRepo;
        private EventsController? _controller;

        [TestInitialize]
        public void TestInitialize()
        {
            _mockRepo = new Mock<IEventRepository>();
            _controller = new EventsController(_mockRepo.Object);
        }

        [TestMethod]
        public async Task GetEvents_ReturnsOkResult_WithListOfEvents()
        {
            // Arrange
            _mockRepo?.Setup(repo => repo.GetEvents()).ReturnsAsync(GetTestEvents());

            // Act
            var result = await _controller!.GetEvents();

            // Assert
            var okResult = result as OkObjectResult;
            var events = okResult?.Value as List<Event>;

            Assert.IsNotNull(okResult);
            Assert.AreEqual(200, okResult.StatusCode);
            Assert.AreEqual(2, events?.Count);
        }

        [TestMethod]
        public async Task GetEvent_ReturnsNotFound_WhenEventDoesNotExist()
        {
            // Arrange
            _mockRepo?.Setup(repo => repo.GetEvent(It.IsAny<int>())).ReturnsAsync((Event?)null);

            // Act
            var result = await _controller!.GetEvent(1);

            // Assert
            Assert.IsInstanceOfType(result, typeof(NotFoundObjectResult));
        }

        [TestMethod]
        public async Task PutEvent_ReturnsBadRequest_WhenIdDoesNotMatch()
        {
            // Arrange
            var eventItem = new Event { ID = 1, Name = "Event 1", Date = DateTime.Now.AddDays(1) };

            // Act
            var result = await _controller!.PutEvent(2, eventItem);

            // Assert
            Assert.IsInstanceOfType(result, typeof(BadRequestObjectResult));
        }

        [TestMethod]
        public async Task PostEvent_ReturnsCreatedAtAction_WithNewEvent()
        {
            // Arrange
            var eventItem = new Event { Name = "New Event", Date = DateTime.Now.AddDays(1) };

            // Act
            var result = await _controller!.PostEvent(eventItem);

            // Assert
            var createdAtActionResult = result as CreatedAtActionResult;
            var returnedEvent = createdAtActionResult?.Value as Event;

            Assert.IsNotNull(createdAtActionResult);
            Assert.AreEqual("GetEvent", createdAtActionResult.ActionName);
            Assert.AreEqual(eventItem.Name, returnedEvent?.Name);
        }

        [TestMethod]
        public async Task DeleteEvent_ReturnsNotFound_WhenEventDoesNotExist()
        {
            // Arrange
            _mockRepo?.Setup(repo => repo.GetEvent(It.IsAny<int>())).ReturnsAsync((Event?)null);

            // Act
            var result = await _controller!.DeleteEvent(1);

            // Assert
            Assert.IsInstanceOfType(result, typeof(NotFoundObjectResult));
        }

        [TestMethod]
        public async Task DeleteEvent_ReturnsNoContent_WhenEventExists()
        {
            // Arrange
            _mockRepo?.Setup(repo => repo.GetEvent(It.IsAny<int>())).ReturnsAsync(new Event());

            // Act
            var result = await _controller!.DeleteEvent(1);

            // Assert
            Assert.IsInstanceOfType(result, typeof(OkResult));
        }

        private List<Event> GetTestEvents()
        {
            return new List<Event>
            {
                new Event { ID = 1, Name = "Event 1", Date = DateTime.Now.AddDays(1) },
                new Event { ID = 2, Name = "Event 2", Date = DateTime.Now.AddDays(2) }
            };
        }
    }
}
