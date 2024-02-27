﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RIK_Proovitöö.Models;


namespace RIK_Proovitöö.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EventIndividualController : ControllerBase
    {
        private readonly AppDbContext _context;

        public EventIndividualController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/EventIndividual
        [HttpGet]
        public async Task<ActionResult<IEnumerable<EventIndividual>>> GetEventIndividuals()
        {
            return await _context.EventIndividuals.ToListAsync();
        }

        // GET: api/EventIndividual/Event/5
        [HttpGet("Event/{eventId}")]
        public async Task<ActionResult<IEnumerable<EventIndividual>>> GetIndividualsForEvent(int eventId)
        {
            return await _context.EventIndividuals
                .Where(ei => ei.EventID == eventId)
                .ToListAsync();
        }




        // GET: api/EventIndividual/5/1
        [HttpGet("{eventId}/{individualId}")]
        public async Task<ActionResult<EventIndividual>> GetEventIndividual(int eventId, int individualId)
        {
            var eventIndividual = await _context.EventIndividuals.FindAsync(eventId, individualId);

            if (eventIndividual == null)
            {
                return NotFound();
            }

            return eventIndividual;
        }

        // PUT: api/EventIndividual/5/1
        [HttpPut("{eventId}/{individualId}")]
        public async Task<IActionResult> PutEventIndividual(int eventId, int individualId, EventIndividual eventIndividual)
        {
            if (eventId != eventIndividual.EventID || individualId != eventIndividual.IndividualID)
            {
                return BadRequest();
            }

            _context.Entry(eventIndividual).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!EventIndividualExists(eventId, individualId))
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

        // POST: api/EventIndividual
        [HttpPost]
        public async Task<ActionResult<EventIndividual>> PostEventIndividual(EventIndividual eventIndividual)
        {
            _context.EventIndividuals.Add(eventIndividual);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetEventIndividual", new { eventId = eventIndividual.EventID, individualId = eventIndividual.IndividualID }, eventIndividual);
        }

        // DELETE: api/EventIndividual/5/1
        [HttpDelete("{eventId}/{individualId}")]
        public async Task<IActionResult> DeleteEventIndividual(int eventId, int individualId)
        {
            var eventIndividual = await _context.EventIndividuals.FindAsync(eventId, individualId);
            if (eventIndividual == null)
            {
                return NotFound();
            }

            _context.EventIndividuals.Remove(eventIndividual);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool EventIndividualExists(int eventId, int individualId)
        {
            return _context.EventIndividuals.Any(e => e.EventID == eventId && e.IndividualID == individualId);
        }
    }
}
