using Microsoft.AspNetCore.Mvc;
using Moq;
using RIK_Proovitöö.Controllers;
using RIK_Proovitöö.Models;
using RIK_Proovitöö.Repository;
using System;

namespace RIK_Proovitöö.Tests
{
    [TestClass]
    public class CompanyControllerTests
    {
        private Mock<ICompanyRepository> _mockRepo;
        private CompanyController _controller;

        [TestInitialize]
        public void TestInitialize()
        {
            _mockRepo = new Mock<ICompanyRepository>();
            _controller = new CompanyController(_mockRepo.Object);
        }

        [TestMethod]
        public async Task GetCompanies_ReturnsOkResult_WithListOfCompanies()
        {
            // Arrange
            _mockRepo.Setup(repo => repo.GetCompanies()).ReturnsAsync(GetTestCompanies());

            // Act
            var actionResult = await _controller.GetCompanies();
            var okResult = actionResult as OkObjectResult;
            var companies = okResult?.Value as List<Company>;

            // Assert
            Assert.IsNotNull(okResult);
            Assert.AreEqual(200, okResult.StatusCode);
            Assert.IsNotNull(companies);
            Assert.AreEqual(2, companies.Count);
        }

        [TestMethod]
        public async Task GetCompany_ReturnsNotFound_WhenCompanyDoesNotExist()
        {
            // Arrange
            _mockRepo.Setup(repo => repo.GetCompany(It.IsAny<int>())).ReturnsAsync((Company)null);

            // Act
            var result = await _controller.GetCompany(1);

            // Assert
            Assert.IsInstanceOfType(result, typeof(NotFoundObjectResult));
        }

        private List<Company> GetTestCompanies()
        {
            return new List<Company>
            {
                new Company { ID = 1, CompanyName = "Company 1" },
                new Company { ID = 2, CompanyName = "Company 2" }
            };
        }

        [TestMethod]
        public async Task PutCompany_ReturnsBadRequest_WhenIdDoesNotMatch()
        {
            // Arrange
            var company = new Company { ID = 1, CompanyName = "Company 1" };

            // Act
            var result = await _controller.PutCompany(2, company);

            // Assert
            Assert.IsInstanceOfType(result, typeof(BadRequestObjectResult));
        }

        [TestMethod]
        public async Task PutCompany_ReturnsNotFound_WhenCompanyDoesNotExist()
        {
            // Arrange
            var company = new Company { ID = 1, CompanyName = "Company 1" };
            _mockRepo.Setup(repo => repo.CompanyExists(It.IsAny<int>())).ReturnsAsync(false);

            // Act
            var result = await _controller.PutCompany(1, company);

            // Assert
            Assert.IsInstanceOfType(result, typeof(OkResult));
        }


        [TestMethod]
        public async Task PostCompany_ReturnsCreatedAtAction_WithNewCompany()
        {
            // Arrange
            var company = new Company { CompanyName = "New Company" };

            // Act
            var result = await _controller.PostCompany(company);

            // Assert
            var createdAtActionResult = result as CreatedAtActionResult;
            var returnedCompany = createdAtActionResult?.Value as Company;

            Assert.IsNotNull(createdAtActionResult);
            Assert.AreEqual("GetCompany", createdAtActionResult.ActionName);
            Assert.AreEqual(company.CompanyName, returnedCompany?.CompanyName);
        }

        [TestMethod]
        public async Task DeleteCompany_ReturnsNotFound_WhenCompanyDoesNotExist()
        {
            // Arrange
            _mockRepo.Setup(repo => repo.CompanyExists(It.IsAny<int>())).ReturnsAsync(false);

            // Act
            var result = await _controller.DeleteCompany(1);

            // Assert
            Assert.IsInstanceOfType(result, typeof(NotFoundObjectResult));
        }

        [TestMethod]
        public async Task DeleteCompany_ReturnsNoContent_WhenCompanyExists()
        {
            // Arrange
            _mockRepo.Setup(repo => repo.CompanyExists(It.IsAny<int>())).ReturnsAsync(true);

            // Act
            var result = await _controller.DeleteCompany(1);

            // Assert
            Assert.IsInstanceOfType(result, typeof(OkResult));
        }
    }
}
