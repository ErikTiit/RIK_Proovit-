using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RIK_Proovitöö.Models;


namespace RIK_Proovitöö.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EventCompanyController : ControllerBase
    {
        private readonly AppDbContext _context;

        public EventCompanyController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/EventCompany
        [HttpGet]
        public async Task<ActionResult<IEnumerable<EventCompany>>> GetEventCompanies()
        {
            try
            {
                return await _context.EventCompanies.ToListAsync();
            }
            catch (Exception ex)
            {
                // Log the exception message
                Console.WriteLine(ex.Message);
                return StatusCode(500, "An error occurred while processing your request.");
            }
        }

        // GET: api/EventCompany/Event/5
        [HttpGet("Event/{eventId}")]
        public async Task<ActionResult<IEnumerable<EventCompany>>> GetCompaniesForEvent(int eventId)
        {
            try
            {
                return await _context.EventCompanies
                    .Where(ec => ec.EventID == eventId)
                    .ToListAsync();
            }
            catch (Exception ex)
            {
                // Log the exception message
                Console.WriteLine(ex.Message);
                return StatusCode(500, "An error occurred while processing your request.");
            }
        }

        // GET: api/EventCompany/5/1
        [HttpGet("{eventId}/{companyId}")]
        public async Task<ActionResult<EventCompany>> GetEventCompany(int eventId, int companyId)
        {
            try
            {
                var eventCompany = await _context.EventCompanies.FindAsync(eventId, companyId);

                if (eventCompany == null)
                {
                    return NotFound();
                }

                return eventCompany;
            }
            catch (Exception ex)
            {
                // Log the exception message
                Console.WriteLine(ex.Message);
                return StatusCode(500, "An error occurred while processing your request.");
            }
        }

        // PUT: api/EventCompany/5/1
        [HttpPut("{eventId}/{companyId}")]
        public async Task<IActionResult> PutEventCompany(int eventId, int companyId, EventCompany eventCompany)
        {
            if (eventId != eventCompany.EventID || companyId != eventCompany.CompanyID)
            {
                return BadRequest();
            }

            _context.Entry(eventCompany).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!EventCompanyExists(eventId, companyId))
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

        // POST: api/EventCompany
        [HttpPost]
        public async Task<ActionResult<EventCompany>> PostEventCompany(EventCompany eventCompany)
        {
            try
            {
                _context.EventCompanies.Add(eventCompany);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                // Log the exception message
                Console.WriteLine(ex.Message);
                return StatusCode(500, "An error occurred while processing your request.");
            }

            return CreatedAtAction("GetEventCompany", new { eventId = eventCompany.EventID, companyId = eventCompany.CompanyID }, eventCompany);
        }

        // DELETE: api/EventCompany/5/1
        [HttpDelete("{eventId}/{companyId}")]
        public async Task<IActionResult> DeleteEventCompany(int eventId, int companyId)
        {
            try
            {
                var eventCompany = await _context.EventCompanies.FindAsync(eventId, companyId);
                if (eventCompany == null)
                {
                    return NotFound();
                }

                _context.EventCompanies.Remove(eventCompany);
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

        private bool EventCompanyExists(int eventId, int companyId)
        {
            return _context.EventCompanies.Any(e => e.EventID == eventId && e.CompanyID == companyId);
        }
    }
}
