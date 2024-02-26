using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RIK_Proovitöö.Models;


namespace RIK_Proovitöö.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EventAttendeesController : ControllerBase
    {
        private readonly AppDbContext _context;

        public EventAttendeesController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/EventAttendees
        [HttpGet]
        public async Task<ActionResult<IEnumerable<EventAttendee>>> GetEventAttendees()
        {
            return await _context.EventAttendees.ToListAsync();
        }

        // GET: api/EventAttendees/5
        [HttpGet("{id}")]
        public async Task<ActionResult<EventAttendee>> GetEventAttendee(int id)
        {
            var eventAttendee = await _context.EventAttendees.FindAsync(id);

            if (eventAttendee == null)
            {
                return NotFound();
            }

            return eventAttendee;
        }

        // PUT: api/EventAttendees/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutEventAttendee(int id, EventAttendee eventAttendee)
        {
            if (id != eventAttendee.Id)
            {
                return BadRequest();
            }

            _context.Entry(eventAttendee).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!EventAttendeeExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/EventAttendees
        [HttpPost]
        public async Task<ActionResult<EventAttendee>> PostEventAttendee(EventAttendee eventAttendee)
        {
            _context.EventAttendees.Add(eventAttendee);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetEventAttendee", new { id = eventAttendee.Id }, eventAttendee);
        }

        // DELETE: api/EventAttendees/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteEventAttendee(int id)
        {
            var eventAttendee = await _context.EventAttendees.FindAsync(id);
            if (eventAttendee == null)
            {
                return NotFound();
            }

            _context.EventAttendees.Remove(eventAttendee);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool EventAttendeeExists(int id)
        {
            return _context.EventAttendees.Any(e => e.Id == id);
        }
    }
}
