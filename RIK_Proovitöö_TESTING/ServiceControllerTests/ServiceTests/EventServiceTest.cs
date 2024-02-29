using Microsoft.Extensions.Configuration;
using Moq;
using Moq.Protected;
using Newtonsoft.Json;
using RIK_Proovitöö.Models;
using RIK_Proovitöö.Services;
using System.Net;


namespace RIK_Proovitöö.Tests
{
    [TestClass]
    public class EventServiceTests
    {
        private Mock<HttpMessageHandler> _handler;
        private HttpClient _http;
        private Mock<IConfiguration> _config;
        private EventService _service;

        [TestInitialize]
        public void TestInitialize()
        {
            _handler = new Mock<HttpMessageHandler>();
            _http = new HttpClient(_handler.Object)
            {
                BaseAddress = new Uri("http://localhost") 
            };
            _config = new Mock<IConfiguration>();
            _service = new EventService(_http, _config.Object);
        }

        [TestMethod]
        public async Task GetEventsAsync_ReturnsEvents_WhenApiCallIsSuccessful()
        {
            // Arrange
            var events = new Event[] { new Event { ID = 1, Name = "Test Event" } };
            _handler.Protected()
                .Setup<Task<HttpResponseMessage>>("SendAsync", ItExpr.IsAny<HttpRequestMessage>(), ItExpr.IsAny<CancellationToken>())
                .ReturnsAsync(new HttpResponseMessage { StatusCode = HttpStatusCode.OK, Content = new StringContent(JsonConvert.SerializeObject(events)) });
            _config.Setup(c => c["ApiEndpoints:Events"]).Returns("/api/Events");

            // Act
            var result = await _service.GetEventsAsync();

            // Assert
            Assert.AreEqual(events.Length, result.Length);
            Assert.AreEqual(events[0].ID, result[0].ID);
            Assert.AreEqual(events[0].Name, result[0].Name);
        }

        [TestMethod]
        public async Task DeleteEventAsync_CallsApi_WithCorrectParameters()
        {
            // Arrange
            var eventId = 1;
            _handler.Protected()
                .Setup<Task<HttpResponseMessage>>("SendAsync", ItExpr.Is<HttpRequestMessage>(req => req.Method == HttpMethod.Delete && req.RequestUri.ToString().EndsWith($"/{eventId}")), ItExpr.IsAny<CancellationToken>())
                .ReturnsAsync(new HttpResponseMessage { StatusCode = HttpStatusCode.OK });
            _config.Setup(c => c["ApiEndpoints:Events"]).Returns("/api/Events");
            _service = new EventService(_http, _config.Object);

            // Act
            await _service.DeleteEventAsync(eventId);

            // Assert
            _handler.Protected().Verify("SendAsync", Times.Exactly(1), ItExpr.Is<HttpRequestMessage>(req => req.Method == HttpMethod.Delete && req.RequestUri.ToString().EndsWith($"/{eventId}")), ItExpr.IsAny<CancellationToken>());
        }

        [TestMethod]
        public async Task GetEventAsync_ReturnsEvent_WhenApiCallIsSuccessful()
        {
            // Arrange
            var eventId = 1;
            var eventObj = new Event { ID = eventId, Name = "Test Event" };
            _handler.Protected()
                .Setup<Task<HttpResponseMessage>>("SendAsync", ItExpr.IsAny<HttpRequestMessage>(), ItExpr.IsAny<CancellationToken>())
                .ReturnsAsync(new HttpResponseMessage { StatusCode = HttpStatusCode.OK, Content = new StringContent(JsonConvert.SerializeObject(eventObj)) });
            _config.Setup(c => c["ApiEndpoints:Events"]).Returns("/api/Events");
            _service = new EventService(_http, _config.Object);

            // Act
            var result = await _service.GetEventAsync(eventId);

            // Assert
            Assert.AreEqual(eventObj.ID, result.ID);
            Assert.AreEqual(eventObj.Name, result.Name);
        }

        [TestMethod]
        public async Task GetEventIndividualsAsync_ReturnsEventIndividuals_WhenApiCallIsSuccessful()
        {
            // Arrange
            var eventId = 1;
            var eventIndividuals = new List<EventIndividual> { new EventIndividual { EventID = eventId, IndividualID = 1 } };
            _handler.Protected()
                .Setup<Task<HttpResponseMessage>>("SendAsync", ItExpr.IsAny<HttpRequestMessage>(), ItExpr.IsAny<CancellationToken>())
                .ReturnsAsync(new HttpResponseMessage { StatusCode = HttpStatusCode.OK, Content = new StringContent(JsonConvert.SerializeObject(eventIndividuals)) });
            _config.Setup(c => c["ApiEndpoints:EventIndividual"]).Returns("/api/EventIndividual/Event");
            _service = new EventService(_http, _config.Object);

            // Act
            var result = await _service.GetEventIndividualsAsync(eventId);

            // Assert
            Assert.AreEqual(eventIndividuals.Count, result.Count);
            Assert.AreEqual(eventIndividuals[0].EventID, result[0].EventID);
            Assert.AreEqual(eventIndividuals[0].IndividualID, result[0].IndividualID);
        }

