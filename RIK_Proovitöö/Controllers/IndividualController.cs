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
            try
            {
                return await _context.Individuals.ToListAsync();
            }
            catch (Exception ex)
            {
                // Log the exception message
                Console.WriteLine(ex.Message);
                return StatusCode(500, "An error occurred while processing your request.");
            }
        }

        // GET: api/Individual/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Individual>> GetIndividual(int id)
        {
            try
            {
                var individual = await _context.Individuals.FindAsync(id);

                if (individual == null)
                {
                    return NotFound();
                }

                return individual;
            }
            catch (Exception ex)
            {
                // Log the exception message
                Console.WriteLine(ex.Message);
                return StatusCode(500, "An error occurred while processing your request.");
            }
        }

        // PUT: api/Individual/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutIndividual(int id, Individual individual)
        {
            if (id != individual.ID)
            {
                return BadRequest();
            }

            _context.Entry(individual).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!IndividualExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
            catch (Exception ex)
            {
                // Log the exception message
                Console.WriteLine(ex.Message);
                return StatusCode(500, "An error occurred while processing your request.");
            }

            return NoContent();
        }

        // POST: api/Individual
        [HttpPost]
        public async Task<ActionResult<Individual>> PostIndividual(Individual individual)
        {
            try
            {
                _context.Individuals.Add(individual);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                // Log the exception message
                Console.WriteLine(ex.Message);
                return StatusCode(500, "An error occurred while processing your request.");
            }

            return CreatedAtAction("GetIndividual", new { id = individual.ID }, individual);
        }

        // DELETE: api/Individual/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteIndividual(int id)
        {
            try
            {
                var individual = await _context.Individuals.FindAsync(id);
                if (individual == null)
                {
                    return NotFound();
                }

                _context.Individuals.Remove(individual);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                // Log the exception message
                Console.WriteLine(ex.Message);
                return StatusCode(500, "An error occurred while processing your request.");
            }

            return NoContent();
        }

        private bool IndividualExists(int id)
        {
            return _context.Individuals.Any(e => e.ID == id);
        }
    }
}
