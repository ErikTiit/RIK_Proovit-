using Microsoft.EntityFrameworkCore;
using RIK_Proovitöö.Models;

namespace RIK_Proovitöö.Repository
{
    public class IndividualRepository : IIndividualRepository
    {
        private readonly AppDbContext _context;

        public IndividualRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Individual>> GetIndividuals()
        {
            return await _context.Individuals.ToListAsync();
        }

        public async Task<Individual> GetIndividual(int id)
        {
            return await _context.Individuals.FindAsync(id);
        }

        public async Task<Individual> AddIndividual(Individual individual)
        {
            _context.Individuals.Add(individual);
            await _context.SaveChangesAsync();
            return individual;
        }

        public async Task<Individual> UpdateIndividual(Individual individual)
        {
            _context.Entry(individual).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return individual;
        }

        public async Task DeleteIndividual(int id)
        {
            var individual = await _context.Individuals.FindAsync(id);
            _context.Individuals.Remove(individual);
            await _context.SaveChangesAsync();
        }

        public async Task<bool> IndividualExists(int id)
        {
            return await _context.Individuals.AnyAsync(e => e.ID == id);
        }
    }

}