        [TestMethod]
        public async Task GetEventCompaniesAsync_ReturnsEventCompanies_WhenApiCallIsSuccessful()
        {
            // Arrange
            var eventId = 1;
            var eventCompanies = new List<EventCompany> { new EventCompany { EventID = eventId, CompanyID = 1 } };
            _handler.Protected()
                .Setup<Task<HttpResponseMessage>>("SendAsync", ItExpr.IsAny<HttpRequestMessage>(), ItExpr.IsAny<CancellationToken>())
                .ReturnsAsync(new HttpResponseMessage { StatusCode = HttpStatusCode.OK, Content = new StringContent(JsonConvert.SerializeObject(eventCompanies)) });
            _config.Setup(c => c["ApiEndpoints:EventCompany"]).Returns("/api/EventCompany/Event");
            _service = new EventService(_http, _config.Object);

            // Act
            var result = await _service.GetEventCompaniesAsync(eventId);

            // Assert
            Assert.AreEqual(eventCompanies.Count, result.Count);
            Assert.AreEqual(eventCompanies[0].EventID, result[0].EventID);
            Assert.AreEqual(eventCompanies[0].CompanyID, result[0].CompanyID);
        }

        [TestMethod]
        public async Task GetIndividualAsync_ReturnsIndividual_WhenApiCallIsSuccessful()
        {
            // Arrange
            var individualId = 1;
            var individual = new Individual { ID = individualId, FirstName = "Test Individual" };
            _handler.Protected()
                .Setup<Task<HttpResponseMessage>>("SendAsync", ItExpr.IsAny<HttpRequestMessage>(), ItExpr.IsAny<CancellationToken>())
                .ReturnsAsync(new HttpResponseMessage { StatusCode = HttpStatusCode.OK, Content = new StringContent(JsonConvert.SerializeObject(individual)) });
            _config.Setup(c => c["ApiEndpoints:Individual"]).Returns("/api/Individual");
            _service = new EventService(_http, _config.Object);

            // Act
            var result = await _service.GetIndividualAsync(individualId);

            // Assert
            Assert.AreEqual(individual.ID, result.ID);
            Assert.AreEqual(individual.FirstName, result.FirstName);
        }

        [TestMethod]
        public async Task GetCompanyAsync_ReturnsCompany_WhenApiCallIsSuccessful()
        {
            // Arrange
            var companyId = 1;
            var company = new Company { ID = companyId, CompanyName = "Test Company" };
            _handler.Protected()
                .Setup<Task<HttpResponseMessage>>("SendAsync", ItExpr.IsAny<HttpRequestMessage>(), ItExpr.IsAny<CancellationToken>())
                .ReturnsAsync(new HttpResponseMessage { StatusCode = HttpStatusCode.OK, Content = new StringContent(JsonConvert.SerializeObject(company)) });
            _config.Setup(c => c["ApiEndpoints:Company"]).Returns("/api/Company");
            _service = new EventService(_http, _config.Object);

            // Act
            var result = await _service.GetCompanyAsync(companyId);

            // Assert
            Assert.AreEqual(company.ID, result.ID);
            Assert.AreEqual(company.CompanyName, result.CompanyName);
        }

        [TestMethod]
        public async Task DeleteIndividualAttendeeAsync_CallsApi_WithCorrectParameters()
        {
            // Arrange
            var eventId = 1;
            var individualId = 1;
            _handler.Protected()
                .Setup<Task<HttpResponseMessage>>("SendAsync", ItExpr.Is<HttpRequestMessage>(req => req.Method == HttpMethod.Delete && req.RequestUri.ToString().EndsWith($"/{eventId}/{individualId}")), ItExpr.IsAny<CancellationToken>())
                .ReturnsAsync(new HttpResponseMessage { StatusCode = HttpStatusCode.OK });
            _config.Setup(c => c["ApiEndpoints:DeletingIndividual"]).Returns("/api/EventIndividual");
            _service = new EventService(_http, _config.Object);

            // Act
            await _service.DeleteIndividualAttendeeAsync(eventId, individualId);

            // Assert
            _handler.Protected().Verify("SendAsync", Times.Exactly(1), ItExpr.Is<HttpRequestMessage>(req => req.Method == HttpMethod.Delete && req.RequestUri.ToString().EndsWith($"/{eventId}/{individualId}")), ItExpr.IsAny<CancellationToken>());
        }

        [TestMethod]
        public async Task DeleteCompanyAttendeeAsync_CallsApi_WithCorrectParameters()
        {
            // Arrange
            var eventId = 1;
            var companyId = 1;
            _handler.Protected()
                .Setup<Task<HttpResponseMessage>>("SendAsync", ItExpr.Is<HttpRequestMessage>(req => req.Method == HttpMethod.Delete && req.RequestUri.ToString().EndsWith($"/{eventId}/{companyId}")), ItExpr.IsAny<CancellationToken>())
                .ReturnsAsync(new HttpResponseMessage { StatusCode = HttpStatusCode.OK });
            _config.Setup(c => c["ApiEndpoints:DeletingCompany"]).Returns("/api/EventCompany");
            _service = new EventService(_http, _config.Object);

            // Act
            await _service.DeleteCompanyAttendeeAsync(eventId, companyId);

            // Assert
            _handler.Protected().Verify("SendAsync", Times.Exactly(1), ItExpr.Is<HttpRequestMessage>(req => req.Method == HttpMethod.Delete && req.RequestUri.ToString().EndsWith($"/{eventId}/{companyId}")), ItExpr.IsAny<CancellationToken>());
        }

