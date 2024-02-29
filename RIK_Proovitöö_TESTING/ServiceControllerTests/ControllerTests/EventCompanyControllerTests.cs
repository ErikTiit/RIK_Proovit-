using Microsoft.AspNetCore.Mvc;
using Moq;
using RIK_Proovitöö.Models;
using RIK_Proovitöö.Repository;

namespace RIK_Proovitöö.Tests
{
    [TestClass]
    public class EventCompanyControllerTests
    {
        private Mock<IEventCompanyRepository>? _mockRepo;
        private EventCompanyController? _controller;

        [TestInitialize]
        public void TestInitialize()
        {
            _mockRepo = new Mock<IEventCompanyRepository>();
            _controller = new EventCompanyController(_mockRepo.Object);
        }

        [TestMethod]
        public async Task GetEventCompanies_ReturnsOkResult_WithListOfEventCompanies()
        {
            // Arrange
            _mockRepo?.Setup(repo => repo.GetEventCompanies()).ReturnsAsync(GetTestEventCompanies());

            // Act
            var result = await _controller!.GetEventCompanies();

            // Assert
            var okResult = result as OkObjectResult;
            var eventCompanies = okResult?.Value as List<EventCompany>;

            Assert.IsNotNull(okResult);
            Assert.AreEqual(200, okResult.StatusCode);
            Assert.AreEqual(2, eventCompanies?.Count);
        }

        [TestMethod]
        public async Task GetEventCompany_ReturnsNotFound_WhenEventCompanyDoesNotExist()
        {
            // Arrange
            _mockRepo?.Setup(repo => repo.GetEventCompany(It.IsAny<int>(), It.IsAny<int>())).ReturnsAsync((EventCompany?)null);

            // Act
            var result = await _controller!.GetEventCompany(1, 1);

            // Assert
            Assert.IsInstanceOfType(result, typeof(NotFoundObjectResult));
        }

        [TestMethod]
        public async Task PutEventCompany_ReturnsBadRequest_WhenIdDoesNotMatch()
        {
            // Arrange
            var eventCompany = new EventCompany { EventID = 1, CompanyID = 1 };

            // Act
            var result = await _controller!.PutEventCompany(2, 2, eventCompany);

            // Assert
            Assert.IsInstanceOfType(result, typeof(BadRequestObjectResult));
        }

        [TestMethod]
        public async Task PostEventCompany_ReturnsCreatedAtAction_WithNewEventCompany()
        {
            // Arrange
            var eventCompany = new EventCompany { EventID = 1, CompanyID = 1 };

            // Act
            var result = await _controller!.PostEventCompany(eventCompany);

            // Assert
            var createdAtActionResult = result as CreatedAtActionResult;
            var returnedEventCompany = createdAtActionResult?.Value as EventCompany;

            Assert.IsNotNull(createdAtActionResult);
            Assert.AreEqual("GetEventCompany", createdAtActionResult.ActionName);
            Assert.AreEqual(eventCompany.EventID, returnedEventCompany?.EventID);
            Assert.AreEqual(eventCompany.CompanyID, returnedEventCompany?.CompanyID);
        }

        [TestMethod]
        public async Task DeleteEventCompany_ReturnsNotFound_WhenEventCompanyDoesNotExist()
        {
            // Arrange
            _mockRepo?.Setup(repo => repo.GetEventCompany(It.IsAny<int>(), It.IsAny<int>())).ReturnsAsync((EventCompany?)null);

            // Act
            var result = await _controller!.DeleteEventCompany(1, 1);

            // Assert
            Assert.IsInstanceOfType(result, typeof(NotFoundObjectResult));
        }

        [TestMethod]
        public async Task DeleteEventCompany_ReturnsNoContent_WhenEventCompanyExists()
        {
            // Arrange
            _mockRepo?.Setup(repo => repo.GetEventCompany(It.IsAny<int>(), It.IsAny<int>())).ReturnsAsync(new EventCompany());

            // Act
            var result = await _controller!.DeleteEventCompany(1, 1);

            // Assert
            Assert.IsInstanceOfType(result, typeof(OkResult));
        }

        private List<EventCompany> GetTestEventCompanies()
        {
            return new List<EventCompany>
            {
                new EventCompany { EventID = 1, CompanyID = 1 },
                new EventCompany { EventID = 2, CompanyID = 2 }
            };
        }
    }
}
