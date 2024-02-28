using RIK_Proovitöö.Models;


namespace RIK_Proovitöö.Services

{
    public interface IEventService
    {
        Task<Event[]> GetEventsAsync();
        Task DeleteEventAsync(int id);
        Task<Event> GetEventAsync(int id);
        Task<List<EventIndividual>> GetEventIndividualsAsync(int eventId);
        Task<List<EventCompany>> GetEventCompaniesAsync(int eventId);
        Task<Individual> GetIndividualAsync(int individualId);
        Task<Company> GetCompanyAsync(int companyId);
        Task DeleteIndividualAttendeeAsync(int eventId, int individualId);
        Task DeleteCompanyAttendeeAsync(int eventId, int companyId);
    }

}
