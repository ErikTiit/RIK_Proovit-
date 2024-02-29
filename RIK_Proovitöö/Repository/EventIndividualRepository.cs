using Microsoft.EntityFrameworkCore;
using RIK_Proovitöö.Models;

namespace RIK_Proovitöö.Repository
{
    public class EventIndividualRepository : IEventIndividualRepository
    {
        private readonly AppDbContext _context;

        public EventIndividualRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<EventIndividual>> GetEventIndividuals()
        {
            return await _context.EventIndividuals.ToListAsync();
        }

        public async Task<IEnumerable<EventIndividual>> GetIndividualsForEvent(int eventId)
        {
            return await _context.EventIndividuals
                .Where(ei => ei.EventID == eventId)
                .ToListAsync();
        }

        public async Task<EventIndividual> GetEventIndividual(int eventId, int individualId)
        {
            return await _context.EventIndividuals.FindAsync(new object[] { eventId, individualId });
        }



        public async Task<EventIndividual> AddEventIndividual(EventIndividual eventIndividual)
        {
            _context.EventIndividuals.Add(eventIndividual);
            await _context.SaveChangesAsync();
            return eventIndividual;
        }

        public async Task<EventIndividual> UpdateEventIndividual(EventIndividual eventIndividual)
        {
            _context.Entry(eventIndividual).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return eventIndividual;
        }

        public async Task DeleteEventIndividual(int eventId, int individualId)
        {
            var eventIndividual = await _context.EventIndividuals.FindAsync(eventId, individualId);
            _context.EventIndividuals.Remove(eventIndividual);
            await _context.SaveChangesAsync();
        }


        public async Task<bool> EventIndividualExists(int eventId, int individualId)
        {
            return await _context.EventIndividuals.AnyAsync(e => e.EventID == eventId && e.IndividualID == individualId);
        }
    }

}
