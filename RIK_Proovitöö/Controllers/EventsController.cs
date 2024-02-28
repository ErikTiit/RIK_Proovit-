﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RIK_Proovitöö.Models;


namespace RIK_Proovitöö.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EventsController : ControllerBase
    {
        private readonly AppDbContext _context;

        public EventsController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/Events
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Event>>> GetEvents()
        {
            try
            {
                return await _context.Events.ToListAsync();
            }
            catch (Exception ex)
            {
                // Log the exception message
                Console.WriteLine(ex.Message);
                return StatusCode(500, "An error occurred while processing your request.");
            }
        }

        // GET: api/Events/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Event>> GetEvent(int id)
        {
            try
            {
                var eventItem = await _context.Events.FindAsync(id);

                if (eventItem == null)
                {
                    return NotFound();
                }

                return eventItem;
            }
            catch (Exception ex)
            {
                // Log the exception message
                Console.WriteLine(ex.Message);
                return StatusCode(500, "An error occurred while processing your request.");
            }
        }

        // PUT: api/Events/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutEvent(int id, Event eventItem)
        {
            if (id != eventItem.ID)
            {
                return BadRequest();
            }

            _context.Entry(eventItem).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!EventExists(id))
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

        // POST: api/Events
        [HttpPost]
        public async Task<ActionResult<Event>> PostEvent(Event eventItem)
        {
            if (eventItem.Date <= DateTime.Now)
            {
                return BadRequest("The event date must be in the future.");
            }

            try
            {
                _context.Events.Add(eventItem);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                // Log the exception message
                Console.WriteLine(ex.Message);
                return StatusCode(500, "An error occurred while processing your request.");
            }

            return CreatedAtAction("GetEvent", new { id = eventItem.ID }, eventItem);
        }

        // DELETE: api/Events/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteEvent(int id)
        {
            try
            {
                var eventItem = await _context.Events.FindAsync(id);
                if (eventItem == null)
                {
                    return NotFound();
                }

                _context.Events.Remove(eventItem);
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

        private bool EventExists(int id)
        {
            return _context.Events.Any(e => e.ID == id);
        }
    }
}
