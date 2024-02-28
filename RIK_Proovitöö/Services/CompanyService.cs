using RIK_Proovitöö.Models;

namespace RIK_Proovitöö.Services
{
    public class CompanyService : ICompanyService
    {
        private readonly HttpClient _http;
        private readonly IConfiguration _config;

        public CompanyService(HttpClient http, IConfiguration config)
        {
            _http = http;
            _config = config;
        }

        public async Task<Company> GetCompanyAsync(int id)
        {
            return await _http.GetFromJsonAsync<Company>($"{_config["ApiEndpoints:Company"]}/{id}");
        }

        public async Task UpdateCompanyAsync(int id, Company company)
        {
            await _http.PutAsJsonAsync($"{_config["ApiEndpoints:Company"]}/{id}", company);
        }
    }
}
