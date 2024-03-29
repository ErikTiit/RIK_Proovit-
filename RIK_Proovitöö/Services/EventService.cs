﻿using RIK_Proovitöö.Models;

namespace RIK_Proovitöö.Services
{
    public class EventService : IEventService
    {
        private readonly HttpClient _http;
        private readonly IConfiguration _config;

        public EventService(HttpClient http, IConfiguration config)
        {
            _http = http;
            _config = config;
        }

        public async Task<Event[]> GetEventsAsync()
        {
            return await _http.GetFromJsonAsync<Event[]>(_config["ApiEndpoints:Events"]);
        }

        public async Task DeleteEventAsync(int id)
        {
            await _http.DeleteAsync($"{_config["ApiEndpoints:Events"]}/{id}");
        }

        public async Task<Event> GetEventAsync(int id)
        {
            return await _http.GetFromJsonAsync<Event>($"{_config["ApiEndpoints:Events"]}/{id}");
        }

        public async Task<List<EventIndividual>> GetEventIndividualsAsync(int eventId)
        {
            return await _http.GetFromJsonAsync<List<EventIndividual>>($"{_config["ApiEndpoints:EventIndividual"]}/{eventId}");
        }

        public async Task<List<EventCompany>> GetEventCompaniesAsync(int eventId)
        {
            return await _http.GetFromJsonAsync<List<EventCompany>>($"{_config["ApiEndpoints:EventCompany"]}/{eventId}");
        }


        public async Task<Individual> GetIndividualAsync(int individualId)
        {
            return await _http.GetFromJsonAsync<Individual>($"{_config["ApiEndpoints:Individual"]}/{individualId}");
        }

        public async Task<Company> GetCompanyAsync(int companyId)
        {
            return await _http.GetFromJsonAsync<Company>($"{_config["ApiEndpoints:Company"]}/{companyId}");
        }

        public async Task DeleteIndividualAttendeeAsync(int eventId, int individualId)
        {
            var url = $"{_config["ApiEndpoints:DeletingIndividual"]}/{eventId}/{individualId}";
            var response = await _http.DeleteAsync(url);

            if (!response.IsSuccessStatusCode)
            {
                throw new Exception($"Failed to delete individual attendee with status code: {response.StatusCode}");
            }
        }

        public async Task DeleteCompanyAttendeeAsync(int eventId, int companyId)
        {
            var url = $"{_config["ApiEndpoints:DeletingCompany"]}/{eventId}/{companyId}";
            await _http.DeleteAsync(url);
        }

        public Task<HttpResponseMessage> CreateIndividualAsync(Individual newIndividual)
        {
            var url = _config["ApiEndpoints:Individual"];
            return _http.PostAsJsonAsync(url, newIndividual);
        }

        public Task<HttpResponseMessage> CreateCompanyAsync(Company newCompany)
        {
            var url = _config["ApiEndpoints:Company"];
            return _http.PostAsJsonAsync(url, newCompany);
        }

        public Task<HttpResponseMessage> CreateEventIndividualAsync(EventIndividual eventIndividual)
        {
            var url = _config["ApiEndpoints:AddingIndividual"];
            return _http.PostAsJsonAsync(url, eventIndividual);
        }

        public Task<HttpResponseMessage> CreateEventCompanyAsync(EventCompany eventCompany)
        {
            var url = _config["ApiEndpoints:AddingCompany"];
            return _http.PostAsJsonAsync(url, eventCompany);
        }


    }
}
