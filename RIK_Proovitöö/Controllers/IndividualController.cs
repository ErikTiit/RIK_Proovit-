using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RIK_Proovitöö.Models;

namespace RIK_Proovitöö.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class IndividualController : ControllerBase
    {
        private readonly AppDbContext _context;

        public IndividualController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/Individual
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Individual>>> GetIndividuals()
        {
            return await _context.Individuals.ToListAsync();
        }

        // GET: api/Individual/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Individual>> GetIndividual(int id)
        {
            var individual = await _context.Individuals.FindAsync(id);

            if (individual == null)
            {
                return NotFound();
            }

            return individual;
        }

        // PUT: api/Individual/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutIndividual(int id, Individual individual)
        {
            if (id != individual.Id)
            {
                return BadRequest();
            }

            _context.Entry(individual).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return NoContent();
        }

        // POST: api/Individual
        [HttpPost]
        public async Task<ActionResult<Individual>> PostIndividual(Individual individual)
        {
            _context.Individuals.Add(individual);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetIndividual", new { id = individual.Id }, individual);
        }

        // DELETE: api/Individual/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteIndividual(int id)
        {
            var individual = await _context.Individuals.FindAsync(id);
            if (individual == null)
            {
                return NotFound();
            }

            _context.Individuals.Remove(individual);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
