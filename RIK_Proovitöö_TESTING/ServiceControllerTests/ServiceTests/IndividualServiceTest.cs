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
    public class IndividualServiceTests
    {

        private HttpClient _http;
        private Mock<IConfiguration> _config;
        private IndividualService _service;

        [TestInitialize]
        public void TestInitialize()
        {

            _config = new Mock<IConfiguration>();
            _service = new IndividualService(_http, _config.Object);
        }

        [TestMethod]
        public async Task GetIndividualAsync_ReturnsIndividual_WhenApiCallIsSuccessful()
        {
            // Arrange
            var individual = new Individual { ID = 1, FirstName = "Test Individual" };
            var messageHandler = new Mock<HttpMessageHandler>();
            messageHandler.Protected()
                .Setup<Task<HttpResponseMessage>>("SendAsync", ItExpr.IsAny<HttpRequestMessage>(), ItExpr.IsAny<CancellationToken>())
                .ReturnsAsync(new HttpResponseMessage { StatusCode = HttpStatusCode.OK, Content = new StringContent(JsonConvert.SerializeObject(individual)) });
            var httpClient = new HttpClient(messageHandler.Object)
            {
                BaseAddress = new Uri("http://localhost") 
            };
            _config.Setup(c => c["ApiEndpoints:Individual"]).Returns("/api/Individual");
            _service = new IndividualService(httpClient, _config.Object);

            // Act
            var result = await _service.GetIndividualAsync(1);

            // Assert
            Assert.AreEqual(individual.ID, result.ID);
            Assert.AreEqual(individual.FirstName, result.FirstName);
        }


        [TestMethod]
        public async Task UpdateIndividualAsync_CallsApi_WithCorrectParameters()
        {
            // Arrange
            var individual = new Individual { ID = 1, FirstName = "Test Individual" };
            var messageHandler = new Mock<HttpMessageHandler>();
            messageHandler.Protected()
                .Setup<Task<HttpResponseMessage>>("SendAsync", ItExpr.Is<HttpRequestMessage>(req => req.Method == HttpMethod.Put && req.RequestUri.ToString().EndsWith("/1")), ItExpr.IsAny<CancellationToken>())
                .ReturnsAsync(new HttpResponseMessage { StatusCode = HttpStatusCode.OK });
            var httpClient = new HttpClient(messageHandler.Object)
            {
                BaseAddress = new Uri("http://localhost") 
            };
            _config.Setup(c => c["ApiEndpoints:Individual"]).Returns("/api/Individual");
            _service = new IndividualService(httpClient, _config.Object);

            // Act
            await _service.UpdateIndividualAsync(1, individual);

            // Assert
            messageHandler.Protected().Verify("SendAsync", Times.Exactly(1), ItExpr.Is<HttpRequestMessage>(req => req.Method == HttpMethod.Put && req.RequestUri.ToString().EndsWith("/1")), ItExpr.IsAny<CancellationToken>());
        }



    }
}
