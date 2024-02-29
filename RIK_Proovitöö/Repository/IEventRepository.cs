using RIK_Proovitöö.Models;

namespace RIK_Proovitöö.Repository
{
    public interface IEventRepository
    {
        Task<IEnumerable<Event>> GetEvents();
        Task<Event> GetEvent(int id);
        Task<Event> AddEvent(Event eventItem);
        Task<Event> UpdateEvent(Event eventItem);
        Task DeleteEvent(int id);
        Task<bool> EventExists(int id);
    }

}
