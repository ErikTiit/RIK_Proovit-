using Microsoft.Extensions.Configuration;
using Moq.Protected;
using Moq;
using Newtonsoft.Json;
using RIK_Proovitöö.Models;
using RIK_Proovitöö.Services;
using System.Net;
using System.Text;

namespace RIK_Proovitöö.Tests { 

[TestClass]
public class CompanyServiceTests
{
    private Mock<HttpMessageHandler> _mockHttpMessageHandler;
    private HttpClient _httpClient;
    private IConfiguration _config;
    private CompanyService _companyService;

    [TestInitialize]
    public void Setup()
    {
        _mockHttpMessageHandler = new Mock<HttpMessageHandler>();
        _httpClient = new HttpClient(_mockHttpMessageHandler.Object);
        _config = new ConfigurationBuilder().AddInMemoryCollection(new Dictionary<string, string>
        {
            {"ApiEndpoints:Company", "http://localhost:5000/api/company"}
        }).Build();
        _companyService = new CompanyService(_httpClient, _config);
    }

    [TestMethod]
    public async Task GetCompanyAsync_ReturnsCompany()
    {
        // Arrange
        var expectedCompany = new Company { ID = 1, CompanyName = "Test Company" };
        var httpResponseMessage = new HttpResponseMessage
        {
            StatusCode = HttpStatusCode.OK,
            Content = new StringContent(JsonConvert.SerializeObject(expectedCompany), Encoding.UTF8, "application/json")
        };
        _mockHttpMessageHandler
            .Protected()
            .Setup<Task<HttpResponseMessage>>(
                "SendAsync",
                ItExpr.IsAny<HttpRequestMessage>(),
                ItExpr.IsAny<CancellationToken>()
            )
            .ReturnsAsync(httpResponseMessage);

        // Act
        var company = await _companyService.GetCompanyAsync(1);

        // Assert
        Assert.AreEqual(expectedCompany.ID, company.ID);
        Assert.AreEqual(expectedCompany.CompanyName, company.CompanyName);
    }
    [TestMethod]
    public async Task UpdateCompanyAsync_CallsPutAsJsonAsyncWithCorrectParameters()
    {
        // Arrange
        var company = new Company { ID = 1, CompanyName = "Test Company" };
        var httpResponseMessage = new HttpResponseMessage
        {
            StatusCode = HttpStatusCode.NoContent
        };
        _mockHttpMessageHandler
            .Protected()
            .Setup<Task<HttpResponseMessage>>(
                "SendAsync",
                ItExpr.Is<HttpRequestMessage>(req =>
                    req.Method == HttpMethod.Put &&
                    req.RequestUri == new Uri($"{_config["ApiEndpoints:Company"]}/{company.ID}") &&
                    JsonConvert.DeserializeObject<Company>(req.Content.ReadAsStringAsync().Result).CompanyName == company.CompanyName),
                ItExpr.IsAny<CancellationToken>()
            )
            .ReturnsAsync(httpResponseMessage);

        // Act
        await _companyService.UpdateCompanyAsync(company.ID, company);

        // Assert
        _mockHttpMessageHandler
            .Protected()
            .Verify(
                "SendAsync",
                Times.Exactly(1),
                ItExpr.Is<HttpRequestMessage>(req =>
                    req.Method == HttpMethod.Put &&
                    req.RequestUri == new Uri($"{_config["ApiEndpoints:Company"]}/{company.ID}") &&
                    JsonConvert.DeserializeObject<Company>(req.Content.ReadAsStringAsync().Result).CompanyName == company.CompanyName),
                ItExpr.IsAny<CancellationToken>()
            );
    }

}
}

