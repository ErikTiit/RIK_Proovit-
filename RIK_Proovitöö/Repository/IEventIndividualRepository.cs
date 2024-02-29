using RIK_Proovitöö.Models;

namespace RIK_Proovitöö.Repository
{
    public interface IEventIndividualRepository
    {
        Task<IEnumerable<EventIndividual>> GetEventIndividuals();
        Task<IEnumerable<EventIndividual>> GetIndividualsForEvent(int eventId);
        Task<EventIndividual> GetEventIndividual(int eventId, int individualId);
        Task<EventIndividual> AddEventIndividual(EventIndividual eventIndividual);
        Task<EventIndividual> UpdateEventIndividual(EventIndividual eventIndividual);
        Task DeleteEventIndividual(int eventId, int individualId);
        Task<bool> EventIndividualExists(int eventId, int individualId);
    }

}
