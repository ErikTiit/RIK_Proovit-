using RIK_Proovitöö.Models;

namespace RIK_Proovitöö.Repository
{
    public interface IEventCompanyRepository
    {
        Task<IEnumerable<EventCompany>> GetEventCompanies();
        Task<IEnumerable<EventCompany>> GetCompaniesForEvent(int eventId);
        Task<EventCompany> GetEventCompany(int eventId, int companyId);
        Task<EventCompany> AddEventCompany(EventCompany eventCompany);
        Task<EventCompany> UpdateEventCompany(EventCompany eventCompany);
        Task DeleteEventCompany(int eventId, int companyId);
        Task<bool> EventCompanyExists(int eventId, int companyId);
    }

}