        [TestMethod]
        public async Task CreateIndividualAsync_CallsApi_WithCorrectParameters()
        {
            // Arrange
            var individual = new Individual { ID = 1, FirstName = "Test Individual" };
            _handler.Protected()
                .Setup<Task<HttpResponseMessage>>("SendAsync", ItExpr.Is<HttpRequestMessage>(req => req.Method == HttpMethod.Post), ItExpr.IsAny<CancellationToken>())
                .ReturnsAsync(new HttpResponseMessage { StatusCode = HttpStatusCode.Created });
            _config.Setup(c => c["ApiEndpoints:Individual"]).Returns("/api/Individual");
            _service = new EventService(_http, _config.Object);

            // Act
            var response = await _service.CreateIndividualAsync(individual);

            // Assert
            Assert.AreEqual(HttpStatusCode.Created, response.StatusCode);
            _handler.Protected().Verify("SendAsync", Times.Exactly(1), ItExpr.Is<HttpRequestMessage>(req => req.Method == HttpMethod.Post && IsExpectedContent(req.Content, individual)), ItExpr.IsAny<CancellationToken>());
        }

        [TestMethod]
        public async Task CreateEventIndividualAsync_CallsApi_WithCorrectParameters()
        {
            // Arrange
            var eventIndividual = new EventIndividual { EventID = 1, IndividualID = 1 };
            _handler.Protected()
                .Setup<Task<HttpResponseMessage>>("SendAsync", ItExpr.Is<HttpRequestMessage>(req => req.Method == HttpMethod.Post), ItExpr.IsAny<CancellationToken>())
                .ReturnsAsync(new HttpResponseMessage { StatusCode = HttpStatusCode.Created });
            _config.Setup(c => c["ApiEndpoints:AddingIndividual"]).Returns("/api/EventIndividual");
            _service = new EventService(_http, _config.Object);

            // Act
            var response = await _service.CreateEventIndividualAsync(eventIndividual);

            // Assert
            Assert.AreEqual(HttpStatusCode.Created, response.StatusCode);
            _handler.Protected().Verify("SendAsync", Times.Exactly(1), ItExpr.Is<HttpRequestMessage>(req => req.Method == HttpMethod.Post && IsExpectedContent(req.Content, eventIndividual)), ItExpr.IsAny<CancellationToken>());
        }

        [TestMethod]
        public async Task CreateEventCompanyAsync_CallsApi_WithCorrectParameters()
        {
            // Arrange
            var eventCompany = new EventCompany { EventID = 1, CompanyID = 1 };
            _handler.Protected()
                .Setup<Task<HttpResponseMessage>>("SendAsync", ItExpr.Is<HttpRequestMessage>(req => req.Method == HttpMethod.Post), ItExpr.IsAny<CancellationToken>())
                .ReturnsAsync(new HttpResponseMessage { StatusCode = HttpStatusCode.Created });
            _config.Setup(c => c["ApiEndpoints:AddingCompany"]).Returns("/api/EventCompany");
            _service = new EventService(_http, _config.Object);

            // Act
            var response = await _service.CreateEventCompanyAsync(eventCompany);

            // Assert
            Assert.AreEqual(HttpStatusCode.Created, response.StatusCode);
            _handler.Protected().Verify("SendAsync", Times.Exactly(1), ItExpr.Is<HttpRequestMessage>(req => req.Method == HttpMethod.Post && IsExpectedContent(req.Content, eventCompany)), ItExpr.IsAny<CancellationToken>());
        }

        private bool IsExpectedContent(HttpContent content, object expectedObject)
        {
            var contentString = content.ReadAsStringAsync().Result;
            var objectFromContent = JsonConvert.DeserializeObject(contentString, expectedObject.GetType());

            if (expectedObject is EventCompany expectedEventCompany && objectFromContent is EventCompany eventCompanyFromContent)
            {
                return expectedEventCompany.EventID == eventCompanyFromContent.EventID && expectedEventCompany.CompanyID == eventCompanyFromContent.CompanyID;
            }

            if (expectedObject is EventIndividual expectedEventIndividual && objectFromContent is EventIndividual eventIndividualFromContent)
            {
                return expectedEventIndividual.EventID == eventIndividualFromContent.EventID && expectedEventIndividual.IndividualID == eventIndividualFromContent.IndividualID;
            }

            if (expectedObject is Individual expectedIndividual && objectFromContent is Individual individualFromContent)
            {
                return expectedIndividual.ID == individualFromContent.ID && expectedIndividual.FirstName == individualFromContent.FirstName;
            }

            return false;
        }



    }
}
