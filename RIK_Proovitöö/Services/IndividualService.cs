using RIK_Proovitöö.Models;

namespace RIK_Proovitöö.Services
{
    public class IndividualService : IIndividualService
    {
        private readonly HttpClient _http;
        private readonly IConfiguration _config;

        public IndividualService(HttpClient http, IConfiguration config)
        {
            _http = http;
            _config = config;
        }

        public async Task<Individual> GetIndividualAsync(int id)
        {
            return await _http.GetFromJsonAsync<Individual>($"{_config["ApiEndpoints:Individual"]}/{id}");
        }

        public async Task UpdateIndividualAsync(int id, Individual individual)
        {
            await _http.PutAsJsonAsync($"{_config["ApiEndpoints:Individual"]}/{id}", individual);
        }
    }
}
