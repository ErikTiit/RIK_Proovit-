using Microsoft.EntityFrameworkCore;
using RIK_Proovitöö.Models;

namespace RIK_Proovitöö.Repository
{
    public class EventCompanyRepository : IEventCompanyRepository
    {
        private readonly AppDbContext _context;

        public EventCompanyRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<EventCompany>> GetEventCompanies()
        {
            return await _context.EventCompanies.ToListAsync();
        }

        public async Task<IEnumerable<EventCompany>> GetCompaniesForEvent(int eventId)
        {
            return await _context.EventCompanies
                .Where(ec => ec.EventID == eventId)
                .ToListAsync();
        }

        public async Task<EventCompany> GetEventCompany(int eventId, int companyId)
        {
            return await _context.EventCompanies.FindAsync(new object[] { eventId, companyId });
        }


        public async Task<EventCompany> AddEventCompany(EventCompany eventCompany)
        {
            _context.EventCompanies.Add(eventCompany);
            await _context.SaveChangesAsync();
            return eventCompany;
        }

        public async Task<EventCompany> UpdateEventCompany(EventCompany eventCompany)
        {
            _context.Entry(eventCompany).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return eventCompany;
        }

        public async Task DeleteEventCompany(int eventId, int companyId)
        {
            var eventCompany = await _context.EventCompanies.FindAsync(eventId, companyId);
            _context.EventCompanies.Remove(eventCompany);
            await _context.SaveChangesAsync();
        }


        public async Task<bool> EventCompanyExists(int eventId, int companyId)
        {
            return await _context.EventCompanies.AnyAsync(e => e.EventID == eventId && e.CompanyID == companyId);
        }
    }

}
